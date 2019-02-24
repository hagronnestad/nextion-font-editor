using System;

namespace ZiLib.FileVersion.V5 {

    public struct CharMapEntry {
        public UInt16 Code;
        public byte Width;
        public byte Unknown1; // Length
        public UInt32 DataAddressOffset; // Offset from start of entry
        public UInt16 Length;
    }

}