using Dalamud.Plugin;
using ECommons.Logging;
using GatherChill.GatheringInfo;
using GatherChill.Utilities.GatheringHelpers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;

namespace GatherChill.GatheringInfo
{
    public class GatheringRouteLoader
    {
        private readonly string _routesDirectory;
        private readonly string _projectDirectory;

        // Make the directory path accessible to other classes
        public string RoutesDirectory => _routesDirectory;

        // Dictionary to store routes by routeId for fast lookup
        public Dictionary<uint, GatheringRoute> Routes { get; private set; } = new();

        // Expansion names for fold.er organization
        private static readonly Dictionary<uint, string> ExpansionNames = new()
        {
            { 0, "2.x - A Realm Reborn" },
            { 1, "3.x - Heavensward" },
            { 2, "4.x - Stormblood" },
            { 3, "5.x - Shadowbringers" },
            { 4, "6.x - Endwalker" },
            { 5, "7.x - Dawntrail" }
        };

        // Add this to LegacyRouteImporter.cs
        private static readonly Dictionary<uint, uint> TerritoryToExpansion = new()
        {
            // A Realm Reborn (0)
            { 134, 0 }, // Limsa Lominsa Lower Decks
            { 148, 0 }, // Central Shroud
            { 152, 0 }, // East Shroud
            { 153, 0 }, // South Shroud
            { 154, 0 }, // North Shroud
            { 155, 0 }, // Central Thanalan
            { 138, 0 }, // Western La Noscea
            { 139, 0 }, // Middle La Noscea
            { 140, 0 }, // Lower La Noscea
            { 141, 0 }, // Eastern La Noscea
            { 145, 0 }, // Upper La Noscea
            { 180, 0 }, // Outer La Noscea
            { 137, 0 }, // Mor Dhona
    
            // Heavensward (1)
            { 397, 1 }, // Coerthas Western Highlands
            { 398, 1 }, // The Dravanian Forelands
            { 399, 1 }, // The Dravanian Hinterlands
            { 400, 1 }, // The Churning Mists
            { 401, 1 }, // The Sea of Clouds
            { 402, 1 }, // Azys Lla
    
            // Stormblood (2)
            { 612, 2 }, // The Fringes
            { 620, 2 }, // The Peaks
            { 621, 2 }, // The Lochs
            { 613, 2 }, // The Ruby Sea
            { 614, 2 }, // Yanxia
            { 622, 2 }, // The Azim Steppe
    
            // Shadowbringers (3)
            { 813, 3 }, // Lakeland
            { 814, 3 }, // Kholusia
            { 815, 3 }, // Amh Araeng
            { 816, 3 }, // Il Mheg
            { 817, 3 }, // The Rak'tika Greatwood
            { 818, 3 }, // The Tempest
    
            // Endwalker (4)
            { 956, 4 }, // Labyrinthos
            { 957, 4 }, // Thavnair
            { 958, 4 }, // Garlemald
            { 959, 4 }, // Mare Lamentorum
            { 960, 4 }, // Ultima Thule
            { 961, 4 }, // Elpis
    
            // Dawntrail (5)
            { 1187, 5 }, // Urqopacha
            { 1188, 5 }, // Kozama'uka
            { 1189, 5 }, // Yak T'el
            { 1190, 5 }, // Shaaloani
            { 1191, 5 }, // Heritage Found
            { 1192, 5 }, // Living Memory
        };

        public GatheringRouteLoader(IDalamudPluginInterface pi)
        {
            // For development: Try to find the project directory
            // Start from the assembly location and walk up to find .csproj
            var assemblyDir = pi.AssemblyLocation.Directory;
            string projectRoot = null;

            // Walk up directories looking for .csproj file
            var currentDir = assemblyDir;
            while (currentDir != null && currentDir.Parent != null)
            {
                // Check if this directory contains a .csproj file
                var csprojFiles = currentDir.GetFiles("*.csproj");
                if (csprojFiles.Length > 0)
                {
                    projectRoot = currentDir.FullName;
                    break;
                }
                currentDir = currentDir.Parent;
            }

            if (projectRoot != null)
            {
                _projectDirectory = projectRoot;
                _routesDirectory = Path.Combine(_projectDirectory, "GatheringInfo", "Routes");
                PluginLog.Information($"Found project directory: {_projectDirectory}");
            }
            else
            {
                // Fallback to config directory if we can't find project directory
                PluginLog.Warning("Could not locate project directory, using config directory instead");
                _routesDirectory = Path.Combine(pi.ConfigDirectory.FullName, "GatheringInfo", "Routes");
            }

            // Create base directory if it doesn't exist
            if (!Directory.Exists(_routesDirectory))
            {
                Directory.CreateDirectory(_routesDirectory);
                PluginLog.Information($"Created gathering routes directory: {_routesDirectory}");
            }
            else
            {
                PluginLog.Information($"Using gathering routes directory: {_routesDirectory}");
            }

            // Create expansion folders
            CreateExpansionFolders();
        }

        /// <summary>
        /// Create the expansion folder structure
        /// </summary>
        private void CreateExpansionFolders()
        {
            foreach (var expansion in ExpansionNames)
            {
                var expansionPath = Path.Combine(_routesDirectory, expansion.Value);
                if (!Directory.Exists(expansionPath))
                {
                    Directory.CreateDirectory(expansionPath);
                    PluginLog.Verbose($"Created expansion folder: {expansion.Value}");
                }
            }
        }

        /// <summary>
        /// Load all gathering route JSON files from the routes directory (recursively)
        /// </summary>
        public void LoadAllRoutes()
        {
            Routes.Clear();

            if (!Directory.Exists(_routesDirectory))
            {
                PluginLog.Warning("Routes directory does not exist");
                return;
            }

            // Search recursively for all .json files
            var jsonFiles = Directory.GetFiles(_routesDirectory, "*.json", SearchOption.AllDirectories);
            PluginLog.Information($"Found {jsonFiles.Length} route files");

            foreach (var filePath in jsonFiles)
            {
                try
                {
                    var route = LoadRoute(filePath);
                    if (route != null)
                    {
                        if (Routes.ContainsKey(route.RouteId))
                        {
                            PluginLog.Warning($"Duplicate routeId {route.RouteId} found in {Path.GetFileName(filePath)}. Skipping.");
                            continue;
                        }

                        Routes[route.RouteId] = route;
                        PluginLog.Information($"Loaded route {route.RouteId}: {route.ZoneName} ({route.NodeIds.Count} unique nodes)");
                    }
                }
                catch (Exception ex)
                {
                    PluginLog.Error($"Failed to load route from {Path.GetFileName(filePath)}: {ex.Message}");
                }
            }

            PluginLog.Information($"Successfully loaded {Routes.Count} gathering routes");
        }

        /// <summary>
        /// Load a single gathering route from a JSON file
        /// </summary>
        public GatheringRoute LoadRoute(string filePath)
        {
            try
            {
                var json = File.ReadAllText(filePath);

                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true,
                    ReadCommentHandling = JsonCommentHandling.Skip,
                    AllowTrailingCommas = true
                };

                var route = JsonSerializer.Deserialize<GatheringRoute>(json, options);

                if (route == null)
                {
                    PluginLog.Error($"Failed to deserialize route from {Path.GetFileName(filePath)}");
                    return null;
                }

                // Validate the route
                if (!ValidateRoute(route, filePath))
                {
                    return null;
                }

                return route;
            }
            catch (JsonException ex)
            {
                PluginLog.Error($"JSON parsing error in {Path.GetFileName(filePath)}: {ex.Message}");
                return null;
            }
            catch (Exception ex)
            {
                PluginLog.Error($"Error loading route from {Path.GetFileName(filePath)}: {ex.Message}");
                return null;
            }
        }

        /// <summary>
        /// Validate that a route has all required data
        /// </summary>
        private bool ValidateRoute(GatheringRoute route, string filePath)
        {
            var fileName = Path.GetFileName(filePath);

            if (route.RouteId == 0)
            {
                PluginLog.Warning($"Route in {fileName} has invalid routeId (0)");
                return false;
            }

            if (string.IsNullOrWhiteSpace(route.ZoneName))
            {
                PluginLog.Warning($"Route {route.RouteId} in {fileName} is missing zone name");
                return false;
            }

            if (route.GatheringJobId < 16 || route.GatheringJobId > 18)
            {
                PluginLog.Warning($"Route {route.RouteId} in {fileName} has invalid gatheringJobId: {route.GatheringJobId} (must be 16, 17, or 18)");
                return false;
            }

            if (route.NodeGroups == null || route.NodeGroups.Count == 0)
            {
                PluginLog.Warning($"Route {route.RouteId} in {fileName} has no node groups");
                return false;
            }

            // Validate that all nodes have locations
            foreach (var group in route.NodeGroups)
            {
                foreach (var node in group.Nodes)
                {
                    if (node.Locations == null || node.Locations.Count == 0)
                    {
                        PluginLog.Warning($"Route {route.RouteId} in {fileName}: Node {node.NodeId} has no locations");
                        return false;
                    }
                }
            }

            return true;
        }

        /// <summary>
        /// Save a gathering route to a JSON file in the organized folder structure
        /// Format: ExpansionId + Expansion/ZoneName/routeId + GatheringArea.json
        /// </summary>
        public void SaveRoute(GatheringRoute route, string gatheringAreaName = null)
        {
            try
            {
                // Get expansion folder name
                if (!ExpansionNames.TryGetValue(route.ExpansionId, out var expansionFolder))
                {
                    PluginLog.Error($"Invalid expansionId {route.ExpansionId} for route {route.RouteId}");
                    return;
                }

                // Build the path: GatheringInfo/ExpansionFolder/ZoneName/
                var zonePath = Path.Combine(_routesDirectory, expansionFolder, SanitizeFolderName(route.ZoneName));

                // Create zone folder if it doesn't exist
                if (!Directory.Exists(zonePath))
                {
                    Directory.CreateDirectory(zonePath);
                }

                // Build filename: routeId + GatheringArea.json (e.g., "12345 - Middle La Noscea.json")
                var fileName = gatheringAreaName != null
                    ? $"{route.RouteId} - {SanitizeFileName(gatheringAreaName)}.json"
                    : $"{route.RouteId} - {SanitizeFileName(route.ZoneName)}.json";

                var filePath = Path.Combine(zonePath, fileName);

                var options = new JsonSerializerOptions
                {
                    WriteIndented = true,
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                };

                var json = JsonSerializer.Serialize(route, options);
                File.WriteAllText(filePath, json);

                PluginLog.Information($"Saved route {route.RouteId} to {Path.Combine(expansionFolder, route.ZoneName, fileName)}");

                // Update the dictionary
                Routes[route.RouteId] = route;
            }
            catch (Exception ex)
            {
                PluginLog.Error($"Failed to save route {route.RouteId}: {ex.Message}");
                throw;
            }
        }

        /// <summary>
        /// Sanitize a string to be used as a folder name
        /// </summary>
        private string SanitizeFolderName(string name)
        {
            var invalid = Path.GetInvalidPathChars();
            return string.Join("_", name.Split(invalid, StringSplitOptions.RemoveEmptyEntries)).TrimEnd('.');
        }

        /// <summary>
        /// Sanitize a string to be used as a file name
        /// </summary>
        private string SanitizeFileName(string name)
        {
            var invalid = Path.GetInvalidFileNameChars();
            return string.Join("_", name.Split(invalid, StringSplitOptions.RemoveEmptyEntries)).TrimEnd('.');
        }

        /// <summary>
        /// Get a route by its ID
        /// </summary>
        public GatheringRoute? GetRoute(uint routeId)
        {
            return Routes.TryGetValue(routeId, out var route) ? route : null;
        }

        /// <summary>
        /// Get all routes for a specific territory
        /// </summary>
        public List<GatheringRoute> GetRoutesForTerritory(uint territoryId)
        {
            return Routes.Values.Where(r => r.TerritoryId == territoryId).ToList();
        }

        /// <summary>
        /// Get all routes for a specific gathering job
        /// </summary>
        public List<GatheringRoute> GetRoutesForJob(uint jobId)
        {
            return Routes.Values.Where(r => r.GatheringJobId == jobId).ToList();
        }

        /// <summary>
        /// Get all routes for a specific expansion
        /// </summary>
        public List<GatheringRoute> GetRoutesForExpansion(uint expansionId)
        {
            return Routes.Values.Where(r => r.ExpansionId == expansionId).ToList();
        }

        /// <summary>
        /// Auto-generate route JSON files from SheetInfo dictionary
        /// This creates baseline routes that can be manually refined later
        /// </summary>
        public void GenerateRoutesFromSheetInfo(Dictionary<uint, GatherPointInfo> sheetInfo)
        {
            PluginLog.Information($"Starting route generation from {sheetInfo.Count} sheet entries");

            int generatedCount = 0;
            int skippedCount = 0;

            foreach (var (routeId, info) in sheetInfo)
            {
                try
                {
                    // Check if route already exists (don't overwrite manual edits)
                    if (Routes.ContainsKey(routeId))
                    {
                        PluginLog.Verbose($"Route {routeId} already exists, skipping generation");
                        skippedCount++;
                        continue;
                    }

                    // Create the route from sheet info
                    var route = new GatheringRoute
                    {
                        RouteId = routeId,
                        ExpansionId = info.ExpId,
                        TerritoryId = info.TerritoryId,
                        ZoneName = info.ZoneName,
                        PlaceName = info.PlaceName,
                        GatheringJobId = info.Type, // Assuming Type is 16/17/18
                        LevelRequirement = (int)info.Level,
                        NodeIds = info.NodeIds?.ToList() ?? new List<uint>(),
                        RequiresFolklore = false, // Unknown at generation time
                        FolkloreBook = "",
                        TimedNode = false,
                        Author = "Ice",
                        LastUpdated = DateTime.Now.ToString("yyyy-MM-dd"),
                        NodeGroups = GenerateDefaultNodeGroups(info)
                    };

                    // Save the route (this will create the folder structure automatically)
                    SaveRoute(route, info.PlaceName);

                    generatedCount++;

                    if (generatedCount % 10 == 0)
                    {
                        PluginLog.Information($"Generated {generatedCount} routes so far...");
                    }
                }
                catch (Exception ex)
                {
                    PluginLog.Error($"Failed to generate route {routeId}: {ex.Message}");
                }
            }

            PluginLog.Information($"Route generation complete: {generatedCount} generated, {skippedCount} skipped");

            // Reload all routes to include the newly generated ones
            LoadAllRoutes();
        }

        /// <summary>
        /// Generate default node groups from sheet info
        /// Creates placeholder nodes that need to be manually populated with actual positions
        /// </summary>
        private List<NodeGroup> GenerateDefaultNodeGroups(GatherPointInfo info)
        {
            var nodeGroups = new List<NodeGroup>();

            if (info.NodeIds == null || info.NodeIds.Count == 0)
            {
                return nodeGroups;
            }

            // Create a single group with all nodes (they can be reorganized later)
            var defaultGroup = new NodeGroup
            {
                GroupId = 0,
                Nodes = new List<GatheringNode>()
            };

            foreach (var nodeId in info.NodeIds)
            {
                var node = new GatheringNode
                {
                    NodeId = nodeId,
                    Locations = new List<NodeLocation>
                    {
                        // Add a placeholder location - MUST be updated manually or via editor
                        new NodeLocation
                        {
                            Position = new Position
                            {
                                X = 0,
                                Y = 0,
                                Z = 0
                            },
                            FlightAngle_Min = 0f,
                            FlightAngle_Max = 0f,
                            FlightDistance_Min = 1.0f,
                            FlightDistance_Max = info.Radius > 0 ? info.Radius : 3.0f,
                            AllowFlying = true
                        }
                    }
                };

                defaultGroup.Nodes.Add(node);
            }

            nodeGroups.Add(defaultGroup);
            return nodeGroups;
        }

        public bool ContainsSpecificNode(GatheringRoute route, uint nodeId, Vector3 position)
        {
            foreach (var group in route.NodeGroups)
            {
                foreach (var node in group.Nodes)
                {
                    if (node.NodeId != nodeId)
                        continue;

                    foreach (var location in (node.Locations))
                    {
                        var existingPos = location.Position.ToVector3();
                        if (position == existingPos)
                            return true;
                    }
                }
            }

            return false;
        }

        public void AddNodeLocationIfMissing(GatheringRoute route, uint nodeId, Vector3 position, float minAngle = 0f, float maxAngle = 0f, float minDistance = 1.0f, float maxDistance = 3.0f, bool allowFlying = true)
        {
            if (ContainsSpecificNode(route, nodeId, position))
                return;

            if (!route.NodeIds.Contains(nodeId))
                return;

            // if somehow group 0 doesn't get created, making sure it exist now
            var group0 = route.NodeGroups.FirstOrDefault(g => g.GroupId == 0);
            if (group0 == null)
            {
                group0 = new NodeGroup { GroupId = 0 };
                route.NodeGroups.Insert(0, group0);
            }

            // Check if this nodeId already exists in group 0
            var existingNode = group0.Nodes.FirstOrDefault(n => n.NodeId == nodeId);

            var newLocation = new NodeLocation
            {
                Position = Position.FromVector3(position),
                FlightAngle_Min = minAngle,
                FlightAngle_Max = maxAngle,
                FlightDistance_Min = minDistance,
                FlightDistance_Max = maxDistance,
                AllowFlying = allowFlying,
                FlightFan_Height = 0.0f
            };

            if (existingNode != null)
            {
                // Node already exists in group 0, just add the new location to it
                existingNode.Locations.Add(newLocation);
            }
            else
            {
                // Node doesn't exist yet, create a new one
                var newNode = new GatheringNode
                {
                    NodeId = nodeId,
                    Locations = new List<NodeLocation> { newLocation }
                };
                group0.Nodes.Add(newNode);
            }
        }
    }
}