using GatherChill.ConfigFiles;

namespace GatherChill.Utilities.GatheringHelpers;

/// <summary>
/// Gather list edits shared by the Gather Playlist tab and the IPC provider.
/// Callers save the config afterwards.
/// </summary>
internal static class GatherList_Util
{
    /// <summary>
    /// Maps an aetherial reduction item (or its sublime variant) to the item that's actually gathered.
    /// </summary>
    public static uint ResolveGatherItemId(uint itemId)
    {
        var reduceInfo = Gather_Util.ReducableItems.FirstOrDefault(x =>
               x.ResultItems.Count != 0
           && (x.ItemId == itemId || x.SublimeItemId == itemId || x.ResultItems[0].ItemId == itemId));

        return reduceInfo?.ResultItems[0].ItemId ?? itemId;
    }

    public static bool IsInList(uint itemId)
    {
        var targetId = ResolveGatherItemId(itemId);
        return C.GatherList.Any(x => x.ItemId == targetId);
    }

    /// <summary>
    /// True if there's at least one gathering route for the item.
    /// </summary>
    public static bool CanGather(uint itemId)
    {
        var targetId = ResolveGatherItemId(itemId);
        return Gather_Util.Sheet_ItemInfo.TryGetValue(targetId, out var itemInfo)
            && (itemInfo.NormalRoutes.Count != 0 || itemInfo.TimedRoutes.Count != 0);
    }

    /// <summary>
    /// Adds an item to the gather list, or raises its amount if it's already there.
    /// Returns false if the item isn't a known gatherable.
    /// </summary>
    public static bool TryAdd(uint itemId, int amount)
    {
        var targetId = ResolveGatherItemId(itemId);

        if (!Gather_Util.Sheet_ItemInfo.TryGetValue(targetId, out var itemInfo))
            return false;

        var existing = C.GatherList.FirstOrDefault(x => x.ItemId == targetId);
        if (existing != null)
        {
            existing.GatherAmount = Math.Max(existing.GatherAmount, amount);
            return true;
        }

        C.GatherList.Add(new Config.ItemInfo { ItemId = targetId, GatherAmount = Math.Max(1, amount) });

        // Make sure the item has usable routes enabled
        if (C.ItemRoutes.TryGetValue(targetId, out var itemConfig))
        {
            var enabled = itemConfig.EnabledRoutes;

            // If both route types exist, only normal routes should be used
            if (itemInfo.TimedRoutes.Count != 0 && itemInfo.NormalRoutes.Count != 0)
            {
                foreach (var route in itemInfo.TimedRoutes)
                    enabled.Remove(route);
            }

            // Nothing enabled: fall back to the first normal route, otherwise all timed routes
            if (enabled.Count == 0)
            {
                if (itemInfo.NormalRoutes.Count != 0)
                    enabled.Add(itemInfo.NormalRoutes[0]);
                else
                    foreach (var route in itemInfo.TimedRoutes)
                        enabled.Add(route);
            }
        }

        return true;
    }
}
