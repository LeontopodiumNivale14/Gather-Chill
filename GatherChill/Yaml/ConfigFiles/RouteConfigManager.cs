using ECommons.Logging;
using GatherChill.Utilities.GatheringHelpers;
using GatherChill.Yaml.ConfigFiles;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace GatherChill.Yaml;

// Manager class to handle bulk route operations
public static class RouteConfigManager
{
    private static readonly string RoutesDirectory = Path.Combine(
        Svc.PluginInterface.ConfigDirectory.FullName,
        "Routes"
    );

    public static void EnsureRouteConfigsExist()
    {
        PluginLog.Information("Checking and creating route config files...");

        // Group routes by zone for better organization info
        var routesByZone = GatherClasses.GatheringDatabase
            .GroupBy(x => new { x.Value.ZoneId, x.Value.ZoneName, x.Value.ExpansionName })
            .ToList();

        foreach (var zoneGroup in routesByZone)
        {
            var zoneInfo = zoneGroup.Key;
            var routesInZone = zoneGroup.ToList();

            PluginLog.Information($"Processing {routesInZone.Count} routes for zone: {zoneInfo.ZoneName} ({zoneInfo.ZoneId})");

            foreach (var entry in routesInZone)
            {
                var routeId = entry.Key;
                var routeData = entry.Value;

                // Create the config object
                var routeConfig = new RouteConfig
                {
                    RouteId = routeId,
                    ZoneName = routeData.ZoneName,
                    ZoneId = routeData.ZoneId,
                    ExpansionName = routeData.ExpansionName,
                    ExpansionId = routeData.ExpansionId,
                    MapCenter = routeData.MapCenter,
                    MapRadius = routeData.MapRadius,
                    GatheringType = routeData.GatheringType,
                    NodeIds = new HashSet<uint>(routeData.NodeIds),
                    Items = new Dictionary<uint, string>(routeData.Items),
                    ItemIds = new HashSet<uint>(routeData.ItemIds)
                };

                // Get the file path
                string filePath = routeConfig.GetDefaultPath();

                // Only create if it doesn't exist
                if (!File.Exists(filePath))
                {
                    PluginLog.Information($"Creating route config: {Path.GetFileName(filePath)}");
                    routeConfig.Save(filePath);
                }
                else
                {
                    PluginLog.Debug($"Route config already exists: {Path.GetFileName(filePath)}");
                }
            }
        }

        PluginLog.Information($"Route config check complete. Files stored in: {RoutesDirectory}");
    }

    public static List<RouteConfig> LoadAllRouteConfigs()
    {
        var configs = new List<RouteConfig>();

        if (!Directory.Exists(RoutesDirectory))
            return configs;

        var yamlFiles = Directory.GetFiles(RoutesDirectory, "*.yaml", SearchOption.AllDirectories);

        foreach (var file in yamlFiles)
        {
            try
            {
                var config = YamlConfig.Load<RouteConfig>(file);
                configs.Add(config);
            }
            catch (Exception ex)
            {
                PluginLog.Warning($"Failed to load route config from {file}: {ex.Message}");
            }
        }

        return configs;
    }

    public static void RefreshAllRouteConfigs()
    {
        PluginLog.Information("Refreshing all route configs with latest data...");

        foreach (var entry in GatherClasses.GatheringDatabase)
        {
            var routeId = entry.Key;
            var routeData = entry.Value;

            var routeConfig = new RouteConfig
            {
                RouteId = routeId,
                ZoneName = routeData.ZoneName,
                ZoneId = routeData.ZoneId,
                ExpansionName = routeData.ExpansionName,
                ExpansionId = routeData.ExpansionId,
                MapCenter = routeData.MapCenter,
                MapRadius = routeData.MapRadius,
                GatheringType = routeData.GatheringType,
                NodeIds = new HashSet<uint>(routeData.NodeIds),
                Items = new Dictionary<uint, string>(routeData.Items),
                ItemIds = new HashSet<uint>(routeData.ItemIds)
            };

            string filePath = routeConfig.GetDefaultPath();
            routeConfig.Save(filePath);
        }
    }

    public static RouteConfig LoadRouteConfig(uint routeId)
    {
        // First try to find it in the database to get the zone/expansion info
        if (GatherClasses.GatheringDatabase.TryGetValue(routeId, out var routeData))
        {
            string filePath = RouteConfig.GetRoutePath(routeId, routeData.ZoneId, routeData.ZoneName, routeData.ExpansionName);
            return YamlConfig.LoadOrCreate<RouteConfig>(filePath);
        }

        // Fallback: search for any file with this route ID
        var routeFiles = Directory.GetFiles(RoutesDirectory, $"Route_{routeId}.yaml", SearchOption.AllDirectories);
        if (routeFiles.Length > 0)
        {
            return YamlConfig.Load<RouteConfig>(routeFiles[0]);
        }

        // Create a new one with minimal info
        var newConfig = new RouteConfig { RouteId = routeId };
        return newConfig;
    }

    public static void CleanupOrphanedConfigs()
    {
        if (!Directory.Exists(RoutesDirectory))
            return;

        var yamlFiles = Directory.GetFiles(RoutesDirectory, "*.yaml", SearchOption.AllDirectories);
        var validRouteIds = GatherClasses.GatheringDatabase.Keys.ToHashSet();

        foreach (var file in yamlFiles)
        {
            try
            {
                var config = YamlConfig.Load<RouteConfig>(file);
                if (!validRouteIds.Contains(config.RouteId))
                {
                    PluginLog.Information($"Removing orphaned route config: {Path.GetFileName(file)}");
                    File.Delete(file);

                    // Clean up empty directories
                    var directory = Path.GetDirectoryName(file);
                    if (Directory.Exists(directory) && !Directory.EnumerateFileSystemEntries(directory).Any())
                    {
                        Directory.Delete(directory);
                        PluginLog.Debug($"Removed empty directory: {Path.GetFileName(directory)}");
                    }
                }
            }
            catch (Exception ex)
            {
                PluginLog.Warning($"Failed to check route config {file}: {ex.Message}");
            }
        }
    }

    // New helper methods for zone-based operations

    public static List<RouteConfig> LoadRouteConfigsForZone(uint zoneId)
    {
        var configs = new List<RouteConfig>();

        if (!Directory.Exists(RoutesDirectory))
            return configs;

        // Find all yaml files that contain the zone ID in their name
        var yamlFiles = Directory.GetFiles(RoutesDirectory, $"Route_*.yaml", SearchOption.AllDirectories);

        foreach (var file in yamlFiles)
        {
            try
            {
                var config = YamlConfig.Load<RouteConfig>(file);
                if (config.ZoneId == zoneId)
                {
                    configs.Add(config);
                }
            }
            catch (Exception ex)
            {
                PluginLog.Warning($"Failed to load route config from {file}: {ex.Message}");
            }
        }

        return configs.OrderBy(c => c.RouteId).ToList();
    }

    public static List<RouteConfig> LoadRouteConfigsForExpansion(string expansionName)
    {
        var configs = new List<RouteConfig>();

        string safeExpansionName = string.IsNullOrEmpty(expansionName) ? "Unknown" :
            string.Join("", expansionName.Split(Path.GetInvalidFileNameChars()));

        string expansionPath = Path.Combine(RoutesDirectory, safeExpansionName);

        if (!Directory.Exists(expansionPath))
            return configs;

        var yamlFiles = Directory.GetFiles(expansionPath, "*.yaml", SearchOption.AllDirectories);

        foreach (var file in yamlFiles)
        {
            try
            {
                var config = YamlConfig.Load<RouteConfig>(file);
                configs.Add(config);
            }
            catch (Exception ex)
            {
                PluginLog.Warning($"Failed to load route config from {file}: {ex.Message}");
            }
        }

        return configs.OrderBy(c => c.ZoneName).ThenBy(c => c.RouteId).ToList();
    }

    public static Dictionary<string, List<RouteConfig>> GetAllRoutesByZone()
    {
        var routesByZone = new Dictionary<string, List<RouteConfig>>();
        var allConfigs = LoadAllRouteConfigs();

        foreach (var config in allConfigs)
        {
            string zoneKey = $"{config.ZoneName} ({config.ZoneId})";

            if (!routesByZone.ContainsKey(zoneKey))
            {
                routesByZone[zoneKey] = new List<RouteConfig>();
            }

            routesByZone[zoneKey].Add(config);
        }

        // Sort routes within each zone
        foreach (var kvp in routesByZone)
        {
            kvp.Value.Sort((a, b) => a.RouteId.CompareTo(b.RouteId));
        }

        return routesByZone;
    }
}