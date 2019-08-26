using System.Drawing;
using System.Windows.Forms;
using System.Diagnostics;

namespace ZiLib.FileVersion.Common
{
    internal enum ValidData : byte
    {
        UNCHANGED = 0x0,
        BITMAP = 0x01,
        CHARDATA = 0x02
    }

    public class Character
    {
        byte ColorMode { get; set; }
        public uint CodePoint { get; set; }

        Bitmap _Bitmap { get; set; }

        ValidData DataState { get; set; }


        byte Width { get; set; }
        byte Height { get; set; }

        byte KerningLeft { get; set; }
        byte KerningRight { get; set; }
        byte[] _CharacterData { get; set; }

        byte TotalWidth => (byte)(Width + KerningLeft + KerningRight);

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

        public Character(Bitmap bmp, uint codepoint, byte kerningL =0, byte kerningR=0)
        {
            CodePoint = codepoint;
            _Bitmap = bmp;
            _CharacterData = new byte[0];
            KerningLeft = kerningL;
            KerningRight = kerningR;
            DataState = ValidData.BITMAP;
        }
        public Character(byte[] bytes, uint codepoint, byte width, byte height, byte kerningL = 0, byte kerningR = 0)
        {
            CodePoint = codepoint;
            if ((width+kerningL+kerningR) > 0 && height > 0)
            {
               // _Bitmap = new Bitmap(width, height);
            }
            else {
                if (_Bitmap!=null && _Bitmap.PixelFormat != System.Drawing.Imaging.PixelFormat.Undefined) {
                    _Bitmap.Dispose();
                }
            }
            _CharacterData = bytes;
            KerningLeft = kerningL;
            KerningRight = kerningR;
            DataState = ValidData.CHARDATA;
            Height = height;
            Width = width;
            //Decompress();
        }

        public Bitmap GetBitmap() {
            if (DataState == ValidData.CHARDATA) {
                Decompress();
                DataState = ValidData.UNCHANGED;
            }
            return _Bitmap;
        }

        public Bitmap RevertBitmap()
        {
            DataState = ValidData.CHARDATA;
            return GetBitmap();
        }
        public bool CanRevert()
        {
            return (DataState == ValidData.BITMAP || (_Bitmap != null && _Bitmap.Tag != null));
        }

        public void SetPixel(int x, int y, System.Drawing.Color pixel)
        {
            DataState = ValidData.BITMAP;
            _Bitmap.SetPixel(x, y, pixel);
        }

        public System.Drawing.Color GetPixel(int x, int y)
        {
            var bmp = GetBitmap();
            return bmp.GetPixel(x, y);
        }

        public void PasteFromClipboard()
        {
            DataState = ValidData.BITMAP;
        }

        public void CopyToClipboard()
        {
            var bmp = GetBitmap();
            System.Windows.Forms.Clipboard.SetImage(bmp);
        }

        public byte[] GetCharacterData()
        {
            if (DataState == ValidData.BITMAP)
            {
                _CharacterData = ZiLib.FileVersion.V5.BinaryTools.BitmapTo3BppData(_Bitmap, false);
                DataState = ValidData.UNCHANGED;
            }
            return _CharacterData;
        }

        public byte[] GetCharacterIndex(uint dataoffset)
        {
            var bytes = new byte[10];
            return bytes;
        }

        private void Decompress() {

            var charData = _CharacterData;
            if (TotalWidth > 0 && Height > 0)
            {
                _Bitmap = new Bitmap(TotalWidth, Height);
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