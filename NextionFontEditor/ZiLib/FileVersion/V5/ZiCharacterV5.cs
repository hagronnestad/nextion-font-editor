using System;
using System.Drawing;
using System.Windows.Forms;
using System.Diagnostics;

namespace ZiLib.FileVersion.V5
{
    internal enum ValidData : byte
    {
        UNCHANGED = 0x0,
        TEXT = 0x01,
        BITMAP = 0x02,
        CHARDATA = 0x04
    }

    public class ZiCharacterV5 : IZiCharacter
    {
        byte ColorMode { get; set; }
        private ValidData DataState { get; set; }

        /* Common Properties */
        public IZiFont Parent { get; set; }
        public uint CodePoint { get; set; }
        public byte Width { get; set; }
        public byte KerningLeft { get; set; }
        public byte KerningRight { get; set; }

        /* FromString Constructor */
        public String Txt { get; set; }
        public PointF TxtPosition { get; set; }
        public Font Font { get; set; }

        /* FromBitmap Constructor and after rendering with DrawString or Decode from bytes */
        private Bitmap _Bitmap { get; set; }

        /* FromBytes constructor and after encoding the bitmap */
        private byte[] _CharacterData { get; set; }

        /* Helpers */
        byte TotalWidth => (byte)(Width + KerningLeft + KerningRight);
        public byte[] ToBytes() => GetCharacterData();

        /* Constructors */
        public ZiCharacterV5(IZiFont parent, uint codepoint, Bitmap bmp, byte kerningL = 0, byte kerningR = 0)
        {
            Parent = parent;
            CodePoint = codepoint;
            _Bitmap = new Bitmap(bmp.Width, bmp.Height);
            using (var graphics = Graphics.FromImage(_Bitmap))
            {
                graphics.DrawImage(bmp, 0, 0);
            }
            _CharacterData = new byte[0];
            KerningLeft = kerningL;
            KerningRight = kerningR;
            Width = (byte)(bmp.Width - kerningL - kerningR);
            DataState = ValidData.BITMAP;
        }
        public ZiCharacterV5(IZiFont parent, uint codepoint, byte[] bytes, byte width, byte kerningL = 0, byte kerningR = 0)
        {
            Parent = parent;
            CodePoint = codepoint;
            if ((width + kerningL + kerningR) > 0 && Parent.CharacterHeight > 0)
            {
                // _Bitmap = new Bitmap(width, height);
            }
            else
            {
                if (_Bitmap != null && _Bitmap.PixelFormat != System.Drawing.Imaging.PixelFormat.Undefined)
                {
                    _Bitmap.Dispose();
                }
            }
            _CharacterData = bytes;
            KerningLeft = kerningL;
            KerningRight = kerningR;
            DataState = ValidData.CHARDATA;
            Width = width;
            // Decode only when needed;
        }

        public ZiCharacterV5(IZiFont parent, uint codepoint, Font font, PointF location, String txt=null) {
            Parent = parent;
            CodePoint = codepoint;
            SetString(font, location, txt);
        }

        /* Bitmap Operations */
        public void SetPixel(int x, int y, System.Drawing.Color pixel)
        {
            DataState = ValidData.BITMAP;
            _Bitmap.SetPixel(x, y, pixel);
        }

        public Color GetPixel(int x, int y)
        {
            var bmp = ToBitmap();
            return bmp.GetPixel(x, y);
        }

        public void SetPixelNumber(int number, Color color)
        {
            if (number >= _Bitmap.Width * _Bitmap.Height)
            {
                Debug.WriteLine($"!!! pixelNumber >= {_Bitmap.Width * _Bitmap.Height} !!!");
                return;
            }

            var l = ZiLib.Extensions.BitmapExtensions.NumberToPoint(number, _Bitmap.Width);
            SetPixel(l.X, l.Y, color);
        }

        public Bitmap ToBitmap() {
            /* Check if Bitmap bit is 0 in the DataState */
            if ((DataState & ValidData.BITMAP) == 0) {
                if ((DataState & ValidData.CHARDATA) != 0) {
                    Decode();
                } else {
                    _Bitmap = DrawString(Txt, Font, Parent.CharacterHeight, TxtPosition);
                    Width = (byte)_Bitmap.Width;
                }
                DataState = DataState | ValidData.BITMAP;
            }
            return _Bitmap;
        }

        public void SetBitmap(Bitmap bmp)
        {
            _Bitmap = new Bitmap(_Bitmap.Width, _Bitmap.Height);
            using (var graphics = Graphics.FromImage(_Bitmap))
            {
                graphics.FillRectangle(Brushes.White, 0, 0, _Bitmap.Width, _Bitmap.Height);
                graphics.DrawImage(bmp, 0, 0);
            }
            DataState = ValidData.BITMAP;
        }

        public void SetString(Font font, PointF location, String txt = null) {
            DataState = ValidData.TEXT;

            Txt = txt;
            TxtPosition = location;
            Font = font;
        }

        public Bitmap RevertBitmap()
        {
            DataState = ValidData.CHARDATA;
            return ToBitmap();
        }
        public bool CanRevert()
        {
            return (DataState == ValidData.BITMAP || (_Bitmap != null && _Bitmap.Tag != null));
        }

        /* Byte Array Operations */
        public byte[] GetCharacterData()
        {
            /* Check if CharData bit is 0 in the DataState */
            if ((DataState & ValidData.CHARDATA) == 0) {
                _CharacterData = ZiLib.FileVersion.V5.BinaryTools.BitmapTo3BppData(_Bitmap, false);
                DataState = DataState & ValidData.CHARDATA;
            }
            return _CharacterData;
        }

        private Bitmap DrawString(string txt, Font font, byte height, PointF location, byte contrast = 0) {
            var fontsize = (float)height;

            var strFormat = (StringFormat)StringFormat.GenericTypographic.Clone();
            strFormat.FormatFlags = strFormat.FormatFlags | StringFormatFlags.MeasureTrailingSpaces | StringFormatFlags.NoWrap | StringFormatFlags.NoFontFallback;

            var tmp = new Bitmap(1, 1);
            var grphtmp = Graphics.FromImage(tmp);

            var width = (int)Math.Round((grphtmp.MeasureString(txt, font, new PointF(0, 0), strFormat)).Width);
            width = width > 255 ? 255 : width;

            if (width > 0) {
                var b = new Bitmap(width, height);

                using (var graphics = Graphics.FromImage(b)) {

                    //Adjust for high quality
                    graphics.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
                    graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                    graphics.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.HighQuality;
                    graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;  // AntiAlias // HighQuality
                    graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAliasGridFit; //AntiAliasGridFit; //    (B/W = SingleBitPerPixelGridFit)
                    graphics.TextContrast = contrast;

                    graphics.FillRectangle(Brushes.White, 0, 0, b.Width, b.Height);
                    graphics.DrawString(txt, font, Brushes.Black, location, strFormat);  // experimental

                    //TextRenderer.DrawText(graphics, txt, font, new Point(x, y), Color.Black, Color.White);
                }
                return b;

            } else {
                // Nextion requires a minimum width of 1 pixel. It doesn't like zero length chardata as it crashes the Application when rotating the font.
                return new Bitmap(1, height);
            }

        }

        private void Decode() {

            var charData = _CharacterData;
            if (TotalWidth > 0 && Parent.CharacterHeight > 0)
            {
                _Bitmap = new Bitmap(TotalWidth, Parent.CharacterHeight);
            }
            else
            {
                if (_Bitmap != null && _Bitmap.PixelFormat != System.Drawing.Imaging.PixelFormat.Undefined)
                {
                    _Bitmap.Dispose();
                }
                return;
            }
            var pixelNumber = 0;

            if (charData.Length == 0) { return;  }

            var bitdepth = charData[0];

            for (int i = 1; i<_CharacterData.Length; i++)
            {
                var item = _CharacterData[i];
                var drawingMode = item >> 6;
                var drawingMode2 = (item & 0b00100000) >> 5;
                var number = (item & 0b00011111);

                // B & W
                //if (charDataChar2[0] == 1) {

                if (drawingMode == 0)
                {

                    if (drawingMode2 == 0)
                    {
                        for (int p = 0; p < number; p++)
                        {
                            SetPixelNumber(pixelNumber, Color.Transparent);

                            pixelNumber++;
                        }
                    }

                    if (drawingMode2 == 1)
                    {
                        for (int p = 0; p < number; p++)
                        {
                            SetPixelNumber(pixelNumber, Color.Black);

                            pixelNumber++;
                        }
                    }

                }

                if (drawingMode == 1)
                {

                    for (int p = 0; p < number; p++)
                    {
                        SetPixelNumber(pixelNumber, Color.Transparent);

                        pixelNumber++;
                    }

                    SetPixelNumber(pixelNumber, Color.Black);
                    pixelNumber++;

                    if (drawingMode2 == 1)
                    {
                        SetPixelNumber(pixelNumber, Color.Black);
                        pixelNumber++;
                    }

                }
                //}

                //// Alpha
                //if (charDataChar2[0] == 3) {

                if (drawingMode == 2)
                {

                    if (bitdepth == 1)
                    {
                        for (int p = 0; p < number; p++)
                        {
                            SetPixelNumber(pixelNumber, Color.Transparent);

                            pixelNumber++;
                        }

                        SetPixelNumber(pixelNumber, Color.Black);
                        pixelNumber++;
                        SetPixelNumber(pixelNumber, Color.Black);
                        pixelNumber++;
                        SetPixelNumber(pixelNumber, Color.Black);
                        pixelNumber++;

                        if (drawingMode2 == 1)
                        {
                            SetPixelNumber(pixelNumber, Color.Black);
                            pixelNumber++;
                        }
                    }
                    else
                    {
                        number = (item & 0b00111000) >> 3;
                        var alpha = (item & 0b00000111);

                        for (int p = 0; p < number; p++)
                        {
                            SetPixelNumber(pixelNumber, Color.Transparent);

                            pixelNumber++;
                        }

                        SetPixelNumber(pixelNumber, Color.FromArgb((255 / 7) * alpha, Color.Black));
                        pixelNumber++;
                    }
                }

                if (drawingMode == 3)
                {

                    if (bitdepth == 1)
                    {
                        var whites = (item & 0b00111000) >> 3;
                        var blacks = (item & 0b00000111);

                        for (int p = 0; p < whites; p++)
                        {
                            SetPixelNumber(pixelNumber, Color.Transparent);

                            pixelNumber++;
                        }

                        for (int p = 0; p < blacks; p++)
                        {
                            SetPixelNumber(pixelNumber, Color.Black);

                            pixelNumber++;
                        }
                    }
                    else
                    {
                        var alpha1 = (item & 0b00111000) >> 3;
                        var alpha2 = (item & 0b00000111);

                        // should be alpha
                        SetPixelNumber(pixelNumber, Color.FromArgb((255 / 7) * alpha1, Color.Black));
                        pixelNumber++;

                        // should be alpha
                        SetPixelNumber(pixelNumber, Color.FromArgb((255 / 7) * alpha2, Color.Black));
                        pixelNumber++;
                    }
                }
                //}

            }

        }

    }
}