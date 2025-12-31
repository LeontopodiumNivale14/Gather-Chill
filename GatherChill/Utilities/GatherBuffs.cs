using System;
using System.Collections.Generic;
using System.Text;

namespace GatherChill.Utilities
{
    internal class GatherBuffs
    {
        public class GatheringActions
        {
            /// <summary>
            /// Internal name for myself to know wtf this is
            /// </summary>
            public string ActionName { get; set; }
            public Dictionary<uint, uint> ClassAction { get; set; } = new();
            /// <summary>
            /// Sheet name
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
            public int RequiredLv { get; set; }
        }

        public static Dictionary<string, GatheringActions> GathActionDict = new()
        {
            { "BoonIncrease1", new GatheringActions
            {
                ActionName = "Pioneer's Gift I",
                ClassAction = new()
                {
                    [16] = 21177,
                    [17] = 21178,
                },
                StatusId = 2666,
                StatusName = "Gift of the Land",
                RequiredGp = 50,
                RequiredLv = 15,
            }},
            { "BoonIncrease2", new GatheringActions
            {
                ActionName = "Pioneer's Gift II",
                ClassAction = new()
                {
                    [16] = 25589,
                    [17] = 25590,
                },
                StatusId = 759,
                StatusName = "Gift of the Land II",
                RequiredGp = 100,
                RequiredLv = 50,
            }},
            { "Tidings", new GatheringActions
            {
                ActionName = "Nophica's Tidings",
                ClassAction = new()
                {
                    [16] = 21203,
                    [17] = 21204,
                },
                StatusId = 2667,
                StatusName = "Gatherer's Bounty",
                RequiredGp = 200,
                RequiredLv = 81,
            }},
            { "YieldI", new GatheringActions
            {
                ActionName = "Blessed Harvest",
                ClassAction = new()
                {
                    [16] = 239,
                    [17] = 222,
                },
                StatusId = 219,
                StatusName = "Gathering Yield Up",
                RequiredGp = 400,
                RequiredLv = 30,
            }},
            { "YieldII", new GatheringActions
            {
                ActionName = "Blessed Harvest II",
                ClassAction = new()
                {
                    [16] = 241,
                    [17] = 224,
                },
                StatusId = 219,
                StatusName = "Gathering Yield Up",
                RequiredGp = 500,
                RequiredLv = 40,
            }},
            { "BonusIntegrity", new GatheringActions
            {
                ActionName = "Ageless Words",
                ClassAction = new()
                {
                    [16] = 232,
                    [17] = 215,
                },
                RequiredGp = 300,
                RequiredLv = 30,
            }},
            { "BonusIntegrityChance", new GatheringActions
            {
                ActionName = "Wise of the World",
                ClassAction = new()
                {
                    [16] = 26521,
                    [17] = 26522,
                },
                StatusId = 2765,
                StatusName = "",
                RequiredGp = 0,
                RequiredLv = 90,
            }},
            { "BountifulYieldII", new GatheringActions
            {
                ActionName = "Bountiful Yield/Harvest II",
                ClassAction = new()
                {
                    [16] = 272,
                    [17] = 273,
                },
                StatusId = 1286,
                StatusName = "",
                RequiredGp = 100,
                RequiredLv = 68,
            }},
            { "FieldMasteryIII", new GatheringActions
            {
                // 50% increase
                ActionName = "Field Mastery III",
                ClassAction = new()
                {
                    [16] = 295,
                    [17] = 294,
                },
                StatusId = 218,
                StatusName = "Gathering Rate Up",
                RequiredGp = 250,
                RequiredLv = 10,
            }},
            { "FieldMasteryII", new GatheringActions
            {
                // 15% increase
                ActionName = "Field Mastery II",
                ClassAction = new()
                {
                    [16] = 237,
                    [17] = 220,
                },
                StatusId = 218,
                StatusName = "Gathering Rate Up",
                RequiredGp = 100,
                RequiredLv = 5,
            }},
            { "FieldMasteryI", new GatheringActions
            {
                // 5% increase
                ActionName = "Field Mastery I",
                ClassAction = new()
                {
                    [16] = 235,
                    [17] = 218,
                },
                StatusId = 218,
                StatusName = "Gathering Rate Up",
                RequiredGp = 50,
                RequiredLv = 4,
            }},
            { "FieldMasteryTemp", new GatheringActions
            {
                // 15% increase [temp]
                ActionName = "Clear Vision | Flora Mastery",
                ClassAction = new()
                {
                    [16] = 4072,
                    [17] = 4086,
                },
                StatusId = 754,
                StatusName = "Gathering Rate Up (Limited)",
                RequiredGp = 50,
                RequiredLv = 23,
            }},
        };

        public static Dictionary<string, GatheringActions> GathCollectableBuffs = new()
        {
            { "Scrutiny", new GatheringActions
            {
                ActionName = "Scrutiny",
                ClassAction = new()
                {
                    [16] = 22185,
                    [17] = 22189
                },
                StatusId = 757,
                StatusName = "",
                RequiredGp = 200,
            }},
            { "Focus", new GatheringActions
            {
                ActionName = "Collector's Focus",
                ClassAction = new()
                {
                    [16] = 21205,
                    [17] = 21206
                },
                StatusId = 2668,
                StatusName = "",
                RequiredGp = 100,
            }},
            { "Priming", new GatheringActions
            {
                ActionName = "Priming Touch",
                ClassAction = new()
                {
                    [16] = 21205,
                    [17] = 34872
                },
                StatusId = 2668,
                StatusName = "",
                RequiredGp = 100,
            }},
            { "CollectorsHigh", new GatheringActions
            {
                // Only available in certain missions... *-sighs-*
                ActionName = "Collectors High Standard",
                ClassAction = new()
                {
                    [16] = 27,
                    [17] = 27,
                },
                StatusId = 3911,
                StatusName = "",
                RequiredGp = 0,
            }},
            { "BonusIntegrity", new GatheringActions
            {
                ActionName = "Ageless Words",
                ClassAction = new()
                {
                    [16] = 232,
                    [17] = 215,
                },
                RequiredGp = 300,
            }},
            { "BonusIntegrityChance", new GatheringActions
            {
                ActionName = "Wise of the World",
                ClassAction = new()
                {
                    [16] = 26521,
                    [17] = 26522,
                },
                StatusId = 2765,
                StatusName = "",
                RequiredGp = 0,
            }},
        };

        public static Dictionary<string, GatheringActions> GathCollectableActions = new()
        {
            { "Scour", new GatheringActions
            {
                // Base general use skill
                ActionName = "Scour",
                ClassAction = new()
                {
                    [16] = 22182,
                    [17] = 22186
                },
                StatusId = 0,
                StatusName = "n/a",
                RequiredGp = 0,
            }},
            { "Brazen", new GatheringActions
            {
                // 50 - 150% buff
                ActionName = "Brazen Woodsman",
                ClassAction = new()
                {
                    [16] = 22183,
                    [17] = 22187
                },
                StatusId = 0,
                StatusName = "n/a",
                RequiredGp = 0,
            }},
            { "Meticulous", new GatheringActions
            {
                // Chance to not use durability/integrity
                ActionName = "Meticulous Woodsman",
                ClassAction = new()
                {
                    [16] = 22184,
                    [17] = 22188
                },
                StatusId = 0,
                StatusName = "n/a",
                RequiredGp = 0,
            }},
            { "BonusIntegrity", new GatheringActions
            {
                ActionName = "Ageless Words",
                ClassAction = new()
                {
                    [16] = 232,
                    [17] = 215
                },
                RequiredGp = 300,
            }},
            { "BonusIntegrityChance", new GatheringActions
            {
                ActionName = "Wise of the World",
                ClassAction = new()
                {
                    [16] = 26521,
                    [17] = 26522
                },
                StatusId = 2765,
                StatusName = "",
                RequiredGp = 0,
            }},
            { "Collect", new GatheringActions
            {
                ActionName = "Collect",
                ClassAction = new()
                {
                    [16] = 240,
                    [17] = 815,
                },
                StatusId = 0,
                StatusName = "",
                RequiredGp = 0,
            } },
        };
    }
}
