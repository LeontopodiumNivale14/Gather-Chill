using GatherChill.Enums;
using System.Collections.Generic;

namespace GatherChill.Utilities.GatheringHelpers;

public static partial class Gather_Util
{
    public class BuffSettings
    {
        public bool Enabled { get; set; } = false;
        public uint GP_Min { get; set; } = 0;
        public uint MaxUse { get; set; } = 0;
        public uint Durability_MinUse { get; set; } = 0;
        public uint MinItems { get; set; } = 0;
    }

    public class GatherProfile
    {
        public int ProfileId { get; set; }
        public string Name { get; set; } = "New Profile";
        public uint GP_MinimumInteraction { get; set; } = 0;
        public Dictionary<GatherBuffId, BuffSettings> GatheringBuffs { get; set; } = new()
        {
            [GatherBuffId.BoonIncrease_1] = new(),
            [GatherBuffId.BoonIncrease_2] = new(),
            [GatherBuffId.Tidings] = new(),
            [GatherBuffId.YieldI] = new(),
            [GatherBuffId.YieldII] = new(),
            [GatherBuffId.BonusIntegrity] = new(),
            [GatherBuffId.BonusIntegrity_Chance] = new(),
            [GatherBuffId.BYII] = new(),
            [GatherBuffId.FieldMasteryI] = new(),
            [GatherBuffId.FieldMasteryII] = new(),
            [GatherBuffId.FieldMasteryIII] = new(),
            [GatherBuffId.FieldMasteryTemp] = new(),
            [GatherBuffId.TwelveBounty] = new(),
            [GatherBuffId.GivingLand] = new(),
            [GatherBuffId.Scrutiny] = new(),
            [GatherBuffId.Focus] = new(),
            [GatherBuffId.Priming] = new(),
            [GatherBuffId.Scour] = new(),
            [GatherBuffId.Brazen] = new(),
            [GatherBuffId.Meticulous] = new(),
        };

        public List<GatherBuffId> BuffPriority { get; set; } = new()
        {
            GatherBuffId.BoonIncrease_1, GatherBuffId.BoonIncrease_2,
            GatherBuffId.Tidings, GatherBuffId.YieldI, GatherBuffId.YieldII,
            GatherBuffId.BonusIntegrity, GatherBuffId.BonusIntegrity_Chance,
            GatherBuffId.BYII, GatherBuffId.FieldMasteryI, GatherBuffId.FieldMasteryII, GatherBuffId.FieldMasteryIII, GatherBuffId.FieldMasteryTemp,
            GatherBuffId.TwelveBounty,
        };

        public GatherProfile() { }

        public GatherProfile(GatherProfile other)
        {
            ProfileId = other.ProfileId;
            Name = other.Name;
            GP_MinimumInteraction = other.GP_MinimumInteraction;
            GatheringBuffs = other.GatheringBuffs.ToDictionary(
                kv => kv.Key,
                kv => new BuffSettings
                {
                    Enabled = kv.Value.Enabled,
                    GP_Min = kv.Value.GP_Min,
                    MaxUse = kv.Value.MaxUse,
                    Durability_MinUse = kv.Value.Durability_MinUse,
                    MinItems = kv.Value.MinItems,
                });
            BuffPriority = new List<GatherBuffId>(other.BuffPriority);
        }
    }

    public static GatherProfile DefaultProfile = new()
    {
        ProfileId = 0,
        Name = "Default",
    };

    public class ProfileClass
    {
        public Dictionary<GatherBuffId, bool> BuffsEnabled { get; set; } = new()
        {
            [GatherBuffId.BoonIncrease_1] = true,
            [GatherBuffId.BoonIncrease_2] = true,
            [GatherBuffId.Tidings] = true,
            [GatherBuffId.YieldI] = true,
            [GatherBuffId.YieldII] = true,
            [GatherBuffId.BonusIntegrity] = true,
            [GatherBuffId.BonusIntegrity_Chance] = true,
            [GatherBuffId.BYII] = true,
            [GatherBuffId.FieldMasteryI] = true,
            [GatherBuffId.FieldMasteryII] = true,
            [GatherBuffId.FieldMasteryIII] = true,
            [GatherBuffId.FieldMasteryTemp] = true,
        };

        public int BYII_Amount { get; set; } = 3;
        public int TidingsChance { get; set; } = 70;
    }
}
