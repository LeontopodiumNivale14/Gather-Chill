using Dalamud.Interface.Utility;
using ECommons.Logging;
using GatherChill.Ui.RouteWindowTabs;

namespace GatherChill.Ui.Settings_Info;

public static partial class SettingsUi
{
    private static readonly string MiscOption = "Misc Options";
    private static SettingEntry ColorTheme = new()
    {
        Label = "Use Custom Theme",
        Category = MiscOption,
        Keywords = new[] { "Theme", "Color", "Ice Theme" },
        Draw = () =>
        {
            var v = C.UseIceTheme;
            if (ImGui.Checkbox("Use Ice Theme", ref v))
            {
                C.UseIceTheme = v;
                C.Save();
            }
        }
    };

    private static SettingEntry CompactOverlay = new()
    {
        Label = "Compact Sidebar",
        Category = MiscOption,
        Keywords = new[] { "Main Window", "Main", "Window", "Compact" },
        Draw = () =>
        {
            var v = C.Compact_Ui;
            if (ImGui.Checkbox("Compact Tabs on Sidebar", ref v))
            {
                C.Compact_Ui = v;
                C.Save();
            }
        }
    };

    private static SettingEntry Overlay_AutoShow = new()
    {
        Label = "Open Overlay upon start",
        Category = MiscOption,
        Keywords = new[] { "Overlay", "Auto Open", "Start" },
        Draw = () =>
        {
            var v = C.Overlay_OpenUponRun;
            if (ImGui.Checkbox("Open overlay when running", ref v))
            {
                C.Overlay_OpenUponRun = v;
                C.Save();
            }
        }
    };

    private static SettingEntry Overlay_AutoClose = new()
    {
        Label = "Close Overlay upon idle",
        Category = MiscOption,
        Keywords = new[] { "Overlay", "Auto Open", "Start" },
        Draw = () =>
        {
            var v = C.Overlay_CloseUponStop;
            if (ImGui.Checkbox("Close overlay when not running", ref v))
            {
                C.Overlay_CloseUponStop = v;
                C.Save();
            }
        }
    };

    private static SettingEntry SafetyGatherDelay = new()
    {
        Label = "Safety Delay [Gather]",
        Category = MiscOption,
        Keywords = new[] { "Safety", "Delay", "Gathering" },
        Draw = () =>
        {
            var v = C.SafetyDelay_Gather;
            if (ImGui.Checkbox("Add a delay between gathering attempts", ref v))
            {
                C.SafetyDelay_Gather = v;
                C.Save();
            }

            if (v)
            {
                var d = C.Gather_DelayTimer;
                ImGui.SetNextItemWidth(150 * ImGuiHelpers.GlobalScale);
                if (ImGui.SliderInt("Gathering delay", ref d, 250, 2500))
                {
                    C.Gather_DelayTimer = d;
                    C.SaveDebounced();
                }
            }
        }
    };
}
