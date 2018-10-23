using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZiLib {
    public class CodePages {

        private static byte[] ASCII = Enumerable.Range(32, 95).Select(x => (byte) x).ToArray();
        private static byte[] ISO88591 = Enumerable.Range(32, 224).Select(x => (byte) x).ToArray();

        public static char[] GetAscii() {
            var encodings = Encoding.GetEncodings();
            return Encoding.ASCII.GetChars(ASCII);
        }

        public static char[] GetBig5(int firstByteStart = 0xA0, int firstByteEnd = 0xF9, int secondByteStart = 0x40, int secondByteEnd = 0xFE) {
            var bytes = new List<byte>();

            for (int i = firstByteStart; i <= firstByteEnd; i++) {
                for (int j = secondByteStart; j <= secondByteEnd; j++) {

                    bytes.Add((byte) i);
                    bytes.Add((byte) j);

                }
            }

            var encoding = Encoding.GetEncoding("big5");

            return encoding.GetChars(bytes.ToArray());
        }
    }
}
