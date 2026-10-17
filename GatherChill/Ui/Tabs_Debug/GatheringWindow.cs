using Dalamud.Interface.Utility.Raii;
using GatherChill.Utilities;
using static ECommons.UIHelpers.AddonMasterImplementations.AddonMaster;

namespace GatherChill.Ui.Tabs_Debug
{
    internal class GatheringWindow
    {
        public static unsafe void Draw()
        {


            if (GenericHelpers.TryGetAddonMaster<Utilities.Gathering>(out var gathering) && gathering.IsAddonReady)
            {
                using (var table = ImRaii.Table("Gathering Items", 5, ImGuiTableFlags.SizingFixedFit | ImGuiTableFlags.Borders | ImGuiTableFlags.RowBg))
                {
                    if (!table.Success)
                        return;

                    ImGui.TableSetupColumn("Name");
                    ImGui.TableSetupColumn("Lv");
                    ImGui.TableSetupColumn("Gather %");
                    ImGui.TableSetupColumn("Boon %");
                    ImGui.TableSetupColumn("Hidden");
                    ImGui.TableSetupColumn("Rare");

                    var itemList = gathering.GatheredItems;
                    for (int i = 0; i < itemList.Count(); i++)
                    {
                        var item = itemList[i];

                        ImGui.TableNextRow();
                        ImGui.TableSetColumnIndex(0);
                        ImGui.Text($"{item.ItemName}");

                        ImGui.TableNextColumn();
                        ImGui.Text($"{item.ItemLevel}");

                        ImGui.TableNextColumn();
                        ImGui.Text($"{item.GatherChance}");

                        ImGui.TableNextColumn();
                        ImGui.Text($"{item.BoonChance}");

                        ImGui.TableNextColumn();
                        ImGui.Text($"{item.IsHidden}");
                    }
                }
            }
            else if (GenericHelpers.TryGetAddonMaster<GatheringMasterpiece>(out var collectable) && collectable.IsAddonReady)
            {

            }
            else
            {
                ImGui.Text("Neither collectables, or normal gathering window is visible. Please open one of them...");
            }
        }
    }
}
