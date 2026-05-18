using Dalamud.Interface.ImGuiFileDialog;
using ECommons.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace GatherChill.Ui.RouteWindowTabs
{
    internal class Route_UpdateAll
    {
        private static FileDialogManager fileDialogManager = new FileDialogManager();
        private static bool _TestExport = false;

        public static void Draw()
        {
            ImGui.Text($"Current Route: {C.SaveLocation}");

            if (ImGui.Button("Browse for Export Folder"))
            {
                fileDialogManager.OpenFolderDialog("Select Export Folder", (success, path) =>
                {
                    if (success && !string.IsNullOrEmpty(path))
                    {
                        C.SaveLocation = path;
                        C.Save();
                        PluginLog.Information($"Export path set to: {path}");
                    }
                });
            }

            if (ImGui.Button("Update All"))
            {
                foreach (var route in P.routeEditor.Routes)
                {
                    if (SheetInfo.TryGetValue(route.Key, out var sheetInfo))
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

            fileDialogManager.Draw();
        }
    }
}
