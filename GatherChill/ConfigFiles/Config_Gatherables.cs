using GatherChill.Enums;
using GatherChill.Utilities.GatheringHelpers;
using Newtonsoft.Json;
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
    public bool Cordial_NormalNode { get; set; } = true;
    public bool Cordial_LegendaryNode { get; set; } = true;
    public bool Cordial_EphemerialNode { get; set; } = true;
    public int Cordial_Amount { get; set; } = 300;
    public bool Cordial_Inverse { get; set; } = false;
    public bool Cordial_PreventOvercap { get; set; } = true;

    // Gather timer based stuff
    public bool HoldGP_TimeNodes { get; set; } = true;
    public uint TimedNodes_HoldTime { get; set; } = 60;
    public bool HoldGP_CrystalBuff { get; set; } = true;
    [JsonProperty("CrystalBuff_HoldGP")]
    public uint CrystalBuff_HoldSeconds { get; set; } = 30;
    public bool WaitGP_Collectable { get; set; } = true;
    public uint Collectable_WaitGP { get; set; } = 700;
    public bool WaitGP_HiddenItem { get; set; } = false;
    public uint TimedNode_MinGP { get; set; } = 0;
    public CollectableTarget Collectable_Target { get; set; } = CollectableTarget.High;
}
