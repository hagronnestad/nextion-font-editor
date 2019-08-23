using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;

namespace ZiLib.Extensions {

    public static class BitmapExtensions {

        public static void SetPixelNumber(this Bitmap b, int number, Color color) {
            if (number >= b.Width * b.Height) {
                Debug.WriteLine($"!!! pixelNumber >= {b.Width * b.Height} !!!");
                return;
            }

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

        public static Bitmap DrawString(string txt, string fontname, byte height, FontStyle style = FontStyle.Regular, byte x = 0, byte y = 0, byte contrast = 0)
        {
            var fontsize = (float)height;

            var strFormat = (StringFormat)StringFormat.GenericTypographic.Clone();
            strFormat.FormatFlags = strFormat.FormatFlags | StringFormatFlags.MeasureTrailingSpaces | StringFormatFlags.NoWrap | StringFormatFlags.NoFontFallback;

            var tmp = new Bitmap(1, 1);
            var grphtmp = Graphics.FromImage(tmp);

            var font = new Font("Arial", 12);

            // Check if it is a otf/ttf file
            if (((Path.GetExtension(fontname) == ".ttf") || (Path.GetExtension(fontname) == ".otf")) && (File.Exists(fontname)))
            {

                var pfc = new System.Drawing.Text.PrivateFontCollection();
                pfc.AddFontFile(fontname);

                var i = 0;

                font = new Font(pfc.Families[0], fontsize, style, GraphicsUnit.Pixel);
                while (Math.Abs(font.Height - height) > 0.1 && i++<10)
                {
                    fontsize += (float)(height - font.Height) / 2;
                    font = new Font(pfc.Families[0], fontsize, style, GraphicsUnit.Pixel);
                }

                /*while (font.Height > height)
                {
                    fontsize -= (float)0.02;
                    font.Dispose();
                    font = new Font(pfc.Families[0], fontsize, style, GraphicsUnit.Pixel);
                }*/

                // System Font
            }
            else
            {
                font = new Font(fontname, fontsize, style, GraphicsUnit.Pixel);
                while (Math.Abs(font.Height - height) > 0.1)
                {
                    fontsize += (float)(height - font.Height) / 2;
                    font = new Font(fontname, fontsize, style, GraphicsUnit.Pixel);
                }

            }
            var width = (int)Math.Round((grphtmp.MeasureString(txt, font, new PointF(0, 0), strFormat)).Width);
            width = width > 255 ? 255 : width;

            if (width > 0)
            {
                var b = new Bitmap(width, height);
                var graphics = Graphics.FromImage(b);

                //Adjust for high quality
                graphics.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
                graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                graphics.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.HighQuality;
                graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;  // AntiAlias // HighQuality
                graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAliasGridFit; //AntiAliasGridFit; //    (B/W = SingleBitPerPixelGridFit)

                var brushBg = Brushes.White;
                var brushFg = Brushes.Black;

                graphics.FillRectangle(brushBg, 0, 0, b.Width, b.Height);
                graphics.TextContrast = contrast;
                graphics.DrawString(txt, font, brushFg, x, y, strFormat);  // experimental
                //TextRenderer.DrawText(graphics, txt, font, new Point(x, y), Color.Black, Color.White);
                graphics.Dispose();
                return b;

            }
            else
            {
                // Nextion requires a minimum width of 1 pixel. It doesn't like zero length chardata as it crashes the Application when rotating the font.
                var b = new Bitmap(1, height);
                //b.Dispose();
                return b;
            }

        }

    }
}