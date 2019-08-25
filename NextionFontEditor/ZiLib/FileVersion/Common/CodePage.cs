using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZiLib.FileVersion.Common
{

    public class CodePage
    {
        public CodePageIdentifier CodePageIdentifier { get; set; }

        public byte FirstByteStart { get; set; }
        public byte FirstByteEnd { get; set; }
        public byte SecondByteStart { get; set; }
        public byte SecondByteEnd { get; set; }

        public byte FirstByteSkipAfter { get; set; }
        public byte FirstByteSkipCount { get; set; }
        public byte SecondByteSkipAfter { get; set; }
        public byte SecondByteSkipCount { get; set; }

        public int CharacterCount { get; set; }
        public ushort[] CodePoints { get; set; }

        public bool IsMultibyte => !Encoding.IsSingleByte; // SecondByteStart.HasValue && SecondByteEnd.HasValue;

        public Encoding Encoding { get; }
        //  public byte MultiByteMode { get; }    // Use Encoding.IsSingleByte

        public CodePage(CodePageIdentifier codePageid)
        {
            switch (codePageid)
            {
                case CodePageIdentifier.ASCII:
                    CreateSingleByte(32, 95, codePageid);
                    break;

                case CodePageIdentifier.ISO_8859_1:
                case CodePageIdentifier.ISO_8859_2:
                case CodePageIdentifier.ISO_8859_3:
                case CodePageIdentifier.ISO_8859_4:
                case CodePageIdentifier.ISO_8859_5:
                case CodePageIdentifier.ISO_8859_6:
                case CodePageIdentifier.ISO_8859_7:
                case CodePageIdentifier.ISO_8859_8:
                case CodePageIdentifier.ISO_8859_9:
                case CodePageIdentifier.ISO_8859_11:
                case CodePageIdentifier.ISO_8859_13:
                case CodePageIdentifier.ISO_8859_15:
                case CodePageIdentifier.WINDOWS_1255:
                case CodePageIdentifier.WINDOWS_1256:
                case CodePageIdentifier.WINDOWS_1257:
                case CodePageIdentifier.WINDOWS_1258:
                case CodePageIdentifier.WINDOWS_874:
                case CodePageIdentifier.KOI8_R:
                    CreateSingleByte(32, 224, codePageid);
                    break;

                // A1 C8 A1 FE = Swapped order
                case CodePageIdentifier.KS_C_5601_1987:
                    CreateDoubleByte(0xA1, 0xC8, 0xA1, 0xFE, 0xFF, 0x00, 0xFF, 0x00, codePageid);
                    break;

                // 81 EF 40 FC = Swapped order
                case CodePageIdentifier.SHIFT_JIS:
                    CreateDoubleByte(0x81, 0xEF, 0x40, 0xFC, 0x9F, 0x40, 0x7E, 0x01, codePageid);
                    break;

                // A1 F7 A1 FE = Swapped order
                case CodePageIdentifier.GB2312:
                    CreateDoubleByte(0xA1, 0xF7, 0xA1, 0xFE, 0xff, 0x00, 0xff, 0x00, codePageid);
                    break;

                // A0 F9 40 FE = Swapped order
                case CodePageIdentifier.BIG5:
                    CreateDoubleByte(0xA0, 0xF9, 0x40, 0xFE, 0xFF, 0x00, 0x7E, 0x22, codePageid);
                    break;

                // FF FF 00 FF = Swapped order
                case CodePageIdentifier.UTF_8:
                    CreateUnicode(codePageid);
                    break;

                default:
                    break;
            }

            if (codePageid == CodePageIdentifier.UTF_8)
            {
                Encoding = Encoding.Unicode;
            }
            else
            {
                Encoding = Encoding.GetEncoding(codePageid.GetDescription());
            }
            // MultiByteMode = (byte)(Encoding.IsSingleByte ? 0 : 1);
        }

        private void CreateSingleByte(byte start, byte count, CodePageIdentifier codepageid)
        {
            var characters = Enumerable.Range(start, count).Select(x => (ushort)x).ToArray();

            FirstByteStart = start;
            FirstByteEnd = (byte)(start + count);
            SecondByteStart = SecondByteEnd = 0;

            // No skipped bytes
            FirstByteSkipAfter = SecondByteSkipAfter = 0xff;
            FirstByteSkipCount = SecondByteSkipCount = 0x00;

            CodePageIdentifier = codepageid;
            CodePoints = characters.ToArray();
            CharacterCount = CodePoints.Length;
        }

        private void CreateDoubleByte(byte outerStart, byte outerEnd, byte innerStart, byte innerEnd,
                                     byte outerSkip, byte outerCount, byte innerSkip, byte innerCount,
                                     CodePageIdentifier codepageid)
        {
            var characters = new List<ushort>();

            // Outer Loop
            for (int i = outerStart; i <= outerEnd; i++)
            {
                if ((i > outerSkip) && (i <= outerSkip + outerCount))
                {
                    continue;   // i is in exclusion range
                }

                // Inner Loop
                for (int j = innerStart; j <= innerEnd; j++)
                {
                    if ((j > innerSkip) && (j <= innerSkip + innerCount))
                    {
                        continue;   // j is in exclusion range
                    }

                    //bytes.Add((byte) i);
                    //bytes.Add((byte) j);
                    characters.Add((char)(j * 256 + i));
                    //characters.Add((uint)(i * 256 + j));
                }
            }

            // End with ascii standard set
            for (int i = 32; i <= 126; i++)
            {
                characters.Add((char)i);
            }

            SecondByteStart = outerStart;
            SecondByteEnd = outerEnd;
            FirstByteStart = innerStart;
            FirstByteEnd = innerEnd;

            SecondByteSkipAfter = outerSkip;
            SecondByteSkipCount = outerCount;
            FirstByteSkipAfter = innerSkip;
            FirstByteSkipCount = innerCount;

            CodePageIdentifier = codepageid;
            CodePoints = characters.ToArray();
            CharacterCount = CodePoints.Length;
        }

        // The TJC/Nextion UTF-8 is actually Unicode encoding
        private void CreateUnicode(CodePageIdentifier codepageid)
        {
            var characters = new List<ushort>();

            SecondByteStart = FirstByteStart = SecondByteSkipCount = FirstByteSkipCount = 0;
            SecondByteEnd = FirstByteEnd = SecondByteSkipAfter = FirstByteSkipAfter = 255;

            // Outer Loop
            for (int i = SecondByteStart; i <= SecondByteEnd; i++)
            {
                // Inner Loop
                for (int j = FirstByteStart; j <= FirstByteEnd; j++)
                {
                    //bytes.Add((byte) j);
                    //bytes.Add((byte) i);
                    characters.Add((char)(i * 256 + j));
                }
            }

            SecondByteStart = 255;
            CodePageIdentifier = codepageid;
            CodePoints = characters.ToArray();
            CharacterCount = CodePoints.Length;
        }
    }
}