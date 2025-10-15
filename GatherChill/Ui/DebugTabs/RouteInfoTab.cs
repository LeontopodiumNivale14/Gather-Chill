using ECommons.Logging;
using GatherChill.Utilities.GatheringHelpers;
using GatherChill.Yaml;
using GatherChill.Yaml.ConfigFiles;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GatherChill.Ui.DebugTabs
{
    internal class RouteInfoTab
    {
        public static void Draw()
        {
            if (ImGui.Button("Export All Routes to Files"))
            {
                ExportAllRoutesToFiles();
            }

            ImGui.SameLine();

            if (ImGui.Button("Create index file"))
            {
                // Clear existing data to rebuild fresh
                GIndex.Expansions.Clear();

                foreach (var entry in GatherClasses.GatheringDatabase)
                {
                    var data = entry.Value;

                    var routeInfo = new GatherIndex.RouteInfo
                    {
                        MapCenter = data.MapCenter,
                        MapRadius = data.MapRadius,
                        GatheringType = data.GatheringType,
                        NodeIds = data.NodeIds,
                    };

                    // Use gathering type as route key, or create a more meaningful identifier
                    var routeKey = entry.Key;
                }

                GIndex.Save();

                // Optional: Log how many entries were processed
                var totalRoutes = GIndex.Expansions.Values
                    .SelectMany(exp => exp.Zones.Values)
                    .SelectMany(zone => zone.Routes.Values)
                    .Count();
                PluginLog.Information($"Created index with {totalRoutes} routes across {GIndex.Expansions.Count} expansions");
            }

            if (ImGui.BeginTable("New Route Information Table", 7, ImGuiTableFlags.Borders | ImGuiTableFlags.SizingFixedFit))
            {
                ImGui.TableSetupColumn("#");
                ImGui.TableSetupColumn("Type");
                ImGui.TableSetupColumn("Expansion");
                ImGui.TableSetupColumn("Zone");
                ImGui.TableSetupColumn("Position");
                ImGui.TableSetupColumn("NodeIds");
                ImGui.TableSetupColumn("Items");

                ImGui.TableHeadersRow();

                foreach (var entry in GatherClasses.GatheringDatabase)
                {
                    ImGui.TableNextRow();

                    // 1st Column | ID #
                    ImGui.TableSetColumnIndex(0);
                    ImGui.Text(entry.Key.ToString());

                    // 2nd Column | Type
                    ImGui.TableNextColumn();
                    var image = GatherClasses.GatheringTypeIcons[entry.Value.GatheringType].MainIcon;
                    ImGui.Image(image.GetWrapOrEmpty().Handle, new Vector2(25, 25));

                    // Expansion (these next 2 are problematic... need to look into why later)
                    ImGui.TableNextColumn();
                    ImGui.Text(entry.Value.ExpansionName);

                    // Zone
                    ImGui.TableNextColumn();
                    ImGui.Text(entry.Value.ZoneName);

                    // Column 3 | Map Location
                    ImGui.TableNextColumn();
                    if (ImGui.Button($"Map {entry.Value.MapCenter.X} | {entry.Value.MapCenter.Y}"))
                    {
                        Svc.Chat.Print($"Map Location: {entry.Value.MapCenter.X} | {entry.Value.MapCenter.Y}");
                    }
                    if (ImGui.IsItemHovered())
                    {
                        ImGui.BeginTooltip();
                        ImGui.Text($"Radius: {entry.Value.MapRadius}");
                        ImGui.EndTooltip();
                    }

                    // Column 4 | NodeIds - Hover for popup
                    ImGui.TableNextColumn();
                    string nodeList = string.Join(", ", entry.Value.NodeIds);
                    ImGui.Text($"NodeIds ({entry.Value.NodeIds.Count})");

                    if (ImGui.IsItemHovered())
                    {
                        ImGui.BeginTooltip();
                        if (ImGui.BeginTable($"NodeIds_Tooltip_{entry.Key}", 2, ImGuiTableFlags.Borders))
                        {
                            ImGui.TableSetupColumn("Index");
                            ImGui.TableSetupColumn("NodeId");
                            ImGui.TableHeadersRow();

                            int index = 0;
                            foreach (var nodeId in entry.Value.NodeIds)
                            {
                                ImGui.TableNextRow();
                                ImGui.TableSetColumnIndex(0);
                                ImGui.Text(index.ToString());
                                ImGui.TableNextColumn();
                                ImGui.Text(nodeId.ToString());
                                index++;
                            }
                            ImGui.EndTable();
                        }
                        ImGui.EndTooltip();
                    }

                    // Column 5 | Items
                    ImGui.TableNextColumn();
                    string itemsText = "No Items";
                    if (entry.Value.Items?.Count > 1)
                    {
                        var limitedItems = entry.Value.Items.Values.Take(10).Where(v => !string.IsNullOrEmpty(v));
                        itemsText = string.Join(", ", limitedItems);
                        if (entry.Value.Items.Count > 10)
                            itemsText += "...";
                    }
                    else if (entry.Value.Items?.Count > 0)
                    {
                        var firstItem = entry.Value.Items.Values.FirstOrDefault();
                        itemsText = firstItem?.Trim() ?? "Invalid Item";
                    }
                    ImGui.Text(itemsText);
                    string itemIdList = string.Join(", ", entry.Value.ItemIds.ToArray());
                    ImGuiEx.HelpMarker(itemIdList);
                }

                ImGui.EndTable();
            }
        }

        private static void ExportAllRoutesToFiles()
        {
            if (GatherClasses.GatheringDatabase == null || GatherClasses.GatheringDatabase.Count == 0)
            {
                PluginLog.Warning("No routes to export!");
                return;
            }

            // Base directory for routes - adjust this path to your project structure
            string baseRoutesPath = Path.Combine(Svc.PluginInterface.AssemblyLocation.Directory?.FullName ?? "", "Routes");

            // Log the export location
            PluginLog.Information($"Exporting routes to: {baseRoutesPath}");

            int successCount = 0;
            int failCount = 0;

            foreach (var entry in GatherClasses.GatheringDatabase)
            {
                try
                {
                    var routeId = entry.Key;
                    var data = entry.Value;

                    // Determine class prefix based on gathering type
                    string classPrefix = (data.GatheringType == 0 || data.GatheringType == 1) ? "MIN" : "BTN";

                    // Create folder structure: Routes/ExpansionName/ZoneId_ZoneName/
                    string expansionFolder = Path.Combine(baseRoutesPath, SanitizeFolderName(data.ExpansionName));
                    string zoneFolderName = $"{SanitizeFolderName(data.ZoneName)}_{data.ZoneId}";
                    string zoneFolder = Path.Combine(expansionFolder, zoneFolderName);

                    // Create directories if they don't exist
                    Directory.CreateDirectory(zoneFolder);

                    // Generate filename: MIN_RouteId.cs or BTN_RouteId.cs
                    string className = $"{classPrefix}_{routeId}";
                    string filename = $"{className}.cs";
                    string fullPath = Path.Combine(zoneFolder, filename);

                    // Generate the C# file content
                    string fileContent = GenerateRouteClassFile(className, data, routeId);

                    // Write to file
                    File.WriteAllText(fullPath, fileContent);

                    successCount++;
                    PluginLog.Information($"Exported route: {filename}");
                }
                catch (Exception ex)
                {
                    failCount++;
                    PluginLog.Error($"Failed to export route {entry.Key}: {ex.Message}");
                }
            }

            PluginLog.Information($"Export complete! Success: {successCount}, Failed: {failCount}");
            PluginLog.Information($"Routes exported to: {baseRoutesPath}");

            // Open the folder in Windows Explorer
            try
            {
                System.Diagnostics.Process.Start("explorer.exe", baseRoutesPath);
            }
            catch (Exception ex)
            {
                PluginLog.Error($"Failed to open folder: {ex.Message}");
            }
        }

        private static string SanitizeFolderName(string name)
        {
            // Remove invalid filename characters and spaces
            var invalidChars = Path.GetInvalidFileNameChars();
            string sanitized = string.Join("_", name.Split(invalidChars, StringSplitOptions.RemoveEmptyEntries));
            sanitized = sanitized.Replace(" ", "");
            return sanitized;
        }

        // Changed parameter type from RouteData to RouteInfo (the old YAML structure type)
        private static string GenerateRouteClassFile(string className, GatherClasses.RouteInfo data, uint routeId)
        {
            var sb = new StringBuilder();

            // Using statements
            sb.AppendLine("using System.Collections.Generic;");
            sb.AppendLine("using System.Numerics;");
            sb.AppendLine("using GatherChill.GatheringInfo;");
            sb.AppendLine();

            // Namespace
            string zoneFolderName = $"{SanitizeFolderName(data.ZoneName)}_{data.ZoneId}";
            string namespaceName = $"GatherChill.Routes.{SanitizeFolderName(data.ExpansionName)}.{zoneFolderName}";
            sb.AppendLine($"namespace {namespaceName}");
            sb.AppendLine("{");

            // Class declaration
            sb.AppendLine($"    public class {className} : RouteInfo");
            sb.AppendLine("    {");

            // Properties
            sb.AppendLine($"        public override uint Id => {routeId};");
            sb.AppendLine($"        public override uint ExpansionId => {data.ExpansionId};");
            sb.AppendLine($"        public override uint ZoneId => {data.ZoneId};");
            sb.AppendLine($"        public override uint GatherType => {data.GatheringType};");
            sb.AppendLine($"        public override Vector2 MapPosition => new Vector2({data.MapCenter.X}f, {data.MapCenter.Y}f);");
            sb.AppendLine($"        public override int Radius => {data.MapRadius};");
            sb.AppendLine();

            // NodeIds
            sb.AppendLine("        public override HashSet<uint> NodeIds => new()");
            sb.AppendLine("        {");
            foreach (var nodeId in data.NodeIds.OrderBy(x => x))
            {
                sb.AppendLine($"            {nodeId},");
            }
            sb.AppendLine("        };");
            sb.AppendLine();

            // ItemIds
            sb.AppendLine("        public override HashSet<uint> ItemIds => new()");
            sb.AppendLine("        {");
            if (data.ItemIds != null)
            {
                foreach (var itemId in data.ItemIds.OrderBy(x => x))
                {
                    sb.AppendLine($"            {itemId},");
                }
            }
            sb.AppendLine("        };");
            sb.AppendLine();

            // NodeInfo (Nodes)
            sb.AppendLine("        public override List<NodeInfo> Nodes => new()");
            sb.AppendLine("        {");
            if (data.NodeIds != null)
            {
                foreach (var nodeInfo in data.NodeIds)
                {
                    sb.AppendLine("            new NodeInfo");
                    sb.AppendLine("            {");
                    sb.AppendLine($"                NodeId = {nodeInfo},");
                    sb.AppendLine("            },");
                }
            }
            sb.AppendLine("        };");

            // Close class and namespace
            sb.AppendLine("    }");
            sb.AppendLine("}");

            return sb.ToString();
        }
    }
}
