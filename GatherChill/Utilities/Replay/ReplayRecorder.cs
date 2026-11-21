using ECommons.GameHelpers;
using GatherChill.Enums;
using GatherChill.Scheduler;
using GatherChill.Utilities.GatheringHelpers;
using GatherChill.Utilities.Utility;
using System.Collections.Generic;
using System.Diagnostics;

namespace GatherChill.Utilities.Replay;

internal static class ReplayRecorder
{
    private const int MaxNodesPerSession = 250;   // a new session starts after this, so files stay a sane size
    private const int SaveEveryNodes = 5;         // autosave, so a crash doesn't lose the whole session
    private const long SwingSettleMs = 1500;      // how long after a swing we keep watching the item count

    private static ReplaySession? _session;
    private static string _sessionPath = "";
    private static ReplayNode? _node;

    private static readonly Stopwatch SessionClock = new();
    private static readonly Stopwatch NodeClock = new();
    private static DateTime _lastActivityUtc = DateTime.UtcNow;

    // Latest known state, stamped onto every event
    private static int _gp;
    private static int _integrity;
    private static int _maxIntegrity;

    // Decisions are only recorded when the reason for a buff changes
    private static readonly Dictionary<string, string> LastDecision = new();

    // Swing tracking
    private static bool _wasExecuting;
    private static bool _gatherPending;
    private static uint _gatherItemId;
    private static int _baselineCount;
    private static ReplayEvent? _openSwing;
    private static long _openSwingMs;
    private static long _buffEdgeUntilMs;
    private static int _openSwingBaseline;

    // Gathering window item list, only written when the set of items changes
    private static string _lastWindowSignature = "";
    private static List<ReplayItemSlot> _lastWindowSlots = new();

    private static bool Enabled => C.Replay_Enabled;

    #region Session / node lifecycle

    private static void BeginSession()
    {
        if (_session != null)
            return;

        var now = DateTime.UtcNow;
        _session = new ReplaySession
        {
            StartedUtc = now,
            EndedUtc = now,
            Name = $"{now.ToLocalTime():yyyy-MM-dd HH:mm}",
        };
        _sessionPath = ReplayStore.NewFilePath(_session);
        SessionClock.Restart();
        _lastActivityUtc = now;
    }

    // Called every tick the gathering task is running. Stops doing shit when a node is open
    public static void EnsureNode(uint itemId, uint routeId, GatherNodeKind kind)
    {
        if (!Enabled || _node != null)
            return;

        // Long sessions roll over into a new file
        if (_session is { } current && current.Nodes.Count >= MaxNodesPerSession)
            EndSession("Node limit reached, starting a new session");

        BeginSession();

        var name = Gather_Util.Sheet_ItemInfo.TryGetValue(itemId, out var info) ? info.Name : $"Item {itemId}";

        _node = new ReplayNode
        {
            NodeId = Svc.Objects.LocalPlayer?.TargetObject?.BaseId ?? 0,
            RouteId = routeId,
            ItemId = itemId,
            ItemName = name,
            Kind = kind,
            Job = Player.Job,
            StartedUtc = DateTime.UtcNow,
            ItemsBefore = Utils.GetItemCount(itemId),
        };

        NodeClock.Restart();
        LastDecision.Clear();
        _wasExecuting = false;
        _gatherPending = false;
        _openSwing = null;
        _lastWindowSignature = "";
        _lastWindowSlots = new();
        _lastActivityUtc = DateTime.UtcNow;
    }

    public static void EndNode(string reason)
    {
        if (_node is not { } node || _session is not { } session)
            return;

        // Last chance to catch the items of the swing that was still settling
        if (_openSwing != null)
        {
            var settledCount = Utils.GetItemCount(_gatherItemId);
            _openSwing.ItemDelta = settledCount - _openSwingBaseline;
            _openSwing.InventoryCount = settledCount;
            _openSwing = null;
        }

        // The final swing often closes the window before its end is seen, so it never got an event. Write it from the inventory.
        if (_gatherPending)
        {
            var finalCount = Utils.GetItemCount(_gatherItemId);
            var finalDelta = finalCount - _baselineCount;

            // finalDelta != 0 means items arrived; _wasExecuting means the action was still playing when the window closed.
            // Neither = the click never turned into a swing, so there is nothing to record.
            if (finalDelta != 0 || _wasExecuting)
            {
                Add(ReplayEventKind.Swing, "Gather", "Window closed on this swing", e =>
                {
                    e.ItemDelta = finalDelta;
                    e.InventoryCount = finalCount;
                });
            }

            _gatherPending = false;
        }

        node.DurationMs = NodeClock.ElapsedMilliseconds;
        node.EndReason = reason;
        node.ItemsAfter = Utils.GetItemCount(node.ItemId);

        session.Nodes.Add(node);
        _node = null;
        _lastActivityUtc = DateTime.UtcNow;

        if (session.Nodes.Count % SaveEveryNodes == 0)
            ReplayStore.Save(_sessionPath, session, C.Replay_KeepAmount, sync: false);
    }

    public static void EndSession(string reason, bool sync = false)
    {
        EndNode(reason);

        if (_session is not { } session)
            return;

        session.EndedUtc = DateTime.UtcNow;
        session.EndReason = reason;

        // Empty sessions aren't worth a file
        if (session.Nodes.Count > 0 || session.Events.Count > 0)
            ReplayStore.Save(_sessionPath, session, C.Replay_KeepAmount, sync);

        _session = null;
        _sessionPath = "";
    }

    public static void Tick()
    {
        if (_session == null)
            return;

        if (!Enabled)
        {
            EndSession("Recording disabled");
            return;
        }

        // _node == null reads "we're not inside a gathering window"
        if (_node == null && Gather_Helper.State == IceState.Idle)
            EndSession("Idle");
    }

    #endregion

    #region Recording

    /// <summary>Latest GP and integrity, stamped onto every event until it's called again.</summary>
    public static void SetState(int gp, int integrity, int maxIntegrity)
    {
        _gp = gp;
        _integrity = integrity;
        _maxIntegrity = maxIntegrity;
    }

    public static void MarkCollectable()
    {
        if (_node != null)
            _node.IsCollectable = true;
    }

    public static void AddPlan(ReplayPlan plan, string summary)
    {
        if (_node == null)
            return;

        _node.Plans.Add(plan);
        LastDecision.Clear();
        Add(ReplayEventKind.Plan, plan.ProfileName, summary);
    }

    /// <summary>
    /// Bountiful Yield: what the plan assumed vs what the window showed after the cast.
    /// Stored on the active plan (for the viewer) and written as a note on the timeline.
    /// </summary>
    public static void BYIIObserved(int planned, int observed)
    {
        if (_node == null)
            return;

        if (_node.Plans.Count > 0)
        {
            var plan = _node.Plans[^1];
            plan.BYIIPlanned = planned;
            plan.BYIIObserved = observed;
        }

        var detail = planned == observed
            ? $"Game gave +{observed} (matches the plan)"
            : $"Game gave +{observed}, the plan assumed +{planned}";

        Add(ReplayEventKind.Note, "BYII amount", detail);
    }

    /// <summary>
    /// The gathering window's item list. The signature is just the item ids, so this only builds and writes a snapshot
    /// when something appears or disappears (a hidden item being revealed, for example).
    /// </summary>
    public static void WindowItems(string signature, Func<List<ReplayItemSlot>> build)
    {
        if (_node == null || signature == _lastWindowSignature)
            return;

        var first = _lastWindowSignature == "";
        _lastWindowSignature = signature;

        var slots = build();

        var previousIds = _lastWindowSlots.Select(x => x.ItemId).ToHashSet();
        var currentIds = slots.Select(x => x.ItemId).ToHashSet();

        string Describe(ReplayItemSlot slot) => slot.Hidden ? $"{slot.Name} (hidden)" : slot.Name;

        string detail;
        if (first)
        {
            detail = string.Join(", ", slots.Select(Describe));
        }
        else
        {
            var added = slots.Where(x => !previousIds.Contains(x.ItemId)).Select(Describe).ToList();
            var removed = _lastWindowSlots.Where(x => !currentIds.Contains(x.ItemId)).Select(x => x.Name).ToList();

            var parts = new List<string>();
            if (added.Count > 0)
                parts.Add($"appeared: {string.Join(", ", added)}");
            if (removed.Count > 0)
                parts.Add($"gone: {string.Join(", ", removed)}");

            detail = parts.Count > 0 ? string.Join(" | ", parts) : "list changed";
        }

        _lastWindowSlots = slots;
        Add(ReplayEventKind.Note, first ? "Window items" : "Window items changed", detail, e => e.Items = slots);
    }

    /// <summary>Records why a buff isn't being cast. Skipped when the reason is the same as last time, or this would flood the log every tick.</summary>
    public static void Decision(string label, string reason, string detail = "")
    {
        if (_node == null)
            return;

        if (LastDecision.TryGetValue(label, out var previous) && previous == reason)
            return;

        LastDecision[label] = reason;
        Add(ReplayEventKind.Decision, label, string.IsNullOrEmpty(detail) ? reason : $"{reason} ({detail})");
    }

    public static void BuffCast(string label, string detail = "", int? progression = null, int? collectability = null)
    {
        if (_node == null)
            return;

        // The cast supersedes whatever gather was waiting to resolve
        _gatherPending = false;

        Add(ReplayEventKind.BuffCast, label, detail, e =>
        {
            e.Progression = progression;
            e.Collectability = collectability;
        });

        _buffEdgeUntilMs = NodeClock.ElapsedMilliseconds + 2000;
    }

    /// <summary>A gather action was sent. The Swing event is written when it resolves.</summary>
    public static void NoteGather(uint itemId)
    {
        if (_node == null || _gatherPending)
            return;

        _gatherItemId = itemId;
        _baselineCount = Utils.GetItemCount(itemId);
        _gatherPending = true;
    }

    /// <summary>Call every tick with the ExecutingGatheringAction flag. A swing is written when it goes from true to false.</summary>
    public static void TrackExecuting(bool executing)
    {
        if (_node == null)
        {
            _wasExecuting = executing;
            return;
        }

        // The item count lags the action, so a recent swing keeps updating for a moment
        if (_openSwing is { } swing)
        {
            var count = Utils.GetItemCount(_gatherItemId);
            swing.ItemDelta = count - _openSwingBaseline;
            swing.InventoryCount = count;

            if (NodeClock.ElapsedMilliseconds - _openSwingMs > SwingSettleMs)
                _openSwing = null;
        }

        if (_wasExecuting && !executing)
        {
            if (NodeClock.ElapsedMilliseconds < _buffEdgeUntilMs)
            {
                _buffEdgeUntilMs = 0;
            }
            else if (_gatherPending)
            {
                _gatherPending = false;
                _openSwingBaseline = _baselineCount;
                _openSwing = Add(ReplayEventKind.Swing, "Gather", "", e =>
                {
                    var count = Utils.GetItemCount(_gatherItemId);
                    e.InventoryCount = count;
                    e.ItemDelta = count - _baselineCount;
                });
                _openSwingMs = NodeClock.ElapsedMilliseconds;
            }
        }

        _wasExecuting = executing;
    }

    public static void Desynth(uint itemId)
    {
        if (!Enabled)
            return;

        BeginSession();

        var name = Gather_Util.Sheet_ItemInfo.TryGetValue(itemId, out var info) ? info.Name : $"Item {itemId}";
        Add(ReplayEventKind.Desynth, name);
    }

    public static void Note(string text)
    {
        if (!Enabled || _session == null)
            return;

        Add(ReplayEventKind.Note, "Note", text);
    }

    private static ReplayEvent? Add(ReplayEventKind kind, string label, string detail = "", Action<ReplayEvent>? configure = null)
    {
        if (_session == null)
            return null;

        var ev = new ReplayEvent
        {
            Kind = kind,
            Label = label,
            Detail = detail,
            Gp = _gp,
            Integrity = _integrity,
            MaxIntegrity = _maxIntegrity,
        };

        configure?.Invoke(ev);
        _lastActivityUtc = DateTime.UtcNow;

        if (_node != null)
        {
            ev.Ms = NodeClock.ElapsedMilliseconds;
            ev.PlanIndex = _node.Plans.Count > 0 ? _node.Plans.Count - 1 : null;
            _node.Events.Add(ev);
        }
        else
        {
            ev.Ms = SessionClock.ElapsedMilliseconds;
            _session.Events.Add(ev);
        }

        return ev;
    }

    #endregion
}