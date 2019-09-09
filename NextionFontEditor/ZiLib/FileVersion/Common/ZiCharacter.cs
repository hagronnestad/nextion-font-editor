using System;
using System.IO;
using System.Drawing;
using ZiLib.FileVersion.V3;
using ZiLib.FileVersion.V5;

namespace ZiLib.FileVersion.Common
{

    public class ZiCharacter
    {
        public static IZiCharacter FromBitmap(IZiFont parent, uint codepoint, Bitmap bmp, byte kerningL, byte kerningR)
        {
            switch (parent.Version)
            {
                //    case 3:
                //        return new ZiCharacterV3();
                case 5:
                    return new ZiCharacterV5(parent, codepoint, bmp, kerningL, kerningR);
                default:
                    return null;
            }
        }

        public static IZiCharacter FromBytes(IZiFont parent, uint codepoint, byte[] bytes, byte width, byte kerningL, byte kerningR) {
            switch (parent.Version)
            {
                //    case 3:
                //        return new ZiCharacterV3();
                case 5:
                    return new ZiCharacterV5(parent, codepoint, bytes, width, kerningL, kerningR);
                default:
                    return null;
            }
        }

        public static IZiCharacter FromString(IZiFont parent, uint codepoint, Font font, PointF location, String txt)
        {
            switch (parent.Version)
            {
                //    case 3:
                //        return new ZiCharacterV3();
                case 5:
                    return new ZiCharacterV5(parent, codepoint, font, location, txt);
                default:
                    return null;
            }
        }

    }
}