using ECommons.Logging;
using GatherChill.GatheringInfo;
using System;
using System.Collections.Generic;
using System.Text;

namespace GatherChill.Utilities.GatheringHelpers
{
    public static class NodeLocationExtensions
    {
        private static readonly Random _random = new Random();

        /// <summary>
        /// Calculates a random landing position within a section of the flight fan range
        /// that is nearest to the player's direction
        /// </summary>
        public static Vector3 GetRandomFlightPosition(this NodeLocation nodeLocation, Vector3 playerPosition, float sectionSize = 15f)
        {
            Vector3 nodePos = nodeLocation.Position.ToVector3();

            // Calculate angle to player (returns -180 to 180)
            float angleToPlayer = CalculateAngleToPlayer(nodePos, playerPosition);

            PluginLog.Debug($"[Flight] Node at {nodePos}, Player at {playerPosition}");
            PluginLog.Debug($"[Flight] Angle to player: {angleToPlayer:F2}°");
            PluginLog.Debug($"[Flight] Allowed range: {nodeLocation.FlightAngle_Min:F2}° to {nodeLocation.FlightAngle_Max:F2}°");

            // Get the section range nearest to the player
            var (sectionMin, sectionMax) = GetNearestSection(
                nodeLocation.FlightAngle_Min,
                nodeLocation.FlightAngle_Max,
                angleToPlayer,
                sectionSize
            );

            PluginLog.Debug($"[Flight] Selected section: {sectionMin:F2}° to {sectionMax:F2}°");

            // Select a random angle within this section
            float selectedAngle = RandomAngleInRange(sectionMin, sectionMax);

            PluginLog.Debug($"[Flight] Random angle selected: {selectedAngle:F2}°");

            // Select a random distance within the allowed range
            float selectedDistance = _random.NextFloat(
                nodeLocation.FlightDistance_Min,
                nodeLocation.FlightDistance_Max
            );

            // Calculate the position
            Vector3 result = CalculateFanPosition(
                nodePos,
                selectedAngle,
                selectedDistance,
                nodeLocation.FlightFan_Height
            );

            PluginLog.Debug($"[Flight] Final position: {result}");

            return result;
        }

        /// <summary>
        /// Calculates a random gathering position within a section of the gather fan range
        /// that is nearest to the player's direction
        /// </summary>
        public static Vector3 GetRandomGatherPosition(this NodeLocation nodeLocation, Vector3 playerPosition, float sectionSize = 15f)
        {
            // If using specific walking spots, pick the nearest one
            if (nodeLocation.UseSpecificWalkingSpots && nodeLocation.WalkablePositions.Count > 0)
            {
                return GetNearestWalkablePosition(nodeLocation.WalkablePositions, playerPosition);
            }

            Vector3 nodePos = nodeLocation.Position.ToVector3();

            // Calculate angle to player (returns -180 to 180)
            float angleToPlayer = CalculateAngleToPlayer(nodePos, playerPosition);

            PluginLog.Debug($"[Gather] Node at {nodePos}, Player at {playerPosition}");
            PluginLog.Debug($"[Gather] Angle to player: {angleToPlayer:F2}°");
            PluginLog.Debug($"[Gather] Allowed range: {nodeLocation.GatherAngle_Min:F2}° to {nodeLocation.GatherAngle_Max:F2}°");

            // Get the section range nearest to the player
            var (sectionMin, sectionMax) = GetNearestSection(
                nodeLocation.GatherAngle_Min,
                nodeLocation.GatherAngle_Max,
                angleToPlayer,
                sectionSize
            );

            PluginLog.Debug($"[Gather] Selected section: {sectionMin:F2}° to {sectionMax:F2}°");

            // Select a random angle within this section
            float selectedAngle = RandomAngleInRange(sectionMin, sectionMax);

            PluginLog.Debug($"[Gather] Random angle selected: {selectedAngle:F2}°");

            // Select a random distance within the allowed range
            float selectedDistance = _random.NextFloat(
                nodeLocation.GatherDist_Min,
                nodeLocation.GatherDist_Max
            );

            // Calculate the position
            Vector3 result = CalculateFanPosition(
                nodePos,
                selectedAngle,
                selectedDistance,
                nodeLocation.GatherFan_Height
            );

            PluginLog.Debug($"[Gather] Final position: {result}");

            return result;
        }

        /// <summary>
        /// Gets both flight and gather positions in one call
        /// </summary>
        public static (Vector3 flightPos, Vector3 gatherPos) GetRandomPositions(
            this NodeLocation nodeLocation,
            Vector3 playerPosition,
            float sectionSize = 15f)
        {
            return (
                nodeLocation.GetRandomFlightPosition(playerPosition, sectionSize),
                nodeLocation.GetRandomGatherPosition(playerPosition, sectionSize)
            );
        }

        #region Helper Methods

        /// <summary>
        /// Calculates the angle from node to player (in degrees, -180 to 180)
        /// North = 0°, East = 90°, South = ±180°, West = -90°
        /// </summary>
        private static float CalculateAngleToPlayer(Vector3 nodePos, Vector3 playerPos)
        {
            Vector3 direction = playerPos - nodePos;
            float angle = MathF.Atan2(direction.X, direction.Z) * (180f / MathF.PI);

            // Returns -180 to 180
            return angle;
        }

        /// <summary>
        /// Normalizes any angle to -180 to 180 range
        /// </summary>
        private static float NormalizeAngle(float angle)
        {
            // Reduce to -360 to 360 range first
            angle = angle % 360f;

            // Then map to -180 to 180
            if (angle > 180f)
                angle -= 360f;
            else if (angle < -180f)
                angle += 360f;

            return angle;
        }

        /// <summary>
        /// Calculates the range span, handling the special case of full 360° ranges
        /// </summary>
        private static float GetRangeSpan(float min, float max)
        {
            // Check if this is a full circle (0 to 360, -180 to 180, etc.)
            float diff = MathF.Abs(max - min);
            if (MathF.Abs(diff - 360f) < 0.01f)
            {
                return 360f;
            }

            // Normalize and calculate span
            min = NormalizeAngle(min);
            max = NormalizeAngle(max);

            float span = max - min;
            if (span < 0)
                span += 360f;

            return span;
        }

        /// <summary>
        /// Gets the section of the allowed fan range that is nearest to the target angle
        /// </summary>
        private static (float sectionMin, float sectionMax) GetNearestSection(
            float allowedMin,
            float allowedMax,
            float targetAngle,
            float sectionSize)
        {
            // Check if the range is a full 360° circle BEFORE normalizing
            float rangeSpan = GetRangeSpan(allowedMin, allowedMax);

            PluginLog.Debug($"  Range span: {rangeSpan:F2}°");

            // If it's a full circle, just create a section around the target
            if (rangeSpan >= 359.9f)
            {
                float halfSection = sectionSize / 2f;
                float sectionMin = targetAngle - halfSection;
                float sectionMax = targetAngle + halfSection;

                PluginLog.Debug($"  Full circle detected, using target-centered section: {sectionMin:F2}° to {sectionMax:F2}°");

                return (sectionMin, sectionMax);
            }

            // Normalize all angles to -180 to 180 for consistent comparison
            allowedMin = NormalizeAngle(allowedMin);
            allowedMax = NormalizeAngle(allowedMax);
            targetAngle = NormalizeAngle(targetAngle);

            PluginLog.Debug($"  Normalized - Min: {allowedMin:F2}°, Max: {allowedMax:F2}°, Target: {targetAngle:F2}°");

            // First, find the point within the allowed range that's closest to the target
            float closestPointInRange;

            if (IsAngleInRange(targetAngle, allowedMin, allowedMax))
            {
                // Target is already in range, use it directly
                closestPointInRange = targetAngle;
                PluginLog.Debug($"  Target is within range, using {closestPointInRange:F2}°");
            }
            else
            {
                // Target is outside range, find which edge is closer
                float distToMin = GetAngularDistance(targetAngle, allowedMin);
                float distToMax = GetAngularDistance(targetAngle, allowedMax);

                PluginLog.Debug($"  Target outside range - Dist to min: {distToMin:F2}°, Dist to max: {distToMax:F2}°");

                closestPointInRange = distToMin < distToMax ? allowedMin : allowedMax;
                PluginLog.Debug($"  Using closest edge: {closestPointInRange:F2}°");
            }

            // Now create a section around the closest point
            float halfSectionSize = sectionSize / 2f;
            float finalSectionMin = closestPointInRange - halfSectionSize;
            float finalSectionMax = closestPointInRange + halfSectionSize;

            PluginLog.Debug($"  Initial section: {finalSectionMin:F2}° to {finalSectionMax:F2}°");

            // Clamp the section to the allowed range
            finalSectionMin = ClampAngleToRange(finalSectionMin, allowedMin, allowedMax, true);
            finalSectionMax = ClampAngleToRange(finalSectionMax, allowedMin, allowedMax, false);

            PluginLog.Debug($"  Clamped section: {finalSectionMin:F2}° to {finalSectionMax:F2}°");

            return (finalSectionMin, finalSectionMax);
        }

        /// <summary>
        /// Clamps an angle to stay within the allowed range
        /// </summary>
        private static float ClampAngleToRange(float angle, float allowedMin, float allowedMax, bool preferMin)
        {
            angle = NormalizeAngle(angle);

            if (IsAngleInRange(angle, allowedMin, allowedMax))
            {
                return angle;
            }

            // Angle is outside range, clamp to nearest edge
            float distToMin = GetAngularDistance(angle, allowedMin);
            float distToMax = GetAngularDistance(angle, allowedMax);

            // If distances are equal, use preference
            if (MathF.Abs(distToMin - distToMax) < 0.01f)
            {
                return preferMin ? allowedMin : allowedMax;
            }

            return distToMin < distToMax ? allowedMin : allowedMax;
        }

        /// <summary>
        /// Calculates the shortest angular distance between two angles
        /// </summary>
        private static float GetAngularDistance(float angle1, float angle2)
        {
            // Normalize both angles
            angle1 = NormalizeAngle(angle1);
            angle2 = NormalizeAngle(angle2);

            float diff = angle2 - angle1;

            // Normalize to -180 to 180
            while (diff > 180f) diff -= 360f;
            while (diff < -180f) diff += 360f;

            return MathF.Abs(diff);
        }

        /// <summary>
        /// Checks if an angle is within a given range
        /// </summary>
        private static bool IsAngleInRange(float angle, float min, float max)
        {
            // Normalize all angles to -180 to 180
            angle = NormalizeAngle(angle);
            min = NormalizeAngle(min);
            max = NormalizeAngle(max);

            // Calculate the span of the range
            float rangeSpan = max - min;
            if (rangeSpan < 0)
                rangeSpan += 360f;

            // If range is >= 360, everything is in range
            if (rangeSpan >= 360f)
                return true;

            // Check if angle is in range
            if (min <= max)
            {
                // Normal range (doesn't wrap)
                return angle >= min && angle <= max;
            }
            else
            {
                // Range wraps around -180/180 boundary
                return angle >= min || angle <= max;
            }
        }

        /// <summary>
        /// Generates a random angle within the specified range
        /// </summary>
        private static float RandomAngleInRange(float min, float max)
        {
            min = NormalizeAngle(min);
            max = NormalizeAngle(max);

            if (min <= max)
            {
                // Simple case: range doesn't wrap
                return _random.NextFloat(min, max);
            }
            else
            {
                // Range wraps around -180/180 boundary
                float rangeSize = (180f - min) + (180f + max);
                float randomValue = _random.NextFloat(0, rangeSize);

                float result = min + randomValue;

                // Normalize back to -180 to 180
                return NormalizeAngle(result);
            }
        }

        /// <summary>
        /// Calculates a 3D position based on angle, distance, and height offset
        /// </summary>
        private static Vector3 CalculateFanPosition(Vector3 center, float angleDegrees, float distance, float heightOffset)
        {
            float angleRadians = angleDegrees * (MathF.PI / 180f);

            return new Vector3(
                center.X + distance * MathF.Sin(angleRadians),
                center.Y + heightOffset,
                center.Z + distance * MathF.Cos(angleRadians)
            );
        }

        /// <summary>
        /// Gets the nearest walkable position to the player
        /// </summary>
        private static Vector3 GetNearestWalkablePosition(List<Position> walkablePositions, Vector3 playerPosition)
        {
            Vector3 nearest = walkablePositions[0].ToVector3();
            float minDistSq = Vector3.DistanceSquared(nearest, playerPosition);

            for (int i = 1; i < walkablePositions.Count; i++)
            {
                Vector3 pos = walkablePositions[i].ToVector3();
                float distSq = Vector3.DistanceSquared(pos, playerPosition);

                if (distSq < minDistSq)
                {
                    minDistSq = distSq;
                    nearest = pos;
                }
            }

            return nearest;
        }

        /// <summary>
        /// Extension method to get a random float between min and max
        /// </summary>
        private static float NextFloat(this Random random, float min, float max)
        {
            return min + (float)random.NextDouble() * (max - min);
        }

        #endregion
    }
}