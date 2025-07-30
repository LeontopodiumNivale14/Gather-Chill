using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GatherChill.Utilities;

internal static class GatheringHelper
{
    public class RouteEntry
    {
        public uint RouteNumber { get; set; }
        public string Expansion { get; set; }     // ExName
        public string Area { get; set; }          // TerritoryName
        public uint AreaId { get; set; }
        public HashSet<uint> ListNodeIds { get; set; } = new();
        public List<GathNodeInfo> GatherPoints { get; set; } = new();
    }

    public class GatheringConfig
    {
        public int GatheringAmount { get; set; } = 0;
        public uint ItemId { get; set; } = 0;
        public string ItemName { get; set; } = string.Empty;
    }

    public class GathNodeInfo
    {
        public Vector3 Position { get; set; }
        public Vector3 LandZone { get; set; }
        public uint NodeId { get; set; }
        public int GatheringType { get; set; }
        public int ZoneId { get; set; }
        public uint NodeSet { get; set; }

        public bool UseRadialPositioning { get; set; } = false;
        public float InnerRadius { get; set; } = 0.0f;
        public float OuterRadius { get; set; } = 5.0f;
        public float StartAngle { get; set; } = 0.0f;
        public float EndAngle { get; set; } = 360.0f;
    }

    public static class RadialPositioning
    {
        private static readonly Random _random = new Random();

        // Conversion functions
        public static float DegreesToRadians(float degrees)
        {
            return degrees * (float)(Math.PI / 180.0);
        }

        public static float RadiansToDegrees(float radians)
        {
            return radians * (float)(180.0 / Math.PI);
        }

        // Get start angle in radians for Pictomancy
        public static float GetStartAngleRadians(float startAngleDegrees)
        {
            return DegreesToRadians(startAngleDegrees);
        }

        // Get end angle in radians for Pictomancy
        public static float GetEndAngleRadians(float endAngleDegrees)
        {
            return DegreesToRadians(endAngleDegrees);
        }

        // Get the fan width in radians (useful for Pictomancy)
        public static float GetFanWidthRadians(float startAngleDegrees, float endAngleDegrees)
        {
            float startRad = DegreesToRadians(startAngleDegrees);
            float endRad = DegreesToRadians(endAngleDegrees);

            // Handle wrapping (e.g., 350° to 10°)
            if (endAngleDegrees < startAngleDegrees)
            {
                endRad += 2 * (float)Math.PI;
            }

            return endRad - startRad;
        }

        public static Vector3 GetRandomPointInFan(Vector3 centerPoint, float innerRadius, float outerRadius, float startAngle, float endAngle)
        {
            // Convert angles to radians
            float startRad = startAngle * (float)(Math.PI / 180);
            float endRad = endAngle * (float)(Math.PI / 180);

            // Handle angle wrapping (e.g., 350° to 10°)
            if (endAngle < startAngle)
            {
                endRad += 2 * (float)Math.PI;
            }

            // Random angle within the fan
            float randomAngle = (float)_random.NextDouble() * (endRad - startRad) + startRad;

            // Random radius (weighted toward outer edge for even distribution)
            float radiusSquared = (float)_random.NextDouble() * (outerRadius * outerRadius - innerRadius * innerRadius) + innerRadius * innerRadius;
            float randomRadius = (float)Math.Sqrt(radiusSquared);

            // Calculate offset from center
            float offsetX = randomRadius * (float)Math.Cos(randomAngle);
            float offsetZ = randomRadius * (float)Math.Sin(randomAngle);

            return new Vector3(
                centerPoint.X + offsetX,
                centerPoint.Y, // Keep Y the same (ground level)
                centerPoint.Z + offsetZ
            );
        }
    }
}
