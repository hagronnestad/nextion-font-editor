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
        public byte FileNameLength { get; set; }
        public string Name { get; set; }
        public long FileSize { get; set; }

        public UInt32 VariableDataLength { get; set; }
        public UInt32 CharDataLength { get; set; }

        public List<Bitmap> CharBitmaps { get; set; } = new List<Bitmap>();

        public void Save(string fileName, CodePage codePage) {
            CodePage = codePage;
            _charData = CreateCharData(CharBitmaps);
            Save(fileName);
        }

        public void Save(string fileName)
        {
            var file = new List<byte>();
            var encodingName = CodePage.CodePageIdentifier.GetDescription();
            var encodingNameLength = encodingName.Length;
            FileNameLength = (byte)Name.Length;
            var charData = new List<byte>();
            var indexData = new List<byte>();

            // Update Character Data and Character Indexes
            var offset = CharacterCount * 10u;
            for (int i = 0; i < CharacterCount; i++)
            {
                byte[] bytes = BinaryTools.BitmapTo3BppData(CharBitmaps.Skip(i).First(), false);
                var chent = CharacterEntries.Skip(i).First();
                chent.DataAddressOffset = offset;
                chent.Length = (ushort)bytes.Length;

                indexData.AddRange(BitConverter.GetBytes(chent.Code));
                indexData.Add(chent.Width);
                indexData.Add(chent.KerningLeft);
                indexData.Add(chent.KerningRight);

                var offsetbytes = BitConverter.GetBytes(chent.DataAddressOffset);
                indexData.Add(offsetbytes[0]);
                indexData.Add(offsetbytes[1]);
                indexData.Add(offsetbytes[2]);
                indexData.AddRange(BitConverter.GetBytes(chent.Length));

                offset += chent.Length;
                charData.AddRange(bytes);
            }
            VariableDataLength = (uint)(offset + FileNameLength + encodingNameLength);

            file.AddRange(MagicNumbers);
            file.Add(CodePage.FirstByteSkipAfter);
            file.Add(CodePage.FirstByteSkipCount);
            file.Add((byte)Orientation);
            file.Add((byte)CodePage.CodePageIdentifier);        // Just one byte for the codepage id

            // The next byte is not part of the codepageid, but has a different meaning: It's The CharacterSet Mode
            if (CharacterCount == CodePage.CharacterCount)
            {
                file.Add((byte)(CodePage.Encoding.IsSingleByte ? CodePageMode.SINGLE_BYTE_FULL_CHARSET : CodePageMode.DOUBLE_BYTE_FULL_CHARSET));
            }
            else
            {
                file.Add((byte)CodePageMode.CHARACTER_SUBSET);
                Version = 6;        // Version 6 font required
            }

            file.Add(CharacterWidth);
            file.Add(CharacterHeight);
            file.Add(CodePage.SecondByteStart);
            file.Add(CodePage.SecondByteEnd);
            file.Add(CodePage.FirstByteStart);
            file.Add(CodePage.FirstByteEnd);
            file.AddRange(BitConverter.GetBytes(CharacterCount));
            file.Add(Version);
            file.Add((byte)(FileNameLength + encodingNameLength));
            file.AddRange(BitConverter.GetBytes((ushort)0));            // Reserved 2 bytes
            file.AddRange(BitConverter.GetBytes(VariableDataLength));   // Length of font name and data
            file.AddRange(BitConverter.GetBytes(FontDataStartAddress));
            file.Add(CodePage.SecondByteSkipAfter);
            file.Add(CodePage.SecondByteSkipCount);
            file.Add(0x01); // AntiAlias
            file.Add(0x01); // VariableWidth
            file.Add(FileNameLength);
            file.Add(0x00); // 0x01 : Chardata is 8 byte aligned or 0x00 chardata is 1 byte aligned

            file.AddRange(BitConverter.GetBytes((ushort)0)); // Reserved 2 bytes

            // Version 5 = 0 - Version 6 = Total Number of characters in the codepage
            file.AddRange(BitConverter.GetBytes(Version == 5 ? 0u : (uint)CodePage.CharacterCount));

            file.AddRange(BitConverter.GetBytes(0u)); // Reserved 4 bytes

            // Variable Length : Fontname and Encoding Name
            file.AddRange(Encoding.ASCII.GetBytes(Name));
            file.AddRange(Encoding.ASCII.GetBytes(encodingName));

            file.AddRange(indexData.ToArray());

            _charData = charData.ToArray();
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
            ziFont.FileNameLength = ziFont._header[0x11];
            ziFont.Name = Encoding.ASCII.GetString(bytes.Skip(HEADER_LENGTH).Take(ziFont.FileNameLength).ToArray());
            ziFont.FileSize = bytes.Length;

            ziFont.CharacterWidth = ziFont._header[0x6];
            ziFont.CharacterHeight = ziFont._header[0x7];

            ziFont.VariableDataLength = BitConverter.ToUInt32(ziFont._header.Skip(0x14).Take(4).ToArray(), 0);
            ziFont.CharDataLength = ziFont.VariableDataLength - ziFont.FileNameLength;

            var dataStartAddress = BitConverter.ToUInt32(ziFont._header.Skip(0x18).Take(4).ToArray(), 0);

            ziFont._charData = bytes.Skip((int) dataStartAddress + ziFont.FileNameLength).ToArray();

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

        private void CreateBitmaps()
        {
            CharBitmaps.Clear();

            for (ushort charIndex = 0; charIndex < CharacterCount; charIndex++)
            {

                var charData = bytes.Skip(HEADER_LENGTH + FileNameLength + (int)CharacterEntries[charIndex].DataAddressOffset).Take(CharacterEntries[charIndex].Length).ToArray();


                //Kerning[0] = kerningL;
                //Kerning[1] = kerningR;
                if ((CharacterEntries[charIndex].TotalWidth == 0) || (CharacterHeight == 0))
                {
                    var bm = new Bitmap(1, 1);
                    bm.Dispose();           // Make it undefined initially
                    CharBitmaps.Add(bm);
                    continue;              // to next character
                }
                var b = new Bitmap(CharacterEntries[charIndex].TotalWidth, CharacterHeight);
                //b.Tag = charData;

                var pixelNumber = 0;
                var bitdepth = charData.First();

                foreach (var item in charData.Skip(1))
                {
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
                                b.SetPixelNumber(pixelNumber, Color.Transparent);

                                pixelNumber++;
                            }
                        }

                        if (drawingMode2 == 1)
                        {
                            for (int p = 0; p < number; p++)
                            {
                                b.SetPixelNumber(pixelNumber, Color.Black);

                                pixelNumber++;
                            }
                        }

                    }

                    if (drawingMode == 1)
                    {

                        for (int p = 0; p < number; p++)
                        {
                            b.SetPixelNumber(pixelNumber, Color.Transparent);

                            pixelNumber++;
                        }

                        b.SetPixelNumber(pixelNumber, Color.Black);
                        pixelNumber++;

                        if (drawingMode2 == 1)
                        {
                            b.SetPixelNumber(pixelNumber, Color.Black);
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
                                b.SetPixelNumber(pixelNumber, Color.Transparent);

                                pixelNumber++;
                            }

                            b.SetPixelNumber(pixelNumber, Color.Black);
                            pixelNumber++;
                            b.SetPixelNumber(pixelNumber, Color.Black);
                            pixelNumber++;
                            b.SetPixelNumber(pixelNumber, Color.Black);
                            pixelNumber++;

                            if (drawingMode2 == 1)
                            {
                                b.SetPixelNumber(pixelNumber, Color.Black);
                                pixelNumber++;
                            }
                        }
                        else
                        {
                            number = (item & 0b00111000) >> 3;
                            var alpha = (item & 0b00000111);

                            for (int p = 0; p < number; p++)
                            {
                                b.SetPixelNumber(pixelNumber, Color.Transparent);

                                pixelNumber++;
                            }

                            b.SetPixelNumber(pixelNumber, Color.FromArgb((255 / 7) * alpha, Color.Black));
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
                                b.SetPixelNumber(pixelNumber, Color.Transparent);

                                pixelNumber++;
                            }

                            for (int p = 0; p < blacks; p++)
                            {
                                b.SetPixelNumber(pixelNumber, Color.Black);

                                pixelNumber++;
                            }
                        }
                        else
                        {
                            var alpha1 = (item & 0b00111000) >> 3;
                            var alpha2 = (item & 0b00000111);

                            // should be alpha
                            b.SetPixelNumber(pixelNumber, Color.FromArgb((255 / 7) * alpha1, Color.Black));
                            pixelNumber++;

                            // should be alpha
                            b.SetPixelNumber(pixelNumber, Color.FromArgb((255 / 7) * alpha2, Color.Black));
                            pixelNumber++;
                        }
                    }
                    //}

                }

                CharBitmaps.Add(b);
            }

        }

        private byte[] CreateCharData(List<Bitmap> characters, bool invertColour = false)
        {
            var charData = new List<byte>();

            for (int i = 0; i < CharacterCount; i++)
            {
                byte[] bytes = BinaryTools.BitmapTo3BppData(characters[i], invertColour);
                //CharacterEntries[i].DataAddressOffset = 0;
                charData.AddRange(bytes);
            }

            return charData.ToArray();
        }


        public static ZiFontV5 FromCharacterBitmaps(string fontName, byte width, byte height, CodePage codePage, List<Bitmap> characters, bool invertColour = false) {
            var bytesPerChar = width * height / 8;
            var charDataLength = (uint) (bytesPerChar * characters.Count());

            var ziFont = new ZiFontV5 {
                Name = fontName,
                FileNameLength = (byte) fontName.Length,
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

        public static bool VerifyHeader(byte[] header)
        {
            if (header.Length < HEADER_LENGTH) return false;
            if (!header.Take(MagicNumbers.Length).SequenceEqual(MagicNumbers)) return false;

            if (!Enum.IsDefined(typeof(FontOrientation), header[3])) return false;
            if (!Enum.IsDefined(typeof(CodePageIdentifier), header[4])) return false;

            return true;
        }
    }
}