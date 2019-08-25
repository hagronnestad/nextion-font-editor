using System;
using System.IO;
using System.Linq;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows;
//using System.Windows.Forms;
using System.Collections.Generic;
using System.Text;
using ZiLib.FileVersion.V5;

namespace ZiLib
{
    public class CharBitmap
    {
        // The Character Index
        public UInt16 CodePoint { get; set; }
        private byte[] _charData { get; set; }

        // The Bitmap image of the character
        public Bitmap Bmp { get; set; }

        // Kerning bytes [Left] [Right]
        public byte[] Kerning = new byte[] { 0x00, 0x00 };

        // Constructors
        public CharBitmap(ushort character)
        {
            CodePoint = character;
            Bmp = new Bitmap(1, 1);
            Bmp.Dispose();          // Make it undefined initially
        }
        public CharBitmap(ushort character, byte width, byte height)
        {
            CodePoint = character;
            //Kerning[0] = kerningL;
            //Kerning[1] = kerningR;
            if (width > 0 && height > 0)
            {
                Bmp = new Bitmap(width, height);
            }
            else {
                Bmp = new Bitmap(1, 1);
                Bmp.Dispose();          // Make it undefined initially
            }
        }
        // From chardata
        public CharBitmap(byte[] index, byte[] chardata, byte height)
        {
            if (index.Length != 10) { return; }

            CodePoint = (ushort)(index[0] + index[1] * 256);

            Kerning[0] = index[3];
            Kerning[1] = index[4];

            var width = index[2];
            Bmp = new Bitmap(width, height);
            _charData = chardata;
            DrawCharacterData(chardata);
        }
        // From Font Spec
        public CharBitmap(ushort character, string fontname, byte height, FontStyle style, byte x = 0, byte y = 0, byte contrast = 0)
        {
            CodePoint = character;
            DrawString(character.ToString(), fontname, height, style, x, y, contrast);
        }

        public byte[] GetCharacterData()
        {
            return _charData;
        }
        public uint GetCharacterDataLength()
        {
            return (uint)_charData.Length;
        }

        public void DrawCharacterData(byte[] chardata)
        {
            if (chardata.Length <= 1)
            {
                return;
            }

            var bpp = chardata[0];
            if (bpp != 3)
            {
                return;
            }

            var x = 0;
            var y = 0;
            for (int i = 1; i < chardata.Length; i++)
            {
                var pixels = CompressedByte.InflateToPixels(chardata[i]);

                foreach (var pixel in pixels)
                {
                    Bmp.SetPixel(x++, y, pixel);
                    if (x >= Bmp.Width)
                    {
                        x = 0;
                        y++;
                    }
                }
            }
        }

        public void DrawCharacterData2(byte width, byte height, byte[] chardata)
        {

            if (chardata.Length <= 1)
            {
                return;
            }

            var bpp = chardata[0];
            if (bpp != 3)
            {
                return;
            }

            /*
            var x = 0;
            var y = 0;
            for (int i=1; i<bytes.Length; i++) {
                var pixels = CompressedByte.ToPixels(bytes[i]);

                foreach (var pixel in pixels) {
                    Bmp.SetPixel(x++, y, pixel );

                    if (x >= Bmp.Width) {
                        x=0;
                        y++;
                    }
                }
            }
            */

            // Convert uint[] to bytes[]
            //byte[] bytes = new byte[colors.Length * sizeof(uint)];
            //Buffer.BlockCopy(colors, 0, bytes, 0, bytes.Length);

            var colors = new List<uint>();
            for (int i = 1; i < chardata.Length; i++)
            {
                var b = chardata[i];
                var size = b & 0b00011111;

                switch (b >> 5)
                {
                    case 0b000:
                        for (int j = 0; j < size; j++)
                        {
                            colors.Add(0x000000); //Color.White);
                        }
                        break;

                    case 0b001:
                        for (int j = 0; j < size; j++)
                        {
                            colors.Add(0xffffff); //Color.Black);
                        }
                        break;

                    case 0b010:
                        for (int j = 0; j < size; j++)
                        {
                            colors.Add(0x000000); //Color.White);
                        }
                        colors.Add(0xffffff); //Color.Black);
                        break;

                    case 0b011:
                        for (int j = 0; j < size; j++)
                        {
                            colors.Add(0x000000); //Color.White);
                        }
                        colors.Add(0xffffff); //Color.Black);
                        colors.Add(0xffffff); //Color.Black);
                        break;

                    case 0b100:
                    case 0b101:
                        size = (b & 0b111000) >> 3;
                        var color = b & 0b111;
                        for (int j = 0; j < size; j++)
                        {
                            colors.Add(0x000000); //Color.White);
                        }
                        colors.Add((uint)(color + color * 256 + color * 256 * 256)); // CompressedByte.ToColor((byte)color));
                        break;

                    case 0b110:
                    case 0b111:
                        var color1 = (b & 0b111000) >> 3;
                        var color2 = b & 0b111;
                        colors.Add((uint)(color1 + color1 * 256 + color1 * 256 * 256)); // CompressedByte.ToColor((byte)color));
                        colors.Add((uint)(color2 + color2 * 256 + color2 * 256 * 256)); // CompressedByte.ToColor((byte)color));
                        break;

                    default:
                        throw new Exception("Invalid drawing mode encounterd.");
                }
            }

            var data = colors.ToArray();
            byte[] decoded = new byte[data.Length * sizeof(uint)];
            Buffer.BlockCopy(data, 0, decoded, 0, decoded.Length);

            /*unsafe
            {
                fixed (byte* ptr = decoded)
                {
                    Bmp = new Bitmap(width, height, width * 4, PixelFormat.Format32bppRgb, new IntPtr(ptr));
                }
            }*/
        }

        public void Compress()
        {
            if (Bmp.PixelFormat == PixelFormat.Undefined)
            {
                // Clear Tag Data
                _charData = new byte[0];
                return;
            }
            if (Bmp.Width <= 0)
            {
                // Clear Tag Data
                _charData = new byte[0];
                return;
            }

            var bytes = BinaryTools.BitmapTo3BppData(Bmp);
            //var depth = (byte)3;

            var length = bytes.Length;
            //var data = new List<byte>();

            // Possible Optimization: strip all trailing transparent bytes as they contain no visible data
            /*while(length>0)
            {
                if (bytes[length - 1] != 0) { break; }
                length--;
            }*/

            if (length < 1)
            {
                return;     // No data to compress
            }

            var data = CompressedByte.Compress3bpp(bytes);
            _charData = data;
        }

        // Get Index, a 10 byte lookup table entry
        public byte[] GetIndex(uint dataoffset = 0)
        {
            var bytes = new byte[10];
            var ch = BitConverter.GetBytes(CodePoint);

            /*    switch (ch.Length) {
                    case 0:
                        bytes[0] = bytes[1] = 0x00;
                        break;
                    case 1:
                        bytes[0] = ch[0];       // ascii
                        bytes[1] = 0x00;
                        break;
                    case 2:
                        if (codepage.Encoding.IsSingleByte) { */
            bytes[0] = ch[0];
            bytes[1] = ch[1];
            /*                break;
                        } else {
                            bytes[0] = ch[0];
                            bytes[1] = ch[1];
                            break;
                        }
                    default:
                        throw new Exception("Character Index has more than 2 bytes allowed.");
                }*/

            var width = (Bmp.PixelFormat == PixelFormat.Undefined) ? 0 : Bmp.Width;

            bytes[2] = (byte)(width - Kerning[0] - Kerning[1]);
            bytes[3] = Kerning[0];
            bytes[4] = Kerning[1];

            bytes[5] = BitConverter.GetBytes(dataoffset)[0];
            bytes[6] = BitConverter.GetBytes(dataoffset)[1];
            bytes[7] = BitConverter.GetBytes(dataoffset)[2];

            bytes[8] = BitConverter.GetBytes(_charData.Length)[0];
            bytes[9] = BitConverter.GetBytes(_charData.Length)[1];

            return bytes;
        }

        // Create Bitmap for a character and paint the txt on it in the specified style
        public void DrawString(string txt, string fontname, byte height, FontStyle style = FontStyle.Regular, byte x = 0, byte y = 0, byte contrast = 0)
        {
            var fontsize = (float)height;

            var strFormat = (StringFormat)StringFormat.GenericTypographic.Clone();
            strFormat.FormatFlags = strFormat.FormatFlags | StringFormatFlags.MeasureTrailingSpaces | StringFormatFlags.NoWrap;

            var tmp = new Bitmap(1, 1);
            var grphtmp = Graphics.FromImage(tmp);

            var font = new Font("Arial", 12);

            // Check if it is a otf/ttf file
            if (((Path.GetExtension(fontname) == ".ttf") || (Path.GetExtension(fontname) == ".otf")) && (File.Exists(fontname)))
            {

                var pfc = new System.Drawing.Text.PrivateFontCollection();
                pfc.AddFontFile(fontname);

                font = new Font(pfc.Families[0], fontsize, style, GraphicsUnit.Pixel);
                while (Math.Abs(font.Height - height) > 0.1)
                {
                    fontsize += (float)(height - font.Height) / 2;
                    //font.Dispose();
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
                    //font.Dispose();
                    font = new Font(fontname, fontsize, style, GraphicsUnit.Pixel);
                }

            }
            var width = (int)Math.Round((grphtmp.MeasureString(txt, font, new PointF(0, 0), strFormat)).Width);
            width = width > 255 ? 255 : width;

            if (width > 0)
            {
                //Bmp.Dispose();
                Bmp = new Bitmap(width, height);
                var graphics = Graphics.FromImage(Bmp);

                //Adjust for high quality
                graphics.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
                graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                graphics.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.HighQuality;
                graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;  // AntiAlias // HighQuality
                graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAliasGridFit; //AntiAliasGridFit; //    (B/W = SingleBitPerPixelGridFit)

                var brushBg = Brushes.White;
                var brushFg = Brushes.Black;

                graphics.FillRectangle(brushBg, 0, 0, Bmp.Width, Bmp.Height);
                graphics.TextContrast = contrast;
                graphics.DrawString(txt, font, brushFg, x, y, (StringFormat)StringFormat.GenericTypographic.Clone());  // experimental
                //TextRenderer.DrawText(graphics, txt, font, new Point(x, y), Color.Black, Color.White);
                graphics.Dispose();

            }
            else
            {
                Bmp = new Bitmap(1, 1);
                Bmp.Dispose();
            }// if

        } // void

    } // class

} // namespace