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
        679, 678, 677, 676, 675, 674, 673, 672, 671, 670, 668, 667, 664, 662, 663, 665, 669, 666, 660, 657, 653, 656, 661, 655, 680, 654, // Old Diadem Building Items
        541, 542, 543, 544, 545, 546, 547, 548, 549, 550, // Namazu Questline
        911, 914, // Unknown what these were for? No data in the sheets...
    };
}
