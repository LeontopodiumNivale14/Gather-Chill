using GatherChill.GatheringInfo;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GatherChill.Utilities.GatheringHelpers
{
    public static class NodeAngleNormalizer
    {
        /// <summary>
        /// Normalizes an angle to 0-360 range
        /// </summary>
        public static float NormalizeTo360(float angle)
        {
            angle = angle % 360f;
            if (angle < 0)
                angle += 360f;
            return angle;
        }

        /// <summary>
        /// Normalizes all angles in a node location to 0-360 range
        /// </summary>
        public static void NormalizeNodeLocation(NodeLocation location)
        {
            // Normalize flight angles
            location.FlightAngle_Min = NormalizeTo360(location.FlightAngle_Min);
            location.FlightAngle_Max = NormalizeTo360(location.FlightAngle_Max);

            // Normalize gather angles
            location.GatherAngle_Min = NormalizeTo360(location.GatherAngle_Min);
            location.GatherAngle_Max = NormalizeTo360(location.GatherAngle_Max);
        }

        /// <summary>
        /// Normalizes all angles in a gathering node
        /// </summary>
        public static void NormalizeGatheringNode(GatheringNode node)
        {
            foreach (var location in node.Locations)
            {
                NormalizeNodeLocation(location);
            }
        }

        /// <summary>
        /// Normalizes all angles in a node group
        /// </summary>
        public static void NormalizeNodeGroup(NodeGroup group)
        {
            foreach (var node in group.Nodes)
            {
                NormalizeGatheringNode(node);
            }
        }

        /// <summary>
        /// Normalizes all angles in a gathering route
        /// </summary>
        public static void NormalizeGatheringRoute(GatheringRoute route)
        {
            foreach (var group in route.NodeGroups)
            {
                NormalizeNodeGroup(group);
            }
        }

        /// <summary>
        /// Normalizes all angles in a list of routes and returns a summary
        /// </summary>
        public static NormalizationResult NormalizeAllRoutes(List<GatheringRoute> routes)
        {
            var result = new NormalizationResult();

            foreach (var route in routes)
            {
                int locationsInRoute = 0;
                int normalizedInRoute = 0;

                foreach (var group in route.NodeGroups)
                {
                    foreach (var node in group.Nodes)
                    {
                        foreach (var location in node.Locations)
                        {
                            locationsInRoute++;

                            bool wasNormalized = false;

                            // Check and normalize flight angles
                            if (location.FlightAngle_Min < 0 || location.FlightAngle_Min >= 360)
                            {
                                location.FlightAngle_Min = NormalizeTo360(location.FlightAngle_Min);
                                wasNormalized = true;
                            }
                            if (location.FlightAngle_Max < 0 || location.FlightAngle_Max > 360)
                            {
                                location.FlightAngle_Max = NormalizeTo360(location.FlightAngle_Max);
                                wasNormalized = true;
                            }

                            // Check and normalize gather angles
                            if (location.GatherAngle_Min < 0 || location.GatherAngle_Min >= 360)
                            {
                                location.GatherAngle_Min = NormalizeTo360(location.GatherAngle_Min);
                                wasNormalized = true;
                            }
                            if (location.GatherAngle_Max < 0 || location.GatherAngle_Max > 360)
                            {
                                location.GatherAngle_Max = NormalizeTo360(location.GatherAngle_Max);
                                wasNormalized = true;
                            }

                            if (wasNormalized)
                            {
                                normalizedInRoute++;
                            }
                        }
                    }
                }

                result.RoutesProcessed++;
                result.TotalLocations += locationsInRoute;
                result.NormalizedLocations += normalizedInRoute;

                if (normalizedInRoute > 0)
                {
                    result.RoutesModified++;
                }
            }

            return result;
        }
    }

    public class NormalizationResult
    {
        public int RoutesProcessed { get; set; }
        public int RoutesModified { get; set; }
        public int TotalLocations { get; set; }
        public int NormalizedLocations { get; set; }

        public override string ToString()
        {
            return $"Processed {RoutesProcessed} routes, modified {RoutesModified} routes, " +
                   $"normalized {NormalizedLocations}/{TotalLocations} locations";
        }
    }
}