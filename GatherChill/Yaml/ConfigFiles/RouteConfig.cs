using GatherChill.Utilities.GatheringHelpers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GatherChill.Yaml.ConfigFiles;

public class RouteConfig : IYamlConfigWithPath
{
    public uint RouteId { get; set; }
    public string ZoneName { get; set; } = string.Empty;
    public uint ZoneId { get; set; }
    public string ExpansionName { get; set; } = string.Empty;
    public uint ExpansionId { get; set; }
    public Vector2 MapCenter { get; set; }
    public uint MapRadius { get; set; }
    public uint GatheringType { get; set; }
    public HashSet<uint> NodeIds { get; set; } = new();
    public Dictionary<uint, string> Items { get; set; } = new();
    public HashSet<uint> ItemIds { get; set; } = new();

    // New field for detailed node information
    public List<GatherClasses.RouteData> NodeInfo { get; set; } = new();

    public void Save(string path) => YamlConfig.Save(this, path);

    public string GetDefaultPath()
    {
        // Create a safe filename from zone name
        string safeZoneName = string.IsNullOrEmpty(ZoneName) ? "Unknown" :
            string.Join("", ZoneName.Split(Path.GetInvalidFileNameChars()));

        string safeExpansionName = string.IsNullOrEmpty(ExpansionName) ? "Unknown" :
            string.Join("", ExpansionName.Split(Path.GetInvalidFileNameChars()));

        return Path.Combine(
            Svc.PluginInterface.ConfigDirectory.FullName,
            "Routes",
            safeExpansionName,
            $"{safeZoneName}_{ZoneId}",
            $"Route_{RouteId}.yaml"
        );
    }

    // Static method to get path for a specific route
    public static string GetRoutePath(uint routeId, uint zoneId, string zoneName, string expansionName)
    {
        string safeZoneName = string.IsNullOrEmpty(zoneName) ? "Unknown" :
            string.Join("", zoneName.Split(Path.GetInvalidFileNameChars()));

        string safeExpansionName = string.IsNullOrEmpty(expansionName) ? "Unknown" :
            string.Join("", expansionName.Split(Path.GetInvalidFileNameChars()));

        return Path.Combine(
            Svc.PluginInterface.ConfigDirectory.FullName,
            "Routes",
            safeExpansionName,
            $"{safeZoneName}_{zoneId}",
            $"Route_{routeId}.yaml"
        );
    }
}