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
using GatherChill.Utilities.Replay;
using GatherChill.Utilities.Tools;
using GatherChill.Utilities.Traveling;
using GatherChill.Utilities.Utility;
using Newtonsoft.Json;
using System.Collections.Generic;
using static ECommons.UIHelpers.AddonMasterImplementations.AddonMaster;
using static GatherChill.ConfigFiles.Config;

namespace GatherChill.Scheduler.Tasks
{
    internal class Task_GatherRoute
    {
        #region State

        private const float LoadRange = 75f;

        // How close do I want the matching minimap marker to be 
        // Right now from testing, it seems like it's directly 1->1 with the cordinates
        // But it never hurts to have some buffer...
        private const float MarkerMatchRadius = 10;

        private static GatheringRoute selectedRoute = null;
        private static int RouteIndex = 0;
        private static List<GatheringNode> GatherRoute = new();
        private static uint? TargetNodeId = null;
        private static int NodeCheckIndex = 0;

        // Which entry of the current node's Locations to use. 0 = default behavior.
        private static int ActiveLocationIndex = 0;

        private static GatherProfile SelectedProfile = null;

        // Collectables don't flag ExecutingGatheringAction during the animation, so we remember what we last sent
        private static string _lastCollectableSig = "";
        private static string _lastCrystalSig = "";   // dedupes crystal casts in the replay, same idea as the collectable one

        // Bountiful Yield bonus, read from the gathering window instead of trusting the BYII_Amount setting.
        // LearnedBYII is kept for the whole session: the first node of an item plans with the setting, later ones with what the game showed.
        private static readonly Dictionary<uint, int> LearnedBYII = new();
        private static int _byiiBaseAmount = 0;      // per-swing amount the window showed while BYII was NOT up
        private static bool _byiiReported = false;   // this cast was already measured
        private static int _plannedBYII = 0;         // bonus the current plan assumed
        private const int BountifulIILevel = 68;             // Bountiful Yield only varies by item from this level (same value as the planner's)
        private static bool _byiiMeasure = false;            // this plan should cast Bountiful Yield first and read its bonus before anything else
        private static bool _planHadBYII = false;            // the plan being followed casts Bountiful Yield
        private const long ByiiMeasureTimeoutMs = 4000;   // give up waiting for the measurement after this long, so a missing reading can never hold the gathering up
        private static long _byiiGateStartMs = 0;            // when we started waiting to measure Bountiful Yield on this node. 0 = not waiting
        private static int _byiiCredit = 0;          // the live Bountiful Yield already covers the next swing, so the re-plan counts it as free (used once)

        private static bool HiddenUsed = false;
        private static bool _hiddenCast = false;      // the hidden-item skill was sent on this node
        private static bool _hiddenReported = false;  // the "item revealed" note was already written
        private static bool _toggleHidden = false;
        private static GatherBuffId BuffCounter = GatherBuffId.Unknown;
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

        // Only these are cast at the start of the node. Ageless, Wise and Bountiful are allowed after integrity drops.
        private static readonly HashSet<GatherBuffId> FullIntegrityOnly = new()
        {
            GatherBuffId.FieldMasteryI, GatherBuffId.FieldMasteryII, GatherBuffId.FieldMasteryIII,
            GatherBuffId.YieldI, GatherBuffId.YieldII,
            GatherBuffId.BoonIncrease_1, GatherBuffId.BoonIncrease_2,
            GatherBuffId.Tidings,
        };

        private static readonly (uint Id, string Name, int GpGain)[] Cordials =
        {
            (12669, "Hi-Cordial", 400),
            (1006141, "HQ Regular Cordial", 350),
            (6141, "NQ Regular Cordial", 300),
            (1016911, "HQ Watered Cordial", 200),
            (16911, "NQ Watered Cordial", 150),
        };

        private static readonly InventoryType[] CordialContainers =
        {
            InventoryType.Inventory1, InventoryType.Inventory2,
            InventoryType.Inventory3, InventoryType.Inventory4,
        };

        #endregion

        #region Shared helpers

        private static IGameObject? FindValidNode(uint nodeId)
        {
            return Svc.Objects.FirstOrDefault(obj => obj.BaseId == nodeId
                                                  && obj.IsTargetable
                                                  && obj.ObjectKind == ObjectKind.GatheringPoint);
        }

        private static GatherNodeKind GetRouteKind(uint routeId)
        {
            return Gather_Util.Sheet_RouteInfo.TryGetValue(routeId, out var info) ? info.Kind : GatherNodeKind.Regular;
        }

        private static bool IsTimedRoute()
        {
            if (!Gather_Util.Sheet_RouteInfo.TryGetValue(Gather_Helper.GatherRoute.routeId, out var info))
                return false;

            return info.Kind == GatherNodeKind.Unspoiled || info.Kind == GatherNodeKind.Legendary;
        }

        private static bool FinishItem(GatherMode? mode, string tag)
        {
            var message = mode switch
            {
                null => "This item is no longer being tracked, so we're going back to the start",
                GatherMode.Keep => "We've hit our keep amount, so we're going to restart back to the beginning",
                _ => "We've hit our gather goal, we're resetting back to start",
            };

            IceLogging.Debug(message, tag);
            ReplayRecorder.EndNode(message);

            Gather_Helper.State = IceState.Start;
            P.TaskManager.Tasks.Clear();
            UpdateGatherBuffs();
            return true;
        }

        #endregion

        #region Task

        public static void NormalItem_Enqueue()
        {
            if (GenericHelpers.TryGetAddonMaster<Utilities.ExtraAddons.Gathering>("Gathering", out var gather) && gather.IsAddonReady || TryGetAddonMaster<GatheringMasterpiece>(out var masterpiece) && masterpiece.IsAddonReady)
            {
                P.TaskManager.Enqueue(() => GatheringInteraction(), "Gathering Interaction");
            }
            else
            {
                P.TaskManager.EnqueueMulti(
                    new(() => CheckDesynth(), "Checking for desynth items"),
                    new(() => ChangeJob(), "Checking for job change"),
                    new(() => CheckEquipment(), "Checking for equipment requirement"),
                    new(() => Task_SelfRepair.Enqueue(), "Self Repair Check"),
                    new(() => Task_Spiritbond.Enqueue(), "Spiritbond Check"),
                    new(() => Travel_FarCheck(), "Traveling to node group"));
            }
        }

        private static unsafe bool CheckDesynth()
        {
            const string tag = "Task Gather: Desynth";
            var goalItemId = Gather_Helper.GatherRoute.itemId;

            if (Svc.Condition[ConditionFlag.Occupied39])
                return false;

            if (!Gather_Util.Sheet_ItemInfo.TryGetValue(goalItemId, out var itemInfo))
            {
                IceLogging.Verbose($"No info on this item can be found??? If it's a reducable, please report itemID: {goalItemId}", tag);
                return true;
            }

            var progress = Gather_Helper.ItemProgress.FirstOrDefault(x => x.ItemId == goalItemId);
            if (progress == null || progress.IsComplete)
                return FinishItem(progress?.Mode, tag);

            var reducableItems = itemInfo.ReducedFromItemIds;

            if (reducableItems.Count > 0 && reducableItems.Any(x => Utils.HasPurifyItem(x)))
            {
                var firstItem = reducableItems.First(x => Utils.HasPurifyItem(x));

                if (EzThrottler.Throttle("Purifying item"))
                {
                    IceLogging.Verbose($"Purifying item {firstItem}", tag);
                    ReplayRecorder.Desynth(firstItem);
                    Utils.PurifyItem(firstItem);
                }

                return false;
            }

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

        private static bool ChangeJob()
        {
            const string tag = "Task Gather: Change Job";

            var routeId = Gather_Helper.GatherRoute.routeId;
            var job = Gather_Util.Sheet_RouteInfo[routeId].Job;

            if (Player.Job == (Job)job)
                return true;

            if (EzThrottler.Throttle("Swapping jobs", 2000))
            {
                IceLogging.Verbose("We need to swap jobs to even be able to do this, so that's what we're going to do. *-I hope you have one unlocked-*", tag);
                Utils.TaskClassChange((Job)job);
            }

            return false;
        }

        private static bool CheckEquipment()
        {
            const string tag = "Gather: Equipment Check";

            var itemId = Gather_Helper.GatherRoute.itemId;
            var requiredItem = Sheet_ItemInfo[itemId].ItemRequired;

            if (requiredItem == 0)
            {
                IceLogging.Debug("No item is required to be equipped for this gathering node, continuing", tag);
                return true;
            }

            if (Utils.IsItemEquipped(requiredItem))
            {
                IceLogging.Debug("We found the item! ./ it's equipped currently. So we're continuing on", tag);
                return true;
            }

            if (EzThrottler.Throttle("Equipping item attempt"))
                Utils.TaskEquipItem(requiredItem);

            return false;
        }

        #endregion

        #region Travel

        private static bool TryGetTimedRouteIndex(out int routeIndex, out int locationIndex)
        {
            routeIndex = -1;
            locationIndex = 0;

            var markers = Gather_Util.GetActiveWorldXZ();
            if (markers.Count == 0)
                return false;

            var bestDist = MarkerMatchRadius;

            for (var i = 0; i < GatherRoute.Count; i++)
            {
                var node = GatherRoute[i];

                for (var j = 0; j < node.Locations.Count; j++)
                {
                    var pos = node.Locations[j].Position;
                    var pos2D = new Vector2(pos.X, pos.Z);

                    foreach (var marker in markers)
                    {
                        var dist = Vector2.Distance(pos2D, marker);

                        // dist >= bestDist means NOT closer than the best so far -> skip.
                        // Only a dist SMALLER than bestDist gets past this line.
                        if (dist >= bestDist)
                            continue;

                        bestDist = dist;
                        routeIndex = i;
                        locationIndex = j;
                    }
                }
            }

            if (routeIndex != -1 && EzThrottler.Throttle("TimedMarkerChosen", 2000))
                IceLogging.Debug($"Timed marker chose route index {routeIndex} (node {GatherRoute[routeIndex].NodeId}), location {locationIndex}, dist {bestDist:F2}", "Timed Marker");

            return routeIndex != -1;
        }

        private static bool Travel_FarCheck()
        {
            const string tag = "Task: Gather Travel";

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

            // RouteIndex >= Count means we ran past the end of the list -> wrap to 0
            if (RouteIndex >= GatherRoute.Count)
                RouteIndex = 0;

            // Timed route + a marker on one of our node locations: retarget, and cancel any path to a different spot.
            // Runs BEFORE the IsBusy check so it can interrupt travel.
            if (IsTimedRoute() && TryGetTimedRouteIndex(out var timedIndex, out var timedLocation))
            {
                if (timedIndex != RouteIndex || timedLocation != ActiveLocationIndex)
                {
                    IceLogging.Debug($"Switching to route index {timedIndex} (node {GatherRoute[timedIndex].NodeId}), location {timedLocation}", tag);
                    RouteIndex = timedIndex;
                    ActiveLocationIndex = timedLocation;

                    if (P.navTask.IsBusy)
                    {
                        P.navTask.Abort();
                        P.navmesh.Stop();
                    }
                }
            }
            else
            {
                // No marker -> normal behavior
                ActiveLocationIndex = 0;
            }

            // Still pathing to the current target, so wait for the next pass
            if (P.navTask.IsBusy)
                return false;

            var currentNode = GatherRoute[RouteIndex];
            if (currentNode.Locations.Count == 0)
            {
                IceLogging.Warning($"Node {currentNode.NodeId} has no locations", tag);
                return true;
            }

            // Guard against a stale index when the node has fewer locations
            var locationIndex = ActiveLocationIndex < currentNode.Locations.Count ? ActiveLocationIndex : 0;
            var location = currentNode.Locations[locationIndex];

            TargetNodeId = currentNode.NodeId;

            // Distance > LoadRange means we're FARTHER than the load range, so keep traveling
            if (Player.DistanceTo(location.Position) > LoadRange)
            {
                TravelUtil.Gathering_TravelCheck(location);
                return false;
            }

            IceLogging.Debug("We're within range of all nodes, continuing on", tag);
            P.navmesh.PathStop();

            var validNode = FindValidNode(currentNode.NodeId);

            if (EzThrottler.Throttle("IsNodeValid"))
                IceLogging.Debug($"Is Node Valid: {validNode != null}");

            if (validNode != null)
            {
                IceLogging.Debug("we're within range, checking travel kind now");
                P.TaskManager.Enqueue(() => Travel_MoveAndInteract(validNode), "Checking Travel Kind");
                return true;
            }

            NodeCheckIndex = 0;
            IceLogging.Debug("We can't seem to find a node that is valid, so we're going to do an individual check in turn JUST to make sure");
            P.TaskManager.Enqueue(() => Travel_IndividualCheck(), "Checking individual nodes");
            return true;
        }

        private static bool Travel_IndividualCheck()
        {
            const string tag = "Travel: Individual Check";

            var currentNode = GatherRoute[RouteIndex];
            TargetNodeId = currentNode.NodeId;
            var navtask = P.navTask;

            // Checked every location of this node group and none had a node up
            if (NodeCheckIndex >= currentNode.Locations.Count)
            {
                if (IsTimedRoute())
                {
                    IceLogging.Debug("We seem to have traveled to the route, and realized that there is no nodes existing whatsoever", tag);
                    IceLogging.Debug("Going to add it to the checked list and not look again till the next time window", tag);
                    Gather_Helper.GatheredTimedRoutes.Add(Gather_Helper.GatherRoute.routeId);

                    Gather_Helper.State = IceState.Start;
                    return true;
                }

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

            if (navtask.IsBusy)
                return false;

            IceLogging.Verbose($"Checking location: {NodeCheckIndex}");
            var location = currentNode.Locations[NodeCheckIndex];
            var distanceToLoc = Player.DistanceTo(location.Position);

            if (EzThrottler.Throttle("Location message throttle"))
                IceLogging.Debug($"Distance to location: {distanceToLoc:N2}", tag);

            // Farther than the load range, so travel there first
            if (distanceToLoc > LoadRange)
            {
                TravelUtil.Gathering_TravelCheck(location);
                return false;
            }

            if (P.navmesh.IsRunning())
                P.navmesh.PathStop();

            // If we're here, that means that we're within load range. 
            var validNode = FindValidNode(currentNode.NodeId);
            if (validNode != null)
            {
                IceLogging.Debug("We've found a valid node! Time to pathfind/interact with it", tag);
                NodeCheckIndex = 0; // Reset for next time
                P.TaskManager.Enqueue(() => Travel_MoveAndInteract(validNode), "Checking Travel Kind");
                return true;
            }

            IceLogging.Debug($"No valid node [ID: {currentNode.NodeId}] at location {NodeCheckIndex}, moving to next location", tag);
            NodeCheckIndex += 1;
            return false;
        }

        private static bool Travel_MoveAndInteract(IGameObject node)
        {
            const string tag = "Travel: Move -> Interact";

            if (P.navTask.IsBusy)
                return false;

            var currentNode = GatherRoute[RouteIndex];
            var targetLocation = currentNode.Locations.FirstOrDefault(x => x.Position == node.Position);

            if (targetLocation is null)
            {
                IceLogging.Error("We seem to be getting an invalid node???", tag);
                IceLogging.Error($"Current node: {currentNode.NodeId} doesn't seem to have a valid location. Please report this and the item you were trying to gather", tag);
                SchedulerMain.Fail($"Node {currentNode.NodeId} has no valid location");
                return true;
            }

            if (Player.DistanceTo(node) < 3.4)
            {
                P.TaskManager.Enqueue(() => InteractWithNode(currentNode.NodeId), "Interacting with node");
                return true;
            }

            // We still need to move closer to the node, so we're going to do that
            TravelUtil.Gathering_TravelToNode(targetLocation, currentNode.NodeId);
            return false;
        }

        private static bool InteractWithNode(uint nodeId)
        {
            SelectedProfile = null;

            var targetNode = Svc.Objects.FirstOrDefault(x => x.BaseId == nodeId && x.IsTargetable);

            var normalWindowReady = Svc.Condition[ConditionFlag.Gathering]
                && GenericHelpers.TryGetAddonMaster<Utilities.ExtraAddons.Gathering>("Gathering", out var gather)
                && gather.IsAddonReady;

            var collectableWindowReady = GenericHelpers.TryGetAddonMaster<GatheringMasterpiece>("GatheringMasterpiece", out var collectable)
                && collectable.IsAddonReady;

            if (normalWindowReady || collectableWindowReady)
            {
                IceLogging.Info($"Gathering window is now visible, continuing onto GatheringInteraction Task");
                RouteIndex += 1;
                return true;
            }

            if (targetNode == null)
            {
                IceLogging.Debug("Somehow we've gotten here, and we shouldn't be here. Adding 1 to the counter and returning");
                RouteIndex += 1;
                return true;
            }

            if (Player.IsJumping)
                return false;

            if (UseConsumables())
                return false;

            if (UseCordial())
                return false;

            var requirement = Gather_Helper.GetGpRequirement();
            var allowedToInteract = requirement is not { } req || req.IsMet(Utils.GetGp());

            if (!allowedToInteract)
            {
                if (EzThrottler.Throttle("Waiting for gp???"))
                    IceLogging.Debug($"Waiting for gp to refill currently. . . currently at: {Utils.GetGp()}");

                return false;
            }

            if (EzThrottler.Throttle("Target + Interaction throttle"))
            {
                Utils.TargetgameObject(targetNode);
                Utils.InteractWithObject(targetNode);
            }

            return false;
        }

        #endregion

        #region Gathering

        private static unsafe bool GatheringInteraction()
        {
            const string tag = "Gather Route: Interaction";

            var routeSelected = Gather_Helper.GatherRoute;
            var routeId = routeSelected.routeId;
            var itemId = routeSelected.itemId;

            if (C.SafetyDelay_Gather && !EzThrottler.Throttle("Safety Throttle", C.Gather_DelayTimer))
                return false;

            if (P.navmesh.IsRunning() && EzThrottler.Throttle("Stopping navmesh, cause we shouldn't be running"))
                P.navmesh.PathStop();

            // Progress is locked in by SyncProgress and only holds targeted items.
            // Not tracked (removed from the list mid-run) is treated the same as done.
            var progress = Gather_Helper.ItemProgress.FirstOrDefault(x => x.ItemId == itemId);
            if (progress == null || progress.IsComplete || Utils.GetFreeSlotCount() <= 1)
                return FinishItem(progress?.Mode, tag);

            if (!Svc.Condition[ConditionFlag.Gathering])
            {
                ReplayRecorder.EndNode("Gathering window closed");
                UpdateGatherBuffs();

                if (IsTimedRoute())
                    Gather_Helper.GatheredTimedRoutes.Add(routeId);

                return true;
            }

            ReplayRecorder.EnsureNode(itemId, routeId, GetRouteKind(routeId));
            ReplayRecorder.TrackExecuting(Svc.Condition[ConditionFlag.ExecutingGatheringAction]);

            // A gathering action is in flight, so nothing else can be sent. Count whatever we just cast and wait.
            if (Svc.Condition[ConditionFlag.ExecutingGatheringAction])
            {
                CountCompletedCast();
                return false;
            }

            if (GenericHelpers.TryGetAddonMaster<Utilities.ExtraAddons.Gathering>(out var gather) && gather.IsAddonReady)
                return HandleNormalGather(gather, itemId, progress.Remaining);

            if (GenericHelpers.TryGetAddonMaster<GatheringMasterpiece>(out var masterpiece) && masterpiece.IsAddonReady)
            {
                HandleCollectable(masterpiece, progress.Remaining);
                return false;
            }

            if (GenericHelpers.TryGetAddonMaster<SelectYesno>(out var yesNo) && yesNo.IsAddonReady)
            {
                yesNo.Yes();
                return false;
            }

            // No window at all: the node closed or is transitioning (a revisit does this). Next window re-plans.
            SelectedProfile = null;
            return false;
        }

        private static unsafe bool HandleNormalGather(Utilities.ExtraAddons.Gathering gather, uint itemId, int remaining)
        {
            const string tag = "Gather Route: Interaction";

            var routeId = Gather_Helper.GatherRoute.routeId;

            if (EzThrottler.Throttle("Gathering Log Time"))
                IceLogging.Debug("Currently in a gathering state! And the gathering window is open", tag);

            // We're targeting a node that belongs to a different route, so close the window
            var currentTarget = Svc.Objects.LocalPlayer?.TargetObject;
            if (currentTarget != null)
            {
                var targetRoute = Gather_Util.Sheet_RouteInfo.FirstOrDefault(x => x.Value.NodeIds.Contains(currentTarget.BaseId));
                if (targetRoute.Key != routeId)
                {
                    if (EzThrottler.Throttle("Exiting Wrong Route"))
                    {
                        IceLogging.Verbose("We seem to be in a gathering window that doesn't contain this item, so we're just gonna get out of this...", tag);
                        Ecom_Handler.CloseWindow(gather.Base);
                    }
                    return false;
                }
            }

            ReplayRecorder.SetState((int)Utils.GetGp(), (int)gather.CurrentIntegrity, (int)gather.TotalIntegrity);
            RecordWindowItems(gather);

            if (gather.CurrentIntegrity == 0)
                return false;

            var goalItem = gather.GatheredItems.FirstOrDefault(x => IsGoalItem(x.ItemID, itemId));
            if (goalItem == null)
                return HandleMissingGoalItem(gather, itemId);

            // The skill was cast earlier and the goal item is in the window now, so it worked
            if (_hiddenCast && !_hiddenReported)
            {
                _hiddenReported = true;
                ReplayRecorder.Note("Hidden item is now in the window");
            }

            // Buff -> gather. Collectables have their own system, and timed nodes may be holding GP.
            if (!Gather_Helper.ShouldHoldGpForTimedNode() && !goalItem.IsCollectable)
            {
                var buffing = Utils.IsCrystal(itemId)
                    ? Crystal_BuffCheck(gather.TotalIntegrity == gather.CurrentIntegrity)
                    : PlanAndCheckBuffs(gather, GetRouteKind(routeId), goalItem.ItemID, goalItem.GatherChance, goalItem.BoonChance, goalItem.GatherAmount, remaining);

                if (buffing)
                    return false;
            }

            // One gather call per tick
            if (EzThrottler.Throttle($"Gathering Item: {goalItem.ItemID}"))
            {
                ReplayRecorder.NoteGather(goalItem.ItemID);
                goalItem.Gather();
            }

            return false;
        }

        /// <summary>Writes the window's item list to the replay. Only does real work when an item appears or disappears.</summary>
        private static void RecordWindowItems(Utilities.ExtraAddons.Gathering gather)
        {
            // ItemID is cheap to read, so the ids decide whether anything changed. The text nodes are only read when it did.
            var items = gather.GatheredItems.Where(x => x.ItemID != 0).ToList();
            var signature = string.Join(",", items.Select(x => x.ItemID));

            ReplayRecorder.WindowItems(signature, () => items.Select(x =>
            {
                try
                {
                    return new ReplayItemSlot
                    {
                        ItemId = x.ItemID,
                        Name = x.ItemName,
                        GatherChance = x.GatherChance,
                        BoonChance = x.BoonChance,
                        Amount = x.GatherAmount,
                        Hidden = x.IsHidden,
                        Rare = x.IsRare,
                        Collectable = x.IsCollectable,
                        Enabled = x.IsEnabled,
                    };
                }
                catch (Exception ex)
                {
                    // A text node that isn't there must never break gathering
                    IceLogging.Verbose($"Couldn't read window item {x.ItemID}: {ex.Message}", "Replay");
                    return new ReplayItemSlot { ItemId = x.ItemID, Name = $"Item {x.ItemID}" };
                }
            }).ToList());
        }

        private static bool IsGoalItem(uint gatheredItemId, uint goalItemId)
        {
            if (gatheredItemId == goalItemId)
                return true;

            if (!Gather_Util.Sheet_ItemInfo.TryGetValue(goalItemId, out var goalInfo))
                return false;

            // goalInfo is the reduce result; ReducedFromItemIds holds every source that feeds it
            return goalInfo.ReducedFromItemIds.Contains(gatheredItemId);
        }

        private static bool HandleMissingGoalItem(Utilities.ExtraAddons.Gathering gather, uint itemId)
        {
            const string tag = "Gather Route: Interaction";

            var routeInfo = Gather_Util.Sheet_RouteInfo[Gather_Helper.GatherRoute.routeId];
            var hiddenItems = routeInfo.ItemIds.Where(x => Gather_Util.Sheet_ItemInfo[x].IsHidden).ToList();

            if (!HiddenUsed && gather.GatheredItems.Any(x => hiddenItems.Contains(x.ItemID)))
            {
                HiddenUsed = true;
                ReplayRecorder.Decision("HiddenFinder", "skipped: a hidden item is already in the window");
            }

            if (!Gather_Util.Sheet_ItemInfo.TryGetValue(itemId, out var itemInfo))
                return false;

            if (EzThrottler.Throttle("Throttle: Hidden Item"))
                IceLogging.Verbose("Hidden item wasn't found. .. and it exist in the sheets", tag);

            if (itemInfo.IsHidden)
            {
                if (CanCheckHidden(out var reason, out var detail))
                {
                    UseHiddenSkill();
                    return false;
                }

                ReplayRecorder.Decision("HiddenFinder", $"skipped: {reason}", detail);
            }

            // For some reason or another, we need to just gather the crystals (probably not available)
            // Skip any we're capped on as its a hard cap and can gather
            var crystal = gather.GatheredItems.FirstOrDefault(x => Utils.IsCrystal(x.ItemID) && x.IsEnabled && !Utils.IsCrystalCapped(x.ItemID));
            if (crystal != null)
            {
                ReplayRecorder.Decision("Goal item", "missing: gathering a crystal instead");

                if (EzThrottler.Throttle($"Gathering Backup: {crystal.ItemID}"))
                    crystal.Gather();

                IceLogging.Verbose($"We found a crystal to fallback on, gathering that ({crystal.ItemID})", tag);
                return false;
            }

            // WELL WE NEED TO GATHER SOMETHING... OR. HERE ME OUT. We just abandon and error
            ReplayRecorder.Note("No goal item, hidden item or crystal available, stopping");
            IceLogging.Error("No backup item could be found (aka crystals?) and no actual. . . hidden item can be found. So we gonna skeet out :fguns:", tag);
            SchedulerMain.DisablePlugin();
            return true;
        }

        private static void HandleCollectable(GatheringMasterpiece gatherMaster, int remaining)
        {
            const string tag = "Gather Route: Interaction";

            // Reducible items count toward the goal through the reduction, not 1:1 per Collect, so the remaining count doesn't apply
            var collectableId = gatherMaster.ItemID;
            var isReducible = Gather_Util.ReducableItems.Any(x => x.ItemId == collectableId || x.SublimeItemId == collectableId);
            var remainingNeeded = isReducible ? 0 : remaining;

            // Fresh state every tick: progression, integrity, GP and buffs all change after each action
            var state = Gather_Util.BuildState(gatherMaster, remainingNeeded, C.Collectable_Target);

            // Only built when something actually logs it, so ticks that do nothing stay cheap
            string Status() =>
                $"Prog: {state.Progression} | Min: {state.MinCollectability} | Att: {state.Attempts}/{state.MaxAttempts} | GP: {state.AvailableGp} | " +
                $"Need: {state.RemainingNeeded} | Inv: {Utils.GetItemCount(collectableId)} | Scrutiny: {state.HasScrutiny} | Priming: {state.HasPriming} | Star: {state.HasCollectorStandard} | Swirl: {state.HasHighStandard}";

            ReplayRecorder.MarkCollectable();
            ReplayRecorder.SetState(state.AvailableGp, state.Attempts, state.MaxAttempts);

            // The planner replans every tick, so there's no per-node preset to pick or store.
            // Log the plan (throttled) so you can see what it's aiming for.
            if (EzThrottler.Throttle("Collectable plan log", 2000))
            {
                var plan = Gather_Util.CollectableSolver.GetPlan(state);
                IceLogging.Debug(
                    $"Collectable plan: Setting {C.Collectable_Target} -> state target {state.TargetTier} (index {state.TargetIndex} of {state.Tiers.Length}) | " +
                    $"Plan target {plan.TargetTier} (Feasible: {plan.Feasible}) | Planned GP: {plan.GpCost} | Next: {plan.NextAction} | " +
                    $"Item: {gatherMaster.ItemName} | Reducible: {isReducible} | Tiers: {state.MinCollectability}/{state.MidCollectability}/{state.HighCollectability} | " +
                    $"Scour: {state.ScourPower} | Met: {state.MeticulousPower} | Brazen: {state.BrazenPowerMin} | {Status()}",
                    tag);

                // Only written when the plan's reason changes, so this doesn't flood the replay
                ReplayRecorder.Decision(
                    "Plan",
                    $"target {plan.TargetTier}, feasible {plan.Feasible}, next {plan.NextAction}",
                    $"GP cost {plan.GpCost}, setting {C.Collectable_Target}, tiers {state.MinCollectability}/{state.MidCollectability}/{state.HighCollectability}");
            }

            var nextAction = Gather_Util.CollectableSolver.GetNextAction(state);
            if (nextAction is null)
            {
                if (EzThrottler.Throttle("Collectable no action"))
                    IceLogging.Debug($"Solver returned no action | {Status()}", tag);
                return;
            }

            var actionId = Gather_Util.CollectableSolver.GetActionId(nextAction.Value, Player.Job);
            if (actionId is null)
            {
                if (EzThrottler.Throttle("Collectable no action id"))
                    IceLogging.Warning($"No action id for {nextAction.Value} on job {Player.Job}", tag);
                return;
            }

            // Sending is exactly as before: every pass the throttle allows. The game ignores the repeats while the action animates.
            if (!UseGatheringAction(actionId.Value))
                return;

            IceLogging.Verbose($"Collectable: {nextAction.Value} | {Status()} | Scour: {state.ScourPower} | Met: {state.MeticulousPower}", tag);

            // Only the replay is deduped: one entry per real action, not one per resend
            var sig = $"{nextAction.Value}|{state.Progression}|{state.Attempts}|{state.AvailableGp}|{state.RemainingNeeded}|{state.HasScrutiny}|{state.HasPriming}|{state.HasCollectorStandard}|{state.HasHighStandard}";
            if (sig == _lastCollectableSig)
                return;

            _lastCollectableSig = sig;
            ReplayRecorder.BuffCast(nextAction.Value.ToString(), Status(), state.Progression);
        }

        #endregion

        #region Gathering Buffs

        /// <summary>Zeroes the per-plan cast counters. Runs every time a plan is made, so a revisit never inherits the old plan's casts.</summary>
        private static void ResetCasts()
        {
            foreach (var key in CurrentUse.Keys.ToList())
                CurrentUse[key] = 0;

            BuffCounter = GatherBuffId.Unknown;
        }

        /// <summary>Runs when the node/item is done.</summary>
        private static void UpdateGatherBuffs()
        {
            HiddenUsed = false;
            _toggleHidden = false;
            _hiddenCast = false;
            _hiddenReported = false;
            _lastCollectableSig = "";
            _lastCrystalSig = "";
            _byiiBaseAmount = 0;
            _byiiReported = false;
            _byiiCredit = 0;
            _byiiGateStartMs = 0;
            _byiiMeasure = false;
            _planHadBYII = false;
            ResetCasts();
            ResetConsumableAttempts();
        }

        private static void CountCompletedCast()
        {
            if (_toggleHidden)
            {
                HiddenUsed = true;
                _toggleHidden = false;
            }

            if (BuffCounter == GatherBuffId.Unknown)
                return;

            CurrentUse[BuffCounter] += 1;
            BuffCounter = GatherBuffId.Unknown;
        }

        private static bool PlanAndCheckBuffs(Utilities.ExtraAddons.Gathering gather, GatherNodeKind kind, uint windowItemId, int gatherChance, int boonChance, int gatherAmount, int remaining)
        {
            SelectedProfile ??= PlanProfile(gather, kind, windowItemId, gatherChance, boonChance, gatherAmount, remaining);

            // Runs before the buff check, so the amount remembered is the one from before this tick's cast.
            // When the game's bonus differs from what the plan assumed, plan again right now (same tick, so no swing happens on the old plan).
            if (LearnBYII(windowItemId, gatherAmount))
                SelectedProfile = PlanProfile(gather, kind, windowItemId, gatherChance, boonChance, gatherAmount, remaining);

            return CheckGatheringBuffs(gather, windowItemId);
        }

        /// <summary>
        /// Bountiful Yield raises the per-swing amount the window shows. So: remember the amount while BYII is down,
        /// and once the status is up the difference is the real bonus for this node.
        /// Returns true when that bonus is different from what the plan assumed, meaning the plan should be redone.
        /// </summary>
        private static bool LearnBYII(uint windowItemId, int shownAmount)
        {
            var byii = Gather_Util.GathActionDict[GatherBuffId.BYII];
            var statusUp = Utils.HasStatusId(byii.StatusId)
                        || (byii.StatusId2 != 0 && Utils.HasStatusId(byii.StatusId2));

            // Not buffed: this is the plain amount, and the next cast can be measured again
            if (!statusUp)
            {
                _byiiBaseAmount = shownAmount;
                _byiiReported = false;
                return false;
            }

            // Already measured this cast, or we never saw a plain amount to compare against
            if (_byiiReported || _byiiBaseAmount <= 0)
                return false;

            // bonus <= 0 means the shown amount has NOT gone up yet (the window hasn't refreshed), so check again next tick
            var bonus = shownAmount - _byiiBaseAmount;
            if (bonus <= 0)
                return false;

            _byiiReported = true;
            LearnedBYII[windowItemId] = bonus;

            IceLogging.Debug($"Bountiful Yield gave +{bonus} (plan assumed +{_plannedBYII}) | shown {_byiiBaseAmount} -> {shownAmount}", "Gather: BYII");
            ReplayRecorder.BYIIObserved(_plannedBYII, bonus);

            // Same as assumed AND the plan already casts it = the plan is right, nothing to redo
            if (bonus == _plannedBYII && _planHadBYII)
                return false;

            // The buff that's up right now already covers the next swing, so that bonus counts as free in the new plan
            _byiiCredit = bonus;
            return true;
        }

        private static GatherProfile PlanProfile(Utilities.ExtraAddons.Gathering gather, GatherNodeKind kind, uint windowItemId, int gatherChance, int boonChance, int gatherAmount, int remaining)
        {
            const string tag = "Gather: Plan";

            ResetCasts();

            // Rules match on the node's MAX integrity at window open, not whatever Ageless has done to it since
            var maxIntegrity = (int)gather.TotalIntegrity;
            var integrity = (int)gather.CurrentIntegrity;
            var gp = Utils.GetGp();

            var ruleReasons = new List<string>();
            foreach (var rule in C.User_BuffRules)
            {
                var why = Gather_Util.RuleMatcher.WhyNot(rule, kind, maxIntegrity);
                var line = why is null ? $"Rule '{rule.Name}' matches" : $"Rule '{rule.Name}' skipped: {why}";

                IceLogging.Verbose(line, tag);
                ruleReasons.Add(line);
            }

            var (settings, matched) = Gather_Util.RuleMatcher.Resolve(C.User_AutoProfile, C.User_BuffRules, kind, maxIntegrity);
            var profileName = matched?.Name ?? "Default";
            IceLogging.Debug($"Using profile '{profileName}' | Kind: {kind} | Max integrity: {maxIntegrity}", tag);

            // A bonus the game showed on an earlier node of this item beats the BYII_Amount setting.
            // The profile is copied first, so the user's saved profile is never changed.
            if (LearnedBYII.TryGetValue(windowItemId, out var learned) && learned != settings.BYII_Amount)
            {
                var copy = JsonConvert.DeserializeObject<Gather_Util.ProfileClass>(JsonConvert.SerializeObject(settings));
                if (copy != null)
                {
                    copy.BYII_Amount = learned;
                    settings = copy;
                    IceLogging.Debug($"Using the learned Bountiful Yield bonus +{learned} for this item", tag);
                }
            }

            _plannedBYII = settings.BYII_Amount;

            // After a mid-node re-plan the live Bountiful Yield will pay out on the next swing without a new cast,
            // so the planner only has to find the rest. Max(...,0) keeps the need from going below zero.
            var credit = _byiiCredit;
            _byiiCredit = 0;
            var planNeed = Math.Max(remaining - credit, 0);

            // The window's amount already includes the live Bountiful Yield bonus. That bonus is counted once, as the credit above,
            // so the per-swing yield the planner sees has to be the plain amount, or every swing would be overestimated by it.
            var baseYield = Math.Max(gatherAmount - credit, 1);

            var result = Gather_Util.PlanBuffs(
                Player.Level,
                gp,
                integrity,
                maxIntegrity,
                gatherChance,
                boonChance,
                planNeed,
                settings,
                baseYield,
                profileName);

            // The planner only saw the need minus the free bonus, so add it back to show the true total for this node
            var expectedTotal = result.ExpectedYield + credit;

            _planHadBYII = result.Profile.BuffPriority.Contains(GatherBuffId.BYII);
            _byiiMeasure = false;

            // Only worth casting Bountiful Yield early just to read it when ALL of these are true:
            //  - this item's bonus isn't known yet (and this isn't already the re-plan after measuring)
            //  - the profile allows Bountiful Yield on this node
            //  - the level has a variable bonus
            //  - at least one possible bonus (+1, +2 or +3) would lead to a plan that actually casts it
            if (credit == 0
                && !LearnedBYII.ContainsKey(windowItemId)
                && Player.Level >= BountifulIILevel
                && settings.IsAllowed(GatherBuffId.BYII, maxIntegrity))
            {
                for (var possibleBonus = 1; possibleBonus <= 3 && !_byiiMeasure; possibleBonus++)
                {
                    // The plan above already covers the bonus the setting assumed
                    if (possibleBonus == settings.BYII_Amount)
                    {
                        _byiiMeasure = _planHadBYII;
                        continue;
                    }

                    var variant = JsonConvert.DeserializeObject<Gather_Util.ProfileClass>(JsonConvert.SerializeObject(settings));
                    if (variant == null)
                        continue;

                    variant.BYII_Amount = possibleBonus;

                    var variantResult = Gather_Util.PlanBuffs(
                        Player.Level, gp, integrity, maxIntegrity, gatherChance, boonChance, planNeed, variant, baseYield, profileName);

                    _byiiMeasure = variantResult.Profile.BuffPriority.Contains(GatherBuffId.BYII);
                }

                IceLogging.Debug(_byiiMeasure
                    ? "Some possible Bountiful Yield bonus would use it, so it's cast first to read the real bonus"
                    : "No possible Bountiful Yield bonus would use it on this node, so it isn't measured", tag);
            }

            var buffs = result.Profile.BuffPriority
                .Select(buff =>
                {
                    var count = result.Profile.MaxUses.GetValueOrDefault(buff);
                    return count == 0 ? $"{buff} (proc)" : $"{buff} x{count}";
                })
                .ToList();

            IceLogging.Debug(
                $"Plan: [{string.Join(", ", buffs)}] | Expected: {expectedTotal:F1} / need {remaining} | GP: {result.Gp} | Meets need: {result.MeetsNeed}" + (credit > 0 ? $" | re-planned after Bountiful Yield gave +{credit}" : ""),
                tag);

            var active = Gather_Util.GathActionDict
                .Where(kv => kv.Value.StatusId != 0 && Utils.HasStatusId(kv.Value.StatusId))
                .Select(kv => kv.Key.ToString())
                .ToList();

            ReplayRecorder.AddPlan(new ReplayPlan
            {
                Level = Player.Level,
                Gp = (int)gp,
                Attempts = integrity,
                MaxIntegrity = maxIntegrity,
                GatherChance = gatherChance,
                BoonChance = boonChance,
                BaseYield = baseYield,
                Need = remaining,
                ProfileName = profileName,
                RuleReasons = ruleReasons,
                ProfileJson = JsonConvert.SerializeObject(settings),
                Buffs = buffs,
                ExpectedYield = expectedTotal,
                PlannedGp = result.Gp,
                MeetsNeed = result.MeetsNeed,
                ActiveBuffs = active,
            }, $"Expected {expectedTotal:F1} / need {remaining}, GP {result.Gp}" + (credit > 0 ? $" (re-planned: Bountiful Yield gave +{credit})" : ""));

            return result.Profile;
        }

        private static unsafe bool CanCheckHidden(out string reason, out string detail)
        {
            reason = "";
            detail = "";

            if (HiddenUsed)
            {
                reason = "already used on this node";
                return false;
            }

            var am = ActionManager.Instance();
            if (am is null)
            {
                reason = "action manager unavailable";
                return false;
            }

            var actionInfo = Gather_Util.GathActionDict[GatherBuffId.HiddenFinder];
            var actionId = actionInfo.ClassAction[Player.Job].ActionId;

            var status = am->GetActionStatus(ActionType.Action, actionId, checkRecastActive: false, checkCastingActive: false);
            if (status != 0)
            {
                reason = "action not usable right now";
                detail = $"status {status}";
                return false;
            }

            var gp = Utils.GetGp();

            // gp < RequiredGp reads "we have less than the cost"
            if (gp < actionInfo.RequiredGp)
            {
                reason = "not enough GP";
                detail = $"have {gp}, need {actionInfo.RequiredGp}";
                return false;
            }

            return true;
        }

        private static unsafe void UseHiddenSkill()
        {
            var actionInfo = Gather_Util.GathActionDict[GatherBuffId.HiddenFinder];
            var actionId = actionInfo.ClassAction[Player.Job].ActionId;

            if (EzThrottler.Throttle("Use buff", 100))
            {
                IceLogging.Verbose("Using the hidden skill", "Gathering Action");

                if (!_hiddenCast)
                {
                    _hiddenCast = true;
                    ReplayRecorder.BuffCast(GatherBuffId.HiddenFinder.ToString(), $"GP {Utils.GetGp()}");
                }

                ActionManager.Instance()->UseAction(ActionType.Action, actionId);
            }

            _toggleHidden = true;
        }

        /// <summary>
        /// True while we're waiting to see how much Bountiful Yield gives on this item. The plan uses a guess for that bonus,
        /// so nothing else is cast until the real number is in and the rest of the plan has been redone with it.
        /// Only applies when the plan uses Bountiful Yield and this item's bonus isn't known yet.
        /// </summary>
        private static bool IsMeasuringBYII(GatherProfile profile, uint windowItemId)
        {
            if (!_byiiMeasure || _byiiReported || LearnedBYII.ContainsKey(windowItemId))
            {
                _byiiGateStartMs = 0;
                return false;
            }

            var now = Environment.TickCount64;
            if (_byiiGateStartMs == 0)
                _byiiGateStartMs = now;

            // Waited this long and still no reading: stop holding things up and carry on with the plan as it is
            return now - _byiiGateStartMs < ByiiMeasureTimeoutMs;
        }

        private static bool CheckGatheringBuffs(Utilities.ExtraAddons.Gathering gathering, uint windowItemId)
        {
            const string tag = "Gather: Check Buffs";

            var profile = SelectedProfile;
            if (profile is null)
                return false;

            var job = Player.Job;
            var currentGp = Utils.GetGp();
            var missingDur = gathering.CurrentIntegrity != gathering.TotalIntegrity;

            // Measuring comes first: Bountiful Yield is cast and its bonus read before anything else is spent or any swing is made.
            // Once the bonus is in, the plan is redone and this stops applying.
            if (IsMeasuringBYII(profile, windowItemId))
            {
                var byiiInfo = Gather_Util.GathActionDict[GatherBuffId.BYII];
                var byiiUp = Utils.HasStatusId(byiiInfo.StatusId) || (byiiInfo.StatusId2 != 0 && Utils.HasStatusId(byiiInfo.StatusId2));

                // Already cast (or sent): just wait for the window to show the new amount
                if (byiiUp || BuffCounter == GatherBuffId.BYII || CurrentUse[GatherBuffId.BYII] > 0)
                {
                    ReplayRecorder.Decision("Gather", "waiting: reading the Bountiful Yield bonus first");
                    return true;
                }

                // Can't cast it right now (not enough GP or level), so there's nothing to measure
                if (currentGp < byiiInfo.RequiredGp || Player.Level < byiiInfo.RequiredLv)
                {
                    _byiiMeasure = false;
                    ReplayRecorder.Decision("BYII", "skipped: can't measure, not enough GP or level");
                }
                else
                {
                    if (!UseGatheringAction(byiiInfo.ClassAction[job].ActionId))
                    {
                        ReplayRecorder.Decision("BYII", "waiting: previous cast still on cooldown");
                        return true;
                    }

                    IceLogging.Verbose("Casting Bountiful Yield first to read its bonus", tag);
                    ReplayRecorder.BuffCast(GatherBuffId.BYII.ToString(), $"GP {currentGp}, integrity {gathering.CurrentIntegrity}/{gathering.TotalIntegrity} (measuring bonus)");
                    BuffCounter = GatherBuffId.BYII;
                    return true;
                }
            }

            foreach (var buff in profile.BuffPriority)
            {
                var label = buff.ToString();
                var actionInfo = Gather_Util.GathActionDict[buff];
                var maxUse = profile.MaxUses.GetValueOrDefault(buff);

                // 0 means no cap; otherwise skip once we've used it as many times as planned
                if (maxUse != 0 && CurrentUse[buff] >= maxUse)
                {
                    IceLogging.Verbose($"Skipping: {buff} due to reaching max use ({maxUse})", tag);
                    ReplayRecorder.Decision(label, "skipped: planned casts used up", $"{CurrentUse[buff]}/{maxUse}");
                    continue;
                }

                // GP on hand is BELOW the cost -> can't cast
                if (currentGp < actionInfo.RequiredGp)
                {
                    IceLogging.Verbose($"Skipping: {buff} due to not enough GP", tag);
                    ReplayRecorder.Decision(label, "skipped: not enough GP", $"have {currentGp}, need {actionInfo.RequiredGp}");
                    continue;
                }

                if (!IsBuffDue(buff, actionInfo, missingDur, out var reason))
                {
                    IceLogging.Verbose($"Skipping: {buff} is not due ({reason})", tag);
                    ReplayRecorder.Decision(label, $"skipped: {reason}");
                    continue;
                }

                // The cast was throttled, so a buff is still due. Report "busy" so the caller waits instead of gathering without it.
                if (!UseGatheringAction(actionInfo.ClassAction[job].ActionId))
                {
                    IceLogging.Verbose($"Waiting: {buff} is due but a cast is still on cooldown", tag);
                    ReplayRecorder.Decision(label, "waiting: previous cast still on cooldown");
                    return true;
                }

                IceLogging.Verbose($"Used buff: {buff}", tag);
                ReplayRecorder.BuffCast(label, $"GP {currentGp}, integrity {gathering.CurrentIntegrity}/{gathering.TotalIntegrity}");
                BuffCounter = buff;

                // Wise and Bountiful don't end the check, so the gather itself can follow right away
                if (buff is GatherBuffId.BonusIntegrity_Chance or GatherBuffId.BYII)
                    continue;

                return true;
            }

            return false;
        }

        private static bool IsBuffDue(GatherBuffId buff, Gather_Util.GatheringActions actionInfo, bool missingDur, out string reason)
        {
            reason = "";

            // Ageless Words only helps if we're actually missing integrity
            if (buff == GatherBuffId.BonusIntegrity)
            {
                if (missingDur)
                    return true;

                reason = "integrity is full";
                return false;
            }

            // Wise is a free proc: it needs the status to be up, and it's wasted at full integrity
            if (buff == GatherBuffId.BonusIntegrity_Chance)
            {
                if (!missingDur)
                {
                    reason = "integrity is full";
                    return false;
                }

                if (!Utils.HasStatusId(actionInfo.StatusId))
                {
                    reason = "Wise proc is not up";
                    return false;
                }

                return true;
            }

            // Field Mastery, Gift, Yield and Tidings are start-of-node buffs only
            if (missingDur && FullIntegrityOnly.Contains(buff))
            {
                reason = "start-of-node buff and integrity has already dropped";
                return false;
            }

            // everything else is due when its status isn't already up
            var statusUp = Utils.HasStatusId(actionInfo.StatusId)
                        || (actionInfo.StatusId2 != 0 && Utils.HasStatusId(actionInfo.StatusId2));

            if (statusUp)
            {
                reason = "status is already up";
                return false;
            }

            return true;
        }

        private static unsafe bool UseGatheringAction(uint ActionId)
        {
            if (!EzThrottler.Throttle("Using Gathering Action", 500))
                return false;

            ActionManager.Instance()->UseAction(ActionType.Action, ActionId);
            return true;
        }

        private static unsafe bool Crystal_BuffCheck(bool maxDurability)
        {
            var job = Player.Job;
            var level = Player.Level;
            var gp = Utils.GetGp();

            var givingLand = Gather_Util.GathActionDict[GatherBuffId.GivingLand];
            var twelveBounty = Gather_Util.GathActionDict[GatherBuffId.TwelveBounty];

            bool CanCast(Gather_Util.GatheringActions action) => gp >= action.RequiredGp && level >= action.RequiredLv;

            string CostDetail(Gather_Util.GatheringActions action) => $"have {gp} GP at level {level}, need {action.RequiredGp} GP at level {action.RequiredLv}";

            void Cast(GatherBuffId id, Gather_Util.GatheringActions action)
            {
                if (!EzThrottler.Throttle("Using Action", 100))
                    return;

                var actionId = action.ClassAction[job].ActionId;
                ActionManager.Instance()->UseAction(ActionType.Action, actionId);

                // The same cast can be resent before the game registers it. GP drops once it lands, so action + GP is one real cast.
                var sig = $"{actionId}|{gp}";
                if (sig == _lastCrystalSig)
                    return;

                _lastCrystalSig = sig;
                ReplayRecorder.BuffCast(id.ToString(), $"GP {gp}");
            }

            if (maxDurability)
            {
                var cooldown = BuffCooldown(givingLand.ClassAction[job].ActionId);

                // Hold window: Giving Land is about to come off cooldown, so don't burn Twelve's Bounty yet
                if (cooldown < C.CrystalBuff_HoldSeconds)
                {
                    if (Utils.HasStatusId(givingLand.StatusId))
                    {
                        ReplayRecorder.Decision(nameof(GatherBuffId.GivingLand), "skipped: status is already up");
                        return false;
                    }

                    // Not ready yet (cooldown != 0), so keep going without a buff
                    if (cooldown != 0)
                    {
                        ReplayRecorder.Decision(nameof(GatherBuffId.GivingLand), "waiting: coming off cooldown soon, holding Twelve's Bounty", $"{cooldown:F1}s left, hold window {C.CrystalBuff_HoldSeconds}s");
                        return false;
                    }

                    if (!CanCast(givingLand))
                    {
                        ReplayRecorder.Decision(nameof(GatherBuffId.GivingLand), "skipped: not enough GP or level", CostDetail(givingLand));
                        return false;
                    }

                    Cast(GatherBuffId.GivingLand, givingLand);
                    return true;
                }

                if (Utils.HasStatusId(twelveBounty.StatusId))
                {
                    ReplayRecorder.Decision(nameof(GatherBuffId.TwelveBounty), "skipped: status is already up");
                    return false;
                }

                if (!CanCast(twelveBounty))
                {
                    ReplayRecorder.Decision(nameof(GatherBuffId.TwelveBounty), "skipped: not enough GP or level", CostDetail(twelveBounty));
                    return false;
                }

                Cast(GatherBuffId.TwelveBounty, twelveBounty);
                return true;
            }

            var wise = Gather_Util.GathActionDict[GatherBuffId.BonusIntegrity_Chance];
            var ageless = Gather_Util.GathActionDict[GatherBuffId.BonusIntegrity];

            // Wise is a free proc, so use it whenever the status is up
            if (Utils.HasStatusId(wise.StatusId))
            {
                Cast(GatherBuffId.BonusIntegrity_Chance, wise);
                return true;
            }

            // Ageless only while Giving Land is running
            if (!Utils.HasStatusId(givingLand.StatusId))
            {
                ReplayRecorder.Decision(nameof(GatherBuffId.BonusIntegrity), "skipped: Giving Land is not running");
                return false;
            }

            if (!CanCast(ageless))
            {
                ReplayRecorder.Decision(nameof(GatherBuffId.BonusIntegrity), "skipped: not enough GP or level", CostDetail(ageless));
                return false;
            }

            Cast(GatherBuffId.BonusIntegrity, ageless);
            return true;
        }

        private static unsafe float BuffCooldown(uint actionId)
        {
            var am = ActionManager.Instance();
            var recast = am->GetRecastGroupDetail(am->GetRecastGroup(1, actionId));

            return recast == null ? 0 : recast->Total - recast->Elapsed;
        }

        #endregion

        #region Cordials

        private static unsafe bool UseCordial()
        {
            const string tag = "Gather: Cordial Check";

            if (!C.CordialConfig.Enable)
                return false;

            var currentRoute = Gather_Helper.GatherRoute;
            var routeInfo = Gather_Util.Sheet_RouteInfo[currentRoute.routeId];

            if (!C.CordialConfig.AllowedNodes.HasFlag(routeInfo.Kind))
                return false;

            var currentGp = Utils.GetGp();

            if (EzThrottler.Throttle("Use Cordial At"))
                IceLogging.Verbose($"Reporting GP: {currentGp:N0} | Minimum GP: {C.Cordial_Amount:N0}");

            // GP is ABOVE the threshold -> we don't need one yet
            if (currentGp > C.Cordial_Amount)
                return false;

            // Cordials share a cooldown, so if the Hi-Cordial is on cooldown none of them can be used
            if (ActionManager.Instance()->GetActionStatus(ActionType.Item, 12669) != 0)
                return false;

            var maxGp = Utils.MaxGp();
            var ordered = C.Cordial_Inverse ? Enumerable.Reverse(Cordials) : Cordials;

            foreach (var (id, name, gpGain) in ordered)
            {
                IceLogging.Verbose($"Checking Cordial: {name}", tag);

                if (Utils.GetItemCount(id) == 0)
                    continue;

                // GP after the cordial would be ABOVE max -> it would overcap
                if (C.Cordial_PreventOvercap && currentGp + gpGain > maxGp)
                {
                    IceLogging.Verbose($"Not going to be using a cordial because we'll overcap | Recovered GP: {currentGp + gpGain} | Max GP: {maxGp}", tag);
                    continue;
                }

                if (TryUseCordial(id, name, tag))
                    return true;
            }

            return false;
        }

        private static unsafe bool TryUseCordial(uint cordialId, string name, string tag)
        {
            var hq = cordialId >= 1_000_000;
            var baseId = hq ? cordialId - 1_000_000 : cordialId;
            var inventory = InventoryManager.Instance();

            foreach (var type in CordialContainers)
            {
                var container = inventory->GetInventoryContainer(type);
                if (container == null)
                    continue;

                for (var i = 0; i < container->Size; i++)
                {
                    var item = container->GetInventorySlot(i);
                    if (item == null || item->ItemId != baseId)
                        continue;

                    if (hq && !item->Flags.HasFlag(InventoryItem.ItemFlags.HighQuality))
                        continue;

                    // The cordial takes a moment to register. Returning true below while this is throttled makes the caller wait, instead of using another one.
                    if (EzThrottler.Throttle("Cordial Usage", 1500))
                    {
                        IceLogging.Verbose($"We're using a cordial: ID: {cordialId} | Name: {name}", tag);
                        AgentInventoryContext.Instance()->UseItem(cordialId, type, (uint)i, 0);
                        ReplayRecorder.Note($"Used {name}");
                    }

                    return true;
                }
            }

            return false;
        }

        #endregion

        #region Food/Potion/Manual Usage

        private enum ConsumableKind
        {
            Food,
            Potion,
            Manual,
        }

        private const int MaxConsumableAttempts = 3;   // uses sent without the status showing up before we give up for now
        private const int ConsumableRetryMs = 2500;    // how long we wait for a use to register before trying again

        private static readonly Dictionary<ConsumableKind, int> ConsumableAttempts = new()
        {
            [ConsumableKind.Food] = 0,
            [ConsumableKind.Potion] = 0,
            [ConsumableKind.Manual] = 0,
        };

        private static void ResetConsumableAttempts()
        {
            foreach (var key in ConsumableAttempts.Keys.ToList())
                ConsumableAttempts[key] = 0;
        }

        // True while we're using or waiting on a consumable, so the caller holds off on interacting
        private static bool UseConsumables()
        {
            var nodeKind = Gather_Util.Sheet_RouteInfo[Gather_Helper.GatherRoute.routeId].Kind;

            return UseConsumable(ConsumableKind.Food, C.FoodConfig, nodeKind)
                || UseConsumable(ConsumableKind.Potion, C.PotionConfig, nodeKind)
                || UseConsumable(ConsumableKind.Manual, C.ManualConfig, nodeKind);
        }

        private static bool UseConsumable(ConsumableKind consumable, ConsumableConfig c, GatherNodeKind nodeKind)
        {
            var tag = $"Gather: {consumable} Check";

            if (!c.Enable || c.ItemId == 0)
                return false;

            if (!c.AllowedNodes.HasFlag(nodeKind))
                return false;

            var statusId = Utils.GetItemStatusId(c.ItemId);
            if (statusId == 0)
                return false;

            // Not up at all -> refresh. Up -> refresh once the time left is LESS than the setting.
            // remaining < SecondsRemaining: the small number is the time left, so "less than" = running low.
            // A setting of 0 means "only when it has fallen off".
            var remaining = Utils.GetStatusRemaining(statusId);
            var needsRefresh = remaining == null || remaining < c.SecondsRemaining;

            if (!needsRefresh)
            {
                ConsumableAttempts[consumable] = 0;
                return false;
            }

            if (ConsumableAttempts[consumable] >= MaxConsumableAttempts)
            {
                if (EzThrottler.Throttle($"Consumable gave up: {consumable}", 30000))
                    IceLogging.Warning($"{consumable}: used {MaxConsumableAttempts} times without the status showing up, giving up until the next node finishes", tag);

                return false;
            }

            var name = ExcelHelper.Sheet_Item[c.ItemId].Name.ToString();

            // Manuals have no HQ version; everything else follows the quality setting
            var hq = false;
            if (consumable == ConsumableKind.Manual)
            {
                if (Utils.GetItemCount(c.ItemId) == 0)
                {
                    if (EzThrottler.Throttle($"Consumable missing: {consumable}", 10000))
                        IceLogging.Verbose($"{name} needs refreshing but none are in the inventory", tag);

                    return false;
                }
            }
            else if (!Utils.TryResolveQuality(c.ItemId, c.Quality, out hq))
            {
                if (EzThrottler.Throttle($"Consumable missing: {consumable}", 10000))
                    IceLogging.Verbose($"{name} needs refreshing but nothing usable for quality mode {c.Quality}", tag);

                return false;
            }

            var itemId = hq ? c.ItemId + 1_000_000 : c.ItemId;

            // Not in the main bags (armory/etc.), so there's nothing we can use right now
            if (!TryFindInventoryItem(itemId, out var containerType, out var slot))
                return false;

            // A use takes a moment to register. Returning true while throttled makes the caller wait instead of using another.
            if (!EzThrottler.Throttle($"Consumable: {consumable}", ConsumableRetryMs))
                return true;

            IceLogging.Verbose($"Using {name}{(hq ? " (HQ)" : "")} | Time left: {remaining?.ToString("F0") ?? "none"}s | Refresh under: {c.SecondsRemaining}s", tag);

            unsafe
            {
                AgentInventoryContext.Instance()->UseItem(itemId, containerType, slot, 0);
            }

            ConsumableAttempts[consumable] += 1;
            ReplayRecorder.Note($"Used {name}{(hq ? " (HQ)" : "")}");
            return true;
        }

        // Finds an exact-quality match: an NQ lookup skips HQ stacks and an HQ lookup skips NQ ones
        private static unsafe bool TryFindInventoryItem(uint itemId, out InventoryType containerType, out uint slot)
        {
            containerType = default;
            slot = 0;

            var hq = itemId >= 1_000_000;
            var baseId = hq ? itemId - 1_000_000 : itemId;

            var inventory = InventoryManager.Instance();
            if (inventory == null)
                return false;

            foreach (var type in CordialContainers)
            {
                var container = inventory->GetInventoryContainer(type);
                if (container == null)
                    continue;

                for (var i = 0; i < container->Size; i++)
                {
                    var item = container->GetInventorySlot(i);
                    if (item == null || item->ItemId != baseId)
                        continue;

                    var isHq = item->Flags.HasFlag(InventoryItem.ItemFlags.HighQuality);
                    if (hq != isHq)
                        continue;

                    containerType = type;
                    slot = (uint)i;
                    return true;
                }
            }

            return false;
        }

        #endregion
    }
}