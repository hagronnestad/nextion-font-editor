namespace ZiLib.FileVersion.V5 {

    public struct CharMapEntry {
        public ushort Code;
        public byte Width;
        public byte KerningLeft;
        public byte KerningRight;
        public uint DataAddressOffset; // Offset from start of entry
        public ushort Length;

        public int TotalWidth => Width + KerningLeft + KerningRight;
    }

}