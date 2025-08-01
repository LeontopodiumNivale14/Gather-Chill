using Dalamud.Interface.Textures;
using System.Collections.Generic;

namespace GatherChill.Utilities;

public static unsafe class Data
{

    #region Gathering Actions

    public class GatheringActions
    {
        /// <summary>
        /// Internal name for myself to know wtf this is
        /// </summary>
        public string ActionName { get; set; }
        /// <summary>
        /// Sheet name
        /// </summary>
        public string BtnName { get; set; }
        /// <summary>
        /// Botanist Action ID
        /// </summary>
        public uint BtnActionId { get; set; }
        /// <summary>
        /// Sheet name
        /// </summary>
        public string MinName { get; set; }
        /// <summary>
        /// Miner Action ID
        /// </summary>
        public uint MinActionId { get; set; }
        /// <summary>
        /// If it has a status, the ID associated with it
        /// </summary>
        public uint StatusId { get; set; }
        /// <summary>
        /// The status name attached to it (personal use)
        /// </summary>
        public string StatusName { get; set; }
        /// <summary>
        /// The amount of GP required for the skill
        /// </summary>
        public int RequiredGp { get; set; }
    }

    public static Dictionary<string, GatheringActions> GathActionDict = new()
    {
        { "BoonIncrease1", new GatheringActions
        {
            ActionName = "Pioneer's Gift I",
            BtnName = "",
            BtnActionId = 21178,
            MinName = "",
            MinActionId = 21177,
            StatusId = 2666,
            StatusName = "Gift of the Land",
            RequiredGp = 50,
        }},
        { "BoonIncrease2", new GatheringActions
        {
            ActionName = "Pioneer's Gift II",
            BtnName = "",
            BtnActionId = 25590,
            MinName = "",
            MinActionId = 25589,
            StatusId = 759,
            StatusName = "Gift of the Land II",
            RequiredGp = 100,
        }},
        { "Tidings", new GatheringActions
        {
            ActionName = "Nophica's Tidings",
            BtnName = "",
            BtnActionId = 21204,
            MinName = "",
            MinActionId = 21203,
            StatusId = 2667,
            StatusName = "Gatherer's Bounty",
            RequiredGp = 200,
        }},
        { "Yield1", new GatheringActions
        {
            ActionName = "Blessed Harvest",
            BtnName = "",
            BtnActionId = 222,
            MinName = "",
            MinActionId = 239,
            StatusId = 219,
            StatusName = "Gathering Yield Up",
            RequiredGp = 400,
        }},
        { "Yield2", new GatheringActions
        {
            ActionName = "Blessed Harvest II",
            BtnName = "",
            BtnActionId = 224,
            MinName = "",
            MinActionId = 241,
            StatusId = 219,
            StatusName = "Gathering Yield Up",
            RequiredGp = 500,
        }},
        { "IntegrityIncrease", new GatheringActions
        {
            ActionName = "Ageless Words",
            BtnName = "",
            BtnActionId = 215,
            MinName = "",
            MinActionId = 232,
            RequiredGp = 300,
        }},
        { "BonusIntegrityChance", new GatheringActions
        {
            ActionName = "Wise of the World",
            BtnName = "",
            BtnActionId = 26522,
            MinName = "",
            MinActionId = 26521,
            StatusId = 2765,
            StatusName = "",
            RequiredGp = 0,
        }},
    };

    public static Dictionary<string, GatheringActions> MinActionDict = new();

    #endregion

    #region Gathering Node Information


    public class GatheringConfig
    {
        public int GatheringAmount { get; set; } = 0;
        public uint ItemId { get; set; } = 0;
        public string ItemName { get; set; } = string.Empty;
    }

    public class GatheringTypes
    {
        public required string Name { get; set; }
        public required ISharedImmediateTexture? MainIcon { get; set; }
        public required ISharedImmediateTexture? ShinyIcon { get; set; }
    }

    public static Dictionary<uint, GatheringTypes> GatheringNodeDict = new();

    public class GPBaseInformation
    {
        public int GatheringType { get; set; }
        public int GatheringLevel { get; set; }
        public HashSet<uint> Items { get; set; }
        public SortedSet<uint> NodeIds { get; set; } = new();
    }
    public static Dictionary<uint, GPBaseInformation> GatheringPointBaseDict = new();

    public static Dictionary<uint, string> GatheringItems = new();

    #endregion
}