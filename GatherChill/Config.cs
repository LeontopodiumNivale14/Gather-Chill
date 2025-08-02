using System.Collections.Generic;
using System.Text.Json.Serialization;
using ECommons.Configuration;

namespace GatherChill;
public class Config : IEzConfig
{
    [JsonIgnore]
    public const int CurrentConfigVersion = 1;

    public List<GatheringConfig> GatheringList = new();

    public class AbilityConfig
    {
        public bool Enable { get; set; } = false;
        public int MinimumGP { get; set; }
        public int ChanceRequirement { get; set; } = 0;
    }

    public Dictionary<string, AbilityConfig> AbilityConfigDict = new()
    {
        { "BoonIncrease1", new AbilityConfig
        {
            Enable = false,
            MinimumGP = 50,
            ChanceRequirement = 60,
        } },
        { "BoonIncrease2", new AbilityConfig
        {
            Enable = false,
            MinimumGP = 100,
            ChanceRequirement = 70,
        } },
        { "Tidings", new AbilityConfig
        {
            Enable = false,
            MinimumGP = 200,
            ChanceRequirement = 70,
        } },
        { "Yield1", new AbilityConfig
        {
            Enable = false,
            MinimumGP = 400,
            ChanceRequirement = 0,
        } },
        { "Yield2", new AbilityConfig
        {
            Enable = false,
            MinimumGP = 500,
            ChanceRequirement = 0,
        } },
        { "IntegrityIncrease", new AbilityConfig {
            Enable = false,
            MinimumGP = 300,
            ChanceRequirement = 0,
        } }
    };

    public void Save()
    {
        EzConfig.Save();
    }
}