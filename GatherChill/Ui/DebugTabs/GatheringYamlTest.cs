using GatherChill.ConfigYaml;
using GatherChill.Utilities;
using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GatherChill.Ui.DebugTabs
{
    internal class GatheringYamlTest
    {
        public static string SqlitePath => Path.Combine(
                                   Svc.PluginInterface.ConfigDirectory.FullName,
                                   "Routes.sqlite");

        public static void Draw()
        {
            if (ImGui.Button("Test Yaml thingy"))
            {
                Utils.CreateYamlFiles();
            }

            ImGui.SameLine();

            if (ImGui.Button("Create Yaml Files"))
            {
                foreach (var entry in Utils.SheetInfo)
                {
                    var config = new RouteEntry
                    {
                        RouteNumber = entry.Key,
                        Expansion = entry.Value.ExName,
                        Area = entry.Value.TerritoryName,
                        AreaId = entry.Value.TerritoryId,
                        ListNodeIds = entry.Value.NodeIds,
                    };
                }
            }

            ImGui.SameLine();

            if (ImGui.Button("Migrate All YAML to SQLite"))
            {

                using var conn = new SqliteConnection($"Data Source={SqlitePath}");
                conn.Open();
                Sqlcreator.EnsureTables(conn);

                var yamlFiles = Directory.GetFiles(
                    Path.Combine(Svc.PluginInterface.ConfigDirectory.FullName, "Routes"),
                    "Route*.yaml",
                    SearchOption.AllDirectories
                );

                foreach (var file in yamlFiles)
                {
                    var config = YamlConfig.Load<RouteEntry>(file);
                    Sqlcreator.SaveToDatabase(config, conn);
                }

                conn.Close();
            }


            if (ImGui.BeginTable("Testing Table", 4, ImGuiTableFlags.SizingFixedFit | ImGuiTableFlags.Borders))
            {
                ImGui.TableSetupColumn("Nodeset");
                ImGui.TableSetupColumn("Territory");
                ImGui.TableSetupColumn("Expansion");
                ImGui.TableSetupColumn("NodeIds");

                ImGui.TableHeadersRow();
                if (Utils.SheetInfo.Count != 0)
                {
                    foreach (var entry in Utils.SheetInfo.OrderBy(kv => kv.Key))
                    {
                        ImGui.TableNextRow();

                        ImGui.PushID((int)entry.Key);

                        // Nodeset
                        ImGui.TableSetColumnIndex(0);
                        ImGui.Text($"{entry.Key}");

                        // Territory
                        ImGui.TableNextColumn();
                        ImGui.Text($"{entry.Value.TerritoryName}");
                        if (ImGui.IsItemHovered())
                        {
                            ImGui.BeginTooltip();
                            ImGui.Text($"ID: {entry.Value.TerritoryId}");
                            ImGui.EndTooltip();
                        }

                        // Expansion
                        ImGui.TableNextColumn();
                        ImGui.Text($"{entry.Value.ExName}");
                        if (ImGui.IsItemHovered())
                        {
                            ImGui.BeginTooltip();
                            ImGui.Text($"ID: {entry.Value.ExVersion}");
                            ImGui.EndTooltip();
                        }

                        // NodeIds
                        ImGui.TableNextColumn();
                        ImGui.TextUnformatted(string.Join(", ", entry.Value.NodeIds));
                    }
                }

                ImGui.EndTable();
            }
        }
    }
}
