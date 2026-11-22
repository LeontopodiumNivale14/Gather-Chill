using ECommons.GameHelpers;
using ECommons.UIHelpers.AddonMasterImplementations;
using FFXIVClientStructs.FFXIV.Component.GUI;

namespace GatherChill.GrandCompany;

internal static unsafe class GcUi
{
    public static bool Available
        => Player.Available
        && !Svc.Condition[ConditionFlag.BetweenAreas]
        && !Svc.Condition[ConditionFlag.BetweenAreas51]
        && !Svc.Condition[ConditionFlag.WatchingCutscene]
        && !Svc.Condition[ConditionFlag.WatchingCutscene78]
        && !Svc.Condition[ConditionFlag.OccupiedInCutSceneEvent];

    public static bool PromptOpen => Open("SelectYesno") != null;

    public static void AbortLifestream() => Svc.PluginInterface.GetIpcSubscriber<object>("Lifestream.Abort").InvokeAction();

    public static AtkUnitBase* Open(string name)
    {
        var addon = (AtkUnitBase*)Svc.GameGui.GetAddonByName(name).Address;
        if (addon == null || !addon->IsVisible || addon->UldManager.LoadedState != AtkLoadState.Loaded)
            return null;

        return addon;
    }

    public static bool Select(int index)
    {
        var addon = Open("SelectString");
        if (addon == null || !IsAddonReady(addon))
            return false;

        var entries = new AddonMaster.SelectString(addon).Entries;
        if (index < 0)
            index = entries.Length - 1;

        if (index < 0 || index >= entries.Length)
            return false;

        entries[index].Select();
        return true;
    }

    public static void Close(string name)
    {
        try
        {
            var addon = Open(name);
            if (addon != null)
                addon->Close(true);
        }
        catch (Exception ex)
        {
            Svc.Log.Warning(ex, $"[GC] Could not close {name}");
        }
    }

    public static void Fire(AtkUnitBase* addon, params int[] numbers)
    {
        var values = stackalloc AtkValue[numbers.Length];
        for (var i = 0; i < numbers.Length; i++)
        {
            values[i].Type = AtkValueType.Int;
            values[i].Int = numbers[i];
        }

        addon->FireCallback((uint)numbers.Length, values, true);
    }
}
