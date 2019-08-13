using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using ZiLib.Extensions;
using ZiLib.FileVersion.Common;

namespace ZiLib.FileVersion.V5 {

    public class ZiFontV5 : IZiFont {
        private static readonly byte[] MagicNumbers = new byte[] { 0x04 };

        private const int HEADER_LENGTH = 0x2C; // 44
        private const UInt32 FontDataStartAddress = 0x2C;

        public byte[] bytes { get; set; }
        private byte[] _header { get; set; }
        private byte[] _charData { get; set; }

        public FontOrientation Orientation { get; set; } = FontOrientation.Vertical;

        public byte CharacterWidth { get; set; } // 0 = variable width font
        public byte CharacterHeight { get; set; }
        public UInt32 CharacterCount { get; set; }

        public List<CharMapEntry> CharacterEntries = new List<CharMapEntry>();

        public CodePage CodePage { get; set; }

        public byte Version { get; set; } = 5;
        public byte FileNameLength => (byte)Name.Length;
        public string Name { get; set; }
        public long FileSize { get; set; }

        public UInt32 VariableDataLength { get; set; }
        public UInt32 CharDataLength { get; set; }

        public List<CharBitmap> CharBitmaps { get; set; } = new List<CharBitmap>();

        public void Save(string fileName, CodePage codePage) {
            CodePage = codePage;
            _charData = CreateCharData(CharBitmaps);
            Save(fileName);
        }

        public void Save(string fileName) {
            var file = new List<byte>();

            file.AddRange(MagicNumbers);
            // Byte 2 and 3 are dependent on the codepage
            file.Add((byte)CodePage.FirstByteSkipAfter);
            file.Add((byte)CodePage.FirstByteSkipCount);
            file.Add((byte) Orientation);
            // Just one byte for the codepage id
            file.Add((byte)CodePage.CodePageIdentifier);
            // The next byte has a different meaning: 
            file.Add(0x00);  // 0x00 for single byte // 0x01 for double bute // In V6: also 0x02 for a subset of characters instead of a full caodepage
            file.Add(CharacterWidth);
            file.Add(CharacterHeight);
            file.Add((byte)CodePage.FirstByteStart);
            file.Add((byte)CodePage.FirstByteEnd);
            file.Add((byte)CodePage.SecondByteStart);
            file.Add((byte)CodePage.SecondByteEnd);
            file.AddRange(BitConverter.GetBytes(CodePage.CharacterCount));
            file.Add(Version);
            file.Add(FileNameLength);    // = FileNameLength + EncondingnameLength
            file.Add(0x00); // Reserved
            file.AddRange(BitConverter.GetBytes(VariableDataLength)); // Length of font name and data
            file.AddRange(BitConverter.GetBytes(FontDataStartAddress));
            file.Add(0x00); // Character width (0 = variable width font)
            file.Add(0x01); // VariableWidth
            file.Add(FileNameLength);
            file.Add(0x00); // 0x01 : Chardata is 8 byte aligned or 0x00 chardata is 1 byte aligned

            file.Add(0x00); // Reserved
            file.Add(0x00); // Reserved

            file.Add(0x00); // Reserved
            file.Add(0x00); // Reserved
            file.Add(0x00); // Reserved
            file.Add(0x00); // Reserved

            file.Add(0x00); // Reserved
            file.Add(0x00); // Reserved
            file.Add(0x00); // Reserved
            file.Add(0x00); // Reserved

            file.AddRange(Encoding.ASCII.GetBytes(Name));
            file.AddRange(_charData);

            File.WriteAllBytes(fileName, file.ToArray());
        }

        public static ZiFontV5 FromFile(string fileName) {
            return FromBytes(File.ReadAllBytes(fileName));
        }

        public static ZiFontV5 FromBytes(byte[] bytes) {
            if (!VerifyHeader(bytes)) return null;

            var ziFont = new ZiFontV5();

            ziFont.bytes = bytes;

            ziFont._header = bytes.Take(HEADER_LENGTH).ToArray();
            var fileNameLength = ziFont._header[0x11];
            ziFont.Name = Encoding.ASCII.GetString(bytes.Skip(HEADER_LENGTH).Take(fileNameLength).ToArray());
            ziFont.FileSize = bytes.Length;

            ziFont.CharacterWidth = ziFont._header[0x6];
            ziFont.CharacterHeight = ziFont._header[0x7];

            ziFont.VariableDataLength = BitConverter.ToUInt32(ziFont._header.Skip(0x14).Take(4).ToArray(), 0);
            ziFont.CharDataLength = ziFont.VariableDataLength - fileNameLength;

            var dataStartAddress = BitConverter.ToUInt32(ziFont._header.Skip(0x18).Take(4).ToArray(), 0);

            ziFont._charData = bytes.Skip((int) dataStartAddress + fileNameLength).ToArray();

            var codePageId = ziFont._header[0x4];
            ziFont.CharacterCount = BitConverter.ToUInt32(ziFont._header.Skip(0x0C).Take(4).ToArray(), 0);

            ziFont.CodePage = new CodePage((CodePageIdentifier) codePageId);

            var charMapData = bytes.Skip(HEADER_LENGTH + ziFont.FileNameLength).Take(10 * (int) ziFont.CharacterCount).ToArray();

            for (int i = 0; i < charMapData.Length; i += 10) {

                var c = new CharMapEntry {
                    Code = BitConverter.ToUInt16(charMapData, i),
                    Width = charMapData[i + 2],
                    KerningLeft = charMapData[i + 3],
                    KerningRight = charMapData[i + 4],
                    DataAddressOffset = BitConverter.ToUInt32(new byte[] { charMapData[i + 5], charMapData[i + 6], charMapData[i + 7], 0x00 }, 0),
                    Length = BitConverter.ToUInt16(charMapData, i + 8)
                };
                ziFont.CharacterEntries.Add(c);

                Debug.WriteLine($"i: {i} code: {c.Code} width: {c.Width} dataOffset: {c.DataAddressOffset} length: {c.Length}");
            }

            ziFont.CreateBitmaps();

            return ziFont;
        }

        private void CreateBitmaps() {
            CharBitmaps.Clear();

            for (ushort charIndex = 0; charIndex < CharacterCount; charIndex++) {

                var charData = bytes.Skip(HEADER_LENGTH + FileNameLength + (int) CharacterEntries[charIndex].DataAddressOffset).Take(CharacterEntries[charIndex].Length).ToArray();

                var b = new CharBitmap(charIndex, (byte) CharacterEntries[charIndex].TotalWidth, CharacterHeight);

                var pixelNumber = 0;

                foreach (var item in charData.Skip(1)) {
                    var drawingMode = item >> 6;
                    var drawingMode2 = (item & 0b00100000) >> 5;
                    var number = (item & 0b00011111);

                    // B & W
                    //if (charDataChar2[0] == 1) {

                    if (drawingMode == 0) {

                        if (drawingMode2 == 0) {
                            for (int p = 0; p < number; p++) {
                                b.Bmp.SetPixelNumber(pixelNumber, Color.Transparent);

                                pixelNumber++;
                            }
                        }

                        if (drawingMode2 == 1) {
                            for (int p = 0; p < number; p++) {
                                b.Bmp.SetPixelNumber(pixelNumber, Color.Black);

                                pixelNumber++;
                            }
                        }

                    }

                    if (drawingMode == 1) {

                        for (int p = 0; p < number; p++) {
                            b.Bmp.SetPixelNumber(pixelNumber, Color.Transparent);

                            pixelNumber++;
                        }

                        b.Bmp.SetPixelNumber(pixelNumber, Color.Black);
                        pixelNumber++;

                        if (drawingMode2 == 1) {
                            b.Bmp.SetPixelNumber(pixelNumber, Color.Black);
                            pixelNumber++;
                        }

                    }
                    //}

                    //// Alpha
                    //if (charDataChar2[0] == 3) {

                    if (drawingMode == 2) {

                        number = (item & 0b00111000) >> 3;
                        var alpha = (item & 0b00000111);

                        for (int p = 0; p < number; p++) {
                            b.Bmp.SetPixelNumber(pixelNumber, Color.Transparent);

                            pixelNumber++;
                        }

                        b.Bmp.SetPixelNumber(pixelNumber, Color.FromArgb((255 / 8) * alpha, Color.Black));
                        pixelNumber++;
                    }

                    if (drawingMode == 3) {

                        var alpha1 = (item & 0b00111000) >> 3;
                        var alpha2 = (item & 0b00000111);

                        // should be alpha
                        b.Bmp.SetPixelNumber(pixelNumber, Color.FromArgb((255 / 8) * alpha1, Color.Black));
                        pixelNumber++;

                        // should be alpha
                        b.Bmp.SetPixelNumber(pixelNumber, Color.FromArgb((255 / 8) * alpha2, Color.Black));
                        pixelNumber++;

                    }
                    //}

                }

                CharBitmaps.Add(b);
            }

        }

        private byte[] CreateCharData(List<CharBitmap> characters, bool invertColour = false) {
            var charData = new List<byte>();

            foreach (var cb in characters) {
                charData.AddRange(BinaryTools.BitmapTo1BppData(cb.Bmp, invertColour));
            }

            return charData.ToArray();
        }

        public static ZiFontV5 FromCharacterBitmaps(string fontName, byte width, byte height, CodePage codePage, List<CharBitmap> characters, bool invertColour = false) {
            var bytesPerChar = width * height / 8;
            var charDataLength = (uint) (bytesPerChar * characters.Count());

            var ziFont = new ZiFontV5 {
                Name = fontName,
                //FileNameLength = (byte) fontName.Length,
                CharacterWidth = width,
                CharacterHeight = height,
                CodePage = codePage,
                CharDataLength = charDataLength,
                VariableDataLength = (uint) fontName.Length + charDataLength,
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
            if (header.Take(MagicNumbers.Length).SequenceEqual(MagicNumbers)) return true;

            return false;
        }
    }
}