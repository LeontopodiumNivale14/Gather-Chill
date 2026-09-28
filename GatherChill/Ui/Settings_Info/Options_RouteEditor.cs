using GatherChill.Ui.RouteWindowTabs;
using GatherChill.Utilities.Tools;

namespace GatherChill.Ui.Settings_Info;

public static partial class SettingsUi
{
    private static readonly string RouteOption = "Route Editor Options";

    private static SettingEntry Route_SaveLoc = new()
    {
        Label = "Route Save Location",
        Category = RouteOption,
        Keywords = new[] { "Route Editor", "Route", "Editor", "Save Location", "Save", "Location" },
        Draw = () =>
        {
            var v = C.SaveLocation;
            ImGui.Text($"Route Save Location: {v}");
            if (ImGui.Button("Browse for Export Folder"))
            {
                fileDialogManager.OpenFolderDialog("Select Export Folder", (success, path) =>
                {
                    if (success && !string.IsNullOrEmpty(path))
                    {
                        C.SaveLocation = path;
                        C.Save();
                        IceLogging.Info($"Export path set to: {path}");
                    }
                });
            }
        }
    };

    private static SettingEntry Route_UpdateAll = new()
    {
        Label = "Route: Update All Routes",
        Category = RouteOption,
        Keywords = new[] { "Update", "Save", "Route" },
        Draw = () =>
        {
            if (ImGui.Button("Update All"))
            {
                foreach (var route in P.routeEditor.Routes)
                {
                    if (Sheet_RouteInfo.TryGetValue(route.Key, out var sheetInfo))
                    {
                        var routeInfo = route.Value;
                        routeInfo.ExpansionId = sheetInfo.ExpId;
                        routeInfo.TerritoryId = sheetInfo.TerritoryId;
                        routeInfo.ZoneName = sheetInfo.ZoneName;
                        routeInfo.PlaceName = sheetInfo.PlaceName;
                        routeInfo.NodeIds = sheetInfo.NodeIds.ToList();
                    }
                }

                Route_Editor.LastSavedAt = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                P.routeEditor.SaveAllRoutes(C.SaveLocation);
            }
        }
    };

    private static SettingEntry LoadExternal = new()
    {
        Label = "Load External Routes",
        Category = RouteOption,
        Keywords = new[] { "Load", "External", "Routes" },
        Draw = () =>
        {
            if (ImGui.Button("Load External Routes"))
            {
                var location = C.SaveLocation;
                P.routeEditor.LoadRoutesFromDirectory(location);
            }

            ImGui.Checkbox("Test Export", ref _TestExport);
            if (ImGui.Button("Export Missing Routes"))
            {
                P.routeEditor.CreateStubsForMissingRoutes(C.SaveLocation, _TestExport);
            }
        }
    };
}
