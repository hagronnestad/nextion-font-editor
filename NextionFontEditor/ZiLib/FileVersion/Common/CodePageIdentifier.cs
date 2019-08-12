using System;
using System.Linq;
using System.Reflection;
using System.ComponentModel;

namespace ZiLib
{

    public enum CodePageIdentifier : ushort
    {
        [Description("ascii")]
        ASCII = 0x01,
        [Description("gb2312")]
        GB2312 = 0x02,
        [Description("iso-8859-1")]
        ISO_8859_1 = 0x03,
        [Description("iso-8859-2")]
        ISO_8859_2 = 0x04,
        [Description("iso-8859-3")]
        ISO_8859_3 = 0x05,
        [Description("iso-8859-4")]
        ISO_8859_4 = 0x06,
        [Description("iso-8859-5")]
        ISO_8859_5 = 0x07,
        [Description("iso-8859-6")]
        ISO_8859_6 = 0x08,
        [Description("iso-8859-7")]
        ISO_8859_7 = 0x09,
        [Description("iso-8859-8")]
        ISO_8859_8 = 0x0A,
        [Description("iso-8859-9")]
        ISO_8859_9 = 0x0B,
        [Description("iso-8859-13")]
        ISO_8859_13 = 0x0C,
        [Description("iso-8859-15")]
        ISO_8859_15 = 0x0D,
        [Description("iso-8859-11")]
        ISO_8859_11 = 0x0E,
        [Description("ks_c_5601-1987")]
        KS_C_5601_1987 = 0x0F,
        [Description("big5")]
        BIG5 = 0x10,
        [Description("windows-1255")]
        WINDOWS_1255 = 0x11,
        [Description("windows-1256")]
        WINDOWS_1256 = 0x12,
        [Description("windows-1257")]
        WINDOWS_1257 = 0x13,
        [Description("windows-1258")]
        WINDOWS_1258 = 0x14,
        [Description("windows-874")]
        WINDOWS_874 = 0x15,
        [Description("koi8-r")]
        KOI8_R = 0x16,
        [Description("shift-jis")]
        SHIFT_JIS = 0x17,
        [Description("utf-8")]
        UTF_8 = 0x18
    }

}

public static class Extensions
{
    public static string GetDescription(this Enum e)
    {
        var attribute =
            e.GetType()
                .GetTypeInfo()
                .GetMember(e.ToString())
                .FirstOrDefault(member => member.MemberType == MemberTypes.Field)
                .GetCustomAttributes(typeof(DescriptionAttribute), false)
                .SingleOrDefault()
                as DescriptionAttribute;

        return attribute?.Description ?? e.ToString();
    }
}