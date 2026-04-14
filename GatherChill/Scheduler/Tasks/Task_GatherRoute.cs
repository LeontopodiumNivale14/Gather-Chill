using Dalamud.Game.ClientState.Conditions;
using Dalamud.Game.ClientState.Objects.Enums;
using Dalamud.Game.ClientState.Objects.Types;
using ECommons.GameHelpers;
using ECommons.Logging;
using ECommons.Throttlers;
using FFXIVClientStructs.FFXIV.Client.Game;
using GatherChill.GatheringInfo;
using GatherChill.Utilities.GatheringHelpers;
using GatherChill.Utilities.Tools;
using GatherChill.Utilities.Utility;
using System.Collections.Generic;
using static ECommons.UIHelpers.AddonMasterImplementations.AddonMaster;

namespace GatherChill.Scheduler.Tasks
{
    internal class Task_GatherRoute
    {
        private static GatheringRoute selectedRoute = null;
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
                P.taskManager.Enqueue(() => TravelFarCheck(), "Traveling to node group", TaskConfig);
            }
        }



        private static bool TravelFarCheck()
        {
            var route = P.routeEditor.GetRoute(SchedulerMain.RouteId.Value);
            if (route != null && selectedRoute != route)
            {
                IceLogging.Verbose("No route was loaded/old route did not match. Updating to current");
                selectedRoute = route;
                GatherRoute.Clear();
                foreach (var group in route.NodeInfo)
                {
                    GatherRoute.Add(group);
                }
            }

            if (GatherRoute.Count == 0)
            {
                IceLogging.Warning("No route is loaded, going to just immediately cancel here");
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
                IceLogging.Verbose("Currently in travel check mode");

            // bool allNodesInRange = currentNode.Locations.All(x => Player.DistanceTo(x.Position.ToVector3()) <= loadRange);
            if (Player.DistanceTo(currentNode.Locations[0].Position) > loadRange)
            {
                TargetFanPoint = null;

                var firstNode = currentNode.Locations[0];
                var randomFanPoint = NodeLocationExtensions.GetRandomFlightPosition(firstNode, Player.Position);
                if (!Task_NavmeshMove.Task_FlyTo(randomFanPoint, false, 50, true).Value)
                {
                    if (EzThrottler.Throttle("Throttle message"))
                    {
                        IceLogging.Verbose($"To far from all nodes to check. Distance: {Player.DistanceTo(randomFanPoint)}");
                    }
                }
                return false;
            }
            else
            {
                IceLogging.Debug("We're within range of all nodes, continuing on");
                P.navmesh.PathStop();

                var validNode = Svc.Objects.Where(obj => obj.BaseId == TargetNodeId)
                           .Where(obj => obj.IsTargetable)
                           .Where(obj => obj.ObjectKind == ObjectKind.GatheringPoint)
                           .FirstOrDefault();

                if (EzThrottler.Throttle("IsNodeValid"))
                {
                    IceLogging.Debug($"Is Node Valid: {validNode != null}");
                }

                if (validNode == null)
                {
                    NodeCheckIndex = 0;
                    P.taskManager.Enqueue(() => IndividualNodeCheck(), "Checking individual nodes");
                    return true;
                }
                else
                {
                    IceLogging.Debug("we're within range, checking travel kind now");
                    P.taskManager.Enqueue(() => CheckTravelKind(validNode), "Checking Travel Kind");
                    return true;
                }
            }
        }
        private static bool IndividualNodeCheck()
        {
            var currentNode = GatherRoute[RouteIndex];
            TargetNodeId = currentNode.NodeId;

            // if we're doing this, that means that all nodes aren't within a close 75 yalms of each other. So going to check each individually
            if (NodeCheckIndex < currentNode.Locations.Count)
            {
                IceLogging.Verbose($"Checking location: {NodeCheckIndex}");
                var location = currentNode.Locations[NodeCheckIndex];
                var distanceToLoc = Player.DistanceTo(location.Position);
                if (EzThrottler.Throttle("Location message throttle"))
                    IceLogging.Debug($"Distance to location: {distanceToLoc:N2}");

                if (distanceToLoc > 75)
                {
                    var randomFanPoint = NodeLocationExtensions.GetRandomFlightPosition(location, Player.Position);
                    if (!Task_NavmeshMove.Task_FlyTo(randomFanPoint, false, 50, true).Value)
                    {
                        if (EzThrottler.Throttle("Throttle message"))
                        {
                            IceLogging.Verbose("Too far from node to check");
                        }
                    }
                    return false; // Still need to reach this location
                }
                else
                {
                    if (P.navmesh.IsRunning())
                        P.navmesh.PathStop();

                    // If we're here, that means that we're within load range. 
                    var validNode = Svc.Objects.Where(obj => obj.BaseId == TargetNodeId)
                                               .Where(obj => obj.IsTargetable)
                                               .Where(obj => obj.ObjectKind == ObjectKind.GatheringPoint)
                                               .FirstOrDefault();

                    if (validNode != null)
                    {
                        IceLogging.Debug("We've found a valid node! Time to pathfind/interact with it");
                        NodeCheckIndex = 0; // Reset for next time
                        P.taskManager.Enqueue(() => CheckTravelKind(validNode), "Checking Travel Kind");
                        return true;
                    }
                    else
                    {
                        IceLogging.Debug($"No valid node at location {NodeCheckIndex}, moving to next location");
                        NodeCheckIndex += 1;
                        return false;
                    }
                }
            }
            else
            {
                // We've checked all locations and found no valid nodes
                IceLogging.Debug("Checked all locations for this node group, moving to next route index");
                TargetFanPoint = null;
                TargetNodeId = null;
                RouteIndex += 1;
                NodeCheckIndex = 0;
                if (EzThrottler.Throttle("Else statement"))
                {
                    IceLogging.Debug($"Gather route count: {GatherRoute.Count}");
                    IceLogging.Debug("Moving to the next node");
                    IceLogging.Debug($"Route Index: {RouteIndex}");
                }
                return true;
            }
        }
        private static bool CheckTravelKind(IGameObject node)
        {
            float minFlyDistance = 25;

            var currentNode = GatherRoute[RouteIndex];
            var targetLocation = currentNode.Locations.Where(x => x.Position == node.Position).FirstOrDefault();
            if (targetLocation == null)
            {
                IceLogging.Error("We're getting an invalid node location");
                return true;
            }

            TargetFanPoint = NodeLocationExtensions.GetRandomFlightPosition(targetLocation, Player.Position);

            var closestWalkPoint = Vector3.Zero;
            if (targetLocation.UseSpecificWalkingSpots)
            {
                var walkPointSpecific = targetLocation.WalkablePositions.OrderBy(x => Vector3.Distance(TargetFanPoint.Value, x)).FirstOrDefault();
                closestWalkPoint = walkPointSpecific;
            }
            else
            {
                closestWalkPoint = NodeLocationExtensions.GetRandomGatherPosition(targetLocation, Player.Position);
            }

            if (Player.DistanceTo(node.Position) >= minFlyDistance && !Svc.Condition[ConditionFlag.Diving])
            {
                IceLogging.Debug("We're moving onto the next set via flying");

                P.taskManager.EnqueueMulti
                (
                    new(() => Task_NavmeshMove.Task_FlyTo(TargetFanPoint.Value, true, 0.5f, true), "True Fly Task", TaskConfig),
                    new(() => Task_NavmeshMove.Task_GroundTo(closestWalkPoint, true, 0.5f), "Moving to the node", TaskConfig),
                    new(() => InteractWithNode(node.BaseId), "Interact with node")
                );
            }
            else
            {
                IceLogging.Debug("We're moving onto the next set via ground movement");
                P.taskManager.EnqueueMulti
                (
                    new(() => Task_NavmeshMove.Task_GroundTo(closestWalkPoint, true, 0.5f), "Moving to the node", TaskConfig),
                    new(() => InteractWithNode(node.BaseId), "Interact with node")
                );
            }

            return true;
        }
        private static bool InteractWithNode(uint nodeId)
        {
            var targetNode = Svc.Objects.Where(x => x.BaseId == nodeId)
                                        .Where(x => x.IsTargetable)
                                        .FirstOrDefault();
            if (Svc.Condition[ConditionFlag.Gathering] && GenericHelpers.TryGetAddonMaster<Gathering>("Gathering", out var gather) && gather.IsAddonReady || GenericHelpers.TryGetAddonMaster<GatheringMasterpiece>("GatheringMasterpiece", out var collectable) && collectable.IsAddonReady)
            {
                IceLogging.Info($"Gathering window is now visible, continuing onto GatheringInteraction Task");
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
                IceLogging.Debug("Somehow we've gotten here, and we shouldn't be here. Adding 1 to the counter and returning");
                RouteIndex += 1;
                return true;
            }


            return false;
        }
        private static bool GatheringInteraction(uint itemId)
        {
            if (P.navmesh.IsRunning())
            {
                if (EzThrottler.Throttle("Stopping navmesh, cause we shouldn't be running"))
                    P.navmesh.PathStop();
            }

            if (Svc.Condition[ConditionFlag.Gathering])
            {
                if (!Svc.Condition[ConditionFlag.ExecutingGatheringAction])
                {
                    if (GenericHelpers.TryGetAddonMaster<Gathering>("Gathering", out var gather) && gather.IsAddonReady)
                    {
                        if (gather.CurrentIntegrity != 0)
                        {
                            if (Basic_BuffCheck())
                                return false;

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
        private static unsafe bool Basic_BuffCheck()
        {
            var actionInfo = Gather_Util.GathActionDict[Enums.GatherBuffId.BYII];
            bool hasBuff = Utils.HasStatusId(actionInfo.StatusId) || Utils.HasStatusId(actionInfo.StatusId2);
            bool hasGp = Utils.GetGp() >= actionInfo.RequiredGp;
            var actionId = actionInfo.ClassAction[Player.Job];

            if (!hasBuff && hasGp)
            {
                ActionManager.Instance()->UseAction(ActionType.Action, actionId);
                return true;
            }
            else
                return false;
        }
    }
}
