using Dalamud.Game.ClientState.Statuses;
using Dalamud.Interface.Textures;
using Dalamud.Interface.Textures.TextureWraps;
using ECommons;
using ECommons.ExcelServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

    public class GathNodeInfo
    {
        public Vector3 Position { get; set; }
        public Vector3 LandZone { get; set; }
        public uint NodeId { get; set; }
        public int GatheringType { get; set; }
        public int ZoneId { get; set; }
        public uint NodeSet { get; set; }
    }

    public static List<GathNodeInfo> GatheringNodeInfoList = new()
    {
        new GathNodeInfo
        {
            Position = new Vector3(0, 0, 0),
            LandZone = new Vector3(0, 0, 0),
            NodeId = 0,
            GatheringType = 0,
            ZoneId = 0
        },

        // Living Memory

#region Set #1004 [Btn]

        new GathNodeInfo
        {
            ZoneId = 1192,
            NodeId = 34929,
            Position = new Vector3 (-155.66f, 38.92f, -242.12f),
            LandZone = new Vector3 (-157.16f, 38.02f, -242.21f),
            GatheringType = 3,
            NodeSet = 1004,
        },
        new GathNodeInfo
        {
            ZoneId = 1192,
            NodeId = 34930,
            Position = new Vector3 (-138.83f, 38.61f, -254.02f),
            LandZone = new Vector3 (-140.29f, 38.26f, -254.64f),
            GatheringType = 3,
            NodeSet = 1004,
        },
        new GathNodeInfo
        {
            ZoneId = 1192,
            NodeId = 34930,
            Position = new Vector3 (-140.49f, 38.56f, -237.23f),
            LandZone = new Vector3 (-140.53f, 38f, -238.47f),
            GatheringType = 3,
            NodeSet = 1004,
        },
        new GathNodeInfo
        {
            ZoneId = 1192,
            NodeId = 34930,
            Position = new Vector3 (-145.01f, 38.59f, -232.01f),
            LandZone = new Vector3 (-145.86f, 38f, -231.03f),
            GatheringType = 3,
            NodeSet = 1004,
        },


        new GathNodeInfo
        {
            ZoneId = 1192,
            NodeId = 34931,
            Position = new Vector3 (-165.75f, 38.71f, -81.64f),
            LandZone = new Vector3 (-166.24f, 38f, -82.86f),
            GatheringType = 3,
            NodeSet = 1004,
        },
        new GathNodeInfo
        {
            ZoneId = 1192,
            NodeId = 34932,
            Position = new Vector3 (-161.85f, 38.41f, -99.57f),
            LandZone = new Vector3 (-163.02f, 38f, -100.11f),
            GatheringType = 3,
            NodeSet = 1004,
        },
        new GathNodeInfo
        {
            ZoneId = 1192,
            NodeId = 34932,
            Position = new Vector3 (-174.57f, 38.64f, -72.29f),
            LandZone = new Vector3 (-174.37f, 38f, -72.98f),
            GatheringType = 3,
            NodeSet = 1004,
        },

        new GathNodeInfo
        {
            ZoneId = 1192,
            NodeId = 34933,
            Position = new Vector3 (-354.8f, 34.61f, -195.45f),
            LandZone = new Vector3 (-352.98f, 34.22f, -195.63f),
            GatheringType = 3,
            NodeSet = 1004,
        },
        new GathNodeInfo
        {
            ZoneId = 1192,
            NodeId = 34934,
            Position = new Vector3 (-354.75f, 35.12f, -213.59f),
            LandZone = new Vector3 (-353.09f, 34.66f, -213.42f),
            GatheringType = 3,
            NodeSet = 1004,
        },
        new GathNodeInfo
        {
            ZoneId = 1192,
            NodeId = 34934,
            Position = new Vector3 (-363.42f, 34.87f, -193.93f),
            LandZone = new Vector3 (-362.86f, 34.38f, -194.74f),
            GatheringType = 3,
            NodeSet = 1004,
        },
        new GathNodeInfo
        {
            ZoneId = 1192,
            NodeId = 34934,
            Position = new Vector3 (-326.91f, 35.96f, -157.68f),
            LandZone = new Vector3 (-328.14f, 35.35f, -156.8f),
            GatheringType = 3,
            NodeSet = 1004,
        },

#endregion

    };

    public static Dictionary<uint, string> GatheringItems = new();

    #endregion
}