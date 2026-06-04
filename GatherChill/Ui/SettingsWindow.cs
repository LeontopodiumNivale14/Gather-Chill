using GatherChill.ConfigFiles;
using GatherChill.Scheduler;
using Dalamud.Interface.Utility.Raii;

namespace GatherChill.Ui;

internal class SettingsWindow : Window
{
    public SettingsWindow() :
        base($"Gather & Chill Settings {P.GetType().Assembly.GetName().Version} ###Gather&ChillSettingsWindow")
    {
        Flags = ImGuiWindowFlags.None;
        SizeConstraints = new()
        {
            MinimumSize = new Vector2(400, 400),
            MaximumSize = new Vector2(2000, 2000),
        };
        P.windowSystem.AddWindow(this);
        AllowPinning = false;
    }

    public void Dispose() { }

    public override void Draw()
    {
        if (ImGui.BeginTabBar("SettingsTabs"))
        {
            if (ImGui.BeginTabItem("Navigation"))
            {
                DrawNavigationSettings();
                ImGui.EndTabItem();
            }

            ImGui.EndTabBar();
        }
    }

    private static void DrawNavigationSettings()
    {
        var enabled = C.NavmeshMovementEnabled;
        if (ImGui.Checkbox("Enable navmesh movement", ref enabled))
            C.NavmeshMovementEnabled = enabled;

        var verbose = C.NavmeshVerboseLogging;
        if (ImGui.Checkbox("Verbose navmesh logging", ref verbose))
            C.NavmeshVerboseLogging = verbose;

        var interact = C.NavmeshInteractDistance;
        if (ImGui.SliderFloat("Interact distance", ref interact, 2f, 12f))
            C.NavmeshInteractDistance = interact;

        var retries = C.NavmeshInteractRetries;
        if (ImGui.SliderInt("Interact move retries", ref retries, 0, 6))
            C.NavmeshInteractRetries = retries;

        if (ImGui.Button("Save"))
            C.Save();
    }
}
