using GatherChill.ConfigYaml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace GatherChill.Ui.DebugTabs
{
    internal class RouteViewer
    {
        // Keep selected values between frames
        static string selectedExpansion = "";
        static string selectedArea = "";
        static string selectedRouteFile = "";

        public static void Draw()
        {
            // Store the list of expansions (folder names under "Routes")
            var expansions = Directory.GetDirectories(Path.Combine(Svc.PluginInterface.ConfigDirectory.FullName, "Routes"))
                                      .Select(Path.GetFileName)
                                      .ToList();

            if (ImGui.BeginCombo("Expansion", selectedExpansion == "" ? "Select Expansion" : selectedExpansion))
            {
                foreach (var exp in expansions)
                {
                    if (ImGui.Selectable(exp, selectedExpansion == exp))
                        selectedExpansion = exp;
                }
                ImGui.EndCombo();
            }

            if (!string.IsNullOrEmpty(selectedExpansion))
            {
                var areaPath = Path.Combine(Svc.PluginInterface.ConfigDirectory.FullName, "Routes", selectedExpansion);
                var areas = Directory.GetDirectories(areaPath)
                                     .Select(Path.GetFileName)
                                     .ToList();

                if (ImGui.BeginCombo("Area", selectedArea == "" ? "Select Area" : selectedArea))
                {
                    foreach (var area in areas)
                    {
                        if (ImGui.Selectable(area, selectedArea == area))
                            selectedArea = area;
                    }
                    ImGui.EndCombo();
                }
            }

            if (!string.IsNullOrEmpty(selectedArea))
            {
                var routeDir = Path.Combine(Svc.PluginInterface.ConfigDirectory.FullName, "Routes", selectedExpansion, selectedArea);
                var files = Directory.GetFiles(routeDir, "Route*.yaml")
                    .Select(path => new
                    {
                        Path = path,
                        Name = Path.GetFileNameWithoutExtension(path),
                        RouteNum = Regex.Match(Path.GetFileNameWithoutExtension(path), @"Route(\d+)")
                                         .Groups[1].Value
                    })
                    .Where(x => int.TryParse(x.RouteNum, out _))
                    .OrderBy(x => int.Parse(x.RouteNum))
                    .Select(x => x.Path)
                    .ToList();


                foreach (var file in files)
                {
                    var fileName = Path.GetFileName(file);

                    if (ImGui.Selectable(fileName, selectedRouteFile == file))
                    {
                        selectedRouteFile = file;
                        var config = YamlConfig.Load<RouteConfig>(file); // Assuming your config loader supports this
                                                                         // store/display the config however you'd like
                    }
                }
            }


        }
    }
}
