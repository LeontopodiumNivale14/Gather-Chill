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
}
