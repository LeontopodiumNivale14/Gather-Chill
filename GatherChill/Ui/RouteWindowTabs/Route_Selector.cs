using Dalamud.Interface.Utility.Raii;
using Lumina.Excel.Sheets;
using System;
using System.Collections.Generic;
using System.Text;

namespace GatherChill.Ui.RouteWindowTabs
{
    internal class Route_Selector
    {
        public static void Draw()
        {
            var size = ImGui.GetContentRegionAvail();
            if (ImGui.BeginChild("Child: Route Selector", size, true))
            {
                if (ImGui.BeginTable("Route Selector Table", 3, ImGuiTableFlags.RowBg | ImGuiTableFlags.Borders | ImGuiTableFlags.SizingFixedFit))
                {
                    ImGui.TableSetupColumn("Route ID");
                    ImGui.TableSetupColumn("Location");
                    ImGui.TableSetupColumn("Items");

                    var sortedTable = P.routeEditor.Routes
                                      .OrderBy(x => x.Value.TerritoryId);

                    foreach (var route in sortedTable)
                    {
                        ImGui.TableNextRow();
                        if (Route_Editor.SelectedRoute == route.Key)
                            ImGui.TableSetBgColor(ImGuiTableBgTarget.RowBg0, ImGui.GetColorU32(new Vector4(0.0f, 1.0f, 0.2f, 0.25f)));

                        ImGui.PushID($"{route.Key}");

                        ImGui.TableSetColumnIndex(0);
                        if (ImGui.Button($"{route.Key}"))
                        {
                            Route_Editor.SelectedRoute = route.Key;

                        }

                        ImGui.TableNextColumn();
                        ImGui.Text($"{route.Value.ZoneName}");
                        if (ImGui.IsItemHovered())
                        {
                            ImGui.SetTooltip($"ID: {route.Value.TerritoryId}");
                        }

                        ImGui.TableNextColumn();
                        if (SheetInfo.TryGetValue(route.Key, out var gatherPointInfo))
                        {
                            foreach (var item in gatherPointInfo.ItemIds)
                            {
                                if (Svc.Data.GetExcelSheet<Item>().TryGetRow(item, out var itemInfo))
                                {
                                    var iconId = (int)itemInfo.Icon;
                                    var icon = Svc.Texture.GetFromGameIcon(iconId).GetWrapOrEmpty();
                                    ImGui.Image(icon.Handle, new(24, 24));
                                    if (ImGui.IsItemHovered())
                                    {
                                        ImGui.SetTooltip($"{itemInfo.Name}");
                                    }
                                    if (ImGui.IsItemClicked(ImGuiMouseButton.Left))
                                    {
                                        ImGui.SetClipboardText($"{itemInfo.Name}");
                                    }
                                    ImGui.SameLine();
                                }
                            }
                        }

                        ImGui.PopID();
                    }

                    ImGui.EndTable();
                }
            }
            ImGui.EndChild();
        }
    }
}
