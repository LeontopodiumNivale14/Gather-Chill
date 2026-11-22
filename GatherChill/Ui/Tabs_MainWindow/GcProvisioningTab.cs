using Dalamud.Interface.Utility.Raii;
using ECommons.GameHelpers;
using GatherChill.GrandCompany;

namespace GatherChill.Ui.Tabs_MainWindow;

internal static class GcProvisioningTab
{
    public static void Draw()
    {
        using var child = ImRaii.Child("GCProvisioningContent");
        if (!child.Success)
            return;

        var gc = P.Gc;
        ImGui.TextWrapped("Gather and deliver today's Miner and Botanist provisioning requests.");
        ImGui.Spacing();

        using (ImRaii.Disabled(gc.Running))
        {
            var miner = gc.MinerEnabled;
            var botanist = gc.BotanistEnabled;
            if (ImGui.Checkbox("Miner", ref miner))
                gc.MinerEnabled = miner;
            ImGui.SameLine();
            if (ImGui.Checkbox("Botanist", ref botanist))
                gc.BotanistEnabled = botanist;
        }

        using (ImRaii.Disabled(gc.Running || !Player.Available))
        {
            if (ImGuiEx.IconButtonWithText(FontAwesomeIcon.Play, "Gather & Deliver"))
                gc.Start();

            SameLineIfFits("Refresh");
            if (ImGui.Button("Refresh"))
                gc.Refresh();

            SameLineIfFits("Deliver Held Items");
            if (ImGui.Button("Deliver Held Items"))
                gc.Start(gather: false);
        }

        if (gc.Running && ImGui.Button("Stop GC"))
            gc.Stop("Stopped by user");

        ImGui.Spacing();
        ImGui.TextWrapped(gc.Status);

        if (!Player.Available)
            return;

        using var table = ImRaii.Table("GCRequests", 4, ImGuiTableFlags.RowBg | ImGuiTableFlags.Borders | ImGuiTableFlags.SizingStretchProp);
        if (!table.Success)
            return;

        ImGui.TableSetupColumn("Job", ImGuiTableColumnFlags.WidthStretch);
        ImGui.TableSetupColumn("Item");
        ImGui.TableSetupColumn("Required", ImGuiTableColumnFlags.WidthStretch);
        ImGui.TableSetupColumn("Held", ImGuiTableColumnFlags.WidthStretch);
        ImGui.TableHeadersRow();

        foreach (var request in gc.Requests)
        {
            ImGui.TableNextRow();
            ImGui.TableNextColumn();
            ImGui.TextUnformatted(request.ClassJob == GcBoard.Miner ? "Miner" : "Botanist");
            ImGui.TableNextColumn();
            ImGui.TextUnformatted(request.ItemName);
            ImGui.TableNextColumn();
            ImGui.TextUnformatted(request.Requested.ToString());
            ImGui.TableNextColumn();
            ImGui.TextUnformatted(GcBoard.Held(request.ItemId).ToString());
        }
    }

    private static void SameLineIfFits(string label)
    {
        var style = ImGui.GetStyle();
        var width = ImGui.CalcTextSize(label).X + (2f * style.FramePadding.X);
        var right = ImGui.GetCursorScreenPos().X + ImGui.GetContentRegionAvail().X;
        if (ImGui.GetItemRectMax().X + style.ItemSpacing.X + width <= right)
            ImGui.SameLine();
    }
}
