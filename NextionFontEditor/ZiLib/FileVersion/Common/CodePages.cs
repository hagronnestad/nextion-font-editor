using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZiLib.FileVersion.Common {

    public class CodePages {

        public static CodePage CreateAscii() {
            var start = 32;
            var count = 95;
            var bytes = Enumerable.Range(start, count).Select(x => (byte) x).ToArray();
            var characters = Encoding.ASCII.GetChars(bytes);

            return new CodePage(CodePageIdentifier.ASCII);

            /*
            return new CodePage {
                CodePageIdentifier = CodePageIdentifier.ASCII,
                FirstByteStart = (byte)start,
                FirstByteEnd = (byte)(start + count),
                CharacterCount = count,
                Characters = characters
            };
            */
        }

        public static CodePage CreateIso_8859_1() {
            var start = 32;
            var count = 224;
            var bytes = Enumerable.Range(start, count).Select(x => (byte) x).ToArray();
            var encoding = Encoding.GetEncoding("ISO-8859-1");
            var characters = encoding.GetChars(bytes);

            return new CodePage(CodePageIdentifier.ISO_8859_1);

            /*
            return new CodePage {
                CodePageIdentifier = CodePageIdentifier.ISO_8859_1,
                FirstByteStart = (byte)start,
                FirstByteEnd = (byte)(start + count),
                CharacterCount = count,
                Characters = characters
            };
            */
        }

        // This one is not working correctly, characters doesn't match the Font Generator in Nextion Editor
        public static CodePage CreateBig5() {
            var firstByteStart = (byte)0xA0;
            var firstByteEnd = (byte)0xF9;
            var secondByteStart = (byte)0x40;
            var secondByteEnd = (byte)0xFE;

            var bytes = new List<byte>();

            for (int i = firstByteStart; i <= firstByteEnd; i++) {
                for (int j = secondByteStart; j <= secondByteEnd; j++) {
                    bytes.Add((byte) i);
                    bytes.Add((byte) j);
                }
            }

            var encoding = Encoding.GetEncoding("big5");
            var characters = encoding.GetChars(bytes.ToArray());

            return new CodePage(CodePageIdentifier.BIG5);

            /*
            return new CodePage {
                CodePageIdentifier = CodePageIdentifier.BIG5,
                FirstByteStart = firstByteStart,
                FirstByteEnd = firstByteEnd,
                SecondByteStart = secondByteStart,
                SecondByteEnd = secondByteEnd,
                CharacterCount = 0,
                Characters = characters
            };
            */
        }

        public static CodePage GetCodePage(CodePageIdentifier codePage) {
            switch (codePage) {
                case CodePageIdentifier.ASCII:
                    return CreateAscii();

                case CodePageIdentifier.ISO_8859_1:
                    return CreateIso_8859_1();

                case CodePageIdentifier.BIG5:
                    return CreateBig5();
            }

            return null;
        }
    }
}