using Dalamud.Game.ClientState.Conditions;
using Dalamud.Game.ClientState.Objects.Enums;
using ECommons.GameHelpers;
using ECommons.Logging;
using ECommons.Throttlers;
using GatherChill.GatheringInfo;
using GatherChill.Utilities;
using System.Collections.Generic;
using static ECommons.UIHelpers.AddonMasterImplementations.AddonMaster;

namespace GatherChill.Scheduler.Tasks
{
    internal class Task_GatherRoute
    {
        private static GatheringRoute selectedRoute = null;
        private static GatheringRouteLoader _routeLoader = P.routeLoader;
        private static readonly Random _random = new Random();

        private static uint LoadedRouteId = 0;
        private static int RouteIndex = 0;
        private static List<GatheringNode> GatherRoute = new();
        private static List<int> NodeGroupIndices = new();
        private static bool NavigatingToNode = false;
        private static Vector3? TargetFanPoint = null;
        private static uint? TargetNodeId = null;

        public static void Enqueue(uint routeId, uint itemId)
        {
            if (GenericHelpers.TryGetAddonMaster<Gathering>("Gathering", out var gather) && gather.IsAddonReady)
            {
                P.taskManager.Enqueue(() => GatheringInteraction(itemId), "Gathering Interaction", TaskConfig);
            }
            else
            {
                P.taskManager.Enqueue(() => LoadRoute(routeId), "Load Route");
                P.taskManager.Enqueue(() => NavigateToNode(), "Navigation to node", TaskConfig);
            }
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
                    PluginLog.Debug($"We've found an invalid route. RouteId: {routeId}");
                }
            }
            return false;
        }

        private static bool? NavigateToNode()
        {
            if (GatherRoute.Count == 0)
            {
                PluginLog.Warning("No route is loaded, going to just immediately cancel here");
                return true;
            }

            var playerPos = Player.Position;
            const float loadRange = 75f;

            if (RouteIndex >= GatherRoute.Count)
            {
                // We've hit a higher index than we should for these, so going to just immediately reset it back to 0
                RouteIndex = 0;
            }

            var currentNode = GatherRoute[RouteIndex];
            TargetNodeId = currentNode.NodeId;

            bool allNodesInRange = currentNode.Locations.All(x => Player.DistanceTo(x.Position.ToVector3()) < loadRange);

            if (EzThrottler.Throttle("Node bool checker"))
            {
                PluginLog.Debug($"Checking node: {TargetNodeId} | Is in Range: {allNodesInRange}");
            }

            if (!allNodesInRange)
            {
                NavigatingToNode = false;
                TargetFanPoint = null;

                var firstNode = currentNode.Locations[0];
                if (!Task_NavmeshMove.Task_MoveToV2(firstNode.Position.ToVector3(), false, 10, true, true).Value)
                {
                    if (EzThrottler.Throttle("Throttle message"))
                    {
                        PluginLog.Verbose("To far from all nodes to check");
                    }
                }
                return false;
            }
            else if (!NavigatingToNode)
            {
                if (P.navmesh.IsRunning())
                {
                    if (EzThrottler.Throttle("Stopping Navmesh | In Range"))
                    {
                        PluginLog.Debug("Stopping Navmesh. Currently in range");
                        P.navmesh.Stop();
                    }
                    return false;
                }
            }

            var validNode = Svc.Objects.Where(obj => obj.BaseId == TargetNodeId)
                                       .Where(obj => obj.IsTargetable)
                                       .Where(obj => obj.ObjectKind == ObjectKind.GatheringPoint)
                                       .FirstOrDefault();

            if (EzThrottler.Throttle("IsNodeValid"))
            {
                PluginLog.Debug($"Is Node Valid: {validNode != null}");
            }

            if (validNode != null)
            {
                var targetLocation = currentNode.Locations.Where(x => x.Position.ToVector3() == validNode.Position).FirstOrDefault();
                if (targetLocation != null)
                {
                    if (TargetFanPoint == null)
                    {
                        NavigatingToNode = true;
                        TargetFanPoint = GetRandomPointInFan(targetLocation);
                    }

                    if (!Task_NavmeshMove.Task_MoveToV2(TargetFanPoint.Value, fly: true).Value)
                    {
                        if (EzThrottler.Throttle("Flying to node", 1000))
                        {
                            var distanceToNode = Player.DistanceTo(TargetFanPoint.Value);
                            PluginLog.Verbose($"Current navmesh moving. Distance: {distanceToNode} | Target: {TargetNodeId}");
                        }
                    }
                    else
                    {
                        PluginLog.Debug($"We're at the destination: {TargetNodeId}");
                        P.taskManager.Insert(() => InteractWithNode(TargetNodeId.Value), "Interacting with gathering node");
                        NavigatingToNode = false;
                        TargetFanPoint = null;
                        return true;
                    }
                }
                else
                {
                    NavigatingToNode = false;
                    TargetFanPoint = null;
                    TargetNodeId = null; // Add this
                    RouteIndex += 1;
                    return false;
                }
            }
            else
            {

                NavigatingToNode = false;
                TargetFanPoint = null;
                TargetNodeId = null; // Add this
                RouteIndex += 1;
                if (EzThrottler.Throttle("Else statement"))
                {
                    PluginLog.Debug($"Gather route count: {GatherRoute.Count}");
                    PluginLog.Debug("We... should be moving to the next node");
                    PluginLog.Debug($"Route Index: {RouteIndex}");
                }
            }


            return false;
        }
        private static bool? InteractWithNode(uint nodeId)
        {
            var targetNode = Svc.Objects.Where(x => x.BaseId == nodeId)
                                        .Where(x => x.IsTargetable)
                                        .FirstOrDefault();
            if (Svc.Condition[ConditionFlag.Gathering] && GenericHelpers.TryGetAddonMaster<Gathering>("Gathering", out var gather) && gather.IsAddonReady || GenericHelpers.TryGetAddonMaster<GatheringMasterpiece>("GatheringMasterpiece", out var collectable) && collectable.IsAddonReady)
            {
                PluginLog.Information($"Gathering window is now visible, continuing onto GatheringInteraction Task");
                return true;
            }
            else if (targetNode != null)
            {
                if (!Player.IsJumping)
                {
                    if (EzThrottler.Throttle("Target + Interaction throttle"))
                    {
                        Utils.TargetgameObject(targetNode);
                        Utils.InteractWithObject(targetNode);
                    }
                }
            }
            else
            {
                PluginLog.Debug("Somehow we've gotten here, and we shouldn't be here. Adding 1 to the counter and returning");
                RouteIndex += 1;
                return true;
            }


            return false;
        }
        private static bool? GatheringInteraction(uint itemId)
        {
            if (P.navmesh.IsRunning())
            {
                if (EzThrottler.Throttle("Stopping navmesh, cause we shouldn't be running"))
                    P.navmesh.Stop();
            }

            if (!Svc.Condition[ConditionFlag.ExecutingGatheringAction])
            {
                if (GenericHelpers.TryGetAddonMaster<Gathering>("Gathering", out var gather) && gather.IsAddonReady)
                {
                    if (gather.CurrentIntegrity != 0)
                    {
                        if (EzThrottler.Throttle($"Gathering Item: {itemId}"))
                            gather.GatheredItems.Where(x => x.ItemID == itemId).FirstOrDefault().Gather();

                        return false;
                    }
                }
                else
                    return true;
            }

            return false;
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
