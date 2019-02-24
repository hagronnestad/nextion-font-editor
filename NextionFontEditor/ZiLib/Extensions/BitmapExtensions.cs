using System;
using System.Drawing;

namespace ZiLib.Extensions {

    public static class BitmapExtensions {

        public static void SetPixelNumber(this Bitmap b, int number, Color color) {
            var l = NumberToPoint(number, b.Width);
            b.SetPixel(l.X, l.Y, color);
        }

        public static Point NumberToPoint(int number, int width) {
            var p = new Point {
                Y = (int) Math.Floor((double) number / width),
                X = number % width
            };
            return p;
        }
    }
}