using ECommons.ExcelServices;
using GatherChill.Scheduler;
using GatherChill.Utilities.GatheringHelpers.GatherListUtils;
using GatherChill.Utilities.Utility;
using System.Collections.Generic;

namespace GatherChill.GrandCompany;

internal static class GC_Helper
{
    private const string Owner = "GrandCompany";

    public static Guid JobId { get; private set; } = Guid.Empty;

    public static bool IsRunning => JobId != Guid.Empty && Status().State == (int)GatherJobState.Running;

    /// <summary>
    /// Starts one job for everything still missing from the given jobs' requests.
    /// Amount is "needed minus what's in the bags", since a job gathers N MORE than it started with.
    /// </summary>
    public static bool Start(IReadOnlyCollection<Job> jobs, out string message)
    {
        if (jobs.Count == 0)
        {
            message = "No jobs selected";
            return false;
        }

        var requests = GCInfo.RequestedItems
            .Where(x => jobs.Contains(x.Key))
            .ToList();

        var gatherable = requests
            .Where(x => GatherList_Util.CanGather(x.Value.ItemId))
            .ToList();

        var items = gatherable
            .GroupBy(x => x.Value.ItemId)
            .Select(g => (ItemId: g.Key, Amount: g.Sum(x => x.Value.Amount) - Utils.GetItemCount(g.Key)))
            .Where(x => x.Amount > 0)
            .ToArray();

        if (items.Length == 0)
        {
            message = requests.Count == 0
                ? "No requests loaded, try Refresh Order"
                : "Nothing left to gather";
            return false;
        }

        var result = GatherJobs.Start(Owner, items, out var id, out message);
        if (result != GatherJobStartResult.Started)
            return false;

        JobId = id;

        var skipped = requests.Count - gatherable.Count;
        if (skipped > 0)
            message += $" ({skipped} without a route skipped)";

        return true;
    }

    public static bool Cancel()
        => JobId != Guid.Empty && GatherJobs.Cancel(Owner, JobId);

    public static (int State, int Gathered, int Wanted, string Message) Status()
        => GatherJobs.Get(JobId);
}