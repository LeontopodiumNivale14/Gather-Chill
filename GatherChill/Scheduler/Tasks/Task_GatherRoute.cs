using Dalamud.Game.ClientState.Conditions;
using Dalamud.Game.ClientState.Objects.Enums;
using Dalamud.Game.ClientState.Objects.Types;
using ECommons.ExcelServices;
using ECommons.GameHelpers;
using ECommons.Throttlers;
using FFXIVClientStructs.FFXIV.Client.Game;
using FFXIVClientStructs.FFXIV.Client.UI.Agent;
using GatherChill.Enums;
using GatherChill.GatheringInfo;
using GatherChill.Scheduler.Handlers;
using GatherChill.Utilities.GatheringHelpers;
using GatherChill.Utilities.Tools;
using GatherChill.Utilities.Traveling;
using GatherChill.Utilities.Utility;
using System.Collections.Generic;
using static ECommons.UIHelpers.AddonMasterImplementations.AddonMaster;
using static GatherChill.Ui.Tables.Table_Items;

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
        private static ItemCatch OffItemInfo = new();
        private static GatherProfile SelectedProfile = null;

        private static void Enqueue_Check()
        {
            string tag = "Task Gather: Enqueue Check";

            var selectedRoute = Gather_Helper.GatherRoute;
            if (Gather_Util.Sheet_RouteInfo.TryGetValue(selectedRoute.routeId, out var routeInfo))
            {
                if (routeInfo.Kind is GatherNodeKind.Unspoiled or GatherNodeKind.Legendary)
                {
                    // These are the "only gather once, and then fuck off"
                }
                else if (routeInfo.Kind is GatherNodeKind.Ephemeral)
                {
                    // These are the "gather as long as the timed node is up
                }
                else
                {
                    // This is just our normal nodes, the sane nodes specifically (bless these ones)
                }
            }
            else
            {
                IceLogging.Verbose("We seem... to have gotten an invalid route. So we just gonna get out of this", tag);
            }
        }

        public static void NormalItem_Enqueue()
        {
            if (GenericHelpers.TryGetAddonMaster<Gathering>("Gathering", out var gather) && gather.IsAddonReady || TryGetAddonMaster<GatheringMasterpiece>(out var masterpiece) && masterpiece.IsAddonReady)
            {
                var itemId = Gather_Helper.GatherRoute.itemId;
                P.TaskManager.Enqueue(() => GatheringInteraction(), "Gathering Interaction");
            }
            else
            {
                P.TaskManager.Enqueue(() => CheckDesynth(), "Checking for desynth items");
                P.TaskManager.Enqueue(() => ChangeJob(), "Checking for job change");
                P.TaskManager.Enqueue(() => CheckEquipment(), "Checking for equipment requirement");
                P.TaskManager.Enqueue(() => Travel_FarCheck(), "Traveling to node group");
            }
        }

        private static unsafe bool CheckDesynth()
        {
            string tag = "Task Gather: Desynth";
            var goalItemId = Gather_Helper.GatherRoute.itemId;

            if (Svc.Condition[ConditionFlag.Occupied39])
                return false;

            if (Gather_Util.Sheet_ItemInfo.TryGetValue(goalItemId, out var itemInfo))
            {
                var currentAmount = Utils.GetItemCount(goalItemId);
                if (LastItemInfo.ItemId != goalItemId)
                {
                    LastItemInfo = new()
                    {
                        ItemId = goalItemId,
                        LastAmount = currentAmount,
                    };
                }

                var config = C.GatherList.Where(x => x.ItemId == goalItemId).FirstOrDefault();
                bool exitLoop = false;

                if (config.Mode == GatherMode.Keep && currentAmount >= config.KeepAmount)
                {
                    exitLoop = true;
                }
                else if (config.Mode == GatherMode.Gather && LastItemInfo.LastAmount != currentAmount)
                {
                    IceLogging.Debug($"Reporting Amount: Last Known: {LastItemInfo.LastAmount} | Current: {currentAmount}", tag);
                    var difference = currentAmount - LastItemInfo.LastAmount;

                    if (difference > 0 && config.Mode is GatherMode.Gather)
                    {
                        config.GatherAmount = config.GatherAmount - difference;
                        if (config.GatherAmount < 0)
                            config.GatherAmount = 0;
                        C.SaveDebounced();
                        IceLogging.Verbose($"Updated count for: {goalItemId}. Took off: {difference}", tag);
                    }
                    LastItemInfo.LastAmount = currentAmount;

                    if (config.Mode == GatherMode.Gather && config.GatherAmount == 0)
                    {
                        exitLoop = true;
                    }
                }

                if (exitLoop)
                {
                    if (config.Mode == GatherMode.Keep)
                    {
                        IceLogging.Debug("We've hit our keep amount, so we're going to restart back to the beginning", tag);
                    }
                    else if (config.Mode == GatherMode.Gather)
                    {
                        IceLogging.Debug("Gather Amount has hit 0, we're resetting back to start", tag);
                    }

                    Gather_Helper.State = IceState.Start;
                    P.TaskManager.Tasks.Clear();
                    UpdateGatherBuffs();
                    return true;
                }

                var reducableItems = itemInfo.ReducedFromItemIds;

                if (reducableItems.Count > 0 && reducableItems.Any(x => Utils.HasPurifyItem(x)))
                {
                    var firstItem = reducableItems.Where(x => Utils.HasPurifyItem(x)).FirstOrDefault();

                    if (EzThrottler.Throttle("Purifying item"))
                    {
                        IceLogging.Verbose($"Purifying item {firstItem}", tag);
                        Utils.PurifyItem(firstItem);
                    }

                    return false;
                }
                else
                {
                    IceLogging.Verbose("Finishing desynth task");

                    if (GenericHelpers.TryGetAddonMaster<PurifyResult>(out var purify) && purify.IsAddonReady)
                    {
                        if (EzThrottler.Throttle("Close purify"))
                            Ecom_Handler.CloseWindow(purify.Base);

                        IceLogging.Verbose("Closing purify window", tag);

                        return false;
                    }

                    IceLogging.Debug("Finished Desynthing, continuing on");
                    return true;
                }
            }
            else
            {
                IceLogging.Verbose($"No info on this item can be found??? If it's a reducable, please report itemID: {goalItemId}", tag);
                return true;
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
        private static bool CheckEquipment()
        {
            string tag = "Gather: Equipment Check";

            var itemId = Gather_Helper.GatherRoute.itemId;
            var requiredItem = Sheet_ItemInfo[itemId].ItemRequired;

            if (requiredItem == 0)
            {
                IceLogging.Debug("No item is required to be equipped for this gathering node, continuing", tag);
                return true;
            }
            else
            {
                if (Utils.IsItemEquipped(requiredItem))
                {
                    IceLogging.Debug("We found the item! ./ it's equipped currently. So we're continuing on", tag);
                    return true;
                }
                else
                {
                    if (EzThrottler.Throttle("Equipping item attempt"))
                        Utils.TaskEquipItem(requiredItem);
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
                var routeInfo = Gather_Util.Sheet_RouteInfo[Gather_Helper.GatherRoute.routeId];
                if (routeInfo.Kind is GatherNodeKind.Unspoiled or GatherNodeKind.Legendary)
                {
                    IceLogging.Debug("We seem to have traveled to the route, and realized that there is no nodes existing whatsoever", tag);
                    IceLogging.Debug("Going to add it to the checked list and not look again till the next time window", tag);
                    Gather_Helper.GatheredTimedRoutes.Add(Gather_Helper.GatherRoute.routeId);

                    Gather_Helper.State = IceState.Start;
                    return true;
                }
                else
                {
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
            if (SelectedProfile != null)
                SelectedProfile = null;

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
                    if (UseCordial())
                        return false;

                    bool allowedToInteract = true;

                    var routeInfo = Gather_Util.Sheet_RouteInfo[Gather_Helper.GatherRoute.routeId];
                    var itemInfo = Gather_Util.Sheet_ItemInfo[Gather_Helper.GatherRoute.itemId];

                    if (routeInfo.Kind is GatherNodeKind.Legendary or GatherNodeKind.Unspoiled)
                    {
                        var timeSlot = routeInfo.TimedInfo.Where(x => x.IsUp()).FirstOrDefault();
                        if (timeSlot.TimeUntilEnd().TotalSeconds > 60 && Utils.GetGp() < C.TimedNode_MinGP)
                            allowedToInteract = false;
                    }
                    else if (routeInfo.Kind == GatherNodeKind.Regular && itemInfo.IsCollectable)
                    {
                        if (C.WaitGP_Collectable && Utils.GetGp() < C.Collectable_WaitGP)
                            allowedToInteract = false;
                    }

                    if (itemInfo.IsHidden && Utils.GetGp() < 200 && C.WaitGP_HiddenItem)
                    {
                        allowedToInteract = false;
                    }

                    if (allowedToInteract)
                    {
                        if (EzThrottler.Throttle("Target + Interaction throttle"))
                        {
                            Utils.TargetgameObject(targetNode);
                            Utils.InteractWithObject(targetNode);
                        }
                    }
                    else
                    {
                        if (EzThrottler.Throttle("Waiting for gp???"))
                        {
                            IceLogging.Debug($"Waiting for gp to refill currently. . . currently at: {Utils.GetGp()}");
                        }
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
        private static unsafe bool GatheringInteraction(bool offItem = false)
        {
            string tag = "Gather Route: Interaction";
            var routeSelected = Gather_Helper.GatherRoute;
            var itemId = routeSelected.itemId;
            if (offItem)
                itemId = LastItemInfo.ItemId;
            var routeId = routeSelected.routeId;

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
                    LastAmount = currentAmount,
                };
            }

            var config = C.GatherList.Where(x => x.ItemId == itemId).FirstOrDefault();
            bool exitLoop = false;

            if (config.Mode == GatherMode.Keep && currentAmount >= config.KeepAmount)
            {
                exitLoop = true;
            }
            else if (config.Mode == GatherMode.Gather && LastItemInfo.LastAmount != currentAmount)
            {
                IceLogging.Debug($"Reporting Amount: Last Known: {LastItemInfo.LastAmount} | Current: {currentAmount}", tag);
                var difference = currentAmount - LastItemInfo.LastAmount;

                if (difference > 0 && config.Mode is GatherMode.Gather)
                {
                    config.GatherAmount = config.GatherAmount - difference;
                    if (config.GatherAmount < 0)
                        config.GatherAmount = 0;
                    C.SaveDebounced();
                    IceLogging.Verbose($"Updated count for: {itemId}. Took off: {difference}", tag);
                }
                LastItemInfo.LastAmount = currentAmount;

                if (config.Mode == GatherMode.Gather && config.GatherAmount == 0)
                {
                    exitLoop = true;
                }
            }

            if (exitLoop)
            {
                if (config.Mode == GatherMode.Keep)
                {
                    IceLogging.Debug("We've hit our keep amount, so we're going to restart back to the beginning", tag);
                }
                else if (config.Mode == GatherMode.Gather)
                {
                    IceLogging.Debug("Gather Amount has hit 0, we're resetting back to start", tag);
                }

                Gather_Helper.State = IceState.Start;
                P.TaskManager.Tasks.Clear();
                UpdateGatherBuffs();
                return true;
            }

            if (Svc.Condition[ConditionFlag.Gathering])
            {
                if (!Svc.Condition[ConditionFlag.ExecutingGatheringAction])
                {
                    bool correctRoute()
                    {
                        var currentTarget = Svc.Objects.LocalPlayer.TargetObject;
                        if (currentTarget != null)
                        {
                            var nodeId = currentTarget.BaseId;
                            var currentRoute = Gather_Util.Sheet_RouteInfo.Where(x => x.Value.NodeIds.Contains(nodeId)).FirstOrDefault();

                            return currentRoute.Key == routeId;
                        }
                        return true;
                    }
                    bool IsGoalItem(uint gatheredItemId, uint goalItemId)
                    {
                        if (gatheredItemId == goalItemId)
                            return true;

                        if (!Gather_Util.Sheet_ItemInfo.TryGetValue(goalItemId, out var goalInfo))
                            return false;

                        // goalInfo is the reduce result; ReducedFromItemIds holds every source that feeds it
                        return goalInfo.ReducedFromItemIds.Contains(gatheredItemId);
                    }
                    uint OtherItem(Utilities.Gathering gather)
                    {
                        var gatherList = gather.GatheredItems.Where(x => x.ItemID != 0).ToList();
                        var itemList = C.GatherList
                            .Where(x => (x.Mode == GatherMode.Gather && x.GatherAmount != 0)
                                     || (x.Mode == GatherMode.Keep && Utils.GetItemCount(x.ItemId) < x.KeepAmount))
                            .ToList();

                        var wantedIds = itemList.Select(x => x.ItemId).ToHashSet();
                        var match = gatherList.FirstOrDefault(x => wantedIds.Contains(x.ItemID));

                        if (match == null)
                            return 0;
                        else
                            return match.ItemID;
                    }

                    if (GenericHelpers.TryGetAddonMaster<Utilities.Gathering>(out var gather) && gather.IsAddonReady)
                    {
                        if (_collectablePreset != CollectablePreset.None)
                            _collectablePreset = CollectablePreset.None;

                        if (EzThrottler.Throttle("Gathering Log Time"))
                        {
                            IceLogging.Debug("Currently in a gathering state! And the gathering window is open", tag);
                        }

                        if (!correctRoute())
                        {
                            if (EzThrottler.Throttle("Exiting Wrong Route"))
                            {
                                IceLogging.Verbose("We seem to be in a gathering window that doesn't contain this item, so we're just gonna get out of this...", tag);
                                Ecom_Handler.CloseWindow(gather.Base);
                            }
                            return false;
                        }

                        if (gather.CurrentIntegrity != 0)
                        {
                            List<uint> Crystals = new()
                            {
                                // shards
                                2, 3, 4, 5, 6, 7,

                                // crystals
                                8, 9, 10, 11, 12, 13,

                                // clusters
                                14, 15, 16, 17, 18, 19,

                                10099 // Unaspected crystal
                            };

                            var goalItem = gather.GatheredItems.FirstOrDefault(x => IsGoalItem(x.ItemID, itemId));
                            if (goalItem != null)
                            {
                                // We found the item we're looking for, we can buff -> gather (if it's not a collectable)
                                if (!Gather_Helper.ShouldHoldGpForTimedNode() && !goalItem.IsCollectable)
                                {
                                    if (Crystals.Contains(itemId))
                                    {
                                        bool maxInteg = gather.TotalIntegrity == gather.CurrentIntegrity;

                                        if (Crystal_BuffCheck(maxInteg))
                                            return false;
                                    }
                                    else
                                    {
                                        if (SelectedProfile is null)
                                        {
                                            var level = Player.Level;
                                            var gp = Utils.GetGp();
                                            var integrity = gather.CurrentIntegrity;
                                            var gatherChance = goalItem.GatherChance;
                                            var boonChance = goalItem.BoonChance;
                                            var amount = config.Mode is GatherMode.Gather ? config.GatherAmount : config.KeepAmount - Utils.GetItemCount(goalItem.ItemID);

                                            var autoProfile = C.User_AutoProfile;

                                            var result = Gather_Util.PlanBuffs(level, gp, integrity, gatherChance, boonChance, amount, autoProfile, goalItem.GatherAmount);
                                            SelectedProfile = result.Profile;
                                        }

                                        if (CheckGatheringBuffs(gather))
                                            return false;
                                    }
                                }

                                goalItem.Gather();
                            }
                            else
                            {
                                var routeInfo = Gather_Util.Sheet_RouteInfo[routeId];
                                var hiddenItems = routeInfo.ItemIds.Where(x => Gather_Util.Sheet_ItemInfo[x].IsHidden).ToList();
                                if (!HiddenUsed)
                                {
                                    if (gather.GatheredItems.Any(x => hiddenItems.Contains(x.ItemID)))
                                        HiddenUsed = true;
                                }

                                if (Gather_Util.Sheet_ItemInfo.TryGetValue(itemId, out var itemInfo))
                                {
                                    if (itemInfo.IsHidden & !HiddenUsed)
                                    {
                                        CanCheckHidden();
                                        return false;
                                    }
                                    else
                                    {
                                        // For some reason or another, we need to just gather the crystals (probably not available)
                                        var crystal = gather.GatheredItems.Where(x => Crystals.Contains(x.ItemID)).FirstOrDefault();
                                        if (crystal != null)
                                        {
                                            if (EzThrottler.Throttle($"Gathering Backup: {crystal.Gather}"))
                                                crystal.Gather();
                                        }
                                        else
                                        {
                                            var otherItems = OtherItem(gather);
                                            if (otherItems != 0)
                                            {
                                                P.TaskManager.Enqueue(() => GatheringInteraction(true), "Checking secondary item");
                                                OffItemInfo = new()
                                                {
                                                    ItemId = otherItems,
                                                    LastAmount = Utils.GetItemCount(otherItems)
                                                };
                                                return true;
                                            }
                                            else
                                            {
                                                // WELL WE NEED TO GATHER SOMETHING... OR. HERE ME OUT. We just abandon and error
                                                IceLogging.Error("No backup item could be found (aka crystals?) and no actual. . . hidden item can be found. So we gonna skeet out :fguns:", tag);
                                                SchedulerMain.DisablePlugin();
                                                return true;
                                            }
                                        }
                                    }
                                }
                            }

                            var item = gather.GatheredItems?.FirstOrDefault(x => x.ItemID == itemId);
                            if (item is null)
                            {
                                if (EzThrottler.Throttle($"Gather item missing: {itemId}"))
                                    IceLogging.Verbose($"Item {itemId} not found in the gathering window's item list [might be mid transition]", tag);
                                return false;
                            }

                            if (EzThrottler.Throttle($"Gathering Item: {itemId}"))
                                item.Gather();
                        }
                    }
                    else if (GenericHelpers.TryGetAddonMaster<GatheringMasterpiece>(out var gatherMaster) && gatherMaster.IsAddonReady)
                    {
                        // Fresh state every tick: progression, integrity, GP and buffs all change after each action
                        var state = Gather_Util.BuildState(gatherMaster);

                        // Preset is chosen once per node, from the GP we had when the window opened
                        if (_collectablePreset == CollectablePreset.None)
                        {
                            var collectableId = gatherMaster.ItemID;
                            bool isEphemeral = Gather_Util.ReducableItems.Any(x => x.ItemId == collectableId || x.SublimeItemId == collectableId);

                            _collectablePreset = Gather_Util.CollectableSolver.ChoosePreset(state, isEphemeral);
                            IceLogging.Debug($"Collectable preset chosen: {_collectablePreset} (GP: {state.AvailableGp}, Ephemeral: {isEphemeral}) | Item: {gatherMaster.ItemName} | Min: {state.MinCollectability} | Mid: {gatherMaster.MidCollectability} | High: {gatherMaster.HighCollectability}", tag);
                        }

                        var nextAction = Gather_Util.CollectableSolver.GetNextAction(_collectablePreset, state);
                        if (nextAction is null)
                        {
                            if (EzThrottler.Throttle("Collectable no action"))
                                IceLogging.Debug($"Solver returned no action | Prog: {state.Progression} | Min: {state.MinCollectability} | Att: {state.Attempts}/{state.MaxAttempts} | GP: {state.AvailableGp}", tag);
                            return false;
                        }

                        var actionId = Gather_Util.CollectableSolver.GetActionId(nextAction.Value, Player.Job);
                        if (actionId is null)
                        {
                            if (EzThrottler.Throttle("Collectable no action id"))
                                IceLogging.Warning($"No action id for {nextAction.Value} on job {Player.Job}", tag);
                            return false;
                        }

                        if (UseGatheringAction(actionId.Value))
                            IceLogging.Verbose($"Collectable: {nextAction.Value} | Prog: {state.Progression} | Min: {state.MinCollectability} | Att: {state.Attempts}/{state.MaxAttempts} | GP: {state.AvailableGp} | Scrutiny: {state.HasScrutiny} | Standard: {state.HasCollectorStandard}", tag);
                    }
                    else if (GenericHelpers.TryGetAddonMaster<SelectYesno>(out var yesNo) && yesNo.IsAddonReady)
                    {
                        yesNo.Yes();
                    }
                    else
                    {
                        SelectedProfile = null;
                    }
                }
                else
                {
                    if (_toggleHidden)
                    {
                        HiddenUsed = true;
                        _toggleHidden = false;
                    }
                    if (BuffCounter != GatherBuffId.Unknown)
                    {
                        CurrentUse[BuffCounter] += 1;
                        BuffCounter = GatherBuffId.Unknown;
                    }
                }
            }
            else
            {
                UpdateGatherBuffs();
                if (Gather_Util.Sheet_RouteInfo[routeId].Kind is GatherNodeKind.Unspoiled or GatherNodeKind.Legendary)
                {
                    Gather_Helper.GatheredTimedRoutes.Add(routeId);
                }

                return true;
            }

            return false;
        }

        #region Gathering Buff Checks

        private static bool HiddenUsed = false;
        private static bool _toggleHidden = false;
        private static GatherBuffId BuffCounter = GatherBuffId.Unknown;
        private static CollectablePreset _collectablePreset = CollectablePreset.None;
        private static Dictionary<GatherBuffId, int> CurrentUse = new()
        {
            [GatherBuffId.BoonIncrease_1] = 0,
            [GatherBuffId.BoonIncrease_2] = 0,
            [GatherBuffId.Tidings] = 0,
            [GatherBuffId.YieldI] = 0,
            [GatherBuffId.YieldII] = 0,
            [GatherBuffId.BonusIntegrity] = 0,
            [GatherBuffId.BonusIntegrity_Chance] = 0,
            [GatherBuffId.BYII] = 0,
            [GatherBuffId.FieldMasteryI] = 0,
            [GatherBuffId.FieldMasteryII] = 0,
            [GatherBuffId.FieldMasteryIII] = 0,
            [GatherBuffId.FieldMasteryTemp] = 0,
            [GatherBuffId.TwelveBounty] = 0,
            [GatherBuffId.GivingLand] = 0,
            [GatherBuffId.Scrutiny] = 0,
            [GatherBuffId.Focus] = 0,
            [GatherBuffId.Priming] = 0,
            [GatherBuffId.Scour] = 0,
            [GatherBuffId.Brazen] = 0,
            [GatherBuffId.Meticulous] = 0,
        };

        private static void UpdateGatherBuffs()
        {
            HiddenUsed = false;
            _toggleHidden = false;
            _collectablePreset = CollectablePreset.None;

            foreach (var buff in CurrentUse)
            {
                CurrentUse[buff.Key] = 0;
            }

        }
        private static unsafe bool CanCheckHidden()
        {
            if (HiddenUsed)
                return false;

            var am = ActionManager.Instance();
            if (am is null)
                return false;

            var actionInfo = Gather_Util.GathActionDict[GatherBuffId.HiddenFinder];
            var actionId = actionInfo.ClassAction[Player.Job].ActionId;

            var canUse = am->GetActionStatus(ActionType.Action, actionId, checkRecastActive: false, checkCastingActive: false) is 0;

            if (canUse)
            {
                if (EzThrottler.Throttle("Use buff", 100))
                    ActionManager.Instance()->UseAction(ActionType.Action, actionId);
                _toggleHidden = true;
                return true;
            }
            else
            {
                return false;
            }
        }
        private static bool CheckGatheringBuffs(Utilities.Gathering gathering)
        {
            const string tag = "Gather: Check Buffs";

            var job = Player.Job;
            var currentGp = Utils.GetGp();
            var missingDur = gathering.CurrentIntegrity != gathering.TotalIntegrity;

            var gatherProfile = SelectedProfile;

            foreach (var buff in gatherProfile.BuffPriority)
            {
                var buffProfile = gatherProfile.GatheringBuffs[buff];
                var actionInfo = Gather_Util.GathActionDict[buff];

                if (!buffProfile.Enabled)
                {
                    IceLogging.Verbose($"Skipping: {buff} due to not enabled", tag);
                    continue;
                }

                // 0 means no cap; otherwise skip once we've used it as many times as planned
                if (buffProfile.MaxUse != 0 && CurrentUse[buff] >= buffProfile.MaxUse)
                {
                    IceLogging.Verbose($"Skipping: {buff} due to reaching max use ({buffProfile.MaxUse})", tag);
                    continue;
                }

                if (currentGp < actionInfo.RequiredGp)
                {
                    IceLogging.Verbose($"Skipping: {buff} due to not enough GP", tag);
                    continue;
                }

                if (!IsBuffDue(buff, actionInfo, missingDur))
                {
                    IceLogging.Verbose($"Skipping: {buff} is not due", tag);
                    continue;
                }

                // The cast was throttled, so a buff is still due. Report "busy" so the caller waits instead of gathering without it.
                if (!UseGatheringAction(actionInfo.ClassAction[job].ActionId))
                {
                    IceLogging.Verbose($"Waiting: {buff} is due but a cast is still on cooldown", tag);
                    return true;
                }

                IceLogging.Verbose($"Used buff: {buff}", tag);
                BuffCounter = buff;

                // Wise and Bountiful don't end the check, same as before, so the gather itself can follow right away
                if (buff is GatherBuffId.BonusIntegrity_Chance or GatherBuffId.BYII)
                {
                    continue;
                }

                return true;
            }

            return false;
        }
        private static bool IsBuffDue(GatherBuffId buff, Gather_Util.GatheringActions actionInfo, bool missingDur)
        {
            // Ageless Words only helps if we're actually missing integrity
            if (buff == GatherBuffId.BonusIntegrity)
            {
                return missingDur;
            }

            // Wise is a free proc: it needs the status to be up, and it's wasted at full integrity
            if (buff == GatherBuffId.BonusIntegrity_Chance)
            {
                return missingDur && Utils.HasStatusId(actionInfo.StatusId);
            }

            // everything else (Field Mastery, Gift, Yield, Tidings, Bountiful) is due when its status isn't already up
            if (Utils.HasStatusId(actionInfo.StatusId))
            {
                return false;
            }

            return actionInfo.StatusId2 == 0 || !Utils.HasStatusId(actionInfo.StatusId2);
        }
        private static unsafe bool UseGatheringAction(uint ActionId)
        {
            if (!EzThrottler.Throttle("Using Gathering Action", 500))
            {
                return false;
            }

            ActionManager.Instance()->UseAction(ActionType.Action, ActionId);
            return true;
        }
        private static unsafe bool Crystal_BuffCheck(bool MaxDurability)
        {
            var job = Player.Job;
            var level = Player.Level;

            var givingLand = Gather_Util.GathActionDict[GatherBuffId.GivingLand];
            var twelveBounty = Gather_Util.GathActionDict[GatherBuffId.TwelveBounty];

            if (MaxDurability)
            {
                var cooldown = BuffCooldown(givingLand.ClassAction[job].ActionId);
                if (cooldown < C.CrystalBuff_HoldGP)
                {
                    // We're within possible range of using the skill, need to see if it's actually 0, if yes then we can buff, if not then we continue onwards.
                    if (cooldown == 0)
                    {
                        if (!Utils.HasStatusId(givingLand.StatusId) && (Utils.GetGp() >= givingLand.RequiredGp) && level >= givingLand.RequiredLv)
                        {
                            if (EzThrottler.Throttle("Using Action", 100))
                                ActionManager.Instance()->UseAction(ActionType.Action, givingLand.ClassAction[job].ActionId);

                            return true;
                        }
                    }
                }
                else
                {
                    if (!Utils.HasStatusId(twelveBounty.StatusId) && Utils.GetGp() >= twelveBounty.RequiredGp && level >= twelveBounty.RequiredLv)
                    {
                        if (EzThrottler.Throttle("Using Action", 100))
                            ActionManager.Instance()->UseAction(ActionType.Action, twelveBounty.ClassAction[job].ActionId);

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
                        ActionManager.Instance()->UseAction(ActionType.Action, bonusInteg.ClassAction[job].ActionId);

                    return true;
                }

                if (Utils.HasStatusId(givingLand.StatusId))
                {
                    if (Utils.GetGp() >= increaseInteg.RequiredGp && level >= increaseInteg.RequiredLv)
                    {
                        if (EzThrottler.Throttle("Using Action", 100))
                            ActionManager.Instance()->UseAction(ActionType.Action, increaseInteg.ClassAction[job].ActionId);

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
        private static unsafe bool UseCordial()
        {
            string tag = "Gather: Cordial Check";

            var autoCordial = C.Cordial_Auto;
            var useCordialBelow = C.Cordial_Amount;
            var reversePriority = C.Cordial_Inverse;
            var preventOvercap = C.Cordial_PreventOvercap;

            var currentGp = Utils.GetGp();
            var maxGp = Utils.MaxGp();

            bool WillOvercap(int recoveryGP)
            {
                bool WillOvercap = (currentGp + recoveryGP) > maxGp;
                if (WillOvercap)
                {
                    IceLogging.Verbose("Not going to be using a cordial because we'll overcap", tag);
                    IceLogging.Verbose($"Recovered GP: {currentGp + recoveryGP} | Max GP: {maxGp}", tag);
                }

                return WillOvercap;
            }

            if (C.Cordial_Auto)
            {
                if (EzThrottler.Throttle($"Use Cordial At"))
                {
                    IceLogging.Verbose($"Reporting GP: {currentGp:N0} | Minimum GP: {useCordialBelow:N0}");
                }

                if (currentGp <= useCordialBelow)
                {
                    Dictionary<uint, (string Name, int GpGain)> cordials = new()
                    {
                        [12669] = ("Hi-Cordial", 400),
                        [1006141] = ("HQ Regular Cordial", 350),
                        [6141] = ("NQ Regular Cordial", 300),
                        [1016911] = ("HQ Watered Cordial", 200),
                        [16911] = ("NQ Watered Cordial", 150),
                    };

                    if (ActionManager.Instance()->GetActionStatus(ActionType.Item, 12669) == 0)
                    {
                        foreach (var cordial in reversePriority ? cordials.Reverse() : cordials)
                        {
                            IceLogging.Verbose($"Checking Cordial: {cordial.Value.Name}", tag);
                            bool hq = cordial.Key >= 1_000_000;
                            uint baseId = hq ? cordial.Key - 1_000_000 : cordial.Key;

                            var count = Utils.GetItemCount(cordial.Key);
                            if (count > 0)
                            {
                                if (!preventOvercap || !WillOvercap(cordial.Value.GpGain))
                                {
                                    // Find the actual inventory slot and use it directly
                                    var inventoryManager = InventoryManager.Instance();
                                    var inventoryTypes = new[]
                                    {
                                            InventoryType.Inventory1, InventoryType.Inventory2,
                                            InventoryType.Inventory3, InventoryType.Inventory4
                                        };

                                    foreach (var invType in inventoryTypes)
                                    {
                                        var container = inventoryManager->GetInventoryContainer(invType);
                                        if (container == null) continue;

                                        for (int i = 0; i < container->Size; i++)
                                        {
                                            var item = container->GetInventorySlot(i);
                                            if (item == null) continue;
                                            if (item->ItemId == baseId && (hq == false || item->Flags.HasFlag(InventoryItem.ItemFlags.HighQuality)))
                                            {
                                                if (EzThrottler.Throttle("Cordial Usage"))
                                                {
                                                    IceLogging.Verbose($"We're using a cordial: ID: {cordial.Key} | Name: {cordial.Value.Name}", tag);
                                                    AgentInventoryContext.Instance()->UseItem(cordial.Key, invType, (uint)i, 0);
                                                }
                                                return true;
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        // We currently have the cordial on cd, so we're just going to ignore it entirely
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }

            return false;
        }

        #endregion
    }
}
