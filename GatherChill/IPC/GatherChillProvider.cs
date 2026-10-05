using ECommons.EzIpcManager;
using GatherChill.Enums;
using GatherChill.Scheduler;
using GatherChill.Utilities.GatheringHelpers;

namespace GatherChill.IPC;

/// <summary>
/// IPC that other plugins can call, registered as "GatherChill.&lt;Method&gt;".
/// Bump <see cref="Version"/> when a signature changes.
/// </summary>
public class GatherChillProvider
{
    private const int Version = 1;

    public GatherChillProvider() => EzIPC.Init(this, "GatherChill");

    [EzIPC]
    public int ApiVersion() => Version;

    /// <summary>
    /// True while Gather n Chill is gathering.
    /// </summary>
    [EzIPC]
    public bool IsBusy() => SchedulerMain.State != IceState.Idle;

    /// <summary>
    /// True if Gather n Chill has a route for the item, so callers only send what it can gather.
    /// </summary>
    [EzIPC]
    public bool CanGather(uint itemId) => GatherList_Util.CanGather(itemId);

    /// <summary>
    /// Adds items to the gather list. Items already in the list keep the larger amount.
    /// Items without a route are skipped. Returns how many items were added or updated.
    /// </summary>
    [EzIPC]
    public int AddToGatherList((uint ItemId, uint Amount)[] items)
    {
        var count = 0;
        foreach (var (itemId, amount) in items)
        {
            if (GatherList_Util.CanGather(itemId) && GatherList_Util.TryAdd(itemId, amount))
                count++;
        }

        if (count != 0)
            C.Save();

        return count;
    }
}
