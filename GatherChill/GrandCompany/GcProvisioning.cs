using ECommons.ExcelServices;
using ECommons.GameHelpers;
using GatherChill.Enums;
using GatherChill.Scheduler;
using GatherChill.Utilities.Tools;
using System.Collections.Generic;

namespace GatherChill.GrandCompany;

internal sealed class GcProvisioning : IDisposable
{
    private enum Stage
    {
        None,
        Timers,
        ReadingBoard,
        Gathering,
        Delivering,
        Stopping,
    }

    private enum RunMode
    {
        Refresh,
        GatherAndDeliver,
        DeliverHeld,
    }

    internal const string Owner = "GatherChill.GC";

    private readonly GcTimers timers = new();
    private readonly GcDelivery delivery = new();
    private readonly Queue<(GcRequest Request, int Amount)> toGather = new();
    private readonly Queue<GcRequest> toDeliver = new();
    private readonly HashSet<uint> completedJobs = new();
    private readonly HashSet<uint> selectedJobs = new();

    private GcRequest[] requests = Array.Empty<GcRequest>();
    private GcRequest[] selected = Array.Empty<GcRequest>();
    private GcRequest gathering;
    private GcRequest wanted;
    private Stage stage;
    private RunMode mode;
    private Guid gatherJob;
    private ulong character;
    private byte company;
    private DateTime resetUtc;
    private int delivered;
    private int skipped;

    public bool Running => stage != Stage.None;
    public bool Failed { get; private set; }
    public string Status { get; private set; } = string.Empty;
    public IReadOnlyList<GcRequest> Requests => requests;
    public IReadOnlySet<uint> CompletedJobs => completedJobs;
    public DateTime LastReadUtc { get; private set; }
    public bool MinerEnabled { get; set; } = true;
    public bool BotanistEnabled { get; set; } = true;

    public bool Start(bool gather = true) => Begin(gather ? RunMode.GatherAndDeliver : RunMode.DeliverHeld);

    public bool Refresh() => Begin(RunMode.Refresh);

    private bool Begin(RunMode nextMode)
    {
        try
        {
            return BeginRun(nextMode);
        }
        catch (Exception ex)
        {
            Fail($"Can't start: {ex.Message}");
            return false;
        }
    }

    private bool BeginRun(RunMode nextMode)
    {
        if (Running)
            return false;

        if (BusyReason() is { } busy)
            return Refuse(busy);

        if (GcBoard.Company == 0)
            return Refuse("Join a Grand Company first");

        if (nextMode != RunMode.Refresh && !MinerEnabled && !BotanistEnabled)
            return Refuse("Select Miner or Botanist first");

        character = Player.CID;
        company = GcBoard.Company;
        resetUtc = GcBoard.LastResetUtc(DateTime.UtcNow);
        mode = nextMode;
        Failed = false;
        delivered = skipped = 0;
        gatherJob = Guid.Empty;
        toGather.Clear();
        toDeliver.Clear();
        selected = Array.Empty<GcRequest>();
        ClearSnapshot();

        selectedJobs.Clear();
        if (MinerEnabled)
            selectedJobs.Add(GcBoard.Miner);
        if (BotanistEnabled)
            selectedJobs.Add(GcBoard.Botanist);

        stage = Stage.Timers;
        Status = "Reading today's provisioning requests";
        timers.Start();
        return true;
    }

    public void Update()
    {
        try
        {
            Drive();
        }
        catch (Exception ex)
        {
            Fail($"Stopped: {ex.Message}");
        }
    }

    private void Drive()
    {
        if (stage == Stage.Stopping)
        {
            UpdateStopping();
            return;
        }

        if (!Running)
        {
            InvalidateOldSnapshot();
            return;
        }

        if (!Svc.ClientState.IsLoggedIn)
        {
            AbortChangedContext("Logged out");
            return;
        }

        if (GcBoard.LastResetUtc(DateTime.UtcNow) != resetUtc)
        {
            AbortChangedContext("Daily reset; refresh");
            return;
        }

        if (!Player.Available)
            return;

        if (Player.CID != character || GcBoard.Company != company)
        {
            AbortChangedContext("Character changed; refresh");
            return;
        }

        if (stage != Stage.Gathering && (Gather_Helper.State != IceState.Idle || GatherJobs.HasUnsettledJob ||
            P.TaskManager.IsBusy || P.navTask.IsBusy || Svc.Condition[ConditionFlag.Gathering]))
        {
            Fail("Other gathering started");
            return;
        }

        switch (stage)
        {
            case Stage.Timers:
                timers.Update();
                Status = timers.Status;
                if (timers.Running)
                    break;

                if (timers.Failed)
                {
                    IceLogging.Info($"Timers failed ({timers.Status}); using GC board", "GC");
                    stage = Stage.ReadingBoard;
                    delivery.Start();
                    Status = "Reading requests at the Grand Company";
                }
                else
                {
                    ReadRequests(timers.Requests);
                }
                break;

            case Stage.ReadingBoard:
                delivery.Update();
                Status = delivery.Status;
                if (delivery.Running)
                    break;

                if (delivery.Failed)
                    Fail($"Can't read requests: {delivery.Status}");
                else
                    ReadRequests(delivery.Requests);
                break;

            case Stage.Gathering:
                CheckGathering();
                break;

            case Stage.Delivering:
                CheckDelivery();
                break;
        }
    }

    private void ReadRequests(IReadOnlyList<GcRequest> found)
    {
        var supported = found.Where(x => GcBoard.Supported(x.ClassJob)).ToArray();
        if (supported.Any(x => x.Tab != EGcTab.Provisioning || x.ItemId == 0 || x.Requested <= 0) ||
            supported.GroupBy(x => x.ClassJob).Any(x => x.Count() != 1))
        {
            Fail("Incomplete requests; refresh");
            return;
        }

        requests = supported;
        LastReadUtc = DateTime.UtcNow;
        var source = stage == Stage.Timers ? "Timers" : "GC board";
        var summary = requests.Length == 0
            ? "no outstanding Miner or Botanist requests"
            : string.Join(", ", requests.Select(x => $"{(Job)(byte)x.ClassJob}: {x.Requested}x {x.ItemName} (item {x.ItemId})"));
        IceLogging.Info($"{source}: {summary}", "GC");

        if (mode == RunMode.Refresh)
        {
            Finish(requests.Length == 0 ? "No outstanding Miner or Botanist requests" : "Provisioning requests refreshed");
            return;
        }

        selected = requests.Where(x => selectedJobs.Contains(x.ClassJob)).OrderBy(x => x.ClassJob).ToArray();
        if (selected.Length == 0)
        {
            Finish("No selected requests are outstanding");
            return;
        }

        if (mode == RunMode.GatherAndDeliver)
        {
            foreach (var request in selected)
            {
                var missing = request.Requested - GcBoard.Held(request.ItemId);
                if (missing > 0)
                    toGather.Enqueue((request, missing));
            }
        }

        stage = Stage.Gathering;
        NextGather();
    }

    private void NextGather()
    {
        if (!toGather.TryDequeue(out var next))
        {
            StartDeliveries();
            return;
        }

        gathering = next.Request;
        var job = (Job)(byte)gathering.ClassJob;
        if (GatherJobs.Start(Owner, gathering.ItemId, next.Amount, out gatherJob, out var message, job) != GatherJobStartResult.Started)
        {
            gatherJob = Guid.Empty;
            Fail($"Can't gather {gathering.ItemName}: {message}");
            return;
        }

        var after = toGather.Count > 0 ? $" ({toGather.Count} more to gather before delivering)" : string.Empty;
        Status = $"Gathering {next.Amount} more {gathering.ItemName}{after}";
    }

    private void CheckGathering()
    {
        if (gatherJob == Guid.Empty)
            return;

        var (state, _, _, message) = GatherJobs.Get(gatherJob);
        Status = message;
        switch ((GatherJobState)state)
        {
            case GatherJobState.Running:
            case GatherJobState.Stopping:
                return;

            case GatherJobState.Succeeded:
                gatherJob = Guid.Empty;
                if (GcBoard.Held(gathering.ItemId) < gathering.Requested)
                {
                    Fail($"Not enough {gathering.ItemName}");
                    return;
                }

                NextGather();
                return;

            default:
                gatherJob = Guid.Empty;
                Fail($"Gathering {gathering.ItemName} stopped: {message}");
                return;
        }
    }

    private void StartDeliveries()
    {
        stage = Stage.Delivering;
        foreach (var request in selected)
        {
            if (GcBoard.Held(request.ItemId) >= request.Requested)
                toDeliver.Enqueue(request);
            else
                skipped++;
        }

        NextDelivery();
    }

    private void NextDelivery()
    {
        if (!toDeliver.TryDequeue(out wanted))
        {
            Finish(skipped > 0
                ? $"Delivered {delivered} request(s); {skipped} request(s) need more items"
                : completedJobs.Count > 0 ? "Selected provisioning requests completed" : "No selected requests are outstanding");
            return;
        }

        delivery.Start(wanted);
        Status = $"Delivering {wanted.Requested} {wanted.ItemName}";
        if (delivery.Failed)
            Fail($"Can't deliver {wanted.ItemName}: {delivery.Status}");
    }

    private void CheckDelivery()
    {
        delivery.Update();
        Status = delivery.Status;
        if (delivery.Running)
            return;

        if (delivery.Failed || (!delivery.Delivered && !delivery.AlreadyDone))
        {
            Fail($"Can't deliver {wanted.ItemName}: {delivery.Status}");
            return;
        }

        completedJobs.Add(wanted.ClassJob);
        requests = requests.Where(x => x.ClassJob != wanted.ClassJob).ToArray();
        if (delivery.Delivered)
            delivered++;

        NextDelivery();
    }

    public void Stop(string reason)
    {
        if (!Running || stage == Stage.Stopping)
            return;

        stage = Stage.Stopping;
        toGather.Clear();
        toDeliver.Clear();
        Status = reason;
        Cleanup(() => timers.Stop(reason));
        Cleanup(() => delivery.Stop(reason));
        if (gatherJob != Guid.Empty)
            Cleanup(() => GatherJobs.Cancel(Owner, gatherJob));
    }

    private void UpdateStopping()
    {
        if (timers.Running)
            timers.Update();
        if (delivery.Running)
            delivery.Update();
        if (timers.Running || delivery.Running)
            return;

        if (gatherJob != Guid.Empty && (GatherJobState)GatherJobs.Get(gatherJob).State is GatherJobState.Running or GatherJobState.Stopping)
            return;

        gatherJob = Guid.Empty;
        stage = Stage.None;
    }

    public void Dispose()
    {
        Stop("Provisioning stopped");
        Cleanup(() => timers.Stop("Provisioning stopped"));
        Cleanup(() => delivery.Stop("Provisioning stopped"));
    }

    private void Fail(string reason)
    {
        Stop(reason);
        Failed = true;
        Status = reason;
        IceLogging.Info(reason, "GC");
    }

    private bool Refuse(string reason)
    {
        Failed = true;
        Status = reason;
        return false;
    }

    private void Finish(string message)
    {
        stage = Stage.None;
        Status = message;
        IceLogging.Info(message, "GC");
    }

    private void AbortChangedContext(string reason)
    {
        Fail(reason);
        ClearSnapshot();
    }

    private void InvalidateOldSnapshot()
    {
        if (LastReadUtc == DateTime.MinValue)
            return;

        if (!Svc.ClientState.IsLoggedIn || GcBoard.LastResetUtc(DateTime.UtcNow) != resetUtc ||
            (Player.Available && (Player.CID != character || GcBoard.Company != company)))
        {
            ClearSnapshot();
            Status = string.Empty;
        }
    }

    private void ClearSnapshot()
    {
        requests = Array.Empty<GcRequest>();
        completedJobs.Clear();
        LastReadUtc = DateTime.MinValue;
    }

    private static string? BusyReason()
    {
        if (!Svc.ClientState.IsLoggedIn || !Player.Available || Player.CID == 0)
            return "Not logged in";

        if (Gather_Helper.State != IceState.Idle || GatherJobs.HasUnsettledJob || P.TaskManager.IsBusy || P.navTask.IsBusy)
            return "Gathering in progress";

        if (Player.IsBusy || Svc.Condition[ConditionFlag.Gathering])
            return "Character busy";

        if (ExternalBusy("vnavmesh", "Path.IsRunning", "Nav.PathfindInProgress") || ExternalBusy("Lifestream", "IsBusy"))
            return "Travel in progress";

        return null;
    }

    private static bool ExternalBusy(string pluginName, params string[] endpoints)
    {
        try
        {
            if (!Svc.PluginInterface.InstalledPlugins.Any(x => x.InternalName == pluginName && x.IsLoaded))
                return false;

            return endpoints.Any(x => Svc.PluginInterface.GetIpcSubscriber<bool>($"{pluginName}.{x}").InvokeFunc());
        }
        catch
        {
            return true;
        }
    }

    private static void Cleanup(Action stop)
    {
        try
        {
            stop();
        }
        catch (Exception ex)
        {
            IceLogging.Info($"Provisioning cleanup: {ex.Message}", "GC");
        }
    }
}
