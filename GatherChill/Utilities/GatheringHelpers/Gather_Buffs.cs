using ECommons.ExcelServices;
using ECommons.GameHelpers;
using GatherChill.Enums;
using Lumina.Excel.Sheets;
using System.Collections.Generic;

namespace GatherChill.Utilities.GatheringHelpers;

public static partial class Gather_Util
{
    public class GatheringActions
    {
        /// <summary>
        /// Internal name for myself to know wtf this is
        /// </summary>
        public string ActionName { get; set; }
        public Dictionary<Job, uint> ClassAction { get; set; } = new();
        /// <summary>
        /// Sheet name
        /// </summary>
        public uint StatusId { get; set; }
        public uint StatusId2 { get; set; }
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

    public static Dictionary<GatherBuffId, GatheringActions> GathActionDict = new()
    {
        [GatherBuffId.BoonIncrease_1] = new()
        {
            ActionName = "Pioneer's Gift I",
            ClassAction = new()
            {
                [Job.MIN] = 21177,
                [Job.BTN] = 21178,
            },
            StatusId = 2666,
            StatusName = "Gift of the Land",
            RequiredGp = 50,
            RequiredLv = 15,
        },
        [GatherBuffId.BoonIncrease_2] = new()
        {
            ActionName = "Pioneer's Gift II",
            ClassAction = new()
            {
                [Job.MIN] = 25589,
                [Job.BTN] = 25590,
            },
            StatusId = 759,
            StatusName = "Gift of the Land II",
            RequiredGp = 100,
            RequiredLv = 50,
        },
        [GatherBuffId.Tidings] = new()
        {
            ActionName = "Nophica's Tidings",
            ClassAction = new()
            {
                [Job.MIN] = 21203,
                [Job.BTN] = 21204,
            },
            StatusId = 2667,
            StatusName = "Gatherer's Bounty",
            RequiredGp = 200,
            RequiredLv = 81,
        },
        [GatherBuffId.YieldI] = new()
        {
            ActionName = "Blessed Harvest",
            ClassAction = new()
            {
                [Job.MIN] = 239,
                [Job.BTN] = 222,
            },
            StatusId = 219,
            StatusName = "Gathering Yield Up",
            RequiredGp = 400,
            RequiredLv = 30,
        },
        [GatherBuffId.YieldII] = new()
        {
            ActionName = "Blessed Harvest II",
            ClassAction = new()
            {
                [Job.MIN] = 241,
                [Job.BTN] = 224,
            },
            StatusId = 219,
            StatusName = "Gathering Yield Up",
            RequiredGp = 500,
            RequiredLv = 40,
        },
        [GatherBuffId.BonusIntegrity] = new()
        {
            ActionName = "Ageless Words",
            ClassAction = new()
            {
                [Job.MIN] = 232,
                [Job.BTN] = 215,
            },
            RequiredGp = 300,
            RequiredLv = 30,
        },
        [GatherBuffId.BonusIntegrity_Chance] = new()
        {
            ActionName = "Wise of the World",
            ClassAction = new()
            {
                [Job.MIN] = 26521,
                [Job.BTN] = 26522,
            },
            StatusId = 2765,
            StatusName = "",
            RequiredGp = 0,
            RequiredLv = 90,
        },
        [GatherBuffId.BYII] = new()
        {
            ActionName = "Bountiful Yield/Harvest II",
            ClassAction = new()
            {
                [Job.MIN] = 272,
                [Job.BTN] = 273,
            },
            StatusId = 1286,
            StatusId2 = 756,
            StatusName = "",
            RequiredGp = 100,
            RequiredLv = 68,
        },
        [GatherBuffId.FieldMasteryIII] = new()
        {
            // 50% increase
            ActionName = "Field Mastery III",
            ClassAction = new()
            {
                [Job.MIN] = 295,
                [Job.BTN] = 294,
            },
            StatusId = 218,
            StatusName = "Gathering Rate Up",
            RequiredGp = 250,
            RequiredLv = 10,
        },
        [GatherBuffId.FieldMasteryII] = new()
        {
            // 15% increase
            ActionName = "Field Mastery II",
            ClassAction = new()
            {
                [Job.MIN] = 237,
                [Job.BTN] = 220,
            },
            StatusId = 218,
            StatusName = "Gathering Rate Up",
            RequiredGp = 100,
            RequiredLv = 5,
        },
        [GatherBuffId.FieldMasteryI] = new()
        {
            // 5% increase
            ActionName = "Field Mastery I",
            ClassAction = new()
            {
                [Job.MIN] = 235,
                [Job.BTN] = 218,
            },
            StatusId = 218,
            StatusName = "Gathering Rate Up",
            RequiredGp = 50,
            RequiredLv = 4,
        },
        [GatherBuffId.FieldMasteryTemp] = new()
        {
            // 15% increase [temp]
            ActionName = "Clear Vision | Flora Mastery",
            ClassAction = new()
            {
                [Job.MIN] = 4072,
                [Job.BTN] = 4086,
            },
            StatusId = 754,
            StatusName = "Gathering Rate Up (Limited)",
            RequiredGp = 50,
            RequiredLv = 23,
        },
        [GatherBuffId.TwelveBounty] = new()
        {
            // +3 yield on crystals, buff through node
            ActionName = "The Twelve Bounty",
            ClassAction = new()
            {
                [Job.MIN] = 280,
                [Job.BTN] = 282,
            },
            StatusId = 825,
            StatusName = "The Twelve's Bounty",
            RequiredGp = 150,
            RequiredLv = 20,
        },
        [GatherBuffId.GivingLand] = new()
        {
            // +3 yield on crystals, buff through node
            ActionName = "The Giving Land",
            ClassAction = new()
            {
                [Job.MIN] = 4589,
                [Job.BTN] = 4590,
            },
            StatusId = 1802,
            StatusName = "The Giving Land",
            RequiredGp = 200,
            RequiredLv = 74,
        },
    };

    public static Dictionary<string, GatheringActions> GathCollectableBuffs = new()
    {
        ["Scrutiny"] = new GatheringActions
        {
            ActionName = "Scrutiny",
            ClassAction = new()
            {
                [Job.MIN] = 22185,
                [Job.BTN] = 22189
            },
            StatusId = 757,
            StatusName = "",
            RequiredGp = 200,
        },
        ["Focus"] = new GatheringActions
        {
            ActionName = "Collector's Focus",
            ClassAction = new()
            {
                [Job.MIN] = 21205,
                [Job.BTN] = 21206
            },
            StatusId = 2668,
            StatusName = "",
            RequiredGp = 100,
        },
        ["Priming"] = new GatheringActions
        {
            ActionName = "Priming Touch",
            ClassAction = new()
            {
                [Job.MIN] = 21205,
                [Job.BTN] = 34872
            },
            StatusId = 2668,
            StatusName = "",
            RequiredGp = 100,
        },
        ["CollectorsHigh"] = new GatheringActions
        {
            // Only available in certain missions... *-sighs-*
            ActionName = "Collectors High Standard",
            ClassAction = new()
            {
                [Job.MIN] = 27,
                [Job.BTN] = 27,
            },
            StatusId = 3911,
            StatusName = "",
            RequiredGp = 0,
        },
        ["BonusIntegrity"] = new GatheringActions
        {
            ActionName = "Ageless Words",
            ClassAction = new()
            {
                [Job.MIN] = 232,
                [Job.BTN] = 215,
            },
            RequiredGp = 300,
        },
        ["BonusIntegrityChance"] = new GatheringActions
        {
            ActionName = "Wise of the World",
            ClassAction = new()
            {
                [Job.MIN] = 26521,
                [Job.BTN] = 26522,
            },
            StatusId = 2765,
            StatusName = "",
            RequiredGp = 0,
        },
    };

    public static Dictionary<string, GatheringActions> GathCollectableActions = new()
    {
        ["Scour"] = new GatheringActions
        {
            // Base general use skill
            ActionName = "Scour",
            ClassAction = new()
            {
                [Job.MIN] = 22182,
                [Job.BTN] = 22186
            },
            StatusId = 0,
            StatusName = "n/a",
            RequiredGp = 0,
        },
        ["Brazen"] = new GatheringActions
        {
            // 50 - 150% buff
            ActionName = "Brazen Woodsman",
            ClassAction = new()
            {
                [Job.MIN] = 22183,
                [Job.BTN] = 22187
            },
            StatusId = 0,
            StatusName = "n/a",
            RequiredGp = 0,
        },
        ["Meticulous"] = new GatheringActions
        {
            // Chance to not use durability/integrity
            ActionName = "Meticulous Woodsman",
            ClassAction = new()
            {
                [Job.MIN] = 22184,
                [Job.BTN] = 22188
            },
            StatusId = 0,
            StatusName = "n/a",
            RequiredGp = 0,
        },
        ["BonusIntegrity"] = new GatheringActions
        {
            ActionName = "Ageless Words",
            ClassAction = new()
            {
                [Job.MIN] = 232,
                [Job.BTN] = 215
            },
            RequiredGp = 300,
        },
        ["BonusIntegrityChance"] = new GatheringActions
        {
            ActionName = "Wise of the World",
            ClassAction = new()
            {
                [Job.MIN] = 26521,
                [Job.BTN] = 26522
            },
            StatusId = 2765,
            StatusName = "",
            RequiredGp = 0,
        },
        ["Collect"] = new GatheringActions
        {
            ActionName = "Collect",
            ClassAction = new()
            {
                [Job.MIN] = 240,
                [Job.BTN] = 815,
            },
            StatusId = 0,
            StatusName = "",
            RequiredGp = 0,
        },
    };
}
