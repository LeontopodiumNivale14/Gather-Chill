using System;
using System.Collections.Generic;
using System.Text;

namespace GatherChill.Utilities.GatheringHelpers;

public static partial class Gather_Util
{
    // List of routes that should be ignored.
    // Either old routes that no longer exist in the world, or just invalid 
    public static List<uint> Ignore_Routes = new()
    {
        677, // Old Diadem Building Items
        541, 542, 543, 544, 545, 546, 547, 548, 549, 550 // Namazu Questline
    };
}
