using ECommons.GameHelpers;
using GatherChill.GatheringInfo;
using System;
using System.Collections.Generic;
using System.Text;

namespace GatherChill.Utilities.Traveling;

public static partial class TravelUtil
{
    private static readonly Random _random = new();

    public static float CalculateAngleToPlayer(Vector3 nodePos, Vector3 playerPos)
    {
        Vector3 direction = playerPos - nodePos;
        float angle = MathF.Atan2(direction.X, direction.Z) * (180f / MathF.PI);
        angle = 180f - angle;

        if (angle < 0f)
            angle += 360f;
        else if (angle >= 360f)
            angle -= 360f;

        return angle;
    }
    private static float NormalizeAngle(float angle)
    {
        angle = angle % 360f;
        if (angle < 0f)
            angle += 360f;
        return angle;
    }
    private static float GetRangeSpan(float min, float max)
    {
        float diff = MathF.Abs(max - min);
        if (MathF.Abs(diff - 360f) < 0.01f)
            return 360f;

        min = NormalizeAngle(min);
        max = NormalizeAngle(max);

        float span = max - min;
        if (span < 0)
            span += 360f;

        return span;
    }
    private static bool IsAngleInRange(float angle, float min, float max)
    {
        angle = NormalizeAngle(angle);
        min = NormalizeAngle(min);
        max = NormalizeAngle(max);

        float rangeSpan = max - min;
        if (rangeSpan < 0)
            rangeSpan += 360f;

        if (rangeSpan >= 360f)
            return true;

        if (min <= max)
            return angle >= min && angle <= max;
        else
            return angle >= min || angle <= max;
    }
    private static float GetAngularDistance(float angle1, float angle2)
    {
        angle1 = NormalizeAngle(angle1);
        angle2 = NormalizeAngle(angle2);

        float diff = angle2 - angle1;
        while (diff > 180f) diff -= 360f;
        while (diff < -180f) diff += 360f;

        return MathF.Abs(diff);
    }
    private static float ClampAngleToRange(float angle, float allowedMin, float allowedMax, bool preferMin)
    {
        angle = NormalizeAngle(angle);

        if (IsAngleInRange(angle, allowedMin, allowedMax))
            return angle;

        float distToMin = GetAngularDistance(angle, allowedMin);
        float distToMax = GetAngularDistance(angle, allowedMax);

        if (MathF.Abs(distToMin - distToMax) < 0.01f)
            return preferMin ? allowedMin : allowedMax;

        return distToMin < distToMax ? allowedMin : allowedMax;
    }
    private static (float sectionMin, float sectionMax) GetNearestSection(float allowedMin, float allowedMax, float targetAngle, float sectionSize)
    {
        float rangeSpan = GetRangeSpan(allowedMin, allowedMax);

        if (rangeSpan >= 359.9f)
        {
            float halfSection = sectionSize / 2f;
            return (NormalizeAngle(targetAngle - halfSection), NormalizeAngle(targetAngle + halfSection));
        }

        allowedMin = NormalizeAngle(allowedMin);
        allowedMax = NormalizeAngle(allowedMax);
        targetAngle = NormalizeAngle(targetAngle);

        if (IsAngleInRange(targetAngle, allowedMin, allowedMax))
        {
            // Target is inside fan — center section on it as before
            float half = sectionSize / 2f;
            float secMin = ClampAngleToRange(NormalizeAngle(targetAngle - half), allowedMin, allowedMax, true);
            float secMax = ClampAngleToRange(NormalizeAngle(targetAngle + half), allowedMin, allowedMax, false);
            return (secMin, secMax);
        }
        else
        {
            // Target is outside fan — find nearest edge and carve inward
            bool nearMax = GetAngularDistance(targetAngle, allowedMax) < GetAngularDistance(targetAngle, allowedMin);

            if (nearMax)
            {
                // Nearest edge is allowedMax, carve inward toward allowedMin
                float secMin = ClampAngleToRange(NormalizeAngle(allowedMax - sectionSize), allowedMin, allowedMax, true);
                return (secMin, allowedMax);
            }
            else
            {
                // Nearest edge is allowedMin, carve inward toward allowedMax
                float secMax = ClampAngleToRange(NormalizeAngle(allowedMin + sectionSize), allowedMin, allowedMax, false);
                return (allowedMin, secMax);
            }
        }
    }
    private static float RandomAngleInRange(float min, float max)
    {
        min = NormalizeAngle(min);
        max = NormalizeAngle(max);

        if (min <= max)
            return NextFloat(min, max);

        float rangeSize = (360f - min) + max;
        return NormalizeAngle(min + NextFloat(0, rangeSize));
    }
    private static float NextFloat(float min, float max)
    {
        return min + (float)_random.NextDouble() * (max - min);
    }
    private static Vector3 CalculateFanPosition(Vector3 center, float angleDegrees, float distance, float height)
    {
        float standardAngle = 180f - angleDegrees;
        float angleRadians = standardAngle * (MathF.PI / 180f);

        return new Vector3(
            center.X + distance * MathF.Sin(angleRadians),
            center.Y + height,   // now actually applies the fan's height offset
            center.Z + distance * MathF.Cos(angleRadians)
        );
    }
    public static Vector3 Gather_RandomFanPosition(NodeLocation nodeInfo, bool flight = false)
    {
        var fanMode = flight ? nodeInfo.Flight_FanInfo : nodeInfo.Gathering_FanInfo;

        float node_MinAngle = fanMode.Fan_StartAngle;
        float node_MaxAngle = fanMode.Fan_EndAngle;


        float rangeSpan = GetRangeSpan(node_MinAngle, node_MaxAngle);
        float sectionSize = C.GatherFanSectionSize;

        float selectedAngle;
        if (rangeSpan >= 359.9f)
        {
            // Full fan — pure random, no bias
            selectedAngle = RandomAngleInRange(node_MinAngle, node_MaxAngle);
        }
        else
        {
            // Partial fan — find the section closest to where the player is approaching from
            float angleToPlayer = CalculateAngleToPlayer(nodeInfo.Position, Player.Position);
            var (sectionMin, sectionMax) = GetNearestSection(node_MinAngle, node_MaxAngle, angleToPlayer, sectionSize);
            selectedAngle = RandomAngleInRange(sectionMin, sectionMax);
        }

        float selectedDistance = NextFloat(fanMode.Fan_DistanceMin, fanMode.Fan_DistanceMax);
        return CalculateFanPosition(nodeInfo.Position, selectedAngle, selectedDistance, fanMode.Fan_Height);
    }
}
