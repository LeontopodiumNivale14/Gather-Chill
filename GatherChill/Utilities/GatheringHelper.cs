using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GatherChill.Utilities;

internal static class GatheringHelper
{
    public class GatheringConfig
    {
        public int GatheringAmount { get; set; } = 0;
        public uint ItemId { get; set; } = 0;
        public string ItemName { get; set; } = string.Empty;
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

        public static Vector3 GetRandomPointInFan(Vector3 center, float innerRadius, float outerRadius,
                                                  float startAngle, float endAngle, float rotationOffset = 0.0f)
        {
            // Apply rotation offset to both angles
            float rotatedStartAngle = startAngle + rotationOffset;
            float rotatedEndAngle = endAngle + rotationOffset;

            // Normalize angles to 0-360 range
            rotatedStartAngle = NormalizeAngle(rotatedStartAngle);
            rotatedEndAngle = NormalizeAngle(rotatedEndAngle);

            Random rand = new Random();

            // Random angle between start and end (handling wrap-around)
            float randomAngle;
            if (rotatedEndAngle < rotatedStartAngle) // Wrap around case
            {
                float totalRange = (360 - rotatedStartAngle) + rotatedEndAngle;
                float randomValue = (float)rand.NextDouble() * totalRange;

                if (randomValue <= (360 - rotatedStartAngle))
                {
                    randomAngle = rotatedStartAngle + randomValue;
                }
                else
                {
                    randomAngle = randomValue - (360 - rotatedStartAngle);
                }
            }
            else
            {
                randomAngle = rotatedStartAngle + (float)rand.NextDouble() * (rotatedEndAngle - rotatedStartAngle);
            }

            // Random radius
            float randomRadius = innerRadius + (float)rand.NextDouble() * (outerRadius - innerRadius);

            // Convert to world coordinates
            float radians = DegreesToRadians(randomAngle);
            return new Vector3(
                center.X + randomRadius * (float)Math.Cos(radians),
                center.Y,
                center.Z + randomRadius * (float)Math.Sin(radians)
            );
        }

        public static float NormalizeAngle(float angle)
        {
            while (angle < 0) angle += 360;
            while (angle >= 360) angle -= 360;
            return angle;
        }

        public static float GetRotatedStartAngleRadians(float startAngle, float rotationOffset)
        {
            return DegreesToRadians(NormalizeAngle(startAngle + rotationOffset));
        }

        public static float GetRotatedEndAngleRadians(float endAngle, float rotationOffset)
        {
            return DegreesToRadians(NormalizeAngle(endAngle + rotationOffset));
        }
    }
}
