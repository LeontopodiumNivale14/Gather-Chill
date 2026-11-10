using ECommons.ExcelServices;
using ECommons.EzIpcManager;
using GatherChill.Enums;
using GatherChill.Scheduler;
using GatherChill.Utilities.GatheringHelpers.GatherListUtils;

namespace GatherChill.IPC;

/// <summary>
/// IPC that other plugins can call, registered as "GatherChill.&lt;Method&gt;".
/// Bump <see cref="Version"/> when a signature changes.
/// </summary>
public class GatherChillProvider
{
    private const int Version = 5;

    public GatherChillProvider() => EzIPC.Init(this, "GatherChill");

    [EzIPC]
    public int ApiVersion() => Version;

    /// <summary>
    /// True while Gather n Chill is gathering, or a job is still settling.
    /// </summary>
    [EzIPC]
    public bool IsBusy() => Gather_Helper.State != IceState.Idle || GatherJobs.HasUnsettledJob;

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
    /// Clears the current gathering list, and will replace the list with it's own. 
    /// Items without a route are skipped. Returns how many items were added.
    /// </summary>
    [EzIPC]
    public int OverwriteGatherList((uint ItemId, int Amount)[] items)
    {
        var count = 0;
        C.GatherList.Clear();
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
    /// Same as GatherItem, but only uses routes for the given job. Uses an enabled route for that job
    /// if there is one, otherwise the first route that works. Returns false if busy or no route fits.
    /// </summary>
    [EzIPC]
    public bool GatherJobItem(uint itemId, int amount, uint jobId)
    {
        if (IsBusy())
            return false;

        var job = (Job)jobId;
        var targetId = GatherList_Util.ResolveGatherItemId(itemId);

        if (!GatherList_Util.CanGather(itemId) || !Gather_Helper.CanGatherJob(targetId, job))
            return false;

        if (!GatherList_Util.TryAdd(itemId, amount))
            return false;

        C.Save();
        Gather_Helper.OnlyItemId = targetId;
        Gather_Helper.OnlyJob = job;
        Gather_Helper.State = IceState.Start;
        return true;
    }

    /// <summary>
    /// Stops gathering, same as the Stop button.
    /// </summary>
    [EzIPC]
    public void Stop() => SchedulerMain.DisablePlugin();

    /// <summary>
    /// Starts a job that gathers <paramref name="amount"/> more of one item, without touching the saved gather list.
    /// Result is a <see cref="GatherJobStartResult"/>; JobId is empty unless it started.
    /// </summary>
    [EzIPC]
    public (int Result, Guid JobId, string Message) StartJob(string owner, uint itemId, int amount)
    {
        var result = GatherJobs.Start(owner, itemId, amount, out var jobId, out var message);
        return ((int)result, jobId, message);
    }

    /// <summary>
    /// Same as StartJob, but only uses routes for the given job (falls back to the first route that works).
    /// </summary>
    [EzIPC]
    public (int Result, Guid JobId, string Message) StartJobForJob(string owner, uint itemId, int amount, uint jobId)
    {
        var result = GatherJobs.Start(owner, itemId, amount, out var newJobId, out var message, (Job)jobId);
        return ((int)result, newJobId, message);
    }

    /// <summary>
    /// State is a <see cref="GatherJobState"/>. A job stays Stopping until travel and the gathering window
    /// have settled, then reports Succeeded, Failed or Cancelled. Unknown means the id isn't known (anymore).
    /// </summary>
    [EzIPC]
    public (int State, int Gathered, int Wanted, string Message) GetJob(Guid jobId) => GatherJobs.Get(jobId);

    /// <summary>
    /// Cancels the job if it is still running and belongs to <paramref name="owner"/>. Never stops other gathering.
    /// </summary>
    [EzIPC]
    public bool CancelJob(string owner, Guid jobId) => GatherJobs.Cancel(owner, jobId);

    /// <summary>
    /// Opens the main window, e.g. after adding items to the gather list. Leaves it open if it already is.
    /// </summary>
    [EzIPC]
    public void OpenWindow() => P.mainWindow.IsOpen = true;

    /// <summary>
    /// Opens the gathering overlay (current item, GP, what's next), e.g. while a job runs. Leaves it open if it already is.
    /// </summary>
    [EzIPC]
    public void OpenOverlay() => P.gatherOverlay.IsOpen = true;
}
