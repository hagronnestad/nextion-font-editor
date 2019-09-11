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
        private const uint FontDataStartAddress = 0x2C;

        public byte[] bytes { get; set; }
        private byte[] _header { get; set; }
        private byte[] _charData { get; set; }

        public FontOrientation Orientation { get; set; } = FontOrientation.Vertical;

        public byte CharacterWidth { get; set; } // 0 = variable width font
        public byte CharacterHeight { get; set; }
        public int CharacterCount => Characters.Count;

        //public List<CharMapEntry> CharacterEntries { get; set; } = new List<CharMapEntry>();

        public CodePage CodePage { get; set; }

        public byte Version { get; set; } = 5;
        public byte FileNameLength => (byte)Name.Length;
        public string Name { get; set; }
        public long FileSize { get; set; }

        public uint VariableDataLength { get; set; }
        public uint CharDataLength { get; set; }

        public List<IZiCharacter> Characters { get; set; } = new List<IZiCharacter>();

        //public List<Bitmap> CharBitmaps { get; set; } = new List<Bitmap>();

        public void Save(string fileName, CodePage codePage) {
            CodePage = codePage;
            Save(fileName);
        }

        public byte[] GetCharacterIndex(IZiCharacter character, uint dataoffset, int length)
        {
            var bytes = new byte[10];

            var codepointbytes = BitConverter.GetBytes(character.CodePoint);
            bytes[0] = codepointbytes[0];
            bytes[1] = codepointbytes[1];

            bytes[2] = character.Width;
            bytes[3] = character.KerningLeft;
            bytes[4] = character.KerningRight;

            var offsetbytes = BitConverter.GetBytes(dataoffset);
            bytes[5] = offsetbytes[0];
            bytes[6] = offsetbytes[1];
            bytes[7] = offsetbytes[2];

            var sizebytes = BitConverter.GetBytes((ushort)length);
            bytes[8] = sizebytes[0];
            bytes[9] = sizebytes[1];

            return bytes;
        }

        public void Save(string fileName)
        {
            var file = new List<byte>();
            var encodingName = CodePage.CodePageIdentifier.GetDescription();
            var encodingNameLength = encodingName.Length;
            var charData = new List<byte>();
            var indexData = new List<byte>();

            // Update Character Data and Character Indexes
            //CharacterCount = (uint)Characters.Length;
            var offset = (uint)CharacterCount * 10u;
            var align8bytes = (byte)0x00;
            for (int i = 0; i < CharacterCount; i++)
            {
                bytes = Characters[i].GetCharacterData();
                indexData.AddRange(GetCharacterIndex(Characters[i], offset, bytes.Length));
                charData.AddRange(bytes);
                offset += (uint)bytes.Length;
            }
            VariableDataLength = (uint)(offset + FileNameLength + encodingNameLength);

            // V6 8 byte aligned charData for files bigger than 16 MB
            if (offset > 16 * 1024 * 1024) {
                Version = 6;
                byte[] paddingbytes = new byte[] { 0, 0, 0, 0, 0, 0, 0, 0 };

                indexData.Clear();
                charData.Clear();
                offset = (uint)CharacterCount * 10u;
                align8bytes = (byte)0x01;

                for (int i = 0; i < CharacterCount; i++) {
                    /* Pad the previous data */
                    var paddinglength = (8 - (int)(offset % 8)) % 8;
                    charData.AddRange(paddingbytes.Take(paddinglength));
                    offset += (uint)paddinglength;

                    /* Add next character */
                    bytes = Characters[i].GetCharacterData();
                    indexData.AddRange(GetCharacterIndex(Characters[i], (uint)(offset / 8), bytes.Length));
                    charData.AddRange(bytes);
                    offset += (uint)bytes.Length;
                }
                VariableDataLength = (uint)(offset + FileNameLength + encodingNameLength);
            }

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
            file.Add(align8bytes); // 0x01 : Chardata is 8 byte aligned or 0x00 chardata is 1 byte aligned

            file.AddRange(BitConverter.GetBytes((ushort)0)); // Reserved 2 bytes

            // Version 5 = 0 // Version 6 = Total Number of characters in the codepage
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
            var characterCount = BitConverter.ToUInt32(ziFont._header.Skip(0x0C).Take(4).ToArray(), 0);
            ziFont.Characters = new List<IZiCharacter>();
            ziFont.CodePage = new CodePage((CodePageIdentifier) codePageId);

            var charMapData = bytes.Skip(HEADER_LENGTH + ziFont.FileNameLength).Take(10 * (int)characterCount).ToArray();
            var align8byte = bytes.Skip(33).First();

            for (int i = 0; i < charMapData.Length; i += 10) {
                var dataAddressOffset = BitConverter.ToUInt32(new byte[] { charMapData[i + 5], charMapData[i + 6], charMapData[i + 7], 0x00 }, 0);
                if (align8byte == 1) {
                    dataAddressOffset *= 8;
                }
                var dataLength = BitConverter.ToUInt16(charMapData, i + 8);

                var data = new byte[dataLength];
                Array.Copy(bytes, HEADER_LENGTH + ziFont.FileNameLength + dataAddressOffset, data, 0, dataLength);

                var ch = new ZiCharacterV5
                (
                    ziFont,
                    BitConverter.ToUInt16(charMapData, i),
                    data,
                    charMapData[i + 2],
                    charMapData[i + 3],
                    charMapData[i + 4]
                );
                ziFont.Characters.Add(ch);

                //Debug.WriteLine($"i: {i} code: {ch.CodePoint} width: {ch.GetBitmap().Width} dataOffset: n/a length: {ch.GetCharacterData().Length}");
            }
            return ziFont;
        }

        public static bool VerifyHeader(byte[] header)
        {
            if (header.Length < HEADER_LENGTH) return false;
            if (!header.Take(MagicNumbers.Length).SequenceEqual(MagicNumbers)) return false;
            if (!Enum.IsDefined(typeof(FontOrientation), header[3])) return false;
            if (!Enum.IsDefined(typeof(CodePageIdentifier), header[4])) return false;
            if (!Enum.IsDefined(typeof(CodePageMode), header[5])) return false;

            return true;
        }

        public void AddCharacter(uint codepoint, IZiCharacter character) {
            Characters.Add(character);
        }

        public void RemoveCharacter(int index) {
            Characters.RemoveAt(index);
        }

    }
}