using System.Numerics;

namespace GatherChill.Utilities.Utility;

/// <summary>
/// Lightweight state for the debug window and stuck-recovery logging.
/// Not persisted — reset when movement stops or the scheduler clears tasks.
/// </summary>
internal static class NavmeshRuntime
{
    /// <summary>True when GatherChill started the current vnavmesh path (so we only stop paths we own).</summary>
    public static bool OwnsPath { get; private set; }
    public static string LastMoveContext { get; private set; } = "";
    public static Vector3 LastDestination { get; private set; }
    public static float LastCloseRange { get; private set; }
    public static int StuckAttempts { get; private set; }
    public static string LastFailure { get; private set; } = "";

    public static void SetMoveAttempt(string context, Vector3 destination, float closeRange)
    {
        LastMoveContext = context;
        LastDestination = destination;
        LastCloseRange = closeRange;
        LastFailure = "";
    }

    public static void SetOwnsPath(bool owns) => OwnsPath = owns;

    public static void SetStuckAttempts(int attempts) => StuckAttempts = attempts;

    public static void SetFailure(string message) => LastFailure = message;

    public static void Reset()
    {
        OwnsPath = false;
        LastMoveContext = "";
        LastDestination = default;
        LastCloseRange = 0;
        StuckAttempts = 0;
        LastFailure = "";
    }
}
