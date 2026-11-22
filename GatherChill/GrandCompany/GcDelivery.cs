using Dalamud.Game.ClientState.Objects.Types;
using ECommons.Throttlers;
using FFXIVClientStructs.FFXIV.Client.Game.Control;
using FFXIVClientStructs.FFXIV.Client.UI;
using FFXIVClientStructs.FFXIV.Component.GUI;
using Lumina.Excel.Sheets;
using System.Collections.Generic;
using System.Threading.Tasks;
using GameObject = FFXIVClientStructs.FFXIV.Client.Game.Object.GameObject;

namespace GatherChill.GrandCompany;

public sealed unsafe class GcDelivery
{
    private enum Step
    {
        Travel,
        Talk,
        Board,
        HandIn,
        Reward,
        Verify,
        Closing,
    }

    private Step step;
    private GcRequest? wanted;
    private DateTime startedAt;
    private DateTime stepAt;
    private DateTime askedAt;
    private DateTime tabAt;
    private DateTime pickedAt;
    private DateTime queryAt;
    private DateTime progressAt;
    private DateTime closedAt;
    private Vector3 progressPosition;
    private Task<List<Vector3>>? query;
    private bool ownsPath;
    private bool ownsLifestream;
    private bool sawLifestreamBusy;
    private bool ownsDialogs;
    private bool openedMissions;
    private bool openedHandIn;
    private bool picked;
    private bool handOverSent;
    private int heldBefore;
    private int asks;
    private int slotTries;
    private uint officer;
    private string completion = string.Empty;

    public bool Running { get; private set; }
    public bool Failed { get; private set; }
    public string Status { get; private set; } = "Idle";
    public IReadOnlyList<GcRequest> Requests { get; private set; } = Array.Empty<GcRequest>();
    public bool Delivered { get; private set; }
    public bool AlreadyDone { get; private set; }

    public void Start(GcRequest? request = null)
    {
        Stop("Restarting GC delivery");
        wanted = request;
        Requests = Array.Empty<GcRequest>();
        Failed = Delivered = AlreadyDone = false;
        picked = handOverSent = sawLifestreamBusy = openedMissions = openedHandIn = false;
        asks = slotTries = heldBefore = 0;
        askedAt = tabAt = pickedAt = DateTime.MinValue;
        officer = GcBoard.OfficerFor(GcBoard.Company);

        var invalidRequest = request is { } r
            && (!GcBoard.Supported(r.ClassJob) || r.Tab != EGcTab.Provisioning || r.ItemId == 0 || r.Requested <= 0);
        if (officer == 0 || invalidRequest)
        {
            Failed = true;
            Status = "Invalid request";
            return;
        }

        if (GcUi.PromptOpen || GcUi.Open("SelectString") != null || GcUi.Open("Request") != null ||
            GcUi.Open("ContextIconMenu") != null || GcUi.Open("GrandCompanySupplyList") != null ||
            GcUi.Open("GrandCompanySupplyReward") != null)
        {
            Failed = true;
            Status = "Close open dialogs first";
            return;
        }

        Running = true;
        startedAt = DateTime.UtcNow;
        Go(Step.Travel, "Heading to the Grand Company");
    }

    public void Stop(string why)
    {
        Running = false;
        Status = why;
        try
        {
            StopTravel();
        }
        catch (Exception ex)
        {
            Svc.Log.Warning(ex, "[GC] Could not stop owned travel");
        }

        try
        {
            CloseOwnedDialogs();
        }
        catch (Exception ex)
        {
            Svc.Log.Warning(ex, "[GC] Could not close owned delivery windows");
        }
    }

    public void Update()
    {
        if (!Running)
            return;

        try
        {
            if (DateTime.UtcNow - startedAt > TimeSpan.FromMinutes(5))
            {
                Fail("Delivery timed out");
                return;
            }

            if (!GcUi.Available)
                return;

            if (GcUi.PromptOpen)
            {
                Fail("Unexpected confirmation open");
                return;
            }

            if (step != Step.Travel && step != Step.Closing && DateTime.UtcNow - stepAt > TimeSpan.FromSeconds(30))
            {
                Fail($"Timed out: {Status}");
                return;
            }

            switch (step)
            {
                case Step.Travel: Travel(); break;
                case Step.Talk: Talk(); break;
                case Step.Board: ReadBoard(); break;
                case Step.HandIn: FillHandIn(); break;
                case Step.Reward: Reward(); break;
                case Step.Verify: Verify(); break;
                case Step.Closing: Leave(); break;
            }
        }
        catch (Exception ex)
        {
            Svc.Log.Error(ex, "[GC] Delivery failed");
            Fail("Delivery error; see log");
        }
    }

    private void Travel()
    {
        var lifestream = P.lifestream;
        if (lifestream.Installed && lifestream.IsBusy())
        {
            if (ownsLifestream)
                sawLifestreamBusy = true;
            Status = "Waiting for Lifestream to reach the Grand Company";
            return;
        }

        if (ownsLifestream && sawLifestreamBusy)
            ownsLifestream = false;

        var player = Svc.Objects.LocalPlayer;
        var target = NearestOfficer();
        if (player == null)
            return;

        if (target == null)
        {
            if (!lifestream.Installed)
            {
                Fail("Lifestream required");
                return;
            }

            if (asks > 0 && DateTime.UtcNow - askedAt < TimeSpan.FromSeconds(30))
                return;

            if (asks >= 2)
            {
                Fail("Lifestream didn't reach the GC");
                return;
            }

            asks++;
            askedAt = DateTime.UtcNow;
            ownsLifestream = true;
            sawLifestreamBusy = false;
            lifestream.ExecuteCommand("gc");
            Status = "Travelling to the Grand Company";
            return;
        }

        ownsLifestream = false;
        if (Vector3.Distance(player.Position, target.Position) <= 4f)
        {
            StopApproach();
            Go(Step.Talk, "Talking to the personnel officer");
        }
        else
        {
            Approach(player.Position, target.Position);
        }
    }

    private void Approach(Vector3 here, Vector3 destination)
    {
        var navmesh = P.navmesh;
        if (!navmesh.Installed)
        {
            Fail("vnavmesh required");
            return;
        }

        if (!navmesh.IsReady())
        {
            Status = "Waiting for the navmesh";
            return;
        }

        if (query != null)
        {
            if (DateTime.UtcNow - queryAt > TimeSpan.FromSeconds(20))
            {
                Fail("Pathfind timed out");
                return;
            }

            if (!query.IsCompleted)
                return;

            var done = query;
            query = null;
            if (done.IsFaulted || done.IsCanceled)
            {
                _ = done.Exception;
                Fail("No path to officer");
                return;
            }

            var path = done.Result;
            if (path == null || path.Count == 0 || Vector3.Distance(path[^1], destination) > 4f)
            {
                Fail("Path ends short of officer");
                return;
            }

            if (navmesh.IsRunning())
            {
                Fail("Other navigation started");
                return;
            }

            navmesh.MoveTo(path, false);
            ownsPath = true;
            progressAt = DateTime.UtcNow;
            progressPosition = here;
            Status = "Walking to the personnel officer";
            return;
        }

        if (ownsPath)
        {
            if (Vector3.Distance(here, progressPosition) >= 0.25f)
            {
                progressPosition = here;
                progressAt = DateTime.UtcNow;
            }

            if (DateTime.UtcNow - progressAt > TimeSpan.FromSeconds(5))
                Fail("Stuck before officer");
            return;
        }

        if (navmesh.IsRunning() || navmesh.PathfindInProgress_Simple())
        {
            Status = "Waiting for other navigation to finish";
            return;
        }

        query = navmesh.Pathfind(here, destination, false);
        queryAt = DateTime.UtcNow;
        Status = "Finding a path to the personnel officer";
        if (query == null)
            Fail("Navmesh rejected path");
    }

    private void Talk()
    {
        if (ownsDialogs && GcUi.Open("GrandCompanySupplyList") != null)
        {
            openedMissions = true;
            Go(Step.Board, "Opening Provisioning");
            return;
        }

        if (ownsDialogs && GcUi.Open("SelectString") != null)
        {
            if (EzThrottler.Throttle("GatherChill.GC.Missions", 1000))
                openedMissions |= GcUi.Select(0);
            return;
        }

        var target = NearestOfficer();
        if (target == null)
        {
            Fail("Officer not found");
            return;
        }

        if (EzThrottler.Throttle("GatherChill.GC.Talk", 2000))
        {
            Svc.Targets.Target = target;
            var targets = TargetSystem.Instance();
            if (targets != null && targets->InteractWithObject((GameObject*)target.Address, false) != 0)
                ownsDialogs = true;
        }
    }

    private void ReadBoard()
    {
        var board = (AddonGrandCompanySupplyList*)GcUi.Open("GrandCompanySupplyList");
        if (board == null)
            return;

        if (tabAt == DateTime.MinValue)
        {
            GcUi.Fire(&board->AtkUnitBase, 0, 1);
            tabAt = DateTime.UtcNow;
            return;
        }

        if (DateTime.UtcNow - tabAt < TimeSpan.FromSeconds(1) || !GcBoard.Ready || board->SupplyProvisioningList == null)
            return;

        if (board->SelectedTab != 1)
        {
            Fail("Provisioning tab didn't open");
            return;
        }

        var found = GcBoard.Read(out var problem);
        var rows = GcBoard.Rows(board);
        Svc.Log.Information($"[GC] Provisioning board (filter {board->SelectedFilter}): " +
            string.Join(", ", rows.Select((row, index) => $"{index}: '{row.ItemName}'{(row.Greyed ? " (disabled)" : string.Empty)}")));

        if (found == null)
        {
            Fail(problem);
            return;
        }

        if (rows.Any(row => string.IsNullOrWhiteSpace(row.ItemName)) ||
            found.Any(request => request.ItemId == 0 || request.Requested <= 0 || string.IsNullOrWhiteSpace(request.ItemName)))
        {
            Fail("Board unreadable");
            return;
        }

        Requests = found.Where(request => GcBoard.RowOf(rows, request.ItemName) >= 0).ToArray();

        if (wanted is not { } expected)
        {
            Finish("Read today's provisioning board");
            return;
        }

        var matching = found.Where(request => request.ClassJob == expected.ClassJob).ToArray();
        if (matching.Length != 1)
        {
            Fail("Job not on board");
            return;
        }

        if (matching[0].ItemId != expected.ItemId || matching[0].Requested != expected.Requested || matching[0].Tab != expected.Tab)
        {
            Fail("Request changed; refresh");
            return;
        }

        var row = GcBoard.RowOf(rows, matching[0].ItemName);
        if (row < 0)
        {
            AlreadyDone = true;
            Finish($"{expected.ItemName} is already delivered today");
            return;
        }

        heldBefore = GcBoard.Held(expected.ItemId);
        if (heldBefore < expected.Requested)
        {
            Fail($"Need {expected.Requested - heldBefore} more {expected.ItemName}");
            return;
        }

        openedHandIn = true;
        GcUi.Fire(&board->AtkUnitBase, 1, row);
        Go(Step.HandIn, $"Delivering {expected.ItemName}");
    }

    private void FillHandIn()
    {
        var request = (AddonRequest*)GcUi.Open("Request");
        if (request == null || wanted is not { } expected)
            return;

        var menu = GcUi.Open("ContextIconMenu");
        if (menu != null)
        {
            if (EzThrottler.Throttle("GatherChill.GC.Item", 800))
            {
                if (!Svc.Data.GetExcelSheet<Item>().TryGetRow(expected.ItemId, out var item))
                {
                    Fail("Unknown item");
                    return;
                }

                GcUi.Fire(menu, 0, 0, (int)item.Icon, 0);
                picked = true;
                pickedAt = DateTime.UtcNow;
            }
            return;
        }

        if (picked && DateTime.UtcNow - pickedAt < TimeSpan.FromMilliseconds(500))
            return;

        if (picked && request->HandOverButton != null && request->HandOverButton->IsEnabled)
        {
            request->AtkUnitBase.FireCallbackInt(0);
            handOverSent = true;
            Go(Step.Reward, "Confirming the provisioning reward");
        }
        else if (EzThrottler.Throttle("GatherChill.GC.Slot", 3000))
        {
            if (++slotTries > 3)
            {
                Fail("Hand-in failed");
                return;
            }

            GcUi.Fire(&request->AtkUnitBase, 2, 0, 44, 0);
        }
    }

    private void Reward()
    {
        var reward = GcUi.Open("GrandCompanySupplyReward");
        if (reward == null)
            return;

        reward->FireCallbackInt(0);
        Go(Step.Verify, "Checking the delivery");
    }

    private void Verify()
    {
        if (wanted is not { } expected || !handOverSent)
        {
            Fail("Delivery unverified");
            return;
        }

        if (GcBoard.Held(expected.ItemId) <= heldBefore - expected.Requested)
        {
            Delivered = true;
            Requests = Requests.Where(request => request.ClassJob != expected.ClassJob).ToArray();
            Finish($"Delivered {expected.ItemName}");
        }
        else if (DateTime.UtcNow - stepAt > TimeSpan.FromSeconds(5))
        {
            Fail("Hand-in not accepted");
        }
    }

    private IGameObject? NearestOfficer()
    {
        var player = Svc.Objects.LocalPlayer;
        if (player == null)
            return null;

        return Svc.Objects
            .Where(obj => obj.BaseId == officer && obj.IsTargetable)
            .OrderBy(obj => Vector3.DistanceSquared(obj.Position, player.Position))
            .FirstOrDefault();
    }

    private void Finish(string message)
    {
        completion = message;
        Go(Step.Closing, message);
        closedAt = stepAt;
    }

    private void Leave()
    {
        if (DateTime.UtcNow - stepAt > TimeSpan.FromSeconds(6))
        {
            Stop(completion);
        }
        else if (GcUi.Open("GrandCompanySupplyList") != null)
        {
            closedAt = DateTime.UtcNow;
            if (EzThrottler.Throttle("GatherChill.GC.CloseBoard", 500))
                GcUi.Open("GrandCompanySupplyList")->FireCallbackInt(-1);
        }
        else if (GcUi.Open("SelectString") != null)
        {
            closedAt = DateTime.UtcNow;
            if (EzThrottler.Throttle("GatherChill.GC.Leave", 500))
                GcUi.Select(-1);
        }
        else if (DateTime.UtcNow - closedAt >= TimeSpan.FromSeconds(1))
        {
            Stop(completion);
        }
    }

    private void Go(Step next, string message)
    {
        step = next;
        stepAt = DateTime.UtcNow;
        Status = message;
    }

    private void Fail(string why)
    {
        Failed = true;
        Stop(why);
    }

    private void StopApproach()
    {
        var pending = query;
        query = null;
        pending?.ContinueWith(done => _ = done.Exception, TaskContinuationOptions.OnlyOnFaulted | TaskContinuationOptions.ExecuteSynchronously);

        var stopPath = ownsPath;
        ownsPath = false;
        if (stopPath && P.navmesh.Installed && P.navmesh.IsRunning())
            P.navmesh.PathStop();
    }

    private void StopTravel()
    {
        var abortLifestream = ownsLifestream;
        ownsLifestream = false;
        try
        {
            StopApproach();
        }
        finally
        {
            if (abortLifestream && P.lifestream.Installed)
                GcUi.AbortLifestream();
        }
    }

    private void CloseOwnedDialogs()
    {
        if (!ownsDialogs)
            return;

        ownsDialogs = false;
        if (openedHandIn)
        {
            GcUi.Close("ContextIconMenu");
            GcUi.Close("Request");
        }

        if (handOverSent)
            GcUi.Close("GrandCompanySupplyReward");

        if (openedMissions)
            GcUi.Close("GrandCompanySupplyList");

        GcUi.Close("SelectString");
    }
}
