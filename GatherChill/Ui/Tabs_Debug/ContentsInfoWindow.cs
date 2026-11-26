using Dalamud.Interface.Utility.Raii;
using ECommons.ExcelServices;
using GatherChill.GrandCompany;
using GatherChill.GrandCompany.Addons;
using GatherChill.GrandCompany.Tasks;
using GatherChill.Utilities.Tools;

namespace GatherChill.Ui.Tabs_Debug
{
    internal class ContentsInfoWindow
    {
        private const uint FirstCrafter = 8;

        private static readonly Vector4 Changed = new(1f, 0.4f, 0.4f, 1f);

        public static unsafe void Draw()
        {
            var hasInfo = GenericHelpers.TryGetAddonMaster<ContentsInfo>(out var info) && info.IsAddonReady;
            var hasDetail = GenericHelpers.TryGetAddonMaster<ContentsInfoDetail>(out var detail) && detail.IsAddonReady;

            ImGui.Text($"ContentsInfo: {(hasInfo ? "ready" : "closed")}   ContentsInfoDetail: {(hasDetail ? "ready" : "closed")}");

            if (ImGui.Button("Open Timers"))
                GCInfo.OpenTimers();

            ImGui.SameLine();
            using (ImRaii.Disabled(P.TaskManager.IsBusy))
            {
                if (ImGui.Button("Refresh Items"))
                    Task_RefreshItems.Enqueue();
            }

            ImGui.SameLine();
            using (ImRaii.Disabled(!hasInfo))
            {
                if (ImGui.Button("Select First Tab"))
                    info.SelectFirstTab();

                ImGui.SameLine();
                if (ImGui.Button("Open GC Detail"))
                    info.OpenGrandCompanyDetail();
            }

            ImGui.Separator();
            DrawRequested();
            ImGui.Separator();

            if (!hasDetail)
            {
                ImGui.Text("ContentsInfoDetail is not open. Open Timers, then the Grand Company detail.");
                return;
            }

            ImGui.Text($"AtkValuesCount: {detail.Addon->AtkValuesCount}   Crafter count: {detail.CrafterSupplyCount}   Provision count: {detail.ProvisionMissionCount}");

            DrawCrafters(detail);
            ImGui.Separator();
            DrawProvisioning(detail);
        }

        private static void DrawRequested()
        {
            ImGui.Text("GCInfo.RequestedItems");

            if (GCInfo.RequestedItems.Count == 0)
            {
                ImGui.TextDisabled("Empty. Press Refresh Items.");
                return;
            }

            using var table = ImRaii.Table("Requested Items", 5, ImGuiTableFlags.SizingFixedFit | ImGuiTableFlags.Borders | ImGuiTableFlags.RowBg);
            if (!table.Success)
                return;

            ImGui.TableSetupColumn("Job");
            ImGui.TableSetupColumn("Item Id");
            ImGui.TableSetupColumn("Name");
            ImGui.TableSetupColumn("Amount");
            ImGui.TableSetupColumn("Bonus");
            ImGui.TableHeadersRow();

            var items = ExcelHelper.Sheet_Item;
            foreach (var (job, request) in GCInfo.RequestedItems)
            {
                var name = items.TryGetRow(request.ItemId, out var item) ? item.Name.ExtractText() : "Unknown item";

                ImGui.TableNextRow();
                ImGui.TableSetColumnIndex(0);
                ImGui.Text($"{job}");

                ImGui.TableNextColumn();
                ImGui.Text($"{request.ItemId}");

                ImGui.TableNextColumn();
                ImGui.Text(name);

                ImGui.TableNextColumn();
                ImGui.Text($"{request.Amount:N0}");

                ImGui.TableNextColumn();
                ImGui.Text($"{request.Bonus}");
            }
        }

        private static void DrawCrafters(ContentsInfoDetail detail)
        {
            ImGui.Text("Crafters");

            var slots = detail.Crafting(out var problem);
            if (slots == null)
            {
                ImGui.TextColored(Changed, problem);
                return;
            }

            using var table = ImRaii.Table("Crafter Slots", 7, ImGuiTableFlags.SizingFixedFit | ImGuiTableFlags.Borders | ImGuiTableFlags.RowBg);
            if (!table.Success)
                return;

            ImGui.TableSetupColumn("Slot");
            ImGui.TableSetupColumn("Assumed Job");
            ImGui.TableSetupColumn("Item Id");
            ImGui.TableSetupColumn("Name");
            ImGui.TableSetupColumn("Icon");
            ImGui.TableSetupColumn("Amount");
            ImGui.TableSetupColumn("Bonus");
            ImGui.TableHeadersRow();

            var items = ExcelHelper.Sheet_Item;
            foreach (var slot in slots)
            {
                var name = slot.ItemId <= 0 ? "-" : items.TryGetRow((uint)slot.ItemId, out var item) ? item.Name.ExtractText() : "Unknown item";

                ImGui.TableNextRow();
                ImGui.TableSetColumnIndex(0);
                ImGui.Text($"{slot.Slot}");

                ImGui.TableNextColumn();
                ImGui.Text($"{(Job)(byte)(FirstCrafter + (uint)slot.Slot)}");

                ImGui.TableNextColumn();
                ImGui.Text($"{slot.ItemId}");

                ImGui.TableNextColumn();
                ImGui.Text(name);

                ImGui.TableNextColumn();
                ImGui.Text($"{slot.IconId}");

                ImGui.TableNextColumn();
                ImGui.Text($"{slot.Amount:N0}");

                ImGui.TableNextColumn();
                ImGui.Text($"{slot.Bonus}");
            }
        }

        private static void DrawProvisioning(ContentsInfoDetail detail)
        {
            ImGui.Text("Provisioning");

            var slots = detail.Provisioning(out var problem);
            if (slots == null)
            {
                ImGui.TextColored(Changed, problem);
                return;
            }

            using var table = ImRaii.Table("Provision Slots", 7, ImGuiTableFlags.SizingFixedFit | ImGuiTableFlags.Borders | ImGuiTableFlags.RowBg);
            if (!table.Success)
                return;

            ImGui.TableSetupColumn("Slot");
            ImGui.TableSetupColumn("Assumed Job");
            ImGui.TableSetupColumn("Item Id");
            ImGui.TableSetupColumn("Name");
            ImGui.TableSetupColumn("Icon");
            ImGui.TableSetupColumn("Amount");
            ImGui.TableSetupColumn("Bonus");
            ImGui.TableHeadersRow();

            var items = ExcelHelper.Sheet_Item;
            foreach (var slot in slots)
            {
                var name = items.TryGetRow(slot.ItemId, out var item) ? item.Name.ExtractText() : "Unknown item";

                ImGui.TableNextRow();
                ImGui.TableSetColumnIndex(0);
                ImGui.Text($"{slot.Slot}");

                ImGui.TableNextColumn();
                ImGui.Text($"{(Job)(byte)((byte)Job.MIN + slot.Slot)}");

                ImGui.TableNextColumn();
                ImGui.Text($"{slot.ItemId}");

                ImGui.TableNextColumn();
                ImGui.Text(name);

                ImGui.TableNextColumn();
                ImGui.Text($"{slot.IconId}");

                ImGui.TableNextColumn();
                ImGui.Text($"{slot.Amount:N0}");

                ImGui.TableNextColumn();
                ImGui.Text($"{slot.Bonus}");
            }
        }
    }
}