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
    private const int Version = 2;

    public GatherChillProvider() => EzIPC.Init(this, "GatherChill");

    [EzIPC]
    public int ApiVersion() => Version;

    /// <summary>
    /// True while Gather n Chill is gathering.
    /// </summary>
    [EzIPC]
    public bool IsBusy() => Gather_Helper.State != IceState.Idle;

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
    public int AddToGatherList((uint ItemId, int Amount)[] items)
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

    /// <summary>
    /// Starts gathering one item right away, ignoring the rest of the gather list. Stops once it's done.
    /// The item is added to the list (an existing entry keeps the larger amount).
    /// Returns false if Gather n Chill is busy or has no route for the item.
    /// </summary>
    [EzIPC]
    public bool GatherItem(uint itemId, int amount)
    {
        if (IsBusy())
            return false;

        if (!GatherList_Util.CanGather(itemId) || !GatherList_Util.TryAdd(itemId, amount))
            return false;

        C.Save();
        Gather_Helper.OnlyItemId = GatherList_Util.ResolveGatherItemId(itemId);
        Gather_Helper.State = IceState.Start;
        return true;
    }

    /// <summary>
    /// Stops gathering, same as the Stop button.
    /// </summary>
    [EzIPC]
    public void Stop() => SchedulerMain.DisablePlugin();
}
