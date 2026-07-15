using System;
using System.Collections.Generic;
using System.Text;

namespace GatherChill.Enums
{
    public enum ExpansionEnum
    {
        ARR = 1 << 0,
        HW = 1 << 1,
        StB = 1 << 2,
        ShB = 1 << 3,
        EW = 1 << 4,
        DT = 1 << 5,
    }
}
