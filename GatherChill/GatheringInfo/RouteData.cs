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
        /// <summary>
        /// Route/KeyId 
        /// </summary>
        public uint RouteId { get; set; }
        /// <summary>
        /// TerritoryID associated with the route
        /// </summary>
        public uint TerritoryId { get; set; }
        /// <summary>
        /// Zone/Territory Name. Stored in english but, just so i know what this gathering route belongs to
        /// </summary>
        public string ZoneName { get; set; }
        /// <summary>
        /// Location of the gathering zone/internal name
        /// </summary>
        public string PlaceName { get; set; }
        /// <summary>
        /// Expansion the route belongs to<br></br>
        /// This is to make sure that it just gets sorted into the right folder<br></br>
        /// 0 = ARR, 1 = HW, 2 = Stb, 3 = ShB, 4 = EW, 5 = DT
        /// </summary>
        public uint ExpansionId { get; set; }
        /// <summary>
        /// JobIds tied to that gathering point
        /// 16 = MIN, 17 = BTN, 18 = FSH
        /// </summary>
        public uint GatheringJobId { get; set; }
        /// <summary>
        /// Lv. Requirement to interact/see the node
        /// </summary>
        public int LevelRequirement { get; set; }
        /// <summary>
        /// List of all the nodeIds that are contains in this route
        /// </summary>
        public List<uint> NodeIds { get; set; } = new();
        /// <summary>
        /// Node Groups/Node Position info. This contains the exact info of all the different nodes
        /// </summary>
        public List<NodeGroup> NodeGroups { get; set; } = new();
        public bool RequiresFolklore { get; set; }
        public string FolkloreBook { get; set; }
        public string Author { get; set; }
        public string LastUpdated { get; set; }
        public bool TimedNode { get; set; } = false;
    }

    /// <summary>
    /// A list/group of the nodes that are close together
    /// ARR can sometimes be one group, while anything post HW are usually 3+
    /// </summary>
    public class NodeGroup
    {
        public int GroupId { get; set; }
        public List<GatheringNode> Nodes { get; set; } = new();
    }

    /// <summary>
    /// The node info itself, contains the nodeId + all the locations it's in
    /// </summary>
    public class GatheringNode
    {
        public uint NodeId { get; set; }
        public List<NodeLocation> Locations { get; set; } = new();
    }

    /// <summary>
    /// Specific info about the node, position, landing angles... etc
    /// </summary>
    public class NodeLocation
    {
        public Vector3 Position { get; set; }
        public bool AllowFlying { get; set; } = true;
        public FanInfo Flight_FanInfo { get; set; } = new();
        public FanInfo Gathering_FanInfo { get; set; } = new();
        // Bool to choose whether we're doing fan position finder
        public bool UseSpecificWalkingSpots { get; set; } = false;
        public List<Vector3> WalkablePositions { get; set; } = new();

    }

    public class FanInfo
    {
        public float Fan_StartAngle { get; set; } = 0.0f;
        public float Fan_EndAngle { get; set; } = 0.0f;
        public float Fan_DistanceMin { get; set; } = 1.0f;
        public float Fan_DistanceMax { get; set; } = 3.0f;
        public float Fan_Height { get; set; } = 0.0f;
    }
}