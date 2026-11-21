using ECommons.ExcelServices;
using GatherChill.Enums;
using System;
using System.Collections.Generic;

namespace GatherChill.Utilities.Replay;

public enum ReplayEventKind
{
    Plan,       // PlanProfile ran
    Decision,   // a buff was skipped/waited, only logged when the reason changes
    BuffCast,   // an action was sent (buffs, collectable actions)
    Swing,      // a gather action resolved
    Desynth,    // a reducible was purified
    NavStart,   // Placeholder... to figure out if I wanna add these or not
    NavStop,    // Placeholder... to figure out if I wanna add these or not
    Note,
}

public class ReplayEvent
{
    public ReplayEventKind Kind { get; set; }

    // How long it took for an event to start
    public long Ms { get; set; }

    public string Label { get; set; } = "";
    public string Detail { get; set; } = "";

    // States right before the event
    public int Gp { get; set; }
    public int Integrity { get; set; }
    public int MaxIntegrity { get; set; }

    // Swings: inventory change of the gathered item. Hit/miss/bonus is inferred from this in the viewer.
    public int ItemDelta { get; set; }
    public int InventoryCount { get; set; }

    // Collectables only
    public int? Progression { get; set; }
    public int? Collectability { get; set; }

    /// <summary>Index into the node's Plans for the plan that was active.</summary>
    public int? PlanIndex { get; set; }

    /// <summary>Note events only: what the gathering window was offering at that moment.</summary>
    public List<ReplayItemSlot>? Items { get; set; }
}

/// <summary>One item row of the gathering window, as it looked when the list changed.</summary>
public class ReplayItemSlot
{
    public uint ItemId { get; set; }
    public string Name { get; set; } = "";
    public int GatherChance { get; set; }
    public int BoonChance { get; set; }
    public int Amount { get; set; }          // per-swing yield the window shows
    public bool Hidden { get; set; }
    public bool Rare { get; set; }
    public bool Collectable { get; set; }
    public bool Enabled { get; set; }
}

public class ReplayPlan
{
    public int Level { get; set; }
    public int Gp { get; set; }
    public int Attempts { get; set; }        // current integrity passed to the planner
    public int MaxIntegrity { get; set; }
    public int GatherChance { get; set; }
    public int BoonChance { get; set; }
    public int BaseYield { get; set; }
    public int Need { get; set; }

    public string ProfileName { get; set; } = "";
    public List<string> RuleReasons { get; set; } = new();
    public string ProfileJson { get; set; } = "";   // snapshot, survives later edits

    public List<string> Buffs { get; set; } = new();
    public double ExpectedYield { get; set; }
    public int PlannedGp { get; set; }
    public bool MeetsNeed { get; set; }
    public List<string> ActiveBuffs { get; set; } = new();

    // Bountiful Yield bonus the plan assumed vs what the game's window showed after the cast. 0 = not measured.
    public int BYIIPlanned { get; set; }
    public int BYIIObserved { get; set; }
}

public class ReplayNode
{
    public uint NodeId { get; set; }
    public uint RouteId { get; set; }
    public uint ItemId { get; set; }
    public string ItemName { get; set; } = "";
    public GatherNodeKind Kind { get; set; }
    public bool IsCollectable { get; set; }
    public Job Job { get; set; }

    public DateTime StartedUtc { get; set; }
    public long DurationMs { get; set; }
    public string EndReason { get; set; } = "";
    public int ItemsBefore { get; set; }
    public int ItemsAfter { get; set; }

    public List<ReplayPlan> Plans { get; set; } = new();
    public List<ReplayEvent> Events { get; set; } = new();
}

public class ReplaySession
{
    public const int CurrentVersion = 1;

    public Guid Id { get; set; } = Guid.NewGuid();
    public int Version { get; set; } = CurrentVersion;
    public string Name { get; set; } = "";
    public DateTime StartedUtc { get; set; }
    public DateTime EndedUtc { get; set; }
    public string EndReason { get; set; } = "";

    public List<ReplayNode> Nodes { get; set; } = new();

    /// <summary>Events that happen between nodes (desynth, notes).</summary>
    public List<ReplayEvent> Events { get; set; } = new();
}