using System.Collections.Generic;
using static GatherChill.Utilities.GatheringHelpers.Gather_Util;

namespace GatherChill.Utilities.GatheringHelpers;

internal static class EorzeaTimeUtil
{
    public static int GetCurrentTimeRaw()
    {
        var et = DateTime.UtcNow.AddSeconds(886919760);
        return et.Hour * 100 + et.Minute;
    }

    public static bool IsWindowActive(EorzeaTimeWindow window, int now)
    {
        if (window.Start == window.End)
            return false;

        if (window.Start < window.End)
            return now >= window.Start && now < window.End;

        return now >= window.Start || now < window.End;
    }

    public static bool IsAnyWindowActive(IReadOnlyList<EorzeaTimeWindow> windows, int now)
    {
        foreach (var window in windows)
        {
            if (IsWindowActive(window, now))
                return true;
        }

        return false;
    }

    public static TimedPriority GetTimedPriority(GatherPointInfo? sheetInfo)
    {
        if (sheetInfo == null || sheetInfo.TimedInfo.Count == 0)
            return TimedPriority.None;

        var now = GetCurrentTimeRaw();
        return IsAnyWindowActive(sheetInfo.TimedInfo, now)
            ? TimedPriority.ActiveNow
            : TimedPriority.TimedInactive;
    }

    internal enum TimedPriority
    {
        None = 0,
        TimedInactive = 1,
        ActiveNow = 2,
    }
}
