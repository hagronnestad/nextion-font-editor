using System;
using System.Drawing;
using System.Windows.Forms;
using System.Diagnostics;
using System.Collections.Generic;

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
        byte TotalWidth => (byte)(Width + KerningLeft + KerningRight);
        public byte[] ToBytes() => GetCharacterData();

        private ValidData OriginalFormat;

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
            if (_CharacterData != null) {
                _CharacterData = null;
            }
            KerningLeft = kerningL;
            KerningRight = kerningR;
            Width = (byte)(bmp.Width - kerningL - kerningR);
            DataState = ValidData.BITMAP;
            OriginalFormat = DataState;
            ForegroundColor = Color.Green;
        }
        public ZiCharacterV5(IZiFont parent, uint codepoint, byte[] bytes, byte width, byte kerningL = 0, byte kerningR = 0)
        {
            Parent = parent;
            CodePoint = codepoint;
            if (_Bitmap != null && _Bitmap.PixelFormat != System.Drawing.Imaging.PixelFormat.Undefined) {
                _Bitmap.Dispose();
            }
            _CharacterData = bytes;
            KerningLeft = kerningL;
            KerningRight = kerningR;
            DataState = ValidData.CHARDATA;
            OriginalFormat = DataState;
            Width = width;
            ForegroundColor = Color.Blue;
        }

        public ZiCharacterV5(IZiFont parent, uint codepoint, Font font, PointF location, String txt=null) {
            Parent = parent;
            CodePoint = codepoint;
            SetString(font, location, txt);
            if (_Bitmap != null && _Bitmap.PixelFormat != System.Drawing.Imaging.PixelFormat.Undefined) {
                _Bitmap.Dispose();
            }
            if (_CharacterData != null) {
                _CharacterData = null;
            }
            ForegroundColor = Color.Red;
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
            _Bitmap.SetPixel(l.X, l.Y, color);
        }

        public Bitmap ToBitmap() {
            /* Check if Bitmap bit is 0 in the DataState */
            if ((DataState & ValidData.BITMAP) == 0) {
                if ((DataState & ValidData.CHARDATA) != 0) {
                    Decode();
                } else {
                    DrawString(Txt, Font, Parent.CharacterHeight, TxtPosition);
                    Width = (byte)_Bitmap.Width;
                }
                DataState = DataState | ValidData.BITMAP;
            }
            return _Bitmap;
        }

        public void SetBitmap(Bitmap bmp)
        {
            if (_Bitmap != null && _Bitmap.PixelFormat != System.Drawing.Imaging.PixelFormat.Undefined) {
                using (var graphics = Graphics.FromImage(_Bitmap)) {
                    graphics.FillRectangle(Brushes.White, 0, 0, _Bitmap.Width, _Bitmap.Height);
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

            ForegroundColor = Color.Magenta;
        }

        public Bitmap RevertBitmap()
        {
            DataState = OriginalFormat;
            return ToBitmap();
        }
        public bool CanRevert()
        {
            return (DataState == ValidData.BITMAP);
        }

        /* Byte Array Operations */
        public byte[] GetCharacterData()
        {
            /* Check if CharData bit is 0 in the DataState */
            if ((DataState & ValidData.CHARDATA) == 0) {
                if ((DataState & ValidData.BITMAP) == 0) {
                    var b = ToBitmap();
                }
                _CharacterData = Encode(_Bitmap, false);
                DataState &= ValidData.CHARDATA;
            }
            return _CharacterData;
        }

        private void DrawString(string txt, Font font, byte height, PointF location, byte contrast = 0) {
            var fontsize = (float)height;

            var strFormat = (StringFormat)StringFormat.GenericTypographic.Clone();
            strFormat.FormatFlags = strFormat.FormatFlags | StringFormatFlags.MeasureTrailingSpaces | StringFormatFlags.NoWrap | StringFormatFlags.NoFontFallback;

            var tmp = new Bitmap(1, 1);
            var grphtmp = Graphics.FromImage(tmp);

            var width = (int)Math.Round((grphtmp.MeasureString(txt, font, new PointF(0, 0), strFormat)).Width);
            width = width > 255 ? 255 : width;

            if (width <= 0) {
                // Nextion requires a minimum width of 1 pixel. It doesn't like zero length chardata as it crashes the Application when rotating the font.
                _Bitmap = new Bitmap(1, height);
                return;
            }

            var b = new Bitmap(width, height);

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
                    var alpha = Get3bppColor(b.GetPixel(x, y), false);
                    SetPixelNumber(y*b.Width+x, Color.FromArgb((255 / 7) * alpha, ForegroundColor));
                }
            }

            b.Dispose();
            //return b;
        }

        private byte Get3bppColor(Color pixel, bool invertColour) {
            var curColor = (byte)0u;
            if (invertColour) {
                // curColor = (byte)((pixel.R + 2 * pixel.G + pixel.B) / 4);        // Weighted Color2Grayscale;
                curColor = (byte)(255 - pixel.A);
            } else {
                // curColor = (byte)(255 - (pixel.R + 2 * pixel.G + pixel.B) / 4);  // Weighted Color2Grayscale;
                curColor = (byte)pixel.A;
            }
            // curColor = (byte)(curColor * pixel.A / 255);
            // convert to 3 bits
            return (byte)(curColor >> 5);
        }

        /* A faster 3-bit encoder and compresser combined into one loop instead of consecutive nested loops */
        private byte[] Encode(Bitmap b, bool invertColour = false) {
            var data = new List<byte>();

            byte curColor;
            var prevColor = (byte)0u;
            var prevCount = 0u;

            var antialias = false;


            if (b == null) {
                //return data.ToArray();
            }

            if (b.PixelFormat == System.Drawing.Imaging.PixelFormat.Undefined) {
                return data.ToArray();
            }
            data.Add(0x03);

            for (int y = 0; y < b.Height; y++) {
                for (int x = 0; x < b.Width; x++) {
                    curColor = Get3bppColor(b.GetPixel(x, y), invertColour);

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

                    if (prevColor == 0) {
                        while (prevCount >= 31) {
                            data.Add(CompressedByte.RepeatedWhites(31));
                            prevCount -= 31;
                        }

                        if (prevCount == 0) {
                            // start with fresh color count
                            prevColor = curColor;
                            prevCount = 1;
                            continue;   // to next pixel
                        }

                        /* also check for second black pixel */
                        if (curColor == 7) {
                            var blacks = 1u;
                            var j = (x + 1) >= b.Width ? y + 1 : y;
                            var i = (x + 1) >= b.Width ? 0 : x + 1;
                            while (blacks < 2 && j < b.Height) {
                                var nextColor = Get3bppColor(b.GetPixel(i, j), invertColour);
                                if (nextColor != 7) {
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

                        if (prevCount >= 8) {
                            data.Add(CompressedByte.RepeatedWhites(prevCount));
                            // start with fresh color count
                            prevColor = curColor;
                            prevCount = 1;
                        } else {
                            data.Add(CompressedByte.RepeatedWhites(prevCount, curColor));
                            prevColor = curColor;
                            prevCount = 0;
                        }

                        continue;   // to next pixel
                    }

                    if (prevColor == 7) {
                        while (prevCount >= 31) {
                            data.Add(CompressedByte.RepeatedBlacks(31));
                            prevCount -= 31;
                        }

                        switch (prevCount) {
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

                    while (prevCount >= 2) {
                        data.Add(CompressedByte.AlphaColors(prevColor, prevColor));
                        prevCount -= 2;
                    }

                    if (prevCount == 0) {
                        prevColor = curColor;
                        prevCount = 1;
                    } else {
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
                if (prevColor == 0) {
                    while (prevCount >= 31) {
                        data.Add(CompressedByte.RepeatedWhites(31));
                        prevCount -= 31;
                    }

                    if (prevCount > 0) {
                        data.Add(CompressedByte.RepeatedWhites(prevCount));
                    }

                    return data.ToArray();
                }

                // remaining blacks
                if (prevColor == 7) {
                    while (prevCount >= 31) {
                        data.Add(CompressedByte.RepeatedBlacks(31));
                        prevCount -= 31;
                    }

                    if (prevCount > 0) {
                        data.Add(CompressedByte.RepeatedBlacks(prevCount));
                    }

                    return data.ToArray();
                }

                // remaining grayscales
                while (prevCount >= 2) {
                    data.Add(CompressedByte.AlphaColors(prevColor, prevColor));
                    prevCount -= 2;
                }

                if (prevCount > 0) {
                    data.Add(CompressedByte.RepeatedWhites((byte)0, prevColor));   // zero whites + one color
                }
            }

            return data.ToArray();
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
                                SetPixelNumber(pixelNumber, ForegroundColor);

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

                        SetPixelNumber(pixelNumber, ForegroundColor);
                        pixelNumber++;

                        if (drawingMode2 == 1)
                        {
                            SetPixelNumber(pixelNumber, ForegroundColor);
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

                            SetPixelNumber(pixelNumber, ForegroundColor);
                            pixelNumber++;
                            SetPixelNumber(pixelNumber, ForegroundColor);
                            pixelNumber++;
                            SetPixelNumber(pixelNumber, ForegroundColor);
                            pixelNumber++;

                            if (drawingMode2 == 1)
                            {
                                SetPixelNumber(pixelNumber, ForegroundColor);
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

                            SetPixelNumber(pixelNumber, Color.FromArgb((255 / 7) * alpha, ForegroundColor));
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
                                SetPixelNumber(pixelNumber, ForegroundColor);
                                pixelNumber++;
                            }
                        }
                        else
                        {
                            var alpha1 = (item & 0b00111000) >> 3;
                            var alpha2 = (item & 0b00000111);

                            // should be alpha
                            SetPixelNumber(pixelNumber, Color.FromArgb((255 / 7) * alpha1, ForegroundColor));
                            pixelNumber++;

                            // should be alpha
                            SetPixelNumber(pixelNumber, Color.FromArgb((255 / 7) * alpha2, ForegroundColor));
                            pixelNumber++;
                        }
                    }
                    //}

                }

            }

    }
}