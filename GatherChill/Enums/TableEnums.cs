using System;
using System.Collections.Generic;
using System.Text;

namespace GatherChill.Enums
{
    [Flags]
    public enum ExpansionEnum
    {
        ARR = 1 << 0,
        HW = 1 << 1,
        StB = 1 << 2,
        ShB = 1 << 3,
        EW = 1 << 4,
        DT = 1 << 5,
    }

    [Flags]
    public enum FolkloreEnum
    {
        None = 1 << 0,
        Has = 1 << 1,
    }

    [Flags]
    public enum LevelEnum
    {
        Lv_1 = 1 << 0,
        Lv_6 = 1 << 1,
        Lv_11 = 1 << 2,
        Lv_16 = 1 << 3,
        Lv_21 = 1 << 4,
        Lv_26 = 1 << 5,
        Lv_31 = 1 << 6,
        Lv_36 = 1 << 7,
        Lv_41 = 1 << 8,
        Lv_46 = 1 << 9,
        Lv_51 = 1 << 10,
        Lv_56 = 1 << 11,
        Lv_61 = 1 << 12,
        Lv_66 = 1 << 13,
        Lv_71 = 1 << 14,
        Lv_76 = 1 << 15,
        Lv_81 = 1 << 16,
        Lv_86 = 1 << 17,
        Lv_91 = 1 << 18,
        Lv_96 = 1 << 19,
    }
    [Flags]
    public enum UptimeEnum
    {
        Always = 1 << 0,
        Currently = 1 << 1,
        Unavailable = 1 << 2,
    }
    [Flags]
    public enum NodeTypes
    {
        Mining = 1 << 0,
        Quarrying = 1 << 1,
        Logging = 1 << 2,
        Harvesting = 1 << 3,
    }
}
