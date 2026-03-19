using ECommons.Logging;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text.Json;

namespace GatherChill.GatheringInfo
{
    public class GatheringRouteLoader
    {
        public Dictionary<uint, GatheringRoute> Routes { get; private set; } = new();

        private static readonly JsonSerializerOptions _readOptions = new()
        {
            PropertyNameCaseInsensitive = true,
            ReadCommentHandling = JsonCommentHandling.Skip,
            AllowTrailingCommas = true,
            Converters = { new Vector3Converter() }
        };

        private static readonly JsonSerializerOptions _writeOptions = new()
        {
            WriteIndented = true,
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            Converters = { new Vector3Converter() }
        };

        private static readonly Dictionary<uint, string> ExpansionNames = new()
        {
            { 0, "2.x - A Realm Reborn" },
            { 1, "3.x - Heavensward" },
            { 2, "4.x - Stormblood" },
            { 3, "5.x - Shadowbringers" },
            { 4, "6.x - Endwalker" },
            { 5, "7.x - Dawntrail" }
        };

        // ── Load ──────────────────────────────────────────────────────────────

        public void LoadAllRoutes()
        {
            Routes.Clear();

            var assembly = Assembly.GetExecutingAssembly();
            var resources = assembly.GetManifestResourceNames()
                .Where(n => n.Contains("Routes") && n.EndsWith(".json"));

            foreach (var resourceName in resources)
            {
                try
                {
                    using var stream = assembly.GetManifestResourceStream(resourceName);
                    if (stream == null) continue;

                    using var reader = new StreamReader(stream);
                    var route = JsonSerializer.Deserialize<GatheringRoute>(reader.ReadToEnd(), _readOptions);

                    if (route == null || !ValidateRoute(route, resourceName)) continue;

                    if (Routes.ContainsKey(route.RouteId))
                    {
                        PluginLog.Warning($"Duplicate routeId {route.RouteId} in {resourceName}, skipping");
                        continue;
                    }

                    Routes[route.RouteId] = route;
                }
                catch (Exception ex)
                {
                    PluginLog.Error($"Failed to load {resourceName}: {ex.Message}");
                }
            }

            PluginLog.Information($"Loaded {Routes.Count} gathering routes");
        }

        // ── Save ──────────────────────────────────────────────────────────────

        public void SaveRoute(GatheringRoute route, string outputDirectory)
        {
            if (!ExpansionNames.TryGetValue(route.ExpansionId, out var expansionFolder))
            {
                PluginLog.Error($"Route {route.RouteId} has unknown expansionId {route.ExpansionId}");
                return;
            }

            // Expansion/ZoneName/PlaceName (if different from zone)
            var dir = Path.Combine(outputDirectory, expansionFolder, SanitizeName(route.ZoneName));
            if (!string.IsNullOrWhiteSpace(route.PlaceName) && route.PlaceName != route.ZoneName)
                dir = Path.Combine(dir, SanitizeName(route.PlaceName));

            Directory.CreateDirectory(dir);

            var filePath = Path.Combine(dir, $"{route.RouteId}.json");
            File.WriteAllText(filePath, JsonSerializer.Serialize(route, _writeOptions));

            Routes[route.RouteId] = route;
            PluginLog.Verbose($"Saved route {route.RouteId} -> {Path.GetRelativePath(outputDirectory, filePath)}");
        }

        private static string SanitizeName(string name)
        {
            var invalid = Path.GetInvalidFileNameChars();
            return string.Join("_", name.Split(invalid, StringSplitOptions.RemoveEmptyEntries)).TrimEnd('.');
        }

        public void SaveAllRoutes(string outputDirectory)
        {
            foreach (var route in Routes.Values)
                SaveRoute(route, outputDirectory);
        }

        // ── Queries ───────────────────────────────────────────────────────────

        public GatheringRoute? GetRoute(uint routeId) => Routes.TryGetValue(routeId, out var route) ? route : null;

        public List<GatheringRoute> GetRoutesForTerritory(uint territoryId) => Routes.Values.Where(r => r.TerritoryId == territoryId).ToList();

        public List<GatheringRoute> GetRoutesForJob(uint jobId) => Routes.Values.Where(r => r.GatheringJobId == jobId).ToList();

        public List<GatheringRoute> GetRoutesForExpansion(uint expansionId) => Routes.Values.Where(r => r.ExpansionId == expansionId).ToList();

        // ── Node Helpers ──────────────────────────────────────────────────────

        public bool ContainsSpecificNode(GatheringRoute route, uint nodeId, Vector3 position)
        {
            foreach (var node in route.NodeInfo)
                if (node.NodeId == nodeId)
                    foreach (var location in node.Locations)
                        if (location.Position == position)
                            return true;
            return false;
        }

        public void AddNodeLocationIfMissing(GatheringRoute route, uint nodeId, Vector3 position)
        {
            if (ContainsSpecificNode(route, nodeId, position)) return;
            if (!route.NodeIds.Contains(nodeId)) return;

            var newLocation = new NodeLocation { Position = position };

            var existing = route.NodeInfo.FirstOrDefault(n => n.NodeId == nodeId);
            if (existing != null)
                existing.Locations.Add(newLocation);
            else
                route.NodeInfo.Add(new GatheringNode { NodeId = nodeId, GroupId = 0, Locations = new() { newLocation } });
        }

        // ── Internals ─────────────────────────────────────────────────────────

        private bool ValidateRoute(GatheringRoute route, string source)
        {
            if (route.NodeIds is null or { Count: 0 }) { PluginLog.Warning($"{source}: no nodes"); return false; }

            foreach (var node in route.NodeInfo)
                if (node.Locations is null or { Count: 0 })
                { PluginLog.Warning($"{source}: node {node.NodeId} has no locations"); return false; }

            return true;
        }
    }
}