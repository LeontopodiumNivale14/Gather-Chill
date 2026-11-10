using ECommons.ExcelServices;
using ECommons.Throttlers;
using GatherChill.ConfigFiles;
using GatherChill.Enums;
using GatherChill.Utilities.GatheringHelpers;
using GatherChill.Utilities.Utility;
using System.Collections.Generic;

namespace GatherChill.Scheduler
{
    internal class Gather_Helper
    {
        public class SelectedRoute
        {
            public uint itemId { get; set; } = 0;
            public uint routeId { get; set; } = 0;
        }

        public static SelectedRoute GatherRoute = new();
        public static IceState State = IceState.Idle;
        public static GatherType gatherMode = GatherType.Normal;

        // Set by the GatherItem IPC so only that item gets gathered. 0 = the whole list.
        public static uint OnlyItemId = 0;

        // Set by the GatherJobItem / StartJobForJob IPC so routes are limited to this job. null = no restriction.
        public static Job? OnlyJob = null;

        /// <summary>
        /// False if the GatherItem IPC limited this run to a different item.
        /// </summary>
        public static bool IsTargeted(uint itemId) => OnlyItemId == 0 || OnlyItemId == itemId;

        /// <summary>
        /// Clears both IPC restrictions. Call anywhere OnlyItemId used to be reset (run end, DisablePlugin, job end).
        /// </summary>
        public static void ClearTargeting()
        {
            OnlyItemId = 0;
            OnlyJob = null;
        }

        public class ProgressInfo
        {
            public uint ItemId { get; set; } = 0;

            // Inventory count when tracking started. Locked in by SyncProgress.
            public int StartInventory { get; set; } = 0;

            // Gather goal when tracking started. Locked in by SyncProgress. Unused in Keep mode.
            public int GatherTarget { get; set; } = 0;

            // The mode we locked in with, so we can detect the user flipping it mid-run.
            public GatherMode LockedMode { get; set; } = GatherMode.Gather;

            private Config.ItemInfo? Info
            {
                get
                {
                    foreach (var info in GatherJobs.ActiveList)
                    {
                        if (info.ItemId == ItemId)
                            return info;
                    }

                    return null;
                }
            }

            public GatherMode Mode => Info?.Mode ?? GatherMode.Gather;

            public int Current => Utils.GetItemCount(ItemId);

            public int Gathered => Math.Max(Current - StartInventory, 0);

            // The overall goal, for display ("x / Target").
            public int Target
            {
                get
                {
                    var info = Info;
                    if (info == null)
                        return 0;

                    if (info.Mode == GatherMode.Keep)
                        return Math.Max(info.KeepAmount, 0);

                    return Math.Max(GatherTarget, 0);
                }
            }

            // What the overlay shows on the left of "x / Target".
            public int Progress => Mode == GatherMode.Keep ? Current : Gathered;

            // How many more are needed.
            public int Remaining => Math.Max(Target - Progress, 0);

            // 0..1 for progress bars.
            public float Fraction
            {
                get
                {
                    var target = Target;
                    if (target <= 0)
                        return 1f;

                    return Math.Clamp((float)Progress / target, 0f, 1f);
                }
            }

            public bool IsComplete => Remaining == 0;
        }

        public static List<ProgressInfo> ItemProgress = new();

        /// <summary>
        /// Call every tick while NOT idle. Keeps ItemProgress in line with the gather list:
        /// - new items (list edits, IPC) get their baseline locked in when they show up
        /// - removed / no-longer-targeted items get dropped
        /// - existing items are left alone, so the baseline never resets mid-run
        /// - if an item's mode is flipped, it is re-locked
        /// </summary>
        public static void SyncProgress()
        {
            var activeList = GatherJobs.ActiveList;

            // Drop anything that left the list or is no longer targeted
            ItemProgress.RemoveAll(p => !activeList.Any(x => x.ItemId == p.ItemId && IsTargeted(x.ItemId)));

            foreach (var info in activeList)
            {
                if (!IsTargeted(info.ItemId))
                    continue;

                var existing = ItemProgress.FirstOrDefault(x => x.ItemId == info.ItemId);
                if (existing == null)
                {
                    var progress = new ProgressInfo { ItemId = info.ItemId };
                    LockInProgress(progress, info);
                    ItemProgress.Add(progress);
                    continue;
                }

                // Mode flipped mid-run, so the old baseline doesn't mean anything anymore
                if (existing.LockedMode != info.Mode)
                {
                    LockInProgress(existing, info);
                    continue;
                }

                // Amount edited mid-run (list edit, IPC add): follow the new goal, keep the baseline
                if (info.Mode == GatherMode.Gather && existing.GatherTarget != info.GatherAmount)
                    existing.GatherTarget = info.GatherAmount;
            }
        }

        /// <summary>
        /// Call while idle. Next time we leave idle, SyncProgress locks fresh baselines.
        /// </summary>
        public static void ResetProgress() => ItemProgress.Clear();

        private static void LockInProgress(ProgressInfo progress, Config.ItemInfo info)
        {
            progress.StartInventory = Utils.GetItemCount(info.ItemId);
            progress.GatherTarget = info.GatherAmount;
            progress.LockedMode = info.Mode;
        }

        #region Route Selection

        // Swap the body for whatever GatherList_Util.CanGather uses to decide a route yields the item.
        private static bool RouteGivesItem(uint routeId, uint itemId)
            => Gather_Util.Sheet_RouteInfo.TryGetValue(routeId, out var info) && info.ItemIds.Contains(itemId);

        private static bool IsUsableRoute(uint routeId)
        {
            var route = P.routeEditor.GetRoute(routeId);
            return route != null && route.AetheryteId != 0;
        }

        /// <summary>
        /// Routes to use for an item:
        /// - no OnlyJob: whatever the user has enabled
        /// - OnlyJob: enabled routes for that job, otherwise the first usable route in the sheet for that job
        /// </summary>
        public static List<uint> GetRoutes(uint itemId)
        {
            var routes = new List<uint>();
            C.ItemRoutes.TryGetValue(itemId, out var selection);

            if (OnlyJob is not { } job)
            {
                if (selection != null)
                    routes.AddRange(selection.EnabledRoutes);

                return routes;
            }

            // Prefer what the user already enabled
            if (selection != null)
            {
                foreach (var routeId in selection.EnabledRoutes)
                {
                    if (Gather_Util.Sheet_RouteInfo.TryGetValue(routeId, out var info) && info.Job == job)
                        routes.Add(routeId);
                }
            }

            if (routes.Count != 0)
                return routes;

            // Nothing enabled for this job, so take the first usable one (sorted so it's deterministic)
            foreach (var (routeId, info) in Gather_Util.Sheet_RouteInfo.OrderBy(x => x.Key))
            {
                if (info.Job != job || !RouteGivesItem(routeId, itemId) || !IsUsableRoute(routeId))
                    continue;

                routes.Add(routeId);
                return routes;
            }

            return routes;
        }

        public static bool CanGatherJob(uint itemId, Job job)
        {
            var previous = OnlyJob;
            OnlyJob = job;
            var found = GetRoutes(itemId).Count != 0;
            OnlyJob = previous;
            return found;
        }

        #endregion

        public static HashSet<uint> GatheredTimedRoutes = new();

        public static void RemoveTimedRoutes()
        {
            if (!EzThrottler.Throttle("Has Checked"))
                return;

            // Drop routes whose window has passed (or that no longer exist)
            GatheredTimedRoutes.RemoveWhere(route =>
                !Gather_Util.Sheet_RouteInfo.TryGetValue(route, out var routeInfo) ||
                !routeInfo.TimedInfo.Any(x => x.IsUp()));
        }

        /// <summary>
        /// Returns true **ONLY IF** any timed node on the currently selected routes are up <br></br>     
        /// upRoutes: returns all the routes that are not on the banned list <br></br>
        /// nextUp: return the time of the closest window from the currently available routes <br></br>
        ///         if there is none, it return null
        /// </summary>
        public static bool AnyTimedNodes(out List<uint> upRoutes, out TimeSpan? nextUp)
        {
            upRoutes = new List<uint>();
            nextUp = null;

            if (GatherJobs.ActiveList.Count == 0)
                return false;

            var visited = new HashSet<uint>();
            TimeSpan? soonest = null;

            // ItemProgress only holds targeted items, so "tracked and not complete" is the whole filter
            var itemList = GatherJobs.ActiveList
                .Where(x => ItemProgress.Any(p => p.ItemId == x.ItemId && !p.IsComplete))
                .ToList();

            foreach (var item in itemList)
            {
                foreach (var route in GetRoutes(item.ItemId))
                {
                    if (GatheredTimedRoutes.Contains(route))
                        continue;

                    // Same route can be shared by multiple items
                    if (!visited.Add(route))
                        continue;

                    if (!Gather_Util.Sheet_RouteInfo.TryGetValue(route, out var info))
                        continue;

                    if (info.TimedInfo.Count == 0)
                        continue;

                    if (info.TimedInfo.Any(x => x.IsUp()))
                    {
                        upRoutes.Add(route);
                        continue;
                    }

                    foreach (var window in info.TimedInfo)
                    {
                        var wait = window.TimeUntilStart();
                        if (soonest == null || wait < soonest.Value)
                            soonest = wait;
                    }
                }
            }

            nextUp = soonest;
            return upRoutes.Count > 0;
        }

        // Convenience overloads
        public static bool AnyTimedNodes(out List<uint> upRoutes) => AnyTimedNodes(out upRoutes, out _);
        public static bool AnyTimedNodes() => AnyTimedNodes(out _, out _);

        /// <summary>
        /// True when GP should be saved for an upcoming (or already up) timed node.
        /// </summary>
        public static bool ShouldHoldGpForTimedNode()
        {
            if (!C.HoldGP_TimeNodes)
                return false;

            var next = NextTimedNode();
            if (next is not { } wait)
                return false;

            // wait is at or below the hold threshold
            return wait.TotalSeconds <= C.TimedNodes_HoldTime;
        }

        /// <summary>
        /// null          -> nothing to wait for (already on an up timed route, or no timed routes exist)
        /// TimeSpan.Zero -> a different timed route is up right now
        /// otherwise     -> real time until the soonest timed route opens
        /// </summary>
        public static TimeSpan? NextTimedNode()
        {
            var anyUp = AnyTimedNodes(out var upRoutes, out var nextUp);

            // Already on a timed route that's up
            if (anyUp && upRoutes.Contains(GatherRoute.routeId))
                return null;

            // A different timed route is up right now
            if (anyUp)
                return TimeSpan.Zero;

            // Nothing up: time until the soonest opens (null if no timed routes at all)
            return nextUp;
        }

        public readonly record struct GpRequirement(float Threshold, string Reason)
        {
            // Mnemonic: the small end points at the smaller value.
            // gp < Threshold means "still waiting", so met is the opposite.
            public bool IsMet(float gp) => gp >= Threshold;
        }

        private const float HiddenItemGp = 200f;

        /// <summary>
        /// The GP we need before interacting with the current route's node, or null if nothing is being waited on.
        /// If several conditions apply, the highest threshold wins.
        /// </summary>
        public static GpRequirement? GetGpRequirement()
        {
            var route = GatherRoute;
            if (route.routeId == 0 || route.itemId == 0)
                return null;

            // Already at the node, spending GP is expected
            if (Svc.Condition[ConditionFlag.Gathering])
                return null;

            if (!Gather_Util.Sheet_RouteInfo.TryGetValue(route.routeId, out var routeInfo))
                return null;

            if (!Gather_Util.Sheet_ItemInfo.TryGetValue(route.itemId, out var itemInfo))
                return null;

            GpRequirement? result = null;

            void Consider(float threshold, string reason)
            {
                if (result is not { } current || threshold > current.Threshold)
                    result = new GpRequirement(threshold, reason);
            }

            if (routeInfo.Kind is GatherNodeKind.Legendary or GatherNodeKind.Unspoiled)
            {
                if (routeInfo.TimedInfo.Any(x => x.IsUp()))
                {
                    var timeSlot = routeInfo.TimedInfo.First(x => x.IsUp());

                    // Inside the last minute there's no point waiting anymore
                    if (timeSlot.TimeUntilEnd().TotalSeconds > 60)
                        Consider(C.TimedNode_MinGP, "Timed node: saving GP");
                }
            }
            else if (routeInfo.Kind == GatherNodeKind.Regular && itemInfo.IsCollectable && C.WaitGP_Collectable)
            {
                Consider(C.Collectable_WaitGP, "Collectable: waiting for GP");
            }

            if (itemInfo.IsHidden && C.WaitGP_HiddenItem)
                Consider(HiddenItemGp, "Hidden item: waiting for GP");

            return result;
        }
    }
}