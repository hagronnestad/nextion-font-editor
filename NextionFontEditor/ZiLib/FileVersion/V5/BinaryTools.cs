using System.Drawing;
using System.Collections.Generic;
using System.Linq;

namespace ZiLib.FileVersion.V5
{

    public class BinaryTools
    {
        public static byte Get3bppColor(System.Drawing.Color pixel, bool invertColour) {
            var curColor = (byte)0u;
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
            return (byte)(curColor >> 5);
        }

        /* A faster 3-bit encoder and compresser combined into one loop instead of consecutive nested loops */
        public static byte[] BitmapTo3BppData(Bitmap b, bool invertColour = false)
        {
            var data = new List<byte>();

            byte curColor;
            var prevColor = (byte)0u;
            var prevCount = 0u;

            var antialias = false;


            if (b == null)
            {
                //return data.ToArray();
            }

            if (b.PixelFormat == System.Drawing.Imaging.PixelFormat.Undefined) {
                 return data.ToArray();
            }
            data.Add(0x03);

            for (int y = 0; y < b.Height; y++)
            {
                for (int x = 0; x < b.Width; x++)
                {
                    curColor = Get3bppColor(b.GetPixel(x, y), invertColour);

                    if (curColor == prevColor)
                    {
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
                        while (prevCount >= 31)
                        {
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

                        /* also check for second black pixel */
                        if (curColor == 7)
                        {
                            var blacks = 1u;
                            var j = (x + 1) >= b.Width ? y + 1 : y;
                            var i = (x + 1) >= b.Width ? 0 : x + 1;
                            while (blacks<2 && j<b.Height) {
                                var nextColor = Get3bppColor(b.GetPixel(i, j), invertColour);
                                if (nextColor != 7)
                                {
                                    break; // while
                                }

                                blacks++;
                                x = i;  // advance pixel for loops
                                y = j;
                                j = (x + 1) >= b.Width ? y + 1 : y;
                                i = (x + 1) >= b.Width ? 0 : x + 1;
                            }
                            data.Add(CompressedByte.RepeatedWhites(prevCount, blacks));
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

                        switch (prevCount)
                        {
                            case 0: // start with fresh color count
                                prevColor = curColor;
                                prevCount = 1;
                                break;
                            case 1:
                                data.Add(CompressedByte.AlphaColors(prevColor, curColor));   // one black + one color
                                prevCount = 0;
                                prevColor = curColor;
                                break;
                            default:
                                data.Add(CompressedByte.RepeatedBlacks(prevCount));         // remaining blacks
                                prevColor = curColor;
                                prevCount = 1;
                                break;
                        }

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
                        prevColor = curColor;
                        prevCount = 1;
                    }
                    else
                    {
                        data.Add(CompressedByte.AlphaColors(prevColor, curColor));   // one black + one color
                        prevColor = curColor;
                        prevCount = 0;
                    }
                    continue;   // to next pixel

                }
            }

            /* Handle remainder stored in PrevColor and PreCount */

            if (prevCount > 0)     // more previous data to encode
            {
                // remaining whites
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

                // remaining blacks
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

                // remaining grayscales
                while (prevCount >= 2)
                {
                    data.Add(CompressedByte.AlphaColors(prevColor, prevColor));
                    prevCount -= 2;
                }

                if (prevCount > 0)
                {
                    data.Add(CompressedByte.RepeatedWhites((byte)0, prevColor));   // zero whites + one color
                }
            }

            return data.ToArray();
        }
    }
}