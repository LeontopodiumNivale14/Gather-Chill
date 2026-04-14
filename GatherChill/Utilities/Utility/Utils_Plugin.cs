using ECommons.Automation.NeoTaskManager;
using ECommons.Reflection;
using System;
using System.Collections.Generic;
using System.Text;

namespace GatherChill.Utilities.Utility;

public static partial class Utils
{
    public static bool HasPlugin(string name) => DalamudReflector.TryGetDalamudPlugin(name, out _, false, true);
    public static TaskManagerConfiguration TaskConfig => new(timeLimitMS: 10 * 60 * 3000, abortOnTimeout: false);
}
