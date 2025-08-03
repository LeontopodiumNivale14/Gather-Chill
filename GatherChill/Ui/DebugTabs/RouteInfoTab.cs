using GatherChill.Utilities.GatheringHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GatherChill.Ui.DebugTabs
{
    internal class RouteInfoTab
    {
        public static void Draw()
        {
            if (ImGui.BeginTable("New Route Information Table", 4, ImGuiTableFlags.Borders | ImGuiTableFlags.SizingFixedFit))
            {
                ImGui.TableSetupColumn("#");
                ImGui.TableSetupColumn("Type");
                ImGui.TableSetupColumn("Position");
                ImGui.TableSetupColumn("Items");

                ImGui.TableHeadersRow();

                foreach (var entry in GatherClasses.RouteDatabase)
                {

                    ImGui.TableNextRow();

                    // 1st Column | ID #
                    ImGui.TableSetColumnIndex(0);
                    ImGui.Text(entry.Key.ToString());

                    // 2nd Column | Type
                    ImGui.TableNextColumn();
                    var image = GatherClasses.GatheringTypeIcons[entry.Value.GatheringType].MainIcon;
                    ImGui.Image(image.GetWrapOrEmpty().ImGuiHandle, new Vector2(25, 25));

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

                    // Column 4 | Items
                    ImGui.TableNextColumn();
                    if (entry.Value.Items.Count > 0)
                    {
                        ImGui.Text("Item Count != 0");
                    }
                    else
                    {
                        ImGui.Text("No items");
                    }
                }

                ImGui.EndTable();
            }
        }
    }
}
