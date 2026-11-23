using ECommons.Throttlers;
using FFXIVClientStructs.FFXIV.Client.UI.Agent;
using System.Collections.Generic;

namespace GatherChill.GrandCompany;

public sealed unsafe class GcTimers
{
    private enum Step
    {
        Opening,
        Tab,
        Detail,
        Reading,
        Closing,
    }

    private Step step;
    private DateTime startedAt;
    private DateTime stepAt;
    private bool ownsWindow;
    private bool ownsDetail;

    public bool Running { get; private set; }
    public bool Failed { get; private set; }
    public string Status { get; private set; } = "Idle";
    public IReadOnlyList<GcRequest> Requests { get; private set; } = Array.Empty<GcRequest>();

    public void Start()
    {
        Stop("Restarting Timers read");
        Failed = false;
        Requests = Array.Empty<GcRequest>();
        if (GcUi.Open("ContentsInfo") != null || GcUi.Open("ContentsInfoDetail") != null)
        {
            Failed = true;
            Status = "Close Timers first";
            return;
        }

        Running = true;
        startedAt = DateTime.UtcNow;
        Status = "Opening Timers";
        Go(Step.Opening);
    }

    public void Stop(string why)
    {
        var closeDetail = ownsDetail;
        var closeWindow = ownsWindow;
        ownsDetail = ownsWindow = false;
        Running = false;
        Status = why;
        try
        {
            if (closeDetail)
                GcUi.Close("ContentsInfoDetail");
            if (closeWindow)
                GcUi.Close("ContentsInfo");
        }
        catch (Exception ex)
        {
            Svc.Log.Warning(ex, "[GC] Could not close owned Timers windows");
        }
    }

    public void Update()
    {
        if (!Running || !GcUi.Available)
            return;

        if (step != Step.Closing && DateTime.UtcNow - startedAt > TimeSpan.FromSeconds(8))
        {
            Fail("Timers read timed out");
            return;
        }

        switch (step)
        {
            case Step.Opening:
            {
                var window = GcUi.Open("ContentsInfo");
                if (window == null)
                {
                    if (EzThrottler.Throttle("GatherChill.GC.Timers.Open", 2000))
                    {
                        var agent = AgentContentsTimer.Instance();
                        if (agent == null)
                        {
                            Fail("Timers is unavailable");
                            break;
                        }

                        ownsWindow = true;
                        agent->Show();
                    }
                }
                else
                {
                    GcUi.Fire(window, 15, 0);
                    Go(Step.Tab);
                }
                break;
            }
            case Step.Tab:
            {
                if (!Settled())
                    break;

                var window = GcUi.Open("ContentsInfo");
                if (window == null)
                {
                    Fail("Timers closed early");
                    break;
                }

                ownsDetail = true;
                GcUi.Fire(window, 12, 1);
                Status = "Reading Grand Company provisioning";
                Go(Step.Detail);
                break;
            }
            case Step.Detail:
                if (GcUi.Open("ContentsInfoDetail") != null)
                    Go(Step.Reading);
                break;
            case Step.Reading:
            {
                if (!Settled())
                    break;

                var found = GcBoard.FromTimers(GcUi.Open("ContentsInfoDetail"), out var problem);
                if (found == null)
                {
                    Fail($"Timers: {problem}");
                    break;
                }

                Requests = found;
                Status = found.Count == 0 ? "No Miner or Botanist requests left today" : "Read today's provisioning requests";
                Go(Step.Closing);
                break;
            }
            case Step.Closing:
                if (DateTime.UtcNow - stepAt > TimeSpan.FromSeconds(3))
                {
                    Stop(Status);
                }
                else if (ownsDetail && GcUi.Open("ContentsInfoDetail") != null)
                {
                    if (EzThrottler.Throttle("GatherChill.GC.Timers.CloseDetail", 500))
                        GcUi.Open("ContentsInfoDetail")->FireCallbackInt(13);
                }
                else if (ownsWindow && GcUi.Open("ContentsInfo") != null)
                {
                    if (EzThrottler.Throttle("GatherChill.GC.Timers.Close", 500))
                        GcUi.Open("ContentsInfo")->FireCallbackInt(-1);
                }
                else
                {
                    ownsDetail = ownsWindow = false;
                    Running = false;
                }
                break;
        }
    }

    private bool Settled() => DateTime.UtcNow - stepAt >= TimeSpan.FromMilliseconds(400);

    private void Go(Step next)
    {
        step = next;
        stepAt = DateTime.UtcNow;
    }

    private void Fail(string why)
    {
        Failed = true;
        Status = why;
        Go(Step.Closing);
    }
}
