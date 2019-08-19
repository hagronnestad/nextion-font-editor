using System;

namespace ZiLib.FileVersion.V5 {

    public struct CharMapEntry {
        public UInt16 Code;
        public byte Width;
        public byte KerningLeft;
        public byte KerningRight;
        public UInt32 DataAddressOffset; // Offset from start of entry
        public UInt16 Length;

        public int TotalWidth => Width + KerningLeft + KerningRight;
    }

}