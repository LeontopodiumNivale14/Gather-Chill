using Dalamud.Game.ClientState.Conditions;
using Dalamud.Game.ClientState.Objects.Enums;
using Dalamud.Game.ClientState.Objects.Types;
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
        private static Vector3? TargetFanPoint = null;
        private static uint? TargetNodeId = null;
        private static int NodeCheckIndex = 0;

        public static void Enqueue(uint routeId, uint itemId)
        {
            if (GenericHelpers.TryGetAddonMaster<Gathering>("Gathering", out var gather) && gather.IsAddonReady)
            {
                P.taskManager.Enqueue(() => GatheringInteraction(itemId), "Gathering Interaction", TaskConfig);
            }
            else
            {
                P.taskManager.Enqueue(() => LoadRoute(routeId), "Load Route");
                P.taskManager.Enqueue(() => TravelFarCheck(), "Traveling to node group", TaskConfig);
            }
        }

        // TODO: Need to just change this entirely
        // I was trying to be smart and tehe lets make it nice and smooth
        // but tbh, it's just causing more issues that needs resolving in the end *-sighs-*
        // Sometimes, the simplier path isn't the best one in the end. And this is good case of it.
        // So: Need to re-factor to be more like moon
        // AKA: Move all the checks into their own seperate thing
        // Then: Once within range, check to see how we should get to that destination once again -> move accordingly. 
        // Probably turn this into it's own task in itself
        // If none are within range, just go ahead and move onto the next
        // This also means refactoring navmesh movement for flying and ground movement as a whole into their own things for sanity check

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

                    foreach (var group in selectedRoute.NodeGroups)
                    {
                        foreach (var node in group.Nodes)
                        {
                            GatherRoute.Add(node);
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
        private static bool? TravelFarCheck()
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

            if (EzThrottler.Throttle("Travel Check throttle message"))
                PluginLog.Verbose("Currently in travel check mode");

            // bool allNodesInRange = currentNode.Locations.All(x => Player.DistanceTo(x.Position.ToVector3()) <= loadRange);
            if (Player.DistanceTo(currentNode.Locations[0].Position.ToVector3()) > loadRange)
            {
                TargetFanPoint = null;

                var firstNode = currentNode.Locations[0];
                var randomFanPoint = GetRandomPointInFan(firstNode);
                if (!Task_NavmeshMove.Task_FlyTo(randomFanPoint, false, 50, true).Value)
                {
                    if (EzThrottler.Throttle("Throttle message"))
                    {
                        PluginLog.Verbose($"To far from all nodes to check. Distance: {Player.DistanceTo(randomFanPoint)}");
                    }
                }
                return false;
            }
            else
            {
                PluginLog.Debug("We're within range of all nodes, continuing on");
                P.navmesh.Stop();

                var validNode = Svc.Objects.Where(obj => obj.BaseId == TargetNodeId)
                           .Where(obj => obj.IsTargetable)
                           .Where(obj => obj.ObjectKind == ObjectKind.GatheringPoint)
                           .FirstOrDefault();

                if (EzThrottler.Throttle("IsNodeValid"))
                {
                    PluginLog.Debug($"Is Node Valid: {validNode != null}");
                }

                if (validNode == null)
                {
                    NodeCheckIndex = 0;
                    P.taskManager.Enqueue(() => IndividualNodeCheck(), "Checking individual nodes");
                    return true;
                }
                else
                {
                    PluginLog.Debug("we're within range, checking travel kind now");
                    P.taskManager.Enqueue(() => CheckTravelKind(validNode));
                    return true;
                }
            }
        }
        private static bool? IndividualNodeCheck()
        {
            var currentNode = GatherRoute[RouteIndex];
            TargetNodeId = currentNode.NodeId;

            // if we're doing this, that means that all nodes aren't within a close 75 yalms of each other. So going to check each individually
            if (NodeCheckIndex < currentNode.Locations.Count)
            {
                PluginLog.Verbose($"Checking location: {NodeCheckIndex}");
                var location = currentNode.Locations[NodeCheckIndex];
                var distanceToLoc = Player.DistanceTo(location.Position.ToVector3());
                if (EzThrottler.Throttle("Location message throttle"))
                    PluginLog.Debug($"Distance to location: {distanceToLoc:N2}");

                if (distanceToLoc > 75)
                {
                    var randomFanPoint = GetRandomPointInFan(location);
                    if (!Task_NavmeshMove.Task_FlyTo(randomFanPoint, false, 50, true).Value)
                    {
                        if (EzThrottler.Throttle("Throttle message"))
                        {
                            PluginLog.Verbose("Too far from node to check");
                        }
                    }
                    return false; // Still need to reach this location
                }
                else
                {
                    if (P.navmesh.IsRunning())
                        P.navmesh.Stop();

                    // If we're here, that means that we're within load range. 
                    var validNode = Svc.Objects.Where(obj => obj.BaseId == TargetNodeId)
                                               .Where(obj => obj.IsTargetable)
                                               .Where(obj => obj.ObjectKind == ObjectKind.GatheringPoint)
                                               .FirstOrDefault();

                    if (validNode != null)
                    {
                        PluginLog.Debug("We've found a valid node! Time to pathfind/interact with it");
                        NodeCheckIndex = 0; // Reset for next time
                        P.taskManager.Enqueue(() => CheckTravelKind(validNode), "Checking Travel Kind");
                        return true;
                    }
                    else
                    {
                        PluginLog.Debug($"No valid node at location {NodeCheckIndex}, moving to next location");
                        NodeCheckIndex += 1;
                        return false;
                    }
                }
            }
            else
            {
                // We've checked all locations and found no valid nodes
                PluginLog.Debug("Checked all locations for this node group, moving to next route index");
                TargetFanPoint = null;
                TargetNodeId = null;
                RouteIndex += 1;
                NodeCheckIndex = 0;
                if (EzThrottler.Throttle("Else statement"))
                {
                    PluginLog.Debug($"Gather route count: {GatherRoute.Count}");
                    PluginLog.Debug("Moving to the next node");
                    PluginLog.Debug($"Route Index: {RouteIndex}");
                }
                return true;
            }
        }
        private static bool? CheckTravelKind(IGameObject node)
        {
            float minFlyDistance = 25;

            var currentNode = GatherRoute[RouteIndex];
            var targetLocation = currentNode.Locations.Where(x => x.Position.ToVector3() == node.Position).FirstOrDefault();
            if (targetLocation == null)
            {
                PluginLog.Error("We're getting an invalid node location");
                return true;
            }

            TargetFanPoint = GetRandomPointInFan(targetLocation);

            if (Player.DistanceTo(node.Position) >= minFlyDistance && !Svc.Condition[ConditionFlag.Diving])
            {
                PluginLog.Debug("We're moving onto the next set via flying");
                P.taskManager.EnqueueMulti
                (
                    new(() => Task_NavmeshMove.Task_FlyTo(TargetFanPoint.Value, true, 0.5f, false), "True Fly Task", TaskConfig),
                    new(() => InteractWithNode(node.BaseId), "Interact with node")
                );
            }
            else
            {
                PluginLog.Debug("We're moving onto the next set via ground movement");
                P.taskManager.EnqueueMulti
                (
                    new(() => Task_NavmeshMove.Task_GroundTo(TargetFanPoint.Value, true, 0.5f, false), "Ground movement", TaskConfig),
                    new(() => InteractWithNode(node.BaseId), "Interact with node")
                );
            }

            return true;
        }

        private static bool? InteractWithNode(uint nodeId)
        {
            var targetNode = Svc.Objects.Where(x => x.BaseId == nodeId)
                                        .Where(x => x.IsTargetable)
                                        .FirstOrDefault();
            if (Svc.Condition[ConditionFlag.Gathering] && GenericHelpers.TryGetAddonMaster<Gathering>("Gathering", out var gather) && gather.IsAddonReady || GenericHelpers.TryGetAddonMaster<GatheringMasterpiece>("GatheringMasterpiece", out var collectable) && collectable.IsAddonReady)
            {
                PluginLog.Information($"Gathering window is now visible, continuing onto GatheringInteraction Task");
                RouteIndex += 1;
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

            if (Svc.Condition[ConditionFlag.Gathering])
            {
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
                }
            }
            else
                return true;

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
