using Dalamud.Game.ClientState.Conditions;
using Dalamud.Game.ClientState.Objects.Enums;
using ECommons.UIHelpers.AddonMasterImplementations;
using System.Collections.Generic;

namespace GatherChill.Ui;

internal class DebugWindow : Window
{
    public DebugWindow() :
        base($"Chilled Leves Debug {P.GetType().Assembly.GetName().Version} ###ChilledLevesDebug")
    {
        Flags = ImGuiWindowFlags.NoScrollbar | ImGuiWindowFlags.NoCollapse;
        SizeConstraints = new WindowSizeConstraints
        {
            MinimumSize = new Vector2(100, 100),
            MaximumSize = new Vector2(3000, 3000)
        };
        P.windowSystem.AddWindow(this);
    }

    public void Dispose() { }

    // variables that hold the "ref"s for ImGui

    public override void Draw()
    {

    }

    #region Gathering Testing

    private static List<uint> NodeIds = new List<uint>();

    public void GatheringTest()
    {
        ImGui.Text("Statuses");
        ImGui.Text($"Gathering [Normal]: {Svc.Condition[ConditionFlag.Gathering]}");
        ImGuiEx.HelpMarker("Interacting with Gathering Node", sameLine: true);

        ImGui.Text($"Gathering [Gathering42] {Svc.Condition[ConditionFlag.Gathering42]}");
        ImGuiEx.HelpMarker("Interacting with Gathering Node/Using Buffs", sameLine: true);

        foreach (var x in Svc.Objects)
        {
            if (x.ObjectKind == ObjectKind.GatheringPoint)
            {
                Vector3 rounded = new Vector3(
                    MathF.Round(x.Position.X, 2),
                    MathF.Round(x.Position.Y, 2),
                    MathF.Round(x.Position.Z, 2)
                    );

                ImGuiEx.Text($"Gathering Point: {x.DataId} |  Location: {rounded} | Distance: {GetDistanceToPlayer(x):N2} |  Targetable: {x.IsTargetable}");
            }
        }

        if (TryGetAddonMaster<AddonMaster.Gathering>("Gathering", out var m) && m.IsAddonReady)
        {
            ImGui.Text("Gathering Test");
            ImGui.Text($"Current Integrity: {m.CurrentIntegrity}");
            ImGui.Text($"Total Integrity: {m.TotalIntegrity}");
            ImGui.Text($"Node ID: {Svc.Targets.Target.DataId}");
            ImGui.Text($"Type: {Svc.Targets.Target.ObjectKind}");

            foreach (var item in m.GatheredItems)
            {
                if (item.ItemID == 0)
                    continue;

                ImGui.Text($"{item.ItemName} ID: ({item.ItemID})");
                ImGui.Text($"Gathering Chance: {item.GatherChance} | Boon %%: {item.BoonChance}");
                ImGui.SameLine();
                if (ImGui.Button("Select##" + item.ItemName)) item.Gather();
            }
        }
    }

    #endregion
}