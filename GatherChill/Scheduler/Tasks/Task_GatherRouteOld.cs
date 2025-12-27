using ECommons.GameHelpers;
using ECommons.Logging;
using ECommons.Throttlers;
using GatherChill.GatheringInfo;
using System;
using System.Collections.Generic;
using System.Text;
using static FFXIVClientStructs.FFXIV.Client.UI.AddonRelicNoteBook;

namespace GatherChill.Scheduler.Tasks
{
    internal class Task_GatherRouteOld
    {
        private static GatheringRoute selectedRoute = null;
        private static GatheringRouteLoader _routeLoader = P.routeLoader;
        private static readonly Random _random = new Random();


        private static uint LoadedRouteId = 0;
        private static int RouteIndex = 0;
        private static List<GatheringNode> GatherRoute = new();
        private static List<int> NodeGroupIndices = new(); // Parallel list tracking which group each node belongs to
        private static bool NavigatingToNode = false; // Track if we're navigating to a specific node
        private static Vector3? TargetFanPoint = null; // Save the calculated fan point
        private static uint? TargetNodeId = null; // Track which node we calculated the fan point for

        public static void Enqueue(uint routeId, uint itemId)
        {
            P.taskManager.Enqueue(() => LoadRoute(routeId));
            P.taskManager.Enqueue(() => NavigateToNode());
        }

        private static bool? LoadRoute(uint routeId)
        {
            var loadedRoute = _routeLoader.GetRoute(routeId);

            if (loadedRoute != null)
            {
                selectedRoute = loadedRoute;

                if (LoadedRouteId != routeId)
                {
                    LoadedRouteId = routeId;
                    RouteIndex = 0;
                    GatherRoute.Clear();
                    NodeGroupIndices.Clear();

                    foreach (var group in selectedRoute.NodeGroups)
                    {
                        foreach (var node in group.Nodes)
                        {
                            GatherRoute.Add(node);
                            NodeGroupIndices.Add(group.GroupId); // Track which group this node belongs to
                        }
                    }
                }

                return true;
            }
            else
            {
                if (EzThrottler.Throttle("Throttling log message", 1000))
                {
                    ECommons.Logging.PluginLog.Debug($"We've found an invalid route. RouteId: {routeId}");
                }
            }
            return false;
        }

        public static bool? NavigateToNode()
        {
            if (GatherRoute.Count == 0)
            {
                PluginLog.Warning("No route loaded!");
                return true;
            }

            var playerPos = Player.Position;

            const float loadRange = 100f;

            // Get current node
            if (RouteIndex >= GatherRoute.Count)
            {
                RouteIndex = 0; // Loop back to start
            }

            var currentNode = GatherRoute[(int)RouteIndex];
            var currentGroupId = NodeGroupIndices[(int)RouteIndex];

            // Get all nodes in the same group
            var nodesInCurrentGroup = GatherRoute
                .Where((node, index) => NodeGroupIndices[index] == currentGroupId)
                .ToList();

            // Check if ANY node in current group is within load range
            bool anyNodeInRange = nodesInCurrentGroup.Any(node =>
                                  node.Locations.Any(loc => Player.DistanceTo(loc.Position.ToVector3()) <= loadRange));

            if (!anyNodeInRange)
            {
                // Reset navigation state when out of range
                NavigatingToNode = false;
                TargetFanPoint = null;
                TargetNodeId = null;

                // Path to first node in current group (which is currentNode)
                var closestLocation = GetClosestLocation(currentNode, playerPos);
                if (!P.navmesh.IsRunning())
                {
                    Task_NavmeshMove.Task_NavTo(closestLocation.Position.ToVector3(), false, 75, true, true);
                }
                PluginLog.Debug($"No nodes in range, pathing to node {currentNode.NodeId} in group {currentGroupId}");
                return false;
            }
            else if (!NavigatingToNode)
            {
                // We just entered load range - stop navmesh once
                if (P.navmesh.IsRunning())
                {
                    if (EzThrottler.Throttle("Stopping navmesh - in range"))
                    {
                        P.navmesh.Stop();
                        PluginLog.Debug("Stopped navigation - nodes are now in load range");
                    }
                    return false;
                }
            }

            // We're in range - check for targetable nodes in current group
            GatheringNode? targetableNode = null;

            foreach (var node in nodesInCurrentGroup)
            {
                var gameObject = Svc.Objects.FirstOrDefault(obj =>
                                 obj.BaseId == node.NodeId && obj.IsTargetable);

                if (gameObject != null)
                {
                    targetableNode = node;
                    break;
                }
            }

            if (targetableNode != null)
            {
                // Calculate fan point only once PER NODE
                if (TargetFanPoint == null || TargetNodeId != targetableNode.NodeId)
                {
                    var closestLocation = GetClosestLocation(targetableNode, playerPos);
                    TargetFanPoint = GetRandomPointInFan(closestLocation);
                    TargetNodeId = targetableNode.NodeId;
                    NavigatingToNode = true;
                }

                if (!Task_NavmeshMove.Task_NavTo(TargetFanPoint.Value, stayMounted: false, fly: true).Value)
                {
                    PluginLog.Debug($"Found targetable node: {targetableNode.NodeId} in group {currentGroupId}");
                    return false;
                }
                else
                {
                    PluginLog.Information($"We're at: {targetableNode.NodeId}");
                    NavigatingToNode = false;
                    TargetFanPoint = null;
                    TargetNodeId = null; // Reset for next node
                    return true;
                }
            }
            else
            {
                // Nodes in range but none targetable - move to next group
                PluginLog.Debug($"Nodes in group {currentGroupId} in range but none targetable");
                NavigatingToNode = false;
                TargetFanPoint = null;
                TargetNodeId = null; // Add this
                MoveToNextGroup();
                return false;
            }
        }

        private static void MoveToNextGroup()
        {
            var currentGroupId = NodeGroupIndices[(int)RouteIndex];

            // Find the index of the first node in the next group
            for (int i = (int)RouteIndex + 1; i < GatherRoute.Count; i++)
            {
                if (NodeGroupIndices[i] != currentGroupId)
                {
                    RouteIndex = i;
                    return;
                }
            }

            // If we didn't find another group, loop back to start
            RouteIndex = 0;
        }

        private static NodeLocation GetClosestLocation(GatheringNode node, Vector3 playerPos)
        {
            return node.Locations
                .OrderBy(loc => Vector3.Distance(playerPos, loc.Position.ToVector3()))
                .First();
        }

        private static Vector3 GetRandomPointInFan(NodeLocation nodeLoc)
        {
            // Get random distance between min and max
            float distance = (float)(_random.NextDouble() * (nodeLoc.MaxDistance - nodeLoc.MinDistance) + nodeLoc.MinDistance);

            // Get random angle between min and max (in degrees)
            float angleDegrees = (float)(_random.NextDouble() * (nodeLoc.MaxAngle - nodeLoc.MinAngle) + nodeLoc.MinAngle);

            // Convert to radians
            float angleRadians = angleDegrees * MathF.PI / 180f;

            float offsetX = distance * -MathF.Sin(angleRadians);
            float offsetZ = distance * MathF.Cos(angleRadians);

            return new Vector3(
                nodeLoc.Position.X + offsetX,
                nodeLoc.Position.Y + nodeLoc.FanHeightIncrease,
                nodeLoc.Position.Z + offsetZ
            );
        }
    }
}
