namespace GatherChill.ConfigFiles;

public partial class Config
{
    /// <summary>Master switch for vnavmesh pathing in routes and the route editor.</summary>
    public bool NavmeshMovementEnabled { get; set; } = true;

    /// <summary>Log per-tick movement debug from Task_NavmeshMove (Settings → Navigation).</summary>
    public bool NavmeshVerboseLogging { get; set; } = false;

    /// <summary>Max horizontal distance to node before interact is attempted.</summary>
    public float NavmeshInteractDistance { get; set; } = 4f;

    /// <summary>How many interact attempts before skipping a node on a route run.</summary>
    public int NavmeshInteractRetries { get; set; } = 3;
}
