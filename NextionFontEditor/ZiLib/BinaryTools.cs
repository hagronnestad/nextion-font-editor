using System.Drawing;

namespace ZiLib {

    internal class BinaryTools {

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
    }
}