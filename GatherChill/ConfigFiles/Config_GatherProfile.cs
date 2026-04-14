using FFXIVClientStructs.FFXIV.Client.Game.UI;
using GatherChill.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace GatherChill.ConfigFiles;

public partial class Config
{
    public Dictionary<int, GatherProfile> GatherProfiles { get; set; } = new()
    {
        [0] = new GatherProfile()
    };

    public class GatherProfile
    {
        public string Name { get; set; } = "";
        public Dictionary<int, BuffProfiles> DurabilityProfile { get; set; } = new()
        {
            [4] = new()
        };
    }

    public class BuffProfiles
    {
        public Dictionary<GatherBuffId, BuffConfig> Buffs { get; set; } = new()
        {
            [GatherBuffId.BoonIncrease_2] = new() { MinGp = 100 },
            [GatherBuffId.BoonIncrease_1] = new() { MinGp = 50 },
            [GatherBuffId.Tidings] = new() { MinGp = 200 },
            [GatherBuffId.YieldII] = new() { MinGp = 500 },
            [GatherBuffId.YieldI] = new() { MinGp = 400 },
            [GatherBuffId.BYII] = new() { MinGp = 100 },
            [GatherBuffId.BonusIntegrity] = new() { MinGp = 300 },
            [GatherBuffId.BonusIntegrity_Chance] = new() { Enabled = true, MinGp = 0 },
            [GatherBuffId.FieldMasteryIII] = new() { MinGp = 250 },
            [GatherBuffId.FieldMasteryII] = new() { MinGp = 100 },
            [GatherBuffId.FieldMasteryI] = new() { MinGp = 50 },
            [GatherBuffId.FieldMasteryTemp] = new() { MinGp = 50 },
        };
        public int BountifulMinItem { get; set; } = 4;
    }

    public class BuffConfig
    {
        public bool Enabled { get; set; } = false;
        public int MinGp { get; set; }
        public int MaxUse { get; set; }
        public int MinDurability { get; set; } = 4;
    }
}
