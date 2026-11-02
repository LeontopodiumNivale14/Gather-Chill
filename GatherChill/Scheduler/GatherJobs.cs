using ECommons.GameHelpers;
using GatherChill.ConfigFiles;
using GatherChill.Enums;
using GatherChill.Utilities.GatheringHelpers;
using GatherChill.Utilities.Tools;
using System.Collections.Generic;

namespace GatherChill.Scheduler;

/// <summary>
/// Job states reported over IPC. Numbered like the owned-operation states other plugins already use.
/// </summary>
public enum GatherJobState
{
    Unknown = 0,
    Running = 1,
    Stopping = 2,
    Succeeded = 3,
    Failed = 4,
    Cancelled = 5,
}

public enum GatherJobStartResult
{
    Started = 0,
    Busy = 1,
    NoRoute = 2,
    Invalid = 3,
    NotReady = 4,
}

/// <summary>
/// Gathers started over IPC. A job gathers from its own list, so the saved gather list is never touched,
/// and it only reports its outcome once travel and the gathering window have settled.
/// </summary>
internal static class GatherJobs
{
    private const int FinishedToKeep = 16;

    private static Job? current;
    private static readonly List<Job> finished = new();

    /// <summary>
    /// The list the scheduler gathers from: the running job's, otherwise the saved gather list.
    /// </summary>
    public static List<Config.ItemInfo> ActiveList => current is { State: GatherJobState.Running } job ? job.Items : C.GatherList;

    /// <summary>
    /// True while a job is running or still settling.
    /// </summary>
    public static bool HasUnsettledJob => current != null;

    public static GatherJobStartResult Start(string owner, uint itemId, int amount, out Guid jobId, out string message)
    {
        jobId = Guid.Empty;
        if (string.IsNullOrWhiteSpace(owner) || itemId == 0 || amount <= 0)
        {
            message = "An owner, an item and an amount above zero are required";
            return GatherJobStartResult.Invalid;
        }

        if (!Player.Available)
        {
            message = "Not logged in";
            return GatherJobStartResult.NotReady;
        }

        if (Gather_Helper.State != IceState.Idle || current != null)
        {
            message = "Gather n Chill is busy";
            return GatherJobStartResult.Busy;
        }

        var targetId = GatherList_Util.ResolveGatherItemId(itemId);
        if (!GatherList_Util.CanGather(itemId) || !GatherList_Util.EnsureEnabledRoutes(targetId))
        {
            message = $"No gathering route for item {itemId}";
            return GatherJobStartResult.NoRoute;
        }

        var job = new Job(Guid.NewGuid(), owner.Trim(), targetId, amount, GetItemCount(targetId));
        current = job;
        jobId = job.Id;
        message = job.Message;

        Gather_Helper.OnlyItemId = targetId;
        Gather_Helper.State = IceState.Start;
        IceLogging.Info($"{job.Owner} started job {job.Id}: {amount}x {targetId}", "IPC");
        return GatherJobStartResult.Started;
    }

    public static (int State, int Gathered, int Wanted, string Message) Get(Guid jobId)
    {
        var job = current?.Id == jobId ? current : finished.FirstOrDefault(x => x.Id == jobId);
        if (job == null)
            return ((int)GatherJobState.Unknown, 0, 0, "Unknown job");

        return ((int)job.State, job.Gathered, job.Wanted, job.Message);
    }

    /// <summary>
    /// Stops the job if it is still running and belongs to the owner. Never stops other gathering.
    /// </summary>
    public static bool Cancel(string owner, Guid jobId)
    {
        if (current is not { State: GatherJobState.Running } job || job.Id != jobId ||
            !string.Equals(job.Owner, owner?.Trim(), StringComparison.OrdinalIgnoreCase))
            return false;

        SchedulerMain.DisablePlugin(GatherJobState.Cancelled, $"Cancelled by {job.Owner}");
        return true;
    }

    /// <summary>
    /// Records why gathering stopped. The job reports it once <see cref="Tick"/> sees everything settled.
    /// </summary>
    public static void End(GatherJobState outcome, string message)
    {
        if (current is not { State: GatherJobState.Running } job)
            return;

        job.Outcome = outcome;
        job.Message = message;
        job.State = GatherJobState.Stopping;
        IceLogging.Info($"Job {job.Id} for {job.Owner} is ending: {outcome}, {message}", "IPC");
    }

    /// <summary>
    /// Called when the scheduler runs out of things to gather.
    /// </summary>
    public static void EndNaturally()
    {
        if (current is not { State: GatherJobState.Running } job)
            return;

        if (job.Items[0].GatherAmount == 0)
            End(GatherJobState.Succeeded, $"Gathered {job.Wanted}x {job.ItemId}");
        else
            End(GatherJobState.Failed, "Nothing left to gather on the enabled routes");
    }

    public static void Tick()
    {
        if (current is not { State: GatherJobState.Stopping } job || !HandsFree())
            return;

        job.State = job.Outcome;
        current = null;
        finished.Add(job);
        if (finished.Count > FinishedToKeep)
            finished.RemoveAt(0);
    }

    private static bool HandsFree()
    {
        if (!Player.Available || Player.IsBusy || Svc.Condition[ConditionFlag.Gathering])
            return false;

        if (Gather_Helper.State != IceState.Idle || P.TaskManager.IsBusy || P.navTask.IsBusy)
            return false;

        return !IsBusy(() => P.navmesh.IsRunning() || P.navmesh.PathfindInProgress())
            && !IsBusy(() => P.lifestream.IsBusy());
    }

    // A plugin that isn't installed isn't moving the character.
    private static bool IsBusy(Func<bool> read)
    {
        try
        {
            return read();
        }
        catch
        {
            return false;
        }
    }

    private sealed class Job(Guid id, string owner, uint itemId, int wanted, int startCount)
    {
        public Guid Id { get; } = id;
        public string Owner { get; } = owner;
        public uint ItemId { get; } = itemId;
        public int Wanted { get; } = wanted;
        public List<Config.ItemInfo> Items { get; } = [new Config.ItemInfo { ItemId = itemId, GatherAmount = wanted }];
        public GatherJobState State { get; set; } = GatherJobState.Running;
        public GatherJobState Outcome { get; set; }
        public string Message { get; set; } = $"Gathering {wanted}x {itemId}";
        public int Gathered => Math.Max(GetItemCount(ItemId) - startCount, 0);
    }
}
