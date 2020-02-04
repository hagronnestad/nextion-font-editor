using System;
using System.Diagnostics;
using System.Drawing;
using System.Collections.Generic;

namespace ZiLib.FileVersion.V3 {
    internal enum ValidData : byte {
        UNKNOWN = 0x0,
        TEXT = 0x01,
        BITMAP = 0x02,
        TEXT_BITMAP = 0x03,
        CHARDATA = 0x04,
        CHARDATA_BITMAP = 0x06,
        TEXT_CHARDATA_BITMAP = 0x07
    }

    public class ZiCharacterV3 : IZiCharacter {
        byte ColorMode { get; set; }
        private ValidData DataState { get; set; }

        /* Common Properties */
        public IZiFont Parent { get; set; }
        public uint CodePoint { get; set; }
        public byte Width { get; set; }
        public byte KerningLeft { get; set; }
        public byte KerningRight { get; set; }
        public Color ForegroundColor { get; set; }

        /* FromString Constructor */
        public String Txt { get; set; }
        public PointF TxtPosition { get; set; }
        public Font Font { get; set; }

        /* FromBitmap Constructor and after rendering with DrawString or Decode from bytes */
        private Bitmap _Bitmap { get; set; }

        /* FromBytes constructor and after encoding the bitmap */
        private byte[] _CharacterData { get; set; }

        /* Helpers */
        byte TotalWidth => (byte)(Parent.CharacterWidth);
        public byte[] ToBytes() => GetCharacterData();

        private ValidData OriginalFormat;

        /* Constructors */
        public ZiCharacterV3(IZiFont parent, uint codepoint, Bitmap bmp, byte kerningL = 0, byte kerningR = 0) {
            Parent = parent;
            CodePoint = codepoint;
            _Bitmap = new Bitmap(Parent.CharacterWidth, Parent.CharacterHeight);
            using (var graphics = Graphics.FromImage(_Bitmap)) {
                graphics.DrawImage(bmp, 0, 0);
            }
            if (_CharacterData != null) {
                _CharacterData = null;
            }
            KerningRight = KerningLeft = 0; // Kerning is not supported in V3
            Width = Parent.CharacterWidth; // Fixed Width set by Parent Font
            DataState = ValidData.BITMAP;
            OriginalFormat = DataState;
            ForegroundColor = Color.Green;
        }
        public ZiCharacterV3(IZiFont parent, uint codepoint, byte[] bytes, byte width=0, byte kerningL = 0, byte kerningR = 0) {
            Parent = parent;
            CodePoint = codepoint;
            if (_Bitmap != null && _Bitmap.PixelFormat != System.Drawing.Imaging.PixelFormat.Undefined) {
                _Bitmap.Dispose();
            }
            _CharacterData = bytes;
            KerningRight = KerningLeft = 0; // Kerning is not supported in V3
            DataState = ValidData.CHARDATA;
            OriginalFormat = DataState;
            Width = Parent.CharacterWidth; // Fixed Width set by Parent Font
            ForegroundColor = Color.Aqua;
        }

        public ZiCharacterV3(IZiFont parent, uint codepoint, Font font, PointF location, String txt = null) {
            Parent = parent;
            CodePoint = codepoint;
            SetString(font, location, txt);
            if (_Bitmap != null && _Bitmap.PixelFormat != System.Drawing.Imaging.PixelFormat.Undefined) {
                _Bitmap.Dispose();
            }
            if (_CharacterData != null) {
                _CharacterData = null;
            }
            ForegroundColor = Color.Chocolate;
        }

        /* Bitmap Operations */
        public void SetPixel(int x, int y, System.Drawing.Color pixel) {
            DataState = ValidData.BITMAP;
            _Bitmap.SetPixel(x, y, pixel);
        }

        public Color GetPixel(int x, int y) {
            var bmp = ToBitmap();
            return bmp.GetPixel(x, y);
        }

        public void SetPixelNumber(int number, Color color) {
            if (number >= _Bitmap.Width * _Bitmap.Height) {
                Debug.WriteLine($"!!! pixelNumber >= {_Bitmap.Width * _Bitmap.Height} !!!");
                return;
            }

            var l = ZiLib.Extensions.BitmapExtensions.NumberToPoint(number, _Bitmap.Width);
            _Bitmap.SetPixel(l.X, l.Y, color);
        }

        public Bitmap ToBitmap() {
            /* Check if Bitmap bit is 0 in the DataState */
            if ((DataState & ValidData.BITMAP) == 0) {
                if ((DataState & ValidData.CHARDATA) != 0) {
                    Decode();
                } else {
                    DrawString(Txt, Font, Parent.CharacterHeight, TxtPosition);
                    Width = 0; // (byte)_Bitmap.Width;
                }
                DataState = DataState | ValidData.BITMAP;
            }
            return _Bitmap;
        }

        public void SetBitmap(Bitmap bmp) {
            if (_Bitmap != null && _Bitmap.PixelFormat != System.Drawing.Imaging.PixelFormat.Undefined) {
                using (var graphics = Graphics.FromImage(_Bitmap)) {
                    graphics.FillRectangle(Brushes.Transparent, 0, 0, _Bitmap.Width, _Bitmap.Height);
                    graphics.DrawImage(bmp, 0, 0);
                }
                DataState = ValidData.BITMAP;
            }
        }

        public String GetString() {
            return Parent.CodePage.GetString((int)CodePoint);
        }

        public void SetString(Font font, PointF location, String txt = null) {
            DataState = ValidData.TEXT;
            OriginalFormat = DataState;

            Txt = txt;
            TxtPosition = location;
            Font = font;

            ForegroundColor = Color.Crimson;
        }

        public Bitmap RevertBitmap() {
            DataState = OriginalFormat;
            return ToBitmap();
        }
        public bool CanRevert() {
            return (DataState == ValidData.BITMAP);
        }

        /* Byte Array Operations */
        public byte[] GetCharacterData() {
            /* Check if CharData bit is 0 in the DataState */
            if ((DataState & ValidData.CHARDATA) == 0) {
                if ((DataState & ValidData.BITMAP) == 0) {
                    var b = ToBitmap();
                }
                _CharacterData = Encode(_Bitmap, false);
                DataState = DataState | ValidData.CHARDATA;
            }
            return _CharacterData;
        }

        private void DrawString(string txt, Font font, byte height, PointF location, byte contrast = 0) {
            var fontsize = (float)height;

            var strFormat = (StringFormat)StringFormat.GenericTypographic.Clone();
            strFormat.FormatFlags = strFormat.FormatFlags | StringFormatFlags.MeasureTrailingSpaces | StringFormatFlags.NoWrap | StringFormatFlags.NoFontFallback;

            var tmp = new Bitmap(1, 1);
            var grphtmp = Graphics.FromImage(tmp);

            var width = Parent.CharacterWidth;
            //width = width > 255 ? 255 : width;

            if (width <= 0) {
                // Nextion requires a minimum width of 1 pixel. It doesn't like zero length chardata as it crashes the Application when rotating the font.
                _Bitmap = new Bitmap(1, height);
                return;
            }

            var b = new Bitmap(width + KerningLeft + KerningRight, height);
            location.X += KerningLeft;

            using (var graphics = Graphics.FromImage(b)) {

                //Adjust for high quality
                graphics.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
                graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                graphics.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.HighQuality;
                graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;  // AntiAlias // HighQuality
                graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAliasGridFit; //AntiAliasGridFit; //    (B/W = SingleBitPerPixelGridFit)
                graphics.TextContrast = contrast;

                // Make sure the background is Transparent as the encoder only looks at the Alpha channel, not Text color
                graphics.FillRectangle(Brushes.Transparent, 0, 0, b.Width, b.Height);
                graphics.DrawString(txt, font, Brushes.Black, location, strFormat);  // experimental

                //TextRenderer.DrawText(graphics, txt, font, new Point(x, y), Color.Black, Color.White);
            }

            _Bitmap = new Bitmap(b.Width, b.Height);
            for (var y = 0; y < b.Height; y++) {
                for (var x = 0; x < b.Width; x++) {
                    var alpha = Get1bppColor(b.GetPixel(x, y), false);
                    SetPixelNumber(y * b.Width + x, Color.FromArgb((255 / 7) * alpha, ForegroundColor));
                }
            }

            b.Dispose();
        }

        private byte Get1bppColor(Color pixel, bool invertColour) {
            var curColor = (byte)0u;
            if (invertColour) {
                curColor = (byte)(255 - pixel.A);
            } else {
                curColor = (byte)pixel.A;
            }
            // convert to 1 bits
            return (byte)(curColor >> 7);
        }

        private byte[] Encode(Bitmap b, bool invertColour = false) {
            var data = new List<byte>();

            return data.ToArray();
        }

        private void Decode() {

            var charData = _CharacterData;
            if (Parent.CharacterWidth > 0 && Parent.CharacterHeight > 0) {
                _Bitmap = new Bitmap(Parent.CharacterWidth, Parent.CharacterHeight);
            } else {
                if (_Bitmap != null && _Bitmap.PixelFormat != System.Drawing.Imaging.PixelFormat.Undefined) {
                    _Bitmap.Dispose();
                }
                return;
            }

            var g = Graphics.FromImage(_Bitmap);
            var bb = new SolidBrush(Color.Black);
            var pr = new Pen(Color.DarkCyan);

            var pixels = BinaryTools.BytesToBits(_CharacterData);
            var pixel = 0;

            for (int y = 0; y < Parent.CharacterHeight; y++) {
                for (int x = 0; x < Parent.CharacterWidth; x++) {
                    if (pixels[pixel]) SetPixelNumber(pixel, Color.Black); // g.FillRectangle(bb, x, y, 1, 1);

                    pixel++;
                }
            }

        }

    }
}