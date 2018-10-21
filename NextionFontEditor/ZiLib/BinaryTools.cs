namespace ZiLib {

    internal class BinaryTools {

        public static bool[] BytesToBits(byte[] bytes) {
            bool[] bits = new bool[bytes.Length * 8];

            for (int i = 0; i < bytes.Length; i++) {
                for (int j = 0; j < 8; j++) {
                    var index = i * 8 + j;
                    bits[index] = (bytes[i] & (1 << (7 - j))) != 0;
                }
            }

            return bits;
        }
    }
}