using Dalamud.Interface.Utility.Raii;
using GatherChill.Gui;
using GatherChill.Utilities.GatheringHelpers;

namespace GatherChill.Ui.Tabs_MainWindow
{
    internal class ReducableViewer
    {
        public static void Draw()
        {
            using (var container = ImRaii.Child("Reduction: Viewer", default, true))
            {
                if (!container.Success)
                    return;

                var list = Gather_Util.ReducableItems
                    .OrderBy(x => x.Expansion)
                    .ThenBy(x => x.ResultItems[0].ItemId)
                    .ThenBy(x => x.SublimeItemId != 0 ? x.ItemId : x.SublimeItemId);

                using (var table = ImRaii.Table("Reduction Table Viewer", 5, ImGuiTableFlags.SizingFixedFit | ImGuiTableFlags.Borders | ImGuiTableFlags.RowBg | ImGuiTableFlags.ScrollY))
                {
                    ImGui.TableSetupColumn("Exp");
                    ImGui.TableSetupColumn("Item");
                    ImGui.TableSetupColumn("Reduce [Main]");
                    ImGui.TableSetupColumn("Reduce [Crystal]");
                    ImGui.TableSetupColumn("Reduce [Cluster]");

                    ImGui.TableSetupScrollFreeze(0, 1);
                    ImGui.TableHeadersRow();

                    foreach (var entry in list)
                    {

                        ImGui.TableNextRow();
                        ImGui.TableSetColumnIndex(0);
                        var exp = Gather_Util.Sheet_Expansion[entry.Expansion];
                        ImGui_Ice.ImageButton(exp.IconId, $"Exp");
                        if (ImGui.IsItemHovered())
                        {
                            ImGui.SetTooltip($"{exp.Name}");
                        }

                        ImGui.TableNextColumn();
                        var mainItem = entry.ItemInfo(entry.ItemId);
                        if (entry.SublimeItemId != 0)
                        {
                            var sublimeItem = entry.ItemInfo(entry.SublimeItemId);
                            ImGui_Ice.ImageButton(sublimeItem.Icon, $"{sublimeItem.Name.ToString()}");
                            if (ImGui.IsItemHovered())
                            {
                                ImGui.SetTooltip($"{sublimeItem.Name}");
                            }
                            ImGui.SameLine();
                        }
                        ImGui_Ice.ImageButtonWithText(mainItem.Icon, mainItem.Name.ToString(), mainItem.Name.ToString());

                        for (int i = 0; i < entry.ResultItems.Count; i++)
                        {
                            var item = entry.ResultItems[i];
                            ImGui.TableNextColumn();
                            ImGui_Ice.ImageButtonWithText(item.IconId, item.ItemName, item.ItemName);
                        }
                    }
                }
            }
        }
    }
}
