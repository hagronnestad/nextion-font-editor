namespace ZiLib
{
    public enum CodePageMode : byte
    {
        SINGLE_BYTE_FULL_CHARSET = 0x00,
        DOUBLE_BYTE_FULL_CHARSET = 0x01,
        CHARACTER_SUBSET = 0x02
    }
}