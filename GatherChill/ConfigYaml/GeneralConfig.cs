using System;
using System.Collections.Generic;
using System.IO;

namespace GatherChill.ConfigYaml;

public class GeneralConfig : IYamlConfig
{
    // Version tracking, used for migration if higher versions are set in the future
    public int ConfigVersion { get; set; } = 1;

    // Gathering item list
    public List<GatheringConfig> GatheringList { get; set; } = new();

    public class AbilityConfig
    {
        public bool Enable { get; set; } = false;
        public int MinimumGP { get; set; }
        public int ChanceRequirement { get; set; } = 0;
    }

    public Dictionary<string, AbilityConfig> AbilityConfigDict { get; set; } = new()
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

    // Debug Stuff
    public uint PictoCircleColor { get; set; } = 0;
    public uint PictoLineColor { get; set; } = 0;
    public uint PictoWPColor { get; set; } = 0;
    public uint PictoTextCol { get; set; } = 0;
    public float DotRadius { get; set; } = 0f;
    public float LineWidth { get; set; } = 0f;
    public Vector2 DonutRadius { get; set; } = new Vector2(0.7f, 1.4f);
    public Vector2 FanPosition { get; set; } = new Vector2(0.0f, 6.283f);
    public float TextFloatPlus { get; set; } = 0.0f;


    // Path Stuff
    public static string ConfigPath => Path.Combine(Svc.PluginInterface.ConfigDirectory.FullName, "GatherChillConfig.yaml");
    public void Save() => YamlConfig.Save(this, ConfigPath);
}
