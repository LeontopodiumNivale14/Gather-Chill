using ECommons.Automation.LegacyTaskManager;
using ECommons.UIHelpers.AddonMasterImplementations;
using FFXIVClientStructs.FFXIV.Client.UI;
using FFXIVClientStructs.FFXIV.Component.GUI;
using System.Collections.Generic;

namespace GatherChill.Scheduler.Handlers
{
    internal static unsafe class GenericManager
    {
        internal static TaskManager taskManager = new();
        static TaskManager TaskManager => taskManager;
        private static List<int> SlotsFilled { get; set; } = new();
        private static bool _pausedPandoraForRequest;
        private static bool? ConfirmOrAbort(AddonRequest* addon)
        {
            if (addon->HandOverButton != null && addon->HandOverButton->IsEnabled)
            {
                new AddonMaster.Request((IntPtr)addon).HandOver();
                return true;
            }
            return false;
        }
        private static bool? TryClickItem(AddonRequest* addon, int i)
        {
            if (SlotsFilled.Contains(i)) return true;

            var contextMenu = (AtkUnitBase*)Svc.GameGui.GetAddonByName("ContextIconMenu", 1).Address;

            if (contextMenu is null || !contextMenu->IsVisible)
            {
                var slot = i - 1;
                var unk = (44 * i) + (i - 1);

                ECommons.Automation.Callback.Fire(&addon->AtkUnitBase, false, 2, slot, 0, 0);

                return false;
            }
            else
            {
                ECommons.Automation.Callback.Fire(contextMenu, false, 0, 0, 1021003, 0, 0);
                Svc.Log.Debug($"Filled slot {i}");
                SlotsFilled.Add(i);
                return true;
            }
        }

        internal static void Tick()
        {
            // Port of Pandora AutoSelectTurnin — only run while the Request (turn-in) addon is open.
            // https://github.com/PunishXIV/PandorasBox/blob/24a4352f5b01751767c7ca7f1d4b48369be98711/PandorasBox/Features/UI/AutoSelectTurnin.cs
            if (!P.pandora.Installed)
                return;

            if (!TryGetAddonByName<AddonRequest>("Request", out var addon))
            {
                _pausedPandoraForRequest = false;
                if (SlotsFilled.Count > 0)
                {
                    SlotsFilled.Clear();
                    TaskManager.Abort();
                }
                return;
            }

            var featureEnabled = P.pandora.GetFeatureEnabled("Auto-select Turn-ins") ?? false;
            var configEnabled = P.pandora.GetConfigEnabled("Auto-select Turn-ins", "AutoSelect") ?? false;
            var pandoraHandlingTurnins = featureEnabled && configEnabled;

            if (pandoraHandlingTurnins)
                return;

            // Feature on but AutoSelect off: pause Pandora once for this Request session (not every second).
            if (featureEnabled && !configEnabled && !_pausedPandoraForRequest)
            {
                P.pandora.PauseFeature("Auto-select Turn-ins", 60_000);
                _pausedPandoraForRequest = true;
            }

            for (var i = 1; i <= addon->EntryCount; i++)
            {
                if (SlotsFilled.Contains(addon->EntryCount))
                    ConfirmOrAbort(addon);
                if (SlotsFilled.Contains(i))
                    return;
                var val = i;
                TaskManager.DelayNext($"ClickTurnin{val}", 10);
                TaskManager.Enqueue(() => TryClickItem(addon, val));
            }
        }
    }
}
