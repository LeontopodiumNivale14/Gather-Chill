using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace GatherChill.GatheringInfo
{
    /// <summary>
    /// Contains all the info for the gathering route
    /// </summary>
    public class GatheringRoute
    {
        [JsonPropertyName("routeId")]
        public uint RouteId { get; set; }

        [JsonPropertyName("territoryId")]
        public uint TerritoryId { get; set; }

        [JsonPropertyName("zoneName")]
        public string ZoneName { get; set; }

        [JsonPropertyName("placeName")]
        public string PlaceName { get; set; }

        [JsonPropertyName("expansionId")]
        public uint ExpansionId { get; set; } // 0 = ARR, 1 = HW, 2 = Stb, 3 = ShB, 4 = EW, 5 = DT

        [JsonPropertyName("gatheringJobId")]
        public uint GatheringJobId { get; set; } // 16 = MIN, 17 = BTN, 18 = FSH

        [JsonPropertyName("levelRequirement")]
        public int LevelRequirement { get; set; }

        [JsonPropertyName("nodeIds")]
        public List<uint> NodeIds { get; set; } = new();

        [JsonPropertyName("requiresFolklore")]
        public bool RequiresFolklore { get; set; }

        [JsonPropertyName("folkloreBook")]
        public string FolkloreBook { get; set; }

        [JsonPropertyName("author")]
        public string Author { get; set; }

        [JsonPropertyName("lastUpdated")]
        public string LastUpdated { get; set; }

        [JsonPropertyName("nodeGroups")]
        public List<NodeGroup> NodeGroups { get; set; } = new();

        [JsonPropertyName("timedNode")]
        public bool TimedNode { get; set; } = false;
    }

    /// <summary>
    /// A list/group of the nodes that are close together
    /// ARR can sometimes be one group, while anything post HW are usually 3+
    /// </summary>
    public class NodeGroup
    {
        [JsonPropertyName("groupId")]
        public int GroupId { get; set; }

        [JsonPropertyName("nodes")]
        public List<GatheringNode> Nodes { get; set; } = new();
    }

    /// <summary>
    /// The node info itself, contains the nodeId + all the locations it's in
    /// </summary>
    public class GatheringNode
    {
        [JsonPropertyName("nodeId")]
        public uint NodeId { get; set; }

        [JsonPropertyName("locations")]
        public List<NodeLocation> Locations { get; set; } = new();
    }

    /// <summary>
    /// Specific info about the node, position, landing angles... etc
    /// </summary>
    public class NodeLocation
    {
        // Node Position itself
        [JsonPropertyName("position")]
        public Position Position { get; set; }

        // Flight Fan Stuff
        [JsonPropertyName("minAngle")]
        public float FlightAngle_Min { get; set; } = 0.0f;

        [JsonPropertyName("maxAngle")]
        public float FlightAngle_Max { get; set; } = 0.0f;

        [JsonPropertyName("minDistance")]
        public float FlightDistance_Min { get; set; } = 3.0f;

        [JsonPropertyName("maxDistance")]
        public float FlightDistance_Max { get; set; } = 5.0f;

        [JsonPropertyName("fanHightIncrease")]
        public float FlightFan_Height { get; set; } = 0.0f;

        [JsonPropertyName("allowFlying")]
        public bool AllowFlying { get; set; } = true;

        // Bool to choose whether we're doing fan position finder, 
        [JsonPropertyName("specificWalkSpots")]
        public bool UseSpecificWalkingSpots { get; set; } = false;

        // if true, use this
        [JsonPropertyName("walkablePositions")]
        public List<Position> WalkablePositions { get; set; } = new();

        // if false, using ring 
        [JsonPropertyName("gatherAngle_Minimum")]
        public float GatherAngle_Min { get; set; } = 0.0f;
        [JsonPropertyName("gatherAngle_Maximum")]
        public float GatherAngle_Max { get; set; } = 0.0f;
        [JsonPropertyName("gatherDistance_Minimum")]
        public float GatherDist_Min { get; set; } = 1.0f;
        [JsonPropertyName("gatherDistance_Maximum")]
        public float GatherDist_Max { get; set; } = 2.4f;
        [JsonPropertyName("gatherFan_Height")]
        public float GatherFan_Height { get; set; } = 0.0f;

    }

    /// <summary>
    /// Actual stored info on the position (just in .json format)
    /// </summary>
    public class Position
    {
        [JsonPropertyName("x")]
        public float X { get; set; }

        [JsonPropertyName("y")]
        public float Y { get; set; }

        [JsonPropertyName("z")]
        public float Z { get; set; }

        public Vector3 ToVector3() => new Vector3(X, Y, Z);

        public static Position FromVector3(Vector3 vector) => new Position
        {
            X = vector.X,
            Y = vector.Y,
            Z = vector.Z
        };
    }
}