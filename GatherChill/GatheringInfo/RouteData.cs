using ECommons.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace GatherChill.GatheringInfo;

public abstract class RouteInfo
{
    public abstract uint ExpansionId { get; }
    public abstract uint ZoneId { get; }
    public abstract Vector2 MapPosition { get; }
    public abstract int Radius { get; }
    public abstract uint Id { get; }
    public abstract HashSet<uint> ItemIds { get; }
    public abstract HashSet<uint> NodeIds { get; }
    public abstract List<NodeInfo> Nodes { get; }
    public abstract uint GatherType { get; }
}

public class NodeInfo
{
    public uint NodeId { get; set; }
    public Vector3 NodePosition { get; set; }
    public LandingInfo LandingInfo { get; set; }
}

public class LandingInfo
{
    public Vector3 LandZone { get; set; }
    public bool UseRadial { get; set; } = true;
    public float InnerRadius { get; set; } = 1.0f;
    public float OuterRadius { get; set; } = 3.0f;
    public float StartAngle { get; set; } = 0.0f;
    public float EndAngle { get; set; } = 360.0f;
    public float RotationOffset { get; set; } = 0.0f;
}

// In RouteData.cs
public class RouteData
{
    /// <summary>
    /// Key - ExpansionId
    /// Value - Dictionary<ZoneId, Dictionary<RouteId, RouteInfo>>
    /// </summary>
    public Dictionary<uint, Dictionary<uint, Dictionary<uint, RouteInfo>>> GatheringInfo = new();

    /// <summary>
    /// Loads all route classes that inherit from RouteInfo using reflection
    /// </summary>
    public void LoadAllRoutes()
    {
        // Get the current assembly
        var assembly = Assembly.GetExecutingAssembly();

        // Find all types that inherit from RouteInfo and aren't abstract
        var routeTypes = assembly.GetTypes()
            .Where(t => t.IsSubclassOf(typeof(RouteInfo)) && !t.IsAbstract);

        foreach (var routeType in routeTypes)
        {
            try
            {
                // Create an instance of the route class
                var route = (RouteInfo)Activator.CreateInstance(routeType);

                // Get expansion, zone, and route IDs
                uint expansionId = route.ExpansionId;
                uint zoneId = route.ZoneId;
                uint routeId = route.Id;

                // Ensure the expansion dictionary exists
                if (!GatheringInfo.ContainsKey(expansionId))
                {
                    GatheringInfo[expansionId] = new Dictionary<uint, Dictionary<uint, RouteInfo>>();
                }

                // Ensure the zone dictionary exists
                if (!GatheringInfo[expansionId].ContainsKey(zoneId))
                {
                    GatheringInfo[expansionId][zoneId] = new Dictionary<uint, RouteInfo>();
                }

                // Add the route using its ID as the key
                GatheringInfo[expansionId][zoneId][routeId] = route;

                PluginLog.Information($"Loaded route: {routeType.Name} (Expansion: {expansionId}, Zone: {zoneId}, Route: {routeId})");
            }
            catch (Exception ex)
            {
                PluginLog.Error($"Failed to load route {routeType.Name}: {ex.Message}");
            }
        }

        PluginLog.Information($"Loaded {routeTypes.Count()} routes across {GatheringInfo.Count} expansions");
    }
}
