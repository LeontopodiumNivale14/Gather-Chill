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

    public enum ItemQuality
    {
        PreferHQ,
        HQOnly,
        NQOnly,
    }

    public class ConsumableConfig
    {
        public bool Enable { get; set; } = false;
        public GatherNodeKind AllowedNodes { get; set; } = GatherNodeKind.Regular | GatherNodeKind.Unspoiled | GatherNodeKind.Legendary | GatherNodeKind.Ephemeral;
        public uint ItemId { get; set; } = 0;
        public uint SecondsRemaining { get; set; } = 0;
        public ItemQuality Quality { get; set; } = ItemQuality.PreferHQ;
    }

    public class RouteSelection
    {
        public HashSet<uint> EnabledRoutes { get; set; } = new();
    }

    public List<ItemInfo> GatherList { get; set; } = new();
    public Dictionary<uint, RouteSelection> ItemRoutes { get; set; } = new();

    public ConsumableConfig CordialConfig { get; set; } = new();
    public ConsumableConfig FoodConfig { get; set; } = new();
    public ConsumableConfig PotionConfig { get; set; } = new();
    public ConsumableConfig ManualConfig { get; set; } = new();

    // Cordial Specific
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

    // Depreciated / Old Configs, probably remove these come EC [MAKE SURE TO MIGRATE THESE]
    public bool Cordial_Auto { get; set; } = false;
    public bool Cordial_NormalNode { get; set; } = true;
    public bool Cordial_LegendaryNode { get; set; } = true;
    public bool Cordial_EphemerialNode { get; set; } = true;

    public void MigrateCordials()
    {
        if (CordialConfig.ItemId != 0)
            return; // already migrated or set up

        if (!Cordial_Auto)
            return;

        var kinds = GatherNodeKind.None;
        if (Cordial_NormalNode)
            kinds |= GatherNodeKind.Regular;
        if (Cordial_LegendaryNode)
            kinds |= GatherNodeKind.Legendary | GatherNodeKind.Unspoiled;
        if (Cordial_EphemerialNode)
            kinds |= GatherNodeKind.Ephemeral;

        CordialConfig.Enable = true;
        CordialConfig.AllowedNodes = kinds;
        CordialConfig.ItemId = 1;
    }
}
