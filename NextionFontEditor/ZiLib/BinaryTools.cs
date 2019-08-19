using System.Drawing;
using System.Collections.Generic;
using System.Linq;

namespace ZiLib {

    public class BinaryTools {

        public static bool[] BytesToBits(byte[] bytes) {
            bool[] bits = new bool[bytes.Length * 8];

            for (int i = 0; i < bytes.Length; i++) {
                for (int j = 0; j < 8; j++) {
                    var index = i * 8 + j;
                    bits[index] = (bytes[i] & (1 << (7 - j))) != 0;
                }
            }

            return bits;
        }

        public static byte[] BitmapTo1BppData(Bitmap b, bool invertColour=false) {
            var pixels = new bool[b.Width * b.Height];

            var A = invertColour ? Color.White.A : Color.Black.A;
            var B = invertColour ? Color.White.B : Color.Black.B;
            var G = invertColour ? Color.White.G : Color.Black.G;
            var R = invertColour ? Color.White.R : Color.Black.R;
            for (int y = 0; y < b.Height; y++) {

                for (int x = 0; x < b.Width; x++) {
                    var pixel = b.GetPixel(x, y);
                    pixels[(y * b.Width) + x] = (pixel.A == A && pixel.R == R && pixel.G == G && pixel.B == B);
                }
              
            }

            var data = new byte[b.Width * b.Height / 8];

            for (int i = 0; i < data.Length; i++) {

                for (int j = 0; j < 8; j++) {

                    var bit = pixels[(i * 8) + j] ? 1 : 0;
                    data[i] |= (byte) (bit << (7 - j));

                }

            }

            return data;
        }

        /*
        public static byte[] BitmapTo3BppData(Bitmap b, bool invertColour = false)
        {
            var pixels = new byte[b.Width * b.Height];

            for (int y = 0; y < b.Height; y++)
            {

                for (int x = 0; x < b.Width; x++)
                {

                    var pixel = b.GetPixel(x, y);
                    pixels[(y * b.Width) + x] = (byte)((pixel.R + 2 * pixel.G + pixel.B) / 4 * pixel.A / 255);    // Weighted Color2Grayscale;

                    if (!invertColour)
                    {
                        pixels[(y * b.Width) + x] = (byte)(255 - pixels[(y * b.Width) + x]);
                    }

                    // convert to 3 bits
                    pixels[(y * b.Width) + x] = (byte)(pixels[(y * b.Width) + x] >> 5);

                }

            }

            return pixels;
        }
        */

        /* A faster 3-bit encoder and compresser combined into one loop instead of consecutive nested loops */
        public static byte[] BitmapTo3BppData(Bitmap b, bool invertColour = false)
        {
            var data = new List<byte>();

            var curColor = (byte)0u;
            var prevColor = (byte)0u;
            var prevCount = 0u;

            var antialias = false;

            data.Add(0x03);

            for (int y = 0; y < b.Height; y++)
            {
                for (int x = 0; x < b.Width; x++)
                {
                    var pixel = b.GetPixel(x, y);

                    if (invertColour)
                    {
                        curColor = (byte)((pixel.R + 2 * pixel.G + pixel.B) / 4);    // Weighted Color2Grayscale;
                    }
                    else
                    {
                        curColor = (byte)(255 - (pixel.R + 2 * pixel.G + pixel.B) / 4);    // Weighted Color2Grayscale;
                    }
                    curColor = (byte)(curColor * pixel.A / 255);
                    // convert to 3 bits
                    curColor = (byte)(curColor >> 5);

                    if (curColor == prevColor) {
                        prevCount++;
                        continue;   // to next pixel
                    }

                    if (prevCount == 0)     // No previous data to encode
                    {
                        prevColor = curColor;
                        prevCount = 1;
                        continue;   // to next pixel
                    }

                    if (prevColor == 0)
                    {
                        while (prevCount >= 31) {
                            data.Add(CompressedByte.RepeatedWhites(31));
                            prevCount -= 31;
                        }

                        if (prevCount == 0)
                        {
                            // start with fresh color count
                            prevColor = curColor;
                            prevCount = 1;
                            continue;   // to next pixel
                        }

                        if (curColor == 7)
                        {
                            data.Add(CompressedByte.RepeatedWhites(prevCount, 1u));
                            // start with fresh color count
                            prevColor = curColor;
                            prevCount = 0;
                            continue;   // to next pixel
                        }

                        if (prevCount >= 8)
                        {
                            data.Add(CompressedByte.RepeatedWhites(prevCount));
                            // start with fresh color count
                            prevColor = curColor;
                            prevCount = 1;
                        }
                        else
                        {
                            data.Add(CompressedByte.RepeatedWhites(prevCount, curColor));
                            prevColor = curColor;
                            prevCount = 0;
                        }

                        continue;   // to next pixel
                    }

                    if (prevColor == 7)
                    {
                        while (prevCount >= 31)
                        {
                            data.Add(CompressedByte.RepeatedBlacks(31));
                            prevCount -= 31;
                        }

                        if (prevCount > 1)
                        {
                            data.Add(CompressedByte.RepeatedBlacks(prevCount));         // remaining blacks
                            prevCount = 1;
                        }
                        else
                        {
                            data.Add(CompressedByte.AlphaColors(prevColor, curColor));   // one black + one color
                            prevCount = 0;
                        }
                        prevColor = curColor;
                        continue;   // to next pixel
                    }

                    // a grayscale
                    antialias = true;

                    while (prevCount >= 2)
                    {
                        data.Add(CompressedByte.AlphaColors(prevColor, prevColor));
                        prevCount -= 2;
                    }

                    if (prevCount == 0)
                    {
                        prevCount = 1;
                    }
                    else
                    {
                        data.Add(CompressedByte.AlphaColors(prevColor, curColor));   // one black + one color
                        prevCount = 0;
                    }
                    prevColor = curColor;
                    continue;   // to next pixel

                }
            }

            /* Handle remainder stored in PrevColor and PreCount */

            if (prevCount == 0)     // no more previous data to encode
            {
                return data.ToArray();
            }

            if (prevColor == 0)
            {
                while (prevCount >= 31)
                {
                    data.Add(CompressedByte.RepeatedWhites(31));
                    prevCount -= 31;
                }

                if (prevCount > 0)
                {
                    data.Add(CompressedByte.RepeatedWhites(prevCount));
                }

                return data.ToArray();
            }

            if (prevColor == 7)
            {
                while (prevCount >= 31)
                {
                    data.Add(CompressedByte.RepeatedBlacks(31));
                    prevCount -= 31;
                }

                if (prevCount > 0)
                {
                    data.Add(CompressedByte.RepeatedBlacks(prevCount));
                }

                return data.ToArray();
            }

            // a grayscale
            while (prevCount >= 2)
            {
                data.Add(CompressedByte.AlphaColors(prevColor, prevColor));
                prevCount -= 2;
            }

            if (prevCount > 0)
            {
                data.Add(CompressedByte.RepeatedWhites((byte)0, prevColor));   // zero whites + one color
            }

            return data.ToArray();
        }
    }
}