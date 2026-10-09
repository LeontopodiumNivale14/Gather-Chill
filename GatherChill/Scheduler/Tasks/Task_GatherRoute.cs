using Dalamud.Game.ClientState.Conditions;
using Dalamud.Game.ClientState.Objects.Enums;
using Dalamud.Game.ClientState.Objects.Types;
using ECommons.ExcelServices;
using ECommons.GameHelpers;
using ECommons.Throttlers;
using FFXIVClientStructs.FFXIV.Client.Game;
using GatherChill.Enums;
using GatherChill.GatheringInfo;
using GatherChill.Utilities.GatheringHelpers;
using GatherChill.Utilities.Tools;
using GatherChill.Utilities.Traveling;
using GatherChill.Utilities.Utility;
using System.Collections.Generic;
using static ECommons.UIHelpers.AddonMasterImplementations.AddonMaster;

namespace GatherChill.Scheduler.Tasks
{
    internal class Task_GatherRoute
    {

        private static GatheringRoute selectedRoute = null;
        private static int RouteIndex = 0;
        private static List<GatheringNode> GatherRoute = new();
        private static uint? TargetNodeId = null;
        private static int NodeCheckIndex = 0;

        public class ItemCatch
        {
            public uint ItemId { get; set; } = 0;
            public int LastAmount { get; set; } = 0;
        }

        private static ItemCatch LastItemInfo = new();

        public static void NormalItem_Enqueue()
        {
            if (GenericHelpers.TryGetAddonMaster<Gathering>("Gathering", out var gather) && gather.IsAddonReady)
            {
                var itemId = Gather_Helper.GatherRoute.itemId;
                P.TaskManager.Enqueue(() => GatheringInteraction(itemId), "Gathering Interaction");
            }
            else
            {
                P.TaskManager.Enqueue(() => ChangeJob(), "Checking for job change");
                P.TaskManager.Enqueue(() => Travel_FarCheck(), "Traveling to node group");
            }
        }

        private static bool ChangeJob()
        {
            string tag = "Task Gather: Change Job";

            var routeId = Gather_Helper.GatherRoute.routeId;
            var job = Gather_Util.Sheet_RouteInfo[routeId].Job;
            if (Player.Job == (Job)job)
            {
                return true;
            }
            else
            {
                if (EzThrottler.Throttle("Swapping jobs", 2000))
                {
                    IceLogging.Verbose("We need to swap jobs to even be able to do this, so that's what we're going to do. *-I hope you have one unlocked-*", tag);
                    Utils.TaskClassChange((Job)job);
                }
            }

            return false;
        }

        private static bool Travel_FarCheck()
        {
            const string tag = "Task: Gather Travel";

            // I want to keep the old way of telling if a node is good for a certain distance, then have it move to the next one if not...
            // just need to incorporate the navmesh task properly. The one way I did it is *-minorly-* jank? Idk I don't like it
            // So I think the general gameplan is to:
            // 1: Do a far check on the nodes (stay mounted = true)
            // 2: Once we're within range of all nodes, find node then travel to it
            // 3: If we're already flying, do a fly -> ground fan location
            // 3.1: If we're not flying, check to see if we should fly, then queue up fly -> ground or just ground
            // 4: Once we're within range, queue up the node gathering task in the normal task

            // 1st: Check the route to make sure it's even loaded right... if not then we need to clear it and start fresh

            var navtask = P.navTask;
            if (!navtask.IsBusy)
            {
                var playerPos = Player.Position;
                const float loadRange = 75f;

                // Checking to see if we even have a route to begin with
                var route = P.routeEditor.GetRoute(Gather_Helper.GatherRoute.routeId);
                if (route != null && selectedRoute != route)
                {
                    IceLogging.Verbose("No route was loaded/old route did not match. Updating to current", tag);
                    selectedRoute = route;

                    GatherRoute.Clear();
                    GatherRoute.AddRange(route.NodeInfo.OrderBy(n => n.GroupId));

                    var order = string.Join(", ", GatherRoute.Select(n => $"{n.NodeId} (G{n.GroupId})"));
                    IceLogging.Verbose($"GatherRoute order: {order}", tag);
                }

                if (GatherRoute.Count == 0)
                {
                    IceLogging.Warning("No nodes were in this route is loaded, which means that it doesn't know where tf to go. Please report the route/item", tag);
                    return true;
                }

                if (RouteIndex >= GatherRoute.Count)
                {
                    // We've hit a higher index than we should for these, so going to just immediately reset it back to 0
                    RouteIndex = 0;
                }

                var currentNode = GatherRoute[RouteIndex];
                TargetNodeId = currentNode.NodeId;
                if (Player.DistanceTo(currentNode.Locations[0].Position) > loadRange)
                {
                    var node = currentNode.Locations[0];
                    TravelUtil.Gathering_TravelCheck(node);
                    return false;
                }
                else
                {
                    IceLogging.Debug("We're within range of all nodes, continuing on", tag);
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
                        IceLogging.Debug("We can't seem to find a node that is valid, so we're going to do an individual check in turn JUST to make sure");
                        P.TaskManager.Enqueue(() => Travel_IndividualCheck(), "Checking individual nodes");
                        return true;
                    }
                    else
                    {
                        IceLogging.Debug("we're within range, checking travel kind now");
                        P.TaskManager.Enqueue(() => Travel_MoveAndInteract(validNode), "Checking Travel Kind");
                        return true;
                    }
                }



            }

            return false;
        }
        private static bool Travel_IndividualCheck()
        {
            string tag = "Travel: Individual Check";

            var currentNode = GatherRoute[RouteIndex];
            TargetNodeId = currentNode.NodeId;
            var navtask = P.navTask;

            if (NodeCheckIndex < currentNode.Locations.Count)
            {
                if (!navtask.IsBusy)
                {
                    IceLogging.Verbose($"Checking location: {NodeCheckIndex}");
                    var location = currentNode.Locations[NodeCheckIndex];
                    var distanceToLoc = Player.DistanceTo(location.Position);
                    if (EzThrottler.Throttle("Location message throttle"))
                        IceLogging.Debug($"Distance to location: {distanceToLoc:N2}", tag);

                    if (distanceToLoc > 75)
                    {
                        TravelUtil.Gathering_TravelCheck(location);
                        return false;
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
                            IceLogging.Debug("We've found a valid node! Time to pathfind/interact with it", tag);
                            NodeCheckIndex = 0; // Reset for next time
                            P.TaskManager.Enqueue(() => Travel_MoveAndInteract(validNode), "Checking Travel Kind");
                            return true;
                        }
                        else
                        {
                            IceLogging.Debug($"No valid node [ID: {currentNode.NodeId}] at location {NodeCheckIndex}, moving to next location", tag);
                            NodeCheckIndex += 1;
                            return false;
                        }
                    }
                }
            }
            else
            {
                // We've checked all locations and found no valid nodes
                IceLogging.Debug("Checked all locations for this node group, moving to next route index", tag);
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

            return false;
        }
        private static bool Travel_MoveAndInteract(IGameObject node)
        {
            const string tag = "Travel: Move -> Interact";
            var navTask = P.navTask;

            var currentNode = GatherRoute[RouteIndex];
            var targetLocation = currentNode.Locations.Where(x => x.Position == node.Position).FirstOrDefault();
            
            if (!navTask.IsBusy)
            {
                if (targetLocation is null)
                {
                    IceLogging.Error("We seem to be getting an invalid node???", tag);
                    IceLogging.Error($"Current node: {currentNode.NodeId} doesn't seem to have a valid location. Please report this and the item you were trying to gather", tag);
                    SchedulerMain.DisablePlugin();
                    return true;
                }
                else
                {
                    if (Player.DistanceTo(node) < 3.4)
                    {
                        P.TaskManager.Enqueue(() => InteractWithNode(currentNode.NodeId), "Interacting with node");
                        return true;
                    }
                    else
                    {
                        // We still need to move closer to the node, so we're going to do that
                        TravelUtil.Gathering_TravelToNode(targetLocation, currentNode.NodeId);
                    }
                }

            }

            return false;
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

            var currentAmount = Utils.GetItemCount(itemId);
            if (LastItemInfo.ItemId != itemId)
            {
                LastItemInfo = new()
                {
                    ItemId = itemId,
                    LastAmount = 0,
                };
            }

            if (LastItemInfo.LastAmount == 0)
            {
                LastItemInfo.LastAmount = currentAmount;
            }
            else if (LastItemInfo.LastAmount != currentAmount)
            {
                IceLogging.Debug($"Reporting Amount: Last Known: {LastItemInfo.LastAmount} | Current: {currentAmount}", "Gathering Action");
                var difference = currentAmount - LastItemInfo.LastAmount;
                var config = C.GatherList.Where(x => x.ItemId == itemId).FirstOrDefault();

                if (difference > 0)
                {
                    config.GatherAmount = config.GatherAmount - difference;
                    if (config.GatherAmount < 0)
                        config.GatherAmount = 0;
                    C.SaveDebounced();
                }
                LastItemInfo.LastAmount = currentAmount;

                if (config.GatherAmount == 0)
                {
                    Gather_Helper.State = IceState.Start;
                    P.TaskManager.Tasks.Clear();
                    return true;
                }
            }

            if (Svc.Condition[ConditionFlag.Gathering])
            {
                if (!Svc.Condition[ConditionFlag.ExecutingGatheringAction])
                {
                    if (GenericHelpers.TryGetAddonMaster<Gathering>("Gathering", out var gather) && gather.IsAddonReady)
                    {
                        if (gather.CurrentIntegrity != 0)
                        {
                            List<uint> Crystals = new()
                            {
                                // shards
                                2, 3, 4, 5, 6, 7,

                                // crystals
                                8, 9, 10, 11, 12, 13,

                                // clusters
                                14, 15, 16, 17, 18, 19
                            };

                            if (Crystals.Contains(itemId))
                            {
                                bool maxInteg = gather.TotalIntegrity == gather.CurrentIntegrity;

                                if (Crystal_BuffCheck(maxInteg))
                                    return false;
                            }
                            else if (Basic_BuffCheck())
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
            var actionInfo = Gather_Util.GathActionDict[GatherBuffId.BYII];
            bool hasBuff = Utils.HasStatusId(actionInfo.StatusId) || Utils.HasStatusId(actionInfo.StatusId2);
            bool hasGp = Utils.GetGp() >= actionInfo.RequiredGp;
            bool isLevel = Player.Level >= actionInfo.RequiredLv;
            var actionId = actionInfo.ClassAction[Player.Job];

            if (!hasBuff && hasGp && isLevel)
            {
                ActionManager.Instance()->UseAction(ActionType.Action, actionId);
                return true;
            }
            else
                return false;
        }
        private static unsafe bool Crystal_BuffCheck(bool MaxDurability)
        {
            var job = Player.Job;
            var level = Player.Level;

            var givingLand = Gather_Util.GathActionDict[GatherBuffId.GivingLand];
            var twelveBounty = Gather_Util.GathActionDict[GatherBuffId.TwelveBounty];

            if (MaxDurability)
            {
                var cooldown = BuffCooldown(givingLand.ClassAction[job]);
                if (cooldown < 40)
                {
                    // We're within possible range of using the skill, need to see if it's actually 0, if yes then we can buff, if not then we continue onwards.
                    if (cooldown == 0)
                    {
                        if (!Utils.HasStatusId(givingLand.StatusId) && (Utils.GetGp() >= givingLand.RequiredGp) && level >= givingLand.RequiredLv)
                        {
                            if (EzThrottler.Throttle("Using Action", 100))
                                ActionManager.Instance()->UseAction(ActionType.Action, givingLand.ClassAction[job]);

                            return true;
                        }
                    }
                }
                else
                {
                    if (!Utils.HasStatusId(twelveBounty.StatusId) && Utils.GetGp() >= twelveBounty.RequiredGp && level >= twelveBounty.RequiredLv)
                    {
                        if (EzThrottler.Throttle("Using Action", 100))
                            ActionManager.Instance()->UseAction(ActionType.Action, twelveBounty.ClassAction[job]);

                        return true;
                    }
                }
            }
            else
            {
                var increaseInteg = Gather_Util.GathActionDict[GatherBuffId.BonusIntegrity];
                var bonusInteg = Gather_Util.GathActionDict[GatherBuffId.BonusIntegrity_Chance];

                if (Utils.HasStatusId(bonusInteg.StatusId))
                {
                    if (EzThrottler.Throttle("Using Action", 100))
                        ActionManager.Instance()->UseAction(ActionType.Action, bonusInteg.ClassAction[job]);

                    return true;
                }

                if (Utils.HasStatusId(givingLand.StatusId))
                {
                    if (Utils.GetGp() >= increaseInteg.RequiredGp && level >= increaseInteg.RequiredLv)
                    {
                        if (EzThrottler.Throttle("Using Action", 100))
                            ActionManager.Instance()->UseAction(ActionType.Action, increaseInteg.ClassAction[job]);

                        return true;
                    }
                }
            }

            return false;
        }
        private static unsafe float BuffCooldown(uint ActionId)
        {
            var recastGroup = ActionManager.Instance()->GetRecastGroupDetail(ActionManager.Instance()->GetRecastGroup(1, ActionId));

            if (recastGroup != null)
            {
                float total = recastGroup->Total;      // total cooldown duration
                float elapsed = recastGroup->Elapsed;  // how much has elapsed
                float remaining = total - elapsed;     // time remaining
                bool isActive = recastGroup->IsActive; // Is Active (leaving these here because it's just nice to know/might use in future)

                return remaining;
            }
            else
            {
                return 0;
            }
        }
    }
}
