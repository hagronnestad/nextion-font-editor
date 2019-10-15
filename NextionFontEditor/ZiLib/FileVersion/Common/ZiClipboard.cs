using System.Windows.Forms;
using System.Drawing;
using System.IO;

namespace ZiLib.FileVersion.Common
{
    public static class ZiClipboard
    {
        public static void CopyToClipboard(IZiCharacter character) {
            var bmp = character.ToBitmap();
            if (bmp == null) {
                return;
            }
            // Remove transparency
            var clip = new Bitmap(bmp.Width, bmp.Height);
            Color color;

            using (var graphics = Graphics.FromImage(clip)) {
                graphics.FillRectangle(Brushes.White, 0, 0, clip.Width, clip.Height);
                //graphics.DrawImage(bmp, 0, 0);
            }

            // Remove ForegroundColor, replace with black
            for (int y = 0; y < bmp.Height; y++) {
                for (int x = 0; x < bmp.Width; x++) {
                    color = Color.FromArgb(bmp.GetPixel(x, y).A, Color.Black);
                    clip.SetPixel(x, y, color);
                }
            }

            var clip2 = new Bitmap(bmp.Width, bmp.Height);

            using (var graphics = Graphics.FromImage(clip2)) {
                graphics.FillRectangle(Brushes.White, 0, 0, clip.Width, clip.Height);
                graphics.DrawImage(clip, 0, 0);
            }

            Clipboard.SetImage(clip2);
            clip.Dispose();
            clip2.Dispose();
        }

        public static bool PasteFromClipboard(IZiCharacter character) {
            try
            {
                if (Clipboard.ContainsData("PNG"))
                {
                    object png = Clipboard.GetData("PNG");
                    if (png is MemoryStream)
                    {
                        MemoryStream pngstream = png as MemoryStream;
                        var clip = Image.FromStream(pngstream);
                        var newbmp = Convert(new Bitmap(clip));
                        character.SetBitmap(newbmp);
                        clip.Dispose();
                        return true;
                    }

                }
            }
            catch {
            }

            if (Clipboard.ContainsImage()) {
                var clip = Clipboard.GetImage();
                if (clip.Height > character.Parent.CharacterHeight)
                {
                    var newbmp = Convert(new Bitmap(clip));
                    character.SetBitmap(newbmp);
                    clip.Dispose();
                    return true;
                }
                else {
                    var newbmp = Convert(new Bitmap(clip));
                    character.SetBitmap(newbmp);
                    clip.Dispose();
                    return true;
                }
            }

            return false;
        }

        private static Color GetAlphaColor(Color pixel) {
            var curColor = (byte)(255 - (pixel.R + 2 * pixel.G + pixel.B) / 4);    // Weighted Color2Grayscale;
            return Color.FromArgb(curColor, Color.Black);
        }

        private static Bitmap Convert(Bitmap clip) {
            var bmp = new Bitmap(clip.Width, clip.Height);
            Color color; 
            for (int y = 0; y < bmp.Height; y++) {
                for (int x = 0; x < bmp.Width; x++) {
                    color = GetAlphaColor(clip.GetPixel(x,y));
                    bmp.SetPixel(x, y, color);
                }
            }
            return bmp;
        }

        public static bool ContainsImage() {
            return Clipboard.ContainsImage() || Clipboard.ContainsData("PNG");
        }

    }
}