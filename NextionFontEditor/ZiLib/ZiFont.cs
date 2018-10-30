using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;

namespace ZiLib {

    public class ZiFont {
        private readonly byte[] MagicNumbers = new byte[] { 0x04, 0xFF, 0x00, 0x0A };
        private const int HEADER_LENGTH = 0x1C; // 28

        public byte[] Header { get; set; }
        public UInt16 CodePage { get; set; }
        public byte CharacterWidth { get; set; }
        public byte CharacterHeight { get; set; }
        public byte CodePageMultibyteFirstByteStart { get; set; }
        public byte CodePageMultibyteFirstByteEnd { get; set; }
        public byte CodePageStartOrMultibyteSecondByteStart { get; set; }
        public byte CodePageEndOrMultibyteSecondByteEnd { get; set; }
        public UInt32 CharacterCount { get; set; }
        public byte FileFormatVersion => 3;
        public byte NameLength { get; set; }
        public string Name { get; set; }

        public UInt32 VariableDataLength { get; set; }
        public UInt32 CharDataLength { get; set; }
        public int BytesPerChar { get; set; }

        public byte[] CharData { get; set; }

        public List<Bitmap> CharBitmaps { get; set; } = new List<Bitmap>();

        public void CreateBitmaps() {
            CharBitmaps.Clear();

            var bb = new SolidBrush(Color.Black);
            var pr = new Pen(Color.DarkCyan);

            for (int charIndex = 0; charIndex < CharacterCount; charIndex++) {
                var charBitmap = new Bitmap(CharacterWidth, CharacterHeight);
                var g = Graphics.FromImage(charBitmap);

                var charData = CharData.Skip(charIndex * BytesPerChar).Take(BytesPerChar).ToArray();
                var pixels = BinaryTools.BytesToBits(charData);

                var pixel = 0;

                for (int y = 0; y < CharacterHeight; y++) {
                    for (int x = 0; x < CharacterWidth; x++) {
                        if (pixels[pixel]) g.FillRectangle(bb, x, y, 1, 1);

                        pixel++;
                    }
                }

                CharBitmaps.Add(charBitmap);
            }
        }

        public void Save(string fileName, CodePage codePage) {
            var file = new List<byte>();

            file.AddRange(MagicNumbers);
            file.AddRange(BitConverter.GetBytes((ushort) codePage.CodePageIdentifier));
            file.Add(CharacterWidth);
            file.Add(CharacterHeight);
            file.Add((byte) (codePage.IsMultibyte ? codePage.FirstByteStart : 0));
            file.Add((byte) (codePage.IsMultibyte ? codePage.FirstByteEnd : 0));
            file.Add((byte) (codePage.IsMultibyte ? codePage.SecondByteStart : codePage.FirstByteStart));
            file.Add((byte) (codePage.IsMultibyte ? codePage.SecondByteEnd : codePage.FirstByteEnd));
            file.AddRange(BitConverter.GetBytes(CharacterCount));
            file.Add(FileFormatVersion);
            file.Add(NameLength);
            file.Add(NameLength);
            file.Add(0x00); // Reserved
            file.AddRange(BitConverter.GetBytes(VariableDataLength));
            file.Add(0x00); // Reserved
            file.Add(0x00); // Reserved
            file.Add(0x00); // Reserved
            file.Add(0x00); // Reserved
            file.AddRange(Encoding.ASCII.GetBytes(Name));
            file.AddRange(CharData);

            File.WriteAllBytes(fileName, file.ToArray());
        }

        public static ZiFont FromFile(string fileName) {
            return FromBytes(File.ReadAllBytes(fileName));
        }

        public static ZiFont FromBytes(byte[] bytes) {
            var ziFont = new ZiFont();

            ziFont.Header = bytes.Take(HEADER_LENGTH).ToArray();
            ziFont.NameLength = ziFont.Header[0x11];
            ziFont.Name = Encoding.ASCII.GetString(bytes.Skip(HEADER_LENGTH).Take(ziFont.NameLength).ToArray());

            ziFont.CharacterWidth = ziFont.Header[0x6];
            ziFont.CharacterHeight = ziFont.Header[0x7];

            ziFont.VariableDataLength = BitConverter.ToUInt32(ziFont.Header.Skip(0x14).Take(4).ToArray(), 0);
            ziFont.CharDataLength = ziFont.VariableDataLength - ziFont.NameLength;

            ziFont.CharData = bytes.Skip(HEADER_LENGTH + ziFont.NameLength).ToArray();
            ziFont.BytesPerChar = (ziFont.CharacterWidth * ziFont.CharacterHeight) / 8;

            ziFont.CharacterCount = BitConverter.ToUInt32(ziFont.Header.Skip(0x0C).Take(4).ToArray(), 0);
            var calculatedCharCount = ziFont.CharDataLength / ziFont.BytesPerChar;

            if (ziFont.CharacterCount != calculatedCharCount) throw new Exception($"{nameof(ziFont.CharacterCount)} and {nameof(calculatedCharCount)} doesn't match.");

            return ziFont;
        }

        public static ZiFont FromCharacterBitmaps(string fontName, byte width, byte height, List<Bitmap> characters) {
            var bytesPerChar = width * height / 8;
            var charDataLength = (uint) (bytesPerChar * characters.Count());

            var ziFont = new ZiFont {
                Name = fontName,
                NameLength = (byte) fontName.Length,
                CharacterWidth = width,
                CharacterHeight = height,
                CharacterCount = (uint) characters.Count(),
                CharDataLength = charDataLength,
                VariableDataLength = (uint) fontName.Length + charDataLength,
                BytesPerChar = bytesPerChar
            };

            var charData = new List<byte>();
            foreach (var cb in characters) {
                charData.AddRange(BinaryTools.BitmapTo1BppData(cb));
            }
            ziFont.CharData = charData.ToArray();

            return ziFont;
        }
    }
}