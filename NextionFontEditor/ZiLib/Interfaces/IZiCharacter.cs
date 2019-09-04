using System.Collections.Generic;
using System.Drawing;
using ZiLib.FileVersion.Common;

namespace ZiLib {

    public interface IZiCharacter {

        uint CodePoint { get; set; }

        string Txt { get; set; }

        Font Font { get; set; }

        Point TxtPosition { get; set; }

        byte KerningLeft { get; set; }
        byte KerningRight { get; set; }

        byte Width { get; set; }

        IZiFont Parent { get; set; }

        Bitmap ToBitmap();

        byte[] ToBytes();

        byte[] GetCharacterData();

        bool CanRevert();

        Bitmap RevertBitmap();

        void SetBitmap(Bitmap bmp);
    }
}