using GatherChill.ConfigFiles;
using GatherChill.Enums;
using GatherChill.Utilities.GatheringHelpers;
using GatherChill.Utilities.Tools;
using System;
using System.Collections.Generic;
using System.Text;
using static GatherChill.ConfigFiles.Config;

namespace GatherChill.Scheduler.Tasks
{
    internal class Task_Start
    {
        public static void Enqueue()
        {
            P.TaskManager.Enqueue(() => CheckList(), "Checking for items");
        }

        public enum GatherReason
        {
            Timed,      // Legendary / Unspoiled
            Ephemeral,
            Regular,
        }

        private static bool CheckList()
        {
            string tag = "Task Start: Check List";

            var itemList = C.GatherList.Where(x => x.GatherAmount != 0).ToList();
            if (itemList.Count != 0)
            {
                var itemSheet = Gather_Util.Sheet_ItemInfo;
                var routeSheet = Gather_Util.Sheet_RouteInfo;
                var itemConfig = C.ItemRoutes;

                List<Gather_Helper.SelectedRoute> LegendaryItems = new();
                List<Gather_Helper.SelectedRoute> EphemeralItems = new();
                List<Gather_Helper.SelectedRoute> NormalItems = new();

                foreach (var item in itemList)
                {
                    if (itemConfig.TryGetValue(item.ItemId, out var selectedRoutes))
                    {
                        foreach (var route in selectedRoutes.EnabledRoutes)
                        {
                            var routeInfo = routeSheet[route];
                            if (routeInfo.Kind is GatherNodeKind.Legendary or GatherNodeKind.Unspoiled)
                            {
                                LegendaryItems.Add(new() { routeId = route, itemId = item.ItemId });
                            }
                            else if (routeInfo.Kind is GatherNodeKind.Ephemeral)
                            {
                                EphemeralItems.Add(new() { routeId = route, itemId = item.ItemId });
                            }
                            else
                            {
                                NormalItems.Add(new() { routeId = route, itemId = item.ItemId });
                            }
                        }
                    }
                }

                if (LegendaryItems.Count != 0)
                {
                    // TODO: add a check here for the current last checked nodes
                    // What i'm thinking is
                    // -> Keep a list of routes that has been actually gathered at
                    // -> Once the route has been sucessfuly gathered, add it to the list
                    // -> I'd say... every so often (could just run this on the main tick thread, but could have it check every x amount of seconds)
                    // -> Once the timed route has passed it's slot, clear it from the recently gathered list
                    // -> Thay way, the route can be gathered at again
                }

                if (EphemeralItems.Count != 0)
                {
                    // TODO: Really just need to see the first one that is up in the list
                    // REALLY could just... make it check for the shortest time, but I know users are going to want to organize which one they have a higher prio for 
                    // So just going to run down the list and see which one is active
                    // First one we find, teleport -> Start gathering
                    // Might be worth making a setting to either reduce after X amount of items gathered, or do it between nodes....
                    // Decisions decisions tch
                }

                if (NormalItems.Count != 0)
                {
                    // This is the "Hey, we wanna gather just normal items"
                    // Will find the first one on the list and have it go from there
                    // IDEALLY, this will be checked between gathering hits
                    // Partially because we might wanna exit out of the gathering screen if we don't see the item we're gathering
                    var first = NormalItems.First();

                    IceLogging.Debug("Found an item that we need to gather in the normal item route", tag);
                    IceLogging.Debug($"Route: {first.routeId} | ItemID: {first.itemId}");

                    Gather_Helper.GatherRoute = first;
                    Gather_Helper.State = IceState.Teleport;
                    return true;
                }
            }
            

            return true;
        }

        public sealed record GatherTarget(ItemInfo Item, uint RouteId, GatherPointInfo Route, GatherReason Reason)
        {
            // True if the route has no windows, or any window is currently open
            public bool IsUp => Route.TimedInfo.Count == 0 || Route.TimedInfo.Any(w => w.IsUp());
        }

        public static class GatherPlanner
        {
            /// <summary>
            /// Priority: Legendary/Unspoiled -> Ephemeral -> Regular.
            /// completedRoutes = route IDs already checked off (timed/ephemeral only; regular is never "done").
            /// </summary>
            public static GatherTarget? GetNextTarget(IEnumerable<ItemInfo> gatherList, IReadOnlyDictionary<uint, RouteSelection> itemRoutes, IReadOnlyDictionary<uint, GatherPointInfo> sheet, ISet<uint> completedRoutes)
            {
                var active = gatherList.Where(i => i.GatherAmount != 0).ToList();
                if (active.Count == 0)
                    return null;

                // 1. Legendary / Unspoiled: "hey, we need to gather this node"
                var timed = FindFirst(active, itemRoutes, sheet, completedRoutes, IsTimedKind, GatherReason.Timed);
                if (timed != null)
                    return timed;

                // 2. Ephemeral: treated like a pseudo-timed node for now
                var ephemeral = FindFirst(active, itemRoutes, sheet, completedRoutes, IsEphemeralKind, GatherReason.Ephemeral);
                if (ephemeral != null)
                    return ephemeral;

                // 3. Regular: first matching route, no completed check
                return FindFirst(active, itemRoutes, sheet, null, IsRegularKind, GatherReason.Regular);
            }

            private static bool IsTimedKind(GatherNodeKind kind)
                => kind == GatherNodeKind.Legendary || kind == GatherNodeKind.Unspoiled;

            private static bool IsEphemeralKind(GatherNodeKind kind)
                => kind == GatherNodeKind.Ephemeral;

            private static bool IsRegularKind(GatherNodeKind kind)
                => !IsTimedKind(kind) && !IsEphemeralKind(kind);

            private static GatherTarget? FindFirst(List<ItemInfo> items, IReadOnlyDictionary<uint, RouteSelection> itemRoutes, IReadOnlyDictionary<uint, GatherPointInfo> sheet, ISet<uint>? skip, Func<GatherNodeKind, bool> kindFilter, GatherReason reason)
            {
                foreach (var item in items)
                {
                    foreach (var (routeId, route) in Candidates(item, itemRoutes, sheet))
                    {
                        if (!kindFilter(route.Kind))
                            continue;

                        if (skip != null && skip.Contains(routeId))
                            continue;

                        return new GatherTarget(item, routeId, route, reason);
                    }
                }

                return null;
            }

            // Enabled routes for this item that exist in the sheet and actually drop the item.
            // Ordered by route ID so "first" is deterministic (HashSet order isn't guaranteed).
            private static IEnumerable<(uint Id, GatherPointInfo Route)> Candidates(ItemInfo item, IReadOnlyDictionary<uint, RouteSelection> itemRoutes, IReadOnlyDictionary<uint, GatherPointInfo> sheet)
            {
                if (!itemRoutes.TryGetValue(item.ItemId, out var selection))
                    yield break;

                foreach (var routeId in selection.EnabledRoutes.OrderBy(id => id))
                {
                    if (!sheet.TryGetValue(routeId, out var route))
                        continue;

                    if (!route.ItemIds.Contains(item.ItemId))
                        continue;

                    yield return (routeId, route);
                }
            }
        }
    }
}
