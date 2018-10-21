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
        public string Name { get; set; }
        public byte CharacterWidth { get; set; }
        public byte CharacterHeight { get; set; }
        public UInt32 CharacterCount { get; set; }
        public byte NameLength { get; set; }

        public UInt32 VariableDataLength { get; set; }
        public UInt32 CharDataLength { get; set; }
        public int BytesPerChar { get; set; }

        public byte[] CharData { get; set; }

        public List<Bitmap> CharBitmaps { get; set; } = new List<Bitmap>();

        public static ZiFont FromFile(string fileName) {
            return FromBytes(File.ReadAllBytes(fileName));
        }

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

        public static ZiFont FromBytes(byte[] bytes) {
            var zifont = new ZiFont();

            zifont.Header = bytes.Take(HEADER_LENGTH).ToArray();
            zifont.NameLength = zifont.Header[0x11];
            zifont.Name = Encoding.ASCII.GetString(bytes.Skip(HEADER_LENGTH).Take(zifont.NameLength).ToArray());

            zifont.CharacterWidth = zifont.Header[0x6];
            zifont.CharacterHeight = zifont.Header[0x7];

            zifont.VariableDataLength = BitConverter.ToUInt32(zifont.Header.Skip(0x14).Take(4).ToArray(), 0);
            zifont.CharDataLength = zifont.VariableDataLength - zifont.NameLength;

            zifont.CharData = bytes.Skip(HEADER_LENGTH + zifont.NameLength).ToArray();
            zifont.BytesPerChar = (zifont.CharacterWidth * zifont.CharacterHeight) / 8;

            zifont.CharacterCount = BitConverter.ToUInt32(zifont.Header.Skip(0x0C).Take(4).ToArray(), 0);
            var calculatedCharCount = zifont.CharDataLength / zifont.BytesPerChar;

            if (zifont.CharacterCount != calculatedCharCount) throw new Exception($"{nameof(zifont.CharacterCount)} and {nameof(calculatedCharCount)} doesn't match.");

            return zifont;
        }
    }
}