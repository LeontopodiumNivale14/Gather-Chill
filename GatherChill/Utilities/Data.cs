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
        // Btn Yellow = 3
        // Btn Blue = 4

        new GathNodeInfo
        {
            Position = new Vector3(0, 0, 0),
            LandZone = new Vector3(0, 0, 0),
            NodeId = 0,
            GatheringType = 0,
            ZoneId = 0
        },

        #region Set #10 [Btn]
        // Lower La Noscea

        // 11
        new GathNodeInfo
        {
            ZoneId = 135,
            NodeId = 30011,
            Position = new Vector3 (563f, 71.65f, -253.43f),
            LandZone = new Vector3 (562.68f, 70.6f, -252.51f),
            GatheringType = 0,
            NodeSet = 10
        },

        new GathNodeInfo
        {
            ZoneId = 135,
            NodeId = 30011,
            Position = new Vector3 (553.76f, 71.03f, -244.17f),
            LandZone = new Vector3 (554.55f, 69.96f, -244.35f),
            GatheringType = 0,
            NodeSet = 10
        },

        new GathNodeInfo
        {
            ZoneId = 135,
            NodeId = 30011,
            Position = new Vector3 (543.13f, 70.22f, -238.42f),
            LandZone = new Vector3 (544.56f, 69.47f, -238.57f),
            GatheringType = 0,
            NodeSet = 10
        },

        // 22
        new GathNodeInfo
        {
            ZoneId = 135,
            NodeId = 30022,
            Position = new Vector3 (499.35f, 73.09f, -265.03f),
            LandZone = new Vector3 (500.14f, 72.29f, -264.58f),
            GatheringType = 0,
            NodeSet = 10
        },

        new GathNodeInfo
        {
            ZoneId = 135,
            NodeId = 30022,
            Position = new Vector3 (513.65f, 73.28f, -260.13f),
            LandZone = new Vector3 (515.19f, 72.25f, -259.72f),
            GatheringType = 0,
            NodeSet = 10
        },

        new GathNodeInfo
        {
            ZoneId = 135,
            NodeId = 30022,
            Position = new Vector3 (511.32f, 74.24f, -271.42f),
            LandZone = new Vector3 (511.06f, 73.41f, -270.68f),
            GatheringType = 0,
            NodeSet = 10
        },

        // 23
        new GathNodeInfo
        {
            ZoneId = 135,
            NodeId = 30023,
            Position = new Vector3 (543.51f, 81.8f, -294.87f),
            LandZone = new Vector3 (543.29f, 80.39f, -293.86f),
            GatheringType = 0,
            NodeSet = 10
        },

        new GathNodeInfo
        {
            ZoneId = 135,
            NodeId = 30023,
            Position = new Vector3 (553.36f, 80.96f, -291.16f),
            LandZone = new Vector3 (552.95f, 79.65f, -290.35f),
            GatheringType = 0,
            NodeSet = 10
        },

        new GathNodeInfo
        {
            ZoneId = 135,
            NodeId = 30023,
            Position = new Vector3 (559.81f, 83.03f, -299.33f),
            LandZone = new Vector3 (559.07f, 82.13f, -298.45f),
            GatheringType = 0,
            NodeSet = 10
        },

        // 51
        new GathNodeInfo
        {
            ZoneId = 135,
            NodeId = 30051,
            Position = new Vector3 (514.74f, 77.64f, -288.12f),
            LandZone = new Vector3 (514.55f, 76.2f, -286.58f),
            GatheringType = 0,
            NodeSet = 10
        },

        new GathNodeInfo
        {
            ZoneId = 135,
            NodeId = 30051,
            Position = new Vector3 (524.62f, 72.17f, -250.41f),
            LandZone = new Vector3 (524.38f, 71.08f, -249.58f),
            GatheringType = 0,
            NodeSet = 10
        },

        new GathNodeInfo
        {
            ZoneId = 135,
            NodeId = 30051,
            Position = new Vector3 (560.35f, 77.93f, -283.51f),
            LandZone = new Vector3 (560.62f, 77.59f, -284.75f),
            GatheringType = 0,
            NodeSet = 10
        },

        #endregion

        #region Set #11 [Btn]
        // Central Gridania

        // 12
        new GathNodeInfo
        {
            ZoneId = 148,
            NodeId = 30012,
            Position = new Vector3 (84.68f, 3.18f, -157.39f),
            LandZone = new Vector3 (85.18f, 1.74f, -154.99f),
            GatheringType = 3,
            NodeSet = 11
        },

        new GathNodeInfo
        {
            ZoneId = 148,
            NodeId = 30012,
            Position = new Vector3 (83.55f, -3.53f, -131.74f),
            LandZone = new Vector3 (85f, -4.43f, -132.15f),
            GatheringType = 3,
            NodeSet = 11
        },

        new GathNodeInfo
        {
            ZoneId = 148,
            NodeId = 30012,
            Position = new Vector3 (101.93f, -1.33f, -135.75f),
            LandZone = new Vector3 (101.25f, -2.58f, -136.3f),
            GatheringType = 3,
            NodeSet = 11
        },

        // 13
        new GathNodeInfo
        {
            ZoneId = 148,
            NodeId = 30013,
            Position = new Vector3 (44.66f, 5.79f, -205.65f),
            LandZone = new Vector3 (44.73f, 4.46f, -203.95f),
            GatheringType = 3,
            NodeSet = 11
        },

        new GathNodeInfo
        {
            ZoneId = 148,
            NodeId = 30013,
            Position = new Vector3 (56.91f, 8.18f, -228.1f),
            LandZone = new Vector3 (57.46f, 6.78f, -227.13f),
            GatheringType = 3,
            NodeSet = 11
        },

        new GathNodeInfo
        {
            ZoneId = 148,
            NodeId = 30013,
            Position = new Vector3 (72.74f, 6.01f, -189.01f),
            LandZone = new Vector3 (72.09f, 4.63f, -190.64f),
            GatheringType = 3,
            NodeSet = 11
        },


        // 14
        new GathNodeInfo
        {
            ZoneId = 148,
            NodeId = 30014,
            Position = new Vector3 (40.62f, 5.67f, -172f),
            LandZone = new Vector3 (41.24f, 4.29f, -172.38f),
            GatheringType = 3,
            NodeSet = 11
        },

        new GathNodeInfo
        {
            ZoneId = 148,
            NodeId = 30014,
            Position = new Vector3 (15.31f, 4.64f, -173.33f),
            LandZone = new Vector3 (14.87f, 3.53f, -173.79f),
            GatheringType = 3,
            NodeSet = 11
        },

        new GathNodeInfo
        {
            ZoneId = 148,
            NodeId = 30014,
            Position = new Vector3 (14.91f, 5.39f, -148.64f),
            LandZone = new Vector3 (14.47f, 4.03f, -148f),
            GatheringType = 3,
            NodeSet = 11
        },


        // 15
        new GathNodeInfo
        {
            ZoneId = 148,
            NodeId = 30015,
            Position = new Vector3 (112.12f, 5.15f, -194.18f),
            LandZone = new Vector3 (111.83f, 3.47f, -193.18f),
            GatheringType = 3,
            NodeSet = 11
        },

        new GathNodeInfo
        {
            ZoneId = 148,
            NodeId = 30015,
            Position = new Vector3 (117.27f, 9.96f, -204.79f),
            LandZone = new Vector3 (116.28f, 8.6f, -204.43f),
            GatheringType = 3,
            NodeSet = 11
        },

        new GathNodeInfo
        {
            ZoneId = 148,
            NodeId = 30015,
            Position = new Vector3 (107.56f, 9.58f, -207.76f),
            LandZone = new Vector3 (107.9f, 8.29f, -207.34f),
            GatheringType = 3,
            NodeSet = 11
        },

        #endregion

        #region Set #12 [Btn]
        // North Shroud

        // 16
        new GathNodeInfo
        {
            ZoneId = 154,
            NodeId = 30016,
            Position = new Vector3 (395.07f, -4.21f, 235.08f),
            LandZone = new Vector3 (395.76f, -5.52f, 235f),
            GatheringType = 3,
            NodeSet = 12
        },
        new GathNodeInfo
        {
            ZoneId = 154,
            NodeId = 30016,
            Position = new Vector3 (388.51f, -1.26f, 222.37f),
            LandZone = new Vector3 (389.02f, -2.65f, 222.6f),
            GatheringType = 3,
            NodeSet = 12
        },
        new GathNodeInfo
        {
            ZoneId = 154,
            NodeId = 30016,
            Position = new Vector3 (404.35f, -2.12f, 216.97f),
            LandZone = new Vector3 (405.55f, -3.5f, 217.07f),
            GatheringType = 3,
            NodeSet = 12
        },

        // 17
        new GathNodeInfo
        {
            ZoneId = 154,
            NodeId = 30017,
            Position = new Vector3 (468.42f, -1.29f, 262.49f),
            LandZone = new Vector3 (467.77f, -2.12f, 262.45f),
            GatheringType = 3,
            NodeSet = 12
        },
        new GathNodeInfo
        {
            ZoneId = 154,
            NodeId = 30017,
            Position = new Vector3 (459.72f, -1.03f, 263.65f),
            LandZone = new Vector3 (459.54f, -2.32f, 262.83f),
            GatheringType = 3,
            NodeSet = 12
        },
        new GathNodeInfo
        {
            ZoneId = 154,
            NodeId = 30017,
            Position = new Vector3 (443f, -1.82f, 251.68f),
            LandZone = new Vector3 (441.52f, -2.79f, 251.47f),
            GatheringType = 3,
            NodeSet = 12
        },

        //18
        new GathNodeInfo
        {
            ZoneId = 154,
            NodeId = 30018,
            Position = new Vector3 (352.12f, -2.55f, 262.78f),
            LandZone = new Vector3 (352.8f, -3.87f, 263.05f),
            GatheringType = 3,
            NodeSet = 12
        },
        new GathNodeInfo
        {
            ZoneId = 154,
            NodeId = 30018,
            Position = new Vector3 (361.04f, -6.14f, 266.75f),
            LandZone = new Vector3 (361.97f, -7.05f, 266.95f),
            GatheringType = 3,
            NodeSet = 12
        },
        new GathNodeInfo
        {
            ZoneId = 154,
            NodeId = 30018,
            Position = new Vector3 (368.05f, -6.09f, 262.36f),
            LandZone = new Vector3 (368.75f, -6.95f, 262.69f),
            GatheringType = 3,
            NodeSet = 12
        },

        // 81
        new GathNodeInfo
        {
            ZoneId = 154,
            NodeId = 30081,
            Position = new Vector3 (404.69f, -3.74f, 283.62f),
            LandZone = new Vector3 (404.28f, -4.61f, 282.75f),
            GatheringType = 3,
            NodeSet = 12
        },
        new GathNodeInfo
        {
            ZoneId = 154,
            NodeId = 30081,
            Position = new Vector3 (394.46f, -3.78f, 286.35f),
            LandZone = new Vector3 (394.46f, -4.86f, 285.54f),
            GatheringType = 3,
            NodeSet = 12
        },
        new GathNodeInfo
        {
            ZoneId = 154,
            NodeId = 30081,
            Position = new Vector3 (406.12f, -4.34f, 271.23f),
            LandZone = new Vector3 (405.16f, -5.26f, 271.08f),
            GatheringType = 3,
            NodeSet = 12
},

        #endregion

        #region Set #13 [Btn]

        // 19
        new GathNodeInfo
        {
            ZoneId = 154,
            NodeId = 30019,
            Position = new Vector3 (238.6f, -22.81f, 254.05f),
            LandZone = new Vector3 (239.21f, -24.09f, 255.05f),
            GatheringType = 3,
            NodeSet = 13
        },
        new GathNodeInfo
        {
            ZoneId = 154,
            NodeId = 30019,
            Position = new Vector3 (234.22f, -22.84f, 265.3f),
            LandZone = new Vector3 (235.62f, -23.67f, 266.32f),
            GatheringType = 3,
            NodeSet = 13
        },
        new GathNodeInfo
        {
            ZoneId = 154,
            NodeId = 30019,
            Position = new Vector3 (257.44f, -20.59f, 246.4f),
            LandZone = new Vector3 (258.66f, -21.16f, 247.44f),
            GatheringType = 3,
            NodeSet = 13
        },

        // 20
        new GathNodeInfo
        {
            ZoneId = 154,
            NodeId = 30020,
            Position = new Vector3 (279.97f, -9.23f, 318.63f),
            LandZone = new Vector3 (279.43f, -10.8f, 317.92f),
            GatheringType = 3,
            NodeSet = 13
        },
        new GathNodeInfo
        {
            ZoneId = 154,
            NodeId = 30020,
            Position = new Vector3 (283.98f, -11.68f, 310.4f),
            LandZone = new Vector3 (283.56f, -13.26f, 310.05f),
            GatheringType = 3,
            NodeSet = 13
        },
        new GathNodeInfo
        {
            ZoneId = 154,
            NodeId = 30020,
            Position = new Vector3 (295.77f, -8.99f, 311.51f),
            LandZone = new Vector3 (295.96f, -10.48f, 311.02f),
            GatheringType = 3,
            NodeSet = 13
        },

        // 21
        new GathNodeInfo
        {
            ZoneId = 154,
            NodeId = 30021,
            Position = new Vector3 (255.31f, -17.9f, 302.63f),
            LandZone = new Vector3 (255.41f, -19.27f, 301.77f),
            GatheringType = 3,
            NodeSet = 13
        },
        new GathNodeInfo
        {
            ZoneId = 154,
            NodeId = 30021,
            Position = new Vector3 (250.37f, -21f, 296.24f),
            LandZone = new Vector3 (250.54f, -21.94f, 295.69f),
            GatheringType = 3,
            NodeSet = 13
        },
        new GathNodeInfo
        {
            ZoneId = 154,
            NodeId = 30021,
            Position = new Vector3 (247.4f, -21.53f, 284.92f),
            LandZone = new Vector3 (247.2f, -22.33f, 286.4f),
            GatheringType = 3,
            NodeSet = 13
        },

        // 82
        new GathNodeInfo
        {
            ZoneId = 154,
            NodeId = 30082,
            Position = new Vector3 (216.81f, -28.69f, 320.54f),
            LandZone = new Vector3 (216.29f, -29.56f, 319.45f),
            GatheringType = 3,
            NodeSet = 13
        },
        new GathNodeInfo
        {
            ZoneId = 154,
            NodeId = 30082,
            Position = new Vector3 (221.39f, -27.69f, 317.59f),
            LandZone = new Vector3 (220.78f, -28.83f, 317.82f),
            GatheringType = 3,
            NodeSet = 13
        },
        new GathNodeInfo
        {
            ZoneId = 154,
            NodeId = 30082,
            Position = new Vector3 (209.79f, -29.03f, 317.41f),
            LandZone = new Vector3 (210.12f, -29.84f, 316.29f),
            GatheringType = 3,
            NodeSet = 13
        },

        #endregion

        #region Set #14 [Btn]

        // 66
        new GathNodeInfo
        {
            ZoneId = 140,
            NodeId = 30066,
            Position = new Vector3 (101.01f, 58.12f, -113.01f),
            LandZone = new Vector3 (101.76f, 57.28f, -114.08f),
            GatheringType = 4,
            NodeSet = 14
        },
        new GathNodeInfo
        {
            ZoneId = 140,
            NodeId = 30066,
            Position = new Vector3 (93.45f, 53.59f, -87.46f),
            LandZone = new Vector3 (94.54f, 52.35f, -87.03f),
            GatheringType = 4,
            NodeSet = 14
        },
        new GathNodeInfo
        {
            ZoneId = 140,
            NodeId = 30066,
            Position = new Vector3 (123.04f, 55.74f, -66.81f),
            LandZone = new Vector3 (121.73f, 55.15f, -66.13f),
            GatheringType = 4,
            NodeSet = 14
        },

        // 53
        new GathNodeInfo
        {
            ZoneId = 140,
            NodeId = 30053,
            Position = new Vector3 (152.34f, 50.72f, -176.19f),
            LandZone = new Vector3 (151.52f, 50.02f, -175.25f),
            GatheringType = 4,
            NodeSet = 14
        },
        new GathNodeInfo
        {
            ZoneId = 140,
            NodeId = 30053,
            Position = new Vector3 (134.41f, 50.6f, -155.32f),
            LandZone = new Vector3 (134.96f, 49.81f, -156.04f),
            GatheringType = 4,
            NodeSet = 14
        },
        new GathNodeInfo
        {
            ZoneId = 140,
            NodeId = 30053,
            Position = new Vector3 (96.26f, 50.84f, -159.16f),
            LandZone = new Vector3 (95.74f, 50.08f, -160.39f),
            GatheringType = 4,
            NodeSet = 14
        },

        // 67
        new GathNodeInfo
        {
            ZoneId = 140,
            NodeId = 30067,
            Position = new Vector3 (198.67f, 53.89f, -159.78f),
            LandZone = new Vector3 (197.83f, 52.72f, -160.29f),
            GatheringType = 4,
            NodeSet = 14
        },
        new GathNodeInfo
        {
            ZoneId = 140,
            NodeId = 30067,
            Position = new Vector3 (217.41f, 55.36f, -143.88f),
            LandZone = new Vector3 (216.58f, 54.02f, -143.77f),
            GatheringType = 4,
            NodeSet = 14
        },
        new GathNodeInfo
        {
            ZoneId = 140,
            NodeId = 30067,
            Position = new Vector3 (207.65f, 56.84f, -184.5f),
            LandZone = new Vector3 (206.44f, 55.7f, -184.52f),
            GatheringType = 4,
            NodeSet = 14
        },

        //52
        new GathNodeInfo
        {
            ZoneId = 140,
            NodeId = 30052,
            Position = new Vector3 (176.58f, 50.23f, -128.34f),
            LandZone = new Vector3 (176.11f, 49.76f, -129.18f),
            GatheringType = 4,
            NodeSet = 14
        },
        new GathNodeInfo
        {
            ZoneId = 140,
            NodeId = 30052,
            Position = new Vector3 (177.51f, 50.58f, -125.26f),
            LandZone = new Vector3 (176.71f, 49.89f, -123.96f),
            GatheringType = 4,
            NodeSet = 14
        },
        new GathNodeInfo
        {
            ZoneId = 140,
            NodeId = 30052,
            Position = new Vector3 (168.36f, 50.97f, -111.69f),
            LandZone = new Vector3 (168.35f, 50.01f, -111.89f),
            GatheringType = 4,
            NodeSet = 14
        },

        #endregion

        #region Set #15 [Btn]
        // Central Shroud

        // 30
        new GathNodeInfo
        {
            ZoneId = 148,
            NodeId = 30030,
            Position = new Vector3 (196.81f, -7.52f, -31.74f),
            LandZone = new Vector3 (196.34f, -8.5f, -32.17f),
            GatheringType = 0,
            NodeSet = 0
        },
        new GathNodeInfo
        {
            ZoneId = 148,
            NodeId = 30030,
            Position = new Vector3 (204.92f, -8.42f, -24.97f),
            LandZone = new Vector3 (203.9f, -9.01f, -25.22f),
            GatheringType = 3,
            NodeSet = 15
        },
        new GathNodeInfo
        {
            ZoneId = 148,
            NodeId = 30030,
            Position = new Vector3 (205.34f, -7.4f, -46.44f),
            LandZone = new Vector3 (205.32f, -8.06f, -47.54f),
            GatheringType = 3,
            NodeSet = 15
        },

        // 28
        new GathNodeInfo
        {
            ZoneId = 148,
            NodeId = 30028,
            Position = new Vector3 (210.45f, -5.8f, -70f),
            LandZone = new Vector3 (211.3f, -6.68f, -69.99f),
            GatheringType = 3,
            NodeSet = 15
        },
        new GathNodeInfo
        {
            ZoneId = 148,
            NodeId = 30028,
            Position = new Vector3 (225.8f, -5.26f, -71.52f),
            LandZone = new Vector3 (225.12f, -6.41f, -70.92f),
            GatheringType = 3,
            NodeSet = 15
        },
        new GathNodeInfo
        {
            ZoneId = 148,
            NodeId = 30028,
            Position = new Vector3 (229.09f, -7.04f, -62.41f),
            LandZone = new Vector3 (228.35f, -8f, -62.15f),
            GatheringType = 3,
            NodeSet = 15
        },

        // 29
        new GathNodeInfo
        {
            ZoneId = 148,
            NodeId = 30029,
            Position = new Vector3 (278.99f, -6.18f, -100.34f),
            LandZone = new Vector3 (277.59f, -7.36f, -100.12f),
            GatheringType = 3,
            NodeSet = 15
        },
        new GathNodeInfo
        {
            ZoneId = 148,
            NodeId = 30029,
            Position = new Vector3 (282.88f, -5.35f, -91.96f),
            LandZone = new Vector3 (281.89f, -6.64f, -91.1f),
            GatheringType = 3,
            NodeSet = 15
        },
        new GathNodeInfo
        {
            ZoneId = 148,
            NodeId = 30029,
            Position = new Vector3 (281.3f, -6.57f, -72.8f),
            LandZone = new Vector3 (280.91f, -8.12f, -72.66f),
            GatheringType = 3,
            NodeSet = 15
        },

        // 27
        new GathNodeInfo
        {
            ZoneId = 148,
            NodeId = 30027,
            Position = new Vector3 (275.03f, -6.34f, -42.47f),
            LandZone = new Vector3 (274.58f, -7.51f, -42.87f),
            GatheringType = 3,
            NodeSet = 15
        },
        new GathNodeInfo
        {
            ZoneId = 148,
            NodeId = 30027,
            Position = new Vector3 (263.51f, -7.84f, -28.76f),
            LandZone = new Vector3 (263.78f, -8.88f, -27.98f),
            GatheringType = 3,
            NodeSet = 15
        },
        new GathNodeInfo
        {
            ZoneId = 148,
            NodeId = 30027,
            Position = new Vector3 (248.57f, -8.91f, -5.17f),
            LandZone = new Vector3 (248.3f, -10.11f, -2.92f),
            GatheringType = 3,
            NodeSet = 15
        },


        #endregion

        #region Set #16 [Btn]
        // North Shroud

        //24
        new GathNodeInfo
        {
            ZoneId = 154,
            NodeId = 30024,
            Position = new Vector3 (271.42f, -25.5f, 172.03f),
            LandZone = new Vector3 (271.96f, -26.99f, 170.81f),
            GatheringType = 3,
            NodeSet = 16
        },
        new GathNodeInfo
        {
            ZoneId = 154,
            NodeId = 30024,
            Position = new Vector3 (277.82f, -25.18f, 172.47f),
            LandZone = new Vector3 (278.86f, -26.04f, 173.43f),
            GatheringType = 3,
            NodeSet = 16
        },
        new GathNodeInfo
        {
            ZoneId = 154,
            NodeId = 30024,
            Position = new Vector3 (285.33f, -24.59f, 176.52f),
            LandZone = new Vector3 (286.74f, -25.63f, 177.37f),
            GatheringType = 3,
            NodeSet = 16
        },

        //09
        new GathNodeInfo
        {
            ZoneId = 154,
            NodeId = 30309,
            Position = new Vector3 (358.5f, 0.49f, 186.09f),
            LandZone = new Vector3 (356.82f, -0.54f, 185.44f),
            GatheringType = 3,
            NodeSet = 16
        },
        new GathNodeInfo
        {
            ZoneId = 154,
            NodeId = 30309,
            Position = new Vector3 (359.9f, 0.11f, 182.69f),
            LandZone = new Vector3 (358.85f, -1.01f, 181.71f),
            GatheringType = 3,
            NodeSet = 16
        },
        new GathNodeInfo
        {
            ZoneId = 154,
            NodeId = 30309,
            Position = new Vector3 (369.16f, 2.58f, 187.93f),
            LandZone = new Vector3 (368.96f, 1.44f, 186.7f),
            GatheringType = 3,
            NodeSet = 16
        },

        // 26
        new GathNodeInfo
        {
            ZoneId = 154,
            NodeId = 30026,
            Position = new Vector3 (330.64f, -6.16f, 146.29f),
            LandZone = new Vector3 (329.78f, -7.84f, 147.69f),
            GatheringType = 3,
            NodeSet = 16
        },
        new GathNodeInfo
        {
            ZoneId = 154,
            NodeId = 30026,
            Position = new Vector3 (318.77f, -7.33f, 129.25f),
            LandZone = new Vector3 (318.47f, -8.76f, 130.56f),
            GatheringType = 3,
            NodeSet = 16
        },
        new GathNodeInfo
        {
            ZoneId = 154,
            NodeId = 30026,
            Position = new Vector3 (307.63f, -9.6f, 131.15f),
            LandZone = new Vector3 (307.01f, -10.97f, 132.66f),
            GatheringType = 3,
            NodeSet = 16
        },

        //25
        new GathNodeInfo
        {
            ZoneId = 154,
            NodeId = 30025,
            Position = new Vector3 (266.56f, -13.06f, 101.91f),
            LandZone = new Vector3 (267.71f, -14.66f, 103.12f),
            GatheringType = 3,
            NodeSet = 16
        },
        new GathNodeInfo
        {
            ZoneId = 154,
            NodeId = 30025,
            Position = new Vector3 (280.74f, -9.92f, 87.98f),
            LandZone = new Vector3 (282.27f, -11.07f, 88.13f),
            GatheringType = 3,
            NodeSet = 16
        },
        new GathNodeInfo
        {
            ZoneId = 154,
            NodeId = 30025,
            Position = new Vector3 (296.22f, -8.51f, 88.84f),
            LandZone = new Vector3 (296.71f, -10.09f, 89.98f),
            GatheringType = 3,
            NodeSet = 16
        },

        #endregion

        #region Set #17 [Btn]
        // Central Shroud

        // 33
        new GathNodeInfo
        {
            ZoneId = 148,
            NodeId = 30033,
            Position = new Vector3 (-37.5f, -4.28f, -52.04f),
            LandZone = new Vector3 (-37.42f, -5.16f, -52.95f),
            GatheringType = 3,
            NodeSet = 17
        },
        new GathNodeInfo
        {
            ZoneId = 148,
            NodeId = 30033,
            Position = new Vector3 (-25.76f, -5.47f, -58.41f),
            LandZone = new Vector3 (-25.74f, -6.47f, -59.24f),
            GatheringType = 3,
            NodeSet = 17
        },
        new GathNodeInfo
        {
            ZoneId = 148,
            NodeId = 30033,
            Position = new Vector3 (-13.54f, -5.26f, -52.24f),
            LandZone = new Vector3 (-12.6f, -6.34f, -53.1f),
            GatheringType = 3,
            NodeSet = 17
        },

        // 32
        new GathNodeInfo
        {
            ZoneId = 148,
            NodeId = 30032,
            Position = new Vector3 (3.68f, -5.96f, -61.09f),
            LandZone = new Vector3 (2.41f, -6.81f, -61.04f),
            GatheringType = 3,
            NodeSet = 17
        },
        new GathNodeInfo
        {
            ZoneId = 148,
            NodeId = 30032,
            Position = new Vector3 (-10.18f, -4.64f, -90.6f),
            LandZone = new Vector3 (-10.45f, -5.55f, -89.37f),
            GatheringType = 3,
            NodeSet = 17
        },
        new GathNodeInfo
        {
            ZoneId = 148,
            NodeId = 30032,
            Position = new Vector3 (-23.1f, -4.22f, -89.21f),
            LandZone = new Vector3 (-22.34f, -5.27f, -88.39f),
            GatheringType = 3,
            NodeSet = 17
        },

        //10
        new GathNodeInfo
        {
            ZoneId = 148,
            NodeId = 30310,
            Position = new Vector3 (-69.54f, -2.4f, -85.38f),
            LandZone = new Vector3 (-68.83f, -3.76f, -83.94f),
            GatheringType = 3,
            NodeSet = 17
        },
        new GathNodeInfo
        {
            ZoneId = 148,
            NodeId = 30310,
            Position = new Vector3 (-81.73f, -1.86f, -83.56f),
            LandZone = new Vector3 (-81.9f, -2.79f, -82.36f),
            GatheringType = 3,
            NodeSet = 17
        },
        new GathNodeInfo
        {
            ZoneId = 148,
            NodeId = 30310,
            Position = new Vector3 (-88.99f, -0.89f, -87.68f),
            LandZone = new Vector3 (-90.29f, -1.8f, -87.24f),
            GatheringType = 3,
            NodeSet = 17
        },

        //31
        new GathNodeInfo
        {
            ZoneId = 148,
            NodeId = 30031,
            Position = new Vector3 (-116.6f, -1.83f, -26.13f),
            LandZone = new Vector3 (-118.04f, -2.68f, -26.29f),
            GatheringType = 3,
            NodeSet = 17
        },
        new GathNodeInfo
        {
            ZoneId = 148,
            NodeId = 30031,
            Position = new Vector3 (-115.9f, -1.74f, -31.06f),
            LandZone = new Vector3 (-116.95f, -2.35f, -32.43f),
            GatheringType = 3,
            NodeSet = 17
        },
        new GathNodeInfo
        {
            ZoneId = 148,
            NodeId = 30031,
            Position = new Vector3 (-96.27f, -3.07f, -41.06f),
            LandZone = new Vector3 (-96.23f, -4.15f, -42.08f),
            GatheringType = 3,
            NodeSet = 17
        },

        #endregion

        #region Set #839

        // 82

        new GathNodeInfo
        {
            ZoneId = 957,
            NodeId = 33982,
            Position = new Vector3 (-274.84f, 17.35f, 118.9f),
            LandZone = new Vector3 (-274.01f, 16.62f, 118.9f),
            GatheringType = 3,
            NodeSet = 839
        },

        // 83

        new GathNodeInfo
        {
            ZoneId = 957,
            NodeId = 33983,
            Position = new Vector3 (-268.33f, 19.74f, 102.59f),
            LandZone = new Vector3 (-267.6f, 18.95f, 103.15f),
            GatheringType = 3,
            NodeSet = 839
        },

        new GathNodeInfo
        {
            ZoneId = 957,
            NodeId = 33983,
            Position = new Vector3 (-287.52f, 18.52f, 101.94f),
            LandZone = new Vector3 (-287.1f, 17.93f, 101.66f),
            GatheringType = 3,
            NodeSet = 839
        },

        new GathNodeInfo
        {
            ZoneId = 957,
            NodeId = 33983,
            Position = new Vector3 (-267.4f, 17.71f, 123.68f),
            LandZone = new Vector3 (-267.24f, 17.06f, 123.21f),
            GatheringType = 3,
            NodeSet = 839
        },

        // 84

        new GathNodeInfo
        {
            ZoneId = 957,
            NodeId = 33984,
            Position = new Vector3 (-152.79f, 38.65f, 20.31f),
            LandZone = new Vector3 (-153.34f, 37.8f, 20.44f),
            GatheringType = 3,
            NodeSet = 839
        },

        // 85

        new GathNodeInfo
        {
            ZoneId = 957,
            NodeId = 33985,
            Position = new Vector3 (-146.38f, 40.57f, 29.28f),
            LandZone = new Vector3 (-146.72f, 39.81f, 30.23f),
            GatheringType = 3,
            NodeSet = 839
        },

        new GathNodeInfo
        {
            ZoneId = 957,
            NodeId = 33985,
            Position = new Vector3 (-142.87f, 41.02f, 21.84f),
            LandZone = new Vector3 (-142.39f, 40.32f, 22.09f),
            GatheringType = 3,
            NodeSet = 839
        },

        new GathNodeInfo
        {
            ZoneId = 957,
            NodeId = 33985,
            Position = new Vector3 (-147.66f, 41.6f, -0.05f),
            LandZone = new Vector3 (-147.26f, 40.85f, 0.22f),
            GatheringType = 3,
            NodeSet = 839
        },

        // 86

        new GathNodeInfo
        {
            ZoneId = 957,
            NodeId = 33986,
            Position = new Vector3 (-267.64f, 35.07f, -44.5f),
            LandZone = new Vector3 (-266.58f, 34.31f, -43.94f),
            GatheringType = 3,
            NodeSet = 839
        },

        new GathNodeInfo
        {
            ZoneId = 957,
            NodeId = 33987,
            Position = new Vector3 (-274.96f, 35.27f, -31.89f),
            LandZone = new Vector3 (-274.75f, 34.35f, -32.59f),
            GatheringType = 3,
            NodeSet = 839
        },

        new GathNodeInfo
        {
            ZoneId = 957,
            NodeId = 33987,
            Position = new Vector3 (-270.03f, 35.83f, -29.08f),
            LandZone = new Vector3 (-269.53f, 35.05f, -28.67f),
            GatheringType = 3,
            NodeSet = 839
        },

        #endregion

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