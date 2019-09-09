using System.Collections.Generic;
using System.Drawing;
using ZiLib.FileVersion.Common;

namespace ZiLib {

    public interface IZiFont {
        string Name { get; set; }
        CodePage CodePage { get; set; }

        byte Version { get; set; }

        byte CharacterWidth { get; set; }
        byte CharacterHeight { get; set; }
        int CharacterCount { get; }

        long FileSize { get; set; }

        //List<Bitmap> CharBitmaps { get; set; }
        List<IZiCharacter> Characters { get; set; }
        void Save(string fileName, CodePage codepage);
        //void AddCharacter(uint codepoint, byte[] bytes, byte width, byte kerningL = 0, byte kerningR = 0);
        void AddCharacter(uint codepoint, IZiCharacter character);
        void RemoveCharacter(int index);
    }
}