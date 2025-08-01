using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GatherChill.Utilities;

public static class RouteHelpers
{
    public class GathNodeInfo
    {
        public Vector3 Position { get; set; }
        public Vector3 LandZone { get; set; }
        public uint NodeId { get; set; }
        public int GatheringType { get; set; }
        public int ZoneId { get; set; }
        public uint NodeSet { get; set; }

        public bool UseRadialPositioning { get; set; } = false;
        public float RotationOffset { get; set; } = 0.0f; // Degrees to rotate the entire fan
        public float InnerRadius { get; set; } = 1.0f;
        public float OuterRadius { get; set; } = 3.0f;
        public float StartAngle { get; set; } = 0.0f;
        public float EndAngle { get; set; } = 360.0f;
    }

    public class RouteEntry
    {
        public uint RouteNumber { get; set; }
        public string Expansion { get; set; }     // ExName
        public string Area { get; set; }          // TerritoryName
        public uint AreaId { get; set; }
        public HashSet<uint> ListNodeIds { get; set; } = new();
        public List<GathNodeInfo> GatherPoints { get; set; } = new();
    }
}
