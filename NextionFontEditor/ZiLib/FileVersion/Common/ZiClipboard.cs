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
            using (var graphics = Graphics.FromImage(clip)) {
                graphics.FillRectangle(Brushes.White, 0, 0, clip.Width, clip.Height);
                graphics.DrawImage(bmp, 0, 0);
            }
            Clipboard.SetImage(clip);
            clip.Dispose();
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
                        character.SetBitmap(new Bitmap(clip));
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
                    character.SetBitmap(new Bitmap(clip));
                    return true;
                }
                else {
                    character.SetBitmap(new Bitmap(clip));
                    return true;
                }
            }

            return false;
        }

        public static bool ContainsImage() {
            return Clipboard.ContainsImage() || Clipboard.ContainsData("PNG");
        }

    }
}