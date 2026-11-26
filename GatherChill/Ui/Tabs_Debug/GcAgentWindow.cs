using Dalamud.Game.Text.SeStringHandling;
using Dalamud.Interface.Utility.Raii;
using ECommons.ExcelServices;
using FFXIVClientStructs.FFXIV.Client.UI.Agent;
using GatherChill.GrandCompany;

namespace GatherChill.Ui.Tabs_Debug
{
    internal class GcAgentWindow
    {
        private const int FirstCrafterJob = 8;
        private const int FirstGathererPosition = 8;
        private const int LastGathererPosition = 10;

        private static readonly Vector4 Bad = new(1f, 0.4f, 0.4f, 1f);
        private static readonly Vector4 Good = new(0.5f, 1f, 0.5f, 1f);

        public static unsafe void Draw()
        {
            var agent = AgentGrandCompanySupply.Instance();
            if (agent == null || !agent->IsAgentActive() || agent->SupplyProvisioningData == null || agent->ItemArray == null)
            {
                ImGui.Text("AgentGrandCompanySupply is not active. Talk to your Grand Company officer and open Missions.");
                return;
            }

            if (GCInfo.RequestedItems.Count == 0)
                ImGui.TextDisabled("GCInfo.RequestedItems is empty, so the comparison column shows '-'. Press Refresh Items first.");

            DrawProvisioningData(agent);
            ImGui.Separator();
            DrawItemArray(agent);
        }

        private static unsafe void DrawProvisioningData(AgentGrandCompanySupply* agent)
        {
            var items = agent->SupplyProvisioningData->ProvisioningData;
            ImGui.Text($"SupplyProvisioningData.ProvisioningData (length {items.Length})");

            using var table = ImRaii.Table("GC Provisioning Data", 8, ImGuiTableFlags.SizingFixedFit | ImGuiTableFlags.Borders | ImGuiTableFlags.RowBg);
            if (!table.Success)
                return;

            ImGui.TableSetupColumn("Idx");
            ImGui.TableSetupColumn("Job (MIN + Idx)");
            ImGui.TableSetupColumn("Item Id");
            ImGui.TableSetupColumn("Name");
            ImGui.TableSetupColumn("Requested");
            ImGui.TableSetupColumn("Exp");
            ImGui.TableSetupColumn("Seals");
            ImGui.TableSetupColumn("Vs RequestedItems");
            ImGui.TableHeadersRow();

            for (var i = 0; i < items.Length; i++)
            {
                ref var item = ref items[i];
                var job = (Job)(byte)((byte)Job.MIN + i);

                ImGui.TableNextRow();
                ImGui.TableSetColumnIndex(0);
                ImGui.Text($"{i}");

                ImGui.TableNextColumn();
                ImGui.Text($"{job}");

                ImGui.TableNextColumn();
                ImGui.Text($"{item.ItemId}");

                ImGui.TableNextColumn();
                ImGui.Text(SeString.Parse(item.ItemName.AsSpan()).TextValue);

                ImGui.TableNextColumn();
                ImGui.Text($"{item.NumRequested}");

                ImGui.TableNextColumn();
                ImGui.Text($"{item.ExpReward:N0}");

                ImGui.TableNextColumn();
                ImGui.Text($"{item.SealReward}");

                ImGui.TableNextColumn();
                DrawRequestedCheck(job, item.ItemId, item.NumRequested);
            }
        }

        private static unsafe void DrawItemArray(AgentGrandCompanySupply* agent)
        {
            ImGui.Text($"ItemArray (NumItems {agent->NumItems}, SelectedTab {agent->SelectedTab})");

            using var table = ImRaii.Table("GC Item Array", 14, ImGuiTableFlags.SizingFixedFit | ImGuiTableFlags.Borders | ImGuiTableFlags.RowBg | ImGuiTableFlags.ScrollX);
            if (!table.Success)
                return;

            ImGui.TableSetupColumn("Idx");
            ImGui.TableSetupColumn("Position");
            ImGui.TableSetupColumn("Slot");
            ImGui.TableSetupColumn("Job (from Position)");
            ImGui.TableSetupColumn("Item Id");
            ImGui.TableSetupColumn("Name");
            ImGui.TableSetupColumn("Requested");
            ImGui.TableSetupColumn("Possessed");
            ImGui.TableSetupColumn("Level");
            ImGui.TableSetupColumn("Exp");
            ImGui.TableSetupColumn("Seals");
            ImGui.TableSetupColumn("Bonus");
            ImGui.TableSetupColumn("Turn-in");
            ImGui.TableSetupColumn("Vs ProvisioningData");
            ImGui.TableHeadersRow();

            var provisioning = agent->SupplyProvisioningData->ProvisioningData;
            for (var i = 0; i < agent->NumItems; i++)
            {
                ref var item = ref agent->ItemArray[i];
                var job = JobOf(item.Position);

                ImGui.TableNextRow();
                ImGui.TableSetColumnIndex(0);
                ImGui.Text($"{i}");

                ImGui.TableNextColumn();
                ImGui.Text($"{item.Position}");

                ImGui.TableNextColumn();
                ImGui.Text($"{item.Slot}");

                ImGui.TableNextColumn();
                ImGui.Text(job == null ? "Expert / other" : $"{job}");

                ImGui.TableNextColumn();
                ImGui.Text($"{item.ItemId}");

                ImGui.TableNextColumn();
                ImGui.Text(SeString.Parse(item.ItemName.AsSpan()).TextValue);

                ImGui.TableNextColumn();
                ImGui.Text($"{item.NumRequested}");

                ImGui.TableNextColumn();
                ImGui.Text($"{item.NumPossessed}");

                ImGui.TableNextColumn();
                ImGui.Text($"{item.ItemLevel}");

                ImGui.TableNextColumn();
                ImGui.Text($"{item.ExpReward:N0}");

                ImGui.TableNextColumn();
                ImGui.Text($"{item.SealReward}");

                ImGui.TableNextColumn();
                ImGui.Text($"{item.IsBonusReward}");

                ImGui.TableNextColumn();
                ImGui.Text($"{item.IsTurnInAvailable}");

                ImGui.TableNextColumn();
                var gatherer = item.Position - FirstGathererPosition;
                if (gatherer < 0 || gatherer >= provisioning.Length)
                {
                    ImGui.Text("-");
                }
                else if (provisioning[gatherer].ItemId == item.ItemId)
                {
                    ImGui.TextColored(Good, "match");
                }
                else
                {
                    ImGui.TextColored(Bad, $"differs ({provisioning[gatherer].ItemId})");
                }
            }
        }

        // Position 0..10 maps to CRP..FSH, which are job ids 8..18.
        private static Job? JobOf(int position)
        {
            if (position < 0 || position > LastGathererPosition)
                return null;

            return (Job)(byte)(FirstCrafterJob + position);
        }

        private static void DrawRequestedCheck(Job job, uint itemId, int requested)
        {
            if (!GCInfo.RequestedItems.TryGetValue(job, out var stored))
            {
                ImGui.Text("-");
                return;
            }

            if (stored.ItemId == itemId && stored.Amount == requested)
                ImGui.TextColored(Good, "match");
            else
                ImGui.TextColored(Bad, $"differs ({stored.Amount}x {stored.ItemId})");
        }
    }
}