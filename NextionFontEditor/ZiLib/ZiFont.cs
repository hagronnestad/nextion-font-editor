﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;

namespace ZiLib {

    public class ZiFont {
        private static readonly byte[] MagicNumbers = new byte[] { 0x04, 0xFF, 0x00, 0x0A };
        private static readonly byte[] MagicNumbersBig5 = new byte[] { 0x04, 0x7E, 0x22, 0x0A }; // The two middle bytes might not be static

        private const int HEADER_LENGTH = 0x1C; // 28

        private byte[] _header { get; set; }
        private byte[] _charData { get; set; }

        public byte CharacterWidth { get; set; }
        public byte CharacterHeight { get; set; }

        public CodePage CodePage { get; set; }

        public byte FileFormatVersion => 3;
        public byte NameLength { get; set; }
        public string Name { get; set; }
        public long FileSize { get; set; }

        public UInt32 VariableDataLength { get; set; }
        public UInt32 CharDataLength { get; set; }
        public int BytesPerChar { get; set; }

        public List<Bitmap> CharBitmaps { get; set; } = new List<Bitmap>();

        //public ZiFont(byte width, byte height, CodePage codePage) {
        //    CharacterWidth = width;
        //    CharacterHeight = height;
        //    CodePage = codePage;
        //}
        public void Save(string fileName, CodePage codePage)
        {
            CodePage = codePage;
            _charData = CreateCharData(CharBitmaps);
            Save(fileName);
        }

            public void Save(string fileName) {
  //          _charData = CreateCharData(CharBitmaps);

            var file = new List<byte>();

            file.AddRange(MagicNumbers);
            file.AddRange(BitConverter.GetBytes((ushort) CodePage.CodePageIdentifier));
            file.Add(CharacterWidth);
            file.Add(CharacterHeight);
            file.Add((byte) (CodePage.IsMultibyte ? CodePage.FirstByteStart : 0));
            file.Add((byte) (CodePage.IsMultibyte ? CodePage.FirstByteEnd : 0));
            file.Add((byte) (CodePage.IsMultibyte ? CodePage.SecondByteStart : CodePage.FirstByteStart));
            file.Add((byte) (CodePage.IsMultibyte ? CodePage.SecondByteEnd : CodePage.FirstByteEnd));
            file.AddRange(BitConverter.GetBytes(CodePage.CharacterCount));
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
            file.AddRange(_charData);

            File.WriteAllBytes(fileName, file.ToArray());
        }

        public static ZiFont FromFile(string fileName) {
            return FromBytes(File.ReadAllBytes(fileName));
        }

        public static ZiFont FromBytes(byte[] bytes) {
            if (!VerifyHeader(bytes)) return null;

            var ziFont = new ZiFont();

            ziFont._header = bytes.Take(HEADER_LENGTH).ToArray();
            ziFont.NameLength = ziFont._header[0x11];
            ziFont.Name = Encoding.ASCII.GetString(bytes.Skip(HEADER_LENGTH).Take(ziFont.NameLength).ToArray());
            ziFont.FileSize = bytes.Length;

            ziFont.CharacterWidth = ziFont._header[0x6];
            ziFont.CharacterHeight = ziFont._header[0x7];

            ziFont.VariableDataLength = BitConverter.ToUInt32(ziFont._header.Skip(0x14).Take(4).ToArray(), 0);
            ziFont.CharDataLength = ziFont.VariableDataLength - ziFont.NameLength;

            ziFont._charData = bytes.Skip(HEADER_LENGTH + ziFont.NameLength).ToArray();
            ziFont.BytesPerChar = (ziFont.CharacterWidth * ziFont.CharacterHeight) / 8;

            var codePageId = BitConverter.ToUInt16(ziFont._header.Skip(0x4).Take(2).ToArray(), 0);
            var characterCount = BitConverter.ToUInt32(ziFont._header.Skip(0x0C).Take(4).ToArray(), 0);
            var calculatedCharCount = ziFont.CharDataLength / ziFont.BytesPerChar;

            ziFont.CodePage = CodePages.GetCodePage((CodePageIdentifier) codePageId);

            if (characterCount != calculatedCharCount) throw new Exception($"{nameof(characterCount)} and {nameof(calculatedCharCount)} doesn't match.");

            ziFont.CreateBitmaps();

            return ziFont;
        }

        private void CreateBitmaps() {
            CharBitmaps.Clear();

            var bb = new SolidBrush(Color.Black);
            var pr = new Pen(Color.DarkCyan);

            for (int charIndex = 0; charIndex < CodePage.CharacterCount; charIndex++) {
                var charBitmap = new Bitmap(CharacterWidth, CharacterHeight);
                var g = Graphics.FromImage(charBitmap);

                var charData = _charData.Skip(charIndex * BytesPerChar).Take(BytesPerChar).ToArray();
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

        private byte[] CreateCharData(List<Bitmap> characters, bool invertColour=false) {
            var charData = new List<byte>();

            foreach (var cb in characters) {
                charData.AddRange(BinaryTools.BitmapTo1BppData(cb, invertColour));
            }

            return charData.ToArray();
        }

        public static ZiFont FromCharacterBitmaps(string fontName, byte width, byte height, CodePage codePage, List<Bitmap> characters, bool invertColour = false) {
            var bytesPerChar = width * height / 8;
            var charDataLength = (uint) (bytesPerChar * characters.Count());

            var ziFont = new ZiFont {
                Name = fontName,
                NameLength = (byte) fontName.Length,
                CharacterWidth = width,
                CharacterHeight = height,
                CodePage = codePage,
                CharDataLength = charDataLength,
                VariableDataLength = (uint) fontName.Length + charDataLength,
                BytesPerChar = bytesPerChar,
            };

            ziFont._charData = ziFont.CreateCharData(characters, invertColour);

            //var charData = new List<byte>();
            //foreach (var cb in characters) {
            //    charData.AddRange(BinaryTools.BitmapTo1BppData(cb));
            //}
            //ziFont._charData = charData.ToArray();

            return ziFont;
        }

        public static bool VerifyHeader(byte[] header) {
            if (header.Length < 4) return false;
            if (header.Length > 5) header = header.Take(4).ToArray();

            if (header.SequenceEqual(MagicNumbers)) return true;
            if (header.SequenceEqual(MagicNumbersBig5)) return true;

            return false;
        }
    }
}