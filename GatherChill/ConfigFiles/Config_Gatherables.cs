using System.Collections.Generic;

namespace GatherChill.ConfigFiles;

public partial class Config
{
    public class ItemInfo
    {
        public uint ItemId { get; set; } = 0;
        public uint GatherAmount { get; set; } = 1;
    }

    public class RouteSelection
    {
        public List<uint> Timed_RouteIds { get; set; } = new();
        public uint Normal_RouteIds { get; set; } = 0;
    }

    public List<ItemInfo> GatherList { get; set; } = new();
    public Dictionary<uint, RouteSelection> ItemRoutes { get; set; } = new();
}
