using GatherChill.Enums;
using System.Collections.Generic;

namespace GatherChill.ConfigFiles;

public partial class Config
{
    public class ItemInfo
    {
        public uint ItemId { get; set; } = 0;
        public int GatherAmount { get; set; } = 1;
        public int KeepAmount { get; set; } = 1;
        public GatherMode Mode { get; set; } = GatherMode.Gather;
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

    // Gather timer based stuff
    public bool HoldGP_TimeNodes { get; set; } = true;
    public uint TimedNodes_HoldTime { get; set; } = 60;
    public bool HoldGP_CrystalBuff { get; set; } = true;
    public uint CrystalBuff_HoldGP { get; set; } = 30;

    public uint TimedNode_MinGP { get; set; } = 0;

    // Auto Buff Settings
    public ProfileClass User_AutoProfile { get; set; } = new();
}
