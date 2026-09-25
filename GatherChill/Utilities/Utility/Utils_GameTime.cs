using FFXIVClientStructs.FFXIV.Client.System.Framework;

namespace GatherChill.Utilities.Utility;

public static partial class Utils
{
    // Eorzea time runs 3600/175 = ~20.571x faster than real time.
    public const double EorzeaRatio = 3600.0 / 175.0;
    public const int EorzeaMinutesPerDay = 24 * 60;

    /// <summary>
    /// Current Eorzea time as minutes since midnight.
    /// Uses the game's own Eorzea clock, falling back to the system clock if unavailable.
    /// </summary>
    public static unsafe double CurrentEorzeaMinutes()
    {
        var framework = Framework.Instance();

        // ClientTime.EorzeaTime is in Eorzea seconds since the epoch.
        var eorzeaSeconds = framework != null
            ? (double)framework->ClientTime.EorzeaTime
            : DateTimeOffset.UtcNow.ToUnixTimeMilliseconds() / 1000.0 * EorzeaRatio;

        return eorzeaSeconds / 60.0 % EorzeaMinutesPerDay;
    }

    /// <summary>
    /// Current Eorzea time in HHMM format (e.g. 1430 = 14:30 ET).
    /// </summary>
    public static int CurrentEorzeaTime()
    {
        var minutes = (int)CurrentEorzeaMinutes();
        return minutes / 60 * 100 + minutes % 60;
    }

    public static TimeSpan EorzeaMinutesToReal(double eorzeaMinutes)
        => TimeSpan.FromSeconds(eorzeaMinutes * 60.0 / EorzeaRatio);
}