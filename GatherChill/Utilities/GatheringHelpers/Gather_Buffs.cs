using Dalamud.Interface.Textures;
using ECommons.ExcelServices;
using GatherChill.Enums;
using GatherChill.Utilities.Tools;
using System.Collections.Generic;

namespace GatherChill.Utilities.GatheringHelpers;

public static partial class Gather_Util
{
    public class GatheringActions
    {
        /// <summary>
        /// Internal name for myself to know wtf this is
        /// </summary>
        public string internalName { get; set; }
        public Dictionary<Job, ClassDetails> ClassAction { get; set; } = new();
        public uint StatusId { get; set; } = 0;
        public uint StatusId2 { get; set; } = 0;
        public string StatusName { get; set; } = "??";
        public int RequiredGp { get; set; } = 0;
        public int RequiredLv { get; set; } = 0;
    }

    public class ClassDetails
    {
        public uint ActionId { get; set; }
        public string Name { get; set; }
        public uint IconId { get; set; }
        public ISharedImmediateTexture Icon { get; set; }
    }

    public static void Update_GatheringDetails()
    {
        if (ExcelHelper.Sheet_Action == null)
        {
            Svc.Log.Error("[Gather_Util] Sheet_Action is null, Update_GatheringDetails called too early.");
            return;
        }

        foreach (var entry in GathActionDict)
        {
            foreach (var job in entry.Value.ClassAction)
            {
                var details = job.Value;

                if (!ExcelHelper.Sheet_Action.TryGetRow(details.ActionId, out var actionSheet))
                {
                    Svc.Log.Warning($"[Gather_Util] Action row not found: {details.ActionId} ({entry.Value.internalName}, {job.Key})");
                    continue;
                }

                details.Name = actionSheet.Name.ToString();

                uint iconId = actionSheet.Icon;
                if (!Svc.Texture.TryGetFromGameIcon(new GameIconLookup(iconId), out var icon))
                {
                    Svc.Log.Warning($"[Gather_Util] Icon lookup failed: {iconId} for action {details.ActionId}");
                    continue;
                }

                details.IconId = iconId;
                details.Icon = icon;
            }
        }
    }

    public static Dictionary<GatherBuffId, GatheringActions> GathActionDict = new()
    {
        [GatherBuffId.BoonIncrease_1] = new()
        {
            internalName = "Pioneer's Gift I",
            ClassAction = new()
            {
                [Job.MIN] = new() { ActionId = 21177, Name = "Pioneer's Gift I" },
                [Job.BTN] = new() { ActionId = 21178, Name = "Pioneer's Gift I" },
            },
            StatusId = 2666,
            StatusName = "Gift of the Land",
            RequiredGp = 50,
            RequiredLv = 15,
        },
        [GatherBuffId.BoonIncrease_2] = new()
        {
            internalName = "Pioneer's Gift II",
            ClassAction = new()
            {
                [Job.MIN] = new() { ActionId = 25589, Name = "Pioneer's Gift II" },
                [Job.BTN] = new() { ActionId = 25590, Name = "Pioneer's Gift II" },
            },
            StatusId = 759,
            StatusName = "Gift of the Land II",
            RequiredGp = 100,
            RequiredLv = 50,
        },
        [GatherBuffId.Tidings] = new()
        {
            internalName = "Nophica's Tidings",
            ClassAction = new()
            {
                [Job.MIN] = new() { ActionId = 21203, Name = "Nophica's Tidings" },
                [Job.BTN] = new() { ActionId = 21204, Name = "Nophica's Tidings" },
            },
            StatusId = 2667,
            StatusName = "Gatherer's Bounty",
            RequiredGp = 200,
            RequiredLv = 81,
        },
        [GatherBuffId.YieldI] = new()
        {
            internalName = "Blessed Harvest",
            ClassAction = new()
            {
                [Job.MIN] = new() { ActionId = 239, Name = "Blessed Harvest" },
                [Job.BTN] = new() { ActionId = 222, Name = "Blessed Harvest" },
            },
            StatusId = 219,
            StatusName = "Gathering Yield Up",
            RequiredGp = 400,
            RequiredLv = 30,
        },
        [GatherBuffId.YieldII] = new()
        {
            internalName = "Blessed Harvest II",
            ClassAction = new()
            {
                [Job.MIN] = new() { ActionId = 241, Name = "Blessed Harvest II" },
                [Job.BTN] = new() { ActionId = 224, Name = "Blessed Harvest II" },
            },
            StatusId = 219,
            StatusName = "Gathering Yield Up",
            RequiredGp = 500,
            RequiredLv = 40,
        },
        [GatherBuffId.BonusIntegrity] = new()
        {
            internalName = "Ageless Words",
            ClassAction = new()
            {
                [Job.MIN] = new() { ActionId = 232, Name = "Ageless Words" },
                [Job.BTN] = new() { ActionId = 215, Name = "Ageless Words" },
            },
            RequiredGp = 300,
            RequiredLv = 25,
        },
        [GatherBuffId.BonusIntegrity_Chance] = new()
        {
            internalName = "Wise of the World",
            ClassAction = new()
            {
                [Job.MIN] = new() { ActionId = 26521, Name = "Wise of the World" },
                [Job.BTN] = new() { ActionId = 26522, Name = "Wise of the World" },
            },
            StatusId = 2765,
            StatusName = "",
            RequiredGp = 0,
            RequiredLv = 90,
        },
        [GatherBuffId.BYII] = new()
        {
            internalName = "Bountiful Yield/Harvest II",
            ClassAction = new()
            {
                [Job.MIN] = new() { ActionId = 272, Name = "Bountiful Yield/Harvest II" },
                [Job.BTN] = new() { ActionId = 273, Name = "Bountiful Yield/Harvest II" },
            },
            StatusId = 1286,
            StatusId2 = 756,
            StatusName = "",
            RequiredGp = 100,
            RequiredLv = 24,
        },
        [GatherBuffId.FieldMasteryIII] = new()
        {
            // 50% increase
            internalName = "Field Mastery III",
            ClassAction = new()
            {
                [Job.MIN] = new() { ActionId = 295, Name = "Field Mastery III" },
                [Job.BTN] = new() { ActionId = 294, Name = "Field Mastery III" },
            },
            StatusId = 218,
            StatusName = "Gathering Rate Up",
            RequiredGp = 250,
            RequiredLv = 10,
        },
        [GatherBuffId.FieldMasteryII] = new()
        {
            // 15% increase
            internalName = "Field Mastery II",
            ClassAction = new()
            {
                [Job.MIN] = new() { ActionId = 237, Name = "Field Mastery II" },
                [Job.BTN] = new() { ActionId = 220, Name = "Field Mastery II" },
            },
            StatusId = 218,
            StatusName = "Gathering Rate Up",
            RequiredGp = 100,
            RequiredLv = 5,
        },
        [GatherBuffId.FieldMasteryI] = new()
        {
            // 5% increase
            internalName = "Field Mastery I",
            ClassAction = new()
            {
                [Job.MIN] = new() { ActionId = 235, Name = "Field Mastery I" },
                [Job.BTN] = new() { ActionId = 218, Name = "Field Mastery I" },
            },
            StatusId = 218,
            StatusName = "Gathering Rate Up",
            RequiredGp = 50,
            RequiredLv = 4,
        },
        [GatherBuffId.FieldMasteryTemp] = new()
        {
            // 15% increase [temp]
            internalName = "Clear Vision | Flora Mastery",
            ClassAction = new()
            {
                [Job.MIN] = new() { ActionId = 4072, Name = "Clear Vision" },
                [Job.BTN] = new() { ActionId = 4086, Name = "Flora Mastery" },
            },
            StatusId = 754,
            StatusName = "Gathering Rate Up (Limited)",
            RequiredGp = 50,
            RequiredLv = 23,
        },

        // Crystal Specific Actions

        [GatherBuffId.TwelveBounty] = new() // +3 yield on crystals, buff through node
        {
            internalName = "The Twelve Bounty",
            ClassAction = new()
            {
                [Job.MIN] = new() { ActionId = 280, Name = "The Twelve Bounty" },
                [Job.BTN] = new() { ActionId = 282, Name = "The Twelve Bounty" },
            },
            StatusId = 825,
            StatusName = "The Twelve's Bounty",
            RequiredGp = 150,
            RequiredLv = 20,
        },
        [GatherBuffId.GivingLand] = new() // increase yield on crystal, random amount gained
        {
            internalName = "The Giving Land",
            ClassAction = new()
            {
                [Job.MIN] = new() { ActionId = 4589, Name = "The Giving Land" },
                [Job.BTN] = new() { ActionId = 4590, Name = "The Giving Land" },
            },
            StatusId = 1802,
            StatusName = "The Giving Land",
            RequiredGp = 200,
            RequiredLv = 74,
        },

        // Collectable Buffs

        [GatherBuffId.Scrutiny] = new()
        {
            internalName = "Scrutiny",
            ClassAction = new()
            {
                [Job.MIN] = new() { ActionId = 22185, Name = "Scrutiny" },
                [Job.BTN] = new() { ActionId = 22189, Name = "Scrutiny" },
            },
            StatusId = 757,
            StatusName = "",
            RequiredGp = 200,
        },
        [GatherBuffId.Focus] = new()
        {
            internalName = "Collector's Focus",
            ClassAction = new()
            {
                [Job.MIN] = new() { ActionId = 21205, Name = "Collector's Focus" },
                [Job.BTN] = new() { ActionId = 21206, Name = "Collector's Focus" },
            },
            StatusId = 2668,
            StatusName = "",
            RequiredGp = 100,
        },
        [GatherBuffId.Priming] = new()
        {
            internalName = "Priming Touch",
            ClassAction = new()
            {
                [Job.MIN] = new() { ActionId = 34871, Name = "Priming Touch" },
                [Job.BTN] = new() { ActionId = 34872, Name = "Priming Touch" },
            },
            StatusId = 2668,
            StatusName = "",
            RequiredGp = 100,
        },

        // Collectable Actions
        [GatherBuffId.Scour] = new()
        {
            // Base general use skill
            internalName = "Scour",
            ClassAction = new()
            {
                [Job.MIN] = new() { ActionId = 22182, Name = "Scour" },
                [Job.BTN] = new() { ActionId = 22186, Name = "Scour" },
            },
            StatusId = 0,
            StatusName = "n/a",
            RequiredGp = 0,
        },
        [GatherBuffId.Brazen] = new()
        {
            // 50 - 150% buff
            internalName = "Brazen Woodsman",
            ClassAction = new()
            {
                [Job.MIN] = new() { ActionId = 22183, Name = "Brazen Prospector" },
                [Job.BTN] = new() { ActionId = 22187, Name = "Brazen Woodsman" },
            },
            StatusId = 0,
            StatusName = "n/a",
            RequiredGp = 0,
        },
        [GatherBuffId.Meticulous] = new()
        {
            // Chance to not use durability/integrity
            internalName = "Meticulous Woodsman",
            ClassAction = new()
            {
                [Job.MIN] = new() { ActionId = 22184, Name = "Meticulous Prospector" },
                [Job.BTN] = new() { ActionId = 22188, Name = "Meticulous Woodsman" },
            },
            StatusId = 0,
            StatusName = "n/a",
            RequiredGp = 0,
        },
        [GatherBuffId.Collect] = new()
        {
            internalName = "Collect",
            ClassAction = new()
            {
                [Job.MIN] = new() { ActionId = 240, Name = "Collect" },
                [Job.BTN] = new() { ActionId = 815, Name = "Collect" },
            },
            StatusId = 0,
            StatusName = "",
            RequiredGp = 0,
        },

        // Hidden Item Finder (Older Expac + Maps)
        [GatherBuffId.HiddenFinder] = new()
        {
            internalName = "Luck of the X",
            ClassAction = new()
            {
                [Job.MIN] = new() { ActionId = 4081, Name = "Hidden Buff" },
                [Job.BTN] = new() { ActionId = 4095, Name = "Hidden Buff"},
            },
            RequiredGp = 200,
            RequiredLv = 55,
        }
    };
}
