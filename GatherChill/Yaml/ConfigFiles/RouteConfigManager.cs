using ECommons.Logging;
using GatherChill.Utilities.GatheringHelpers;
using GatherChill.Yaml.ConfigFiles;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace GatherChill.Yaml;

// Manager class to handle bulk route operations
public class RouteConfigManager
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

    public static void SaveRouteConfig(RouteConfig config)
    {
        string filePath = config.GetDefaultPath();
        config.Save(filePath);
        PluginLog.Information($"Saved route config: {Path.GetFileName(filePath)}");
    }

    public static bool MoveRouteConfig(RouteConfig config, string oldZoneName, uint oldZoneId, string oldExpansionName)
    {
        try
        {
            // Get old and new paths
            string oldPath = RouteConfig.GetRoutePath(config.RouteId, oldZoneId, oldZoneName, oldExpansionName);
            string newPath = config.GetDefaultPath();

            // If paths are the same, no need to move
            if (oldPath == newPath)
                return true;

            // Create new directory structure
            Directory.CreateDirectory(Path.GetDirectoryName(newPath)!);

            // Move the file if old one exists
            if (File.Exists(oldPath))
            {
                File.Move(oldPath, newPath);
                PluginLog.Information($"Moved route config from {oldPath} to {newPath}");

                // Clean up empty directories
                CleanupEmptyDirectories(Path.GetDirectoryName(oldPath));
            }
            else
            {
                // Just save to new location
                config.Save(newPath);
                PluginLog.Information($"Saved route config to new location: {newPath}");
            }

            return true;
        }
        catch (Exception ex)
        {
            PluginLog.Error($"Failed to move route config: {ex.Message}");
            return false;
        }
    }

    public static void DeleteRouteConfig(RouteConfig config)
    {
        try
        {
            string filePath = config.GetDefaultPath();
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
                PluginLog.Information($"Deleted route config: {Path.GetFileName(filePath)}");

                // Clean up empty directories
                CleanupEmptyDirectories(Path.GetDirectoryName(filePath));
            }
        }
        catch (Exception ex)
        {
            PluginLog.Error($"Failed to delete route config: {ex.Message}");
        }
    }

    private static void CleanupEmptyDirectories(string? directoryPath)
    {
        if (string.IsNullOrEmpty(directoryPath) || !Directory.Exists(directoryPath))
            return;

        try
        {
            // Don't delete the root Routes directory
            if (Path.GetFileName(directoryPath).Equals("Routes", StringComparison.OrdinalIgnoreCase))
                return;

            // If directory is empty, delete it and check parent
            if (!Directory.EnumerateFileSystemEntries(directoryPath).Any())
            {
                Directory.Delete(directoryPath);
                PluginLog.Debug($"Removed empty directory: {directoryPath}");

                // Recursively clean parent directories
                CleanupEmptyDirectories(Path.GetDirectoryName(directoryPath));
            }
        }
        catch (Exception ex)
        {
            PluginLog.Warning($"Failed to cleanup directory {directoryPath}: {ex.Message}");
        }
    }

    public static List<(string Display, RouteConfig Config)> GetAllRoutesForSelection()
    {
        var routes = new List<(string Display, RouteConfig Config)>();
        var allConfigs = LoadAllRouteConfigs();

        foreach (var config in allConfigs.OrderBy(c => c.ExpansionName).ThenBy(c => c.ZoneName).ThenBy(c => c.RouteId))
        {
            string display = $"[{config.ExpansionName}] {config.ZoneName} - Route {config.RouteId}";
            routes.Add((display, config));
        }

        return routes;
    }
}