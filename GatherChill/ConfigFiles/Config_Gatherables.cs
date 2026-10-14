using System.Collections.Generic;

namespace GatherChill.ConfigFiles;

public partial class Config
{
    public class ItemInfo
    {
        public uint ItemId { get; set; } = 0;
        public int GatherAmount { get; set; } = 1;
    }

    public class RouteSelection
    {
        public HashSet<uint> EnabledRoutes { get; set; } = new();
    }

    public List<ItemInfo> GatherList { get; set; } = new();
    public Dictionary<uint, RouteSelection> ItemRoutes { get; set; } = new();

    // Cordial Specific
    public bool Cordial_Auto { get; set; } = false;
    public int Cordial_Amount { get; set; } = 300;
    public bool Cordial_Inverse { get; set; } = false;
    public bool Cordial_PreventOvercap { get; set; } = true;
}
