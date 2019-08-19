using System;
using System.IO;
using ZiLib.FileVersion.V3;
using ZiLib.FileVersion.V5;

namespace ZiLib.FileVersion.Common {

    public class ZiFont {
        private const long FILE_VERSION_OFFSET = 0x10;

        public static IZiFont FromFile(string fileName) {
            var version = 0;

            using (var fs = File.OpenRead(fileName)) {
                fs.Seek(FILE_VERSION_OFFSET, SeekOrigin.Begin);
                version = fs.ReadByte();
                fs.Close();
            }

            switch (version) {
                case 3:
                    return ZiFontV3.FromFile(fileName);

                case 5:
                case 6:
                    return ZiFontV5.FromFile(fileName);
            }

            throw new NotSupportedException();
        }

    }
}