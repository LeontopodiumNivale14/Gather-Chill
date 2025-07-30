using Dalamud.Game.ClientState.Conditions;
using Dalamud.Game.ClientState.Objects.Enums;
using Dalamud.Game.ClientState.Objects.Types;
using ECommons.GameHelpers;
using ECommons.UIHelpers.AddonMasterImplementations;
using GatherChill.Utilities;

namespace GatherChill.Ui
{
    internal class PopupDebugWindow : Window
    {
        public PopupDebugWindow() :
            base($"Gather & Chill Debug Gather {P.GetType().Assembly.GetName().Version} ###GatherChillDebugGather")
        {
            Flags = ImGuiWindowFlags.None;
            SizeConstraints = new WindowSizeConstraints
            {
                MinimumSize = new Vector2(100, 100),
                MaximumSize = new Vector2(3000, 3000)
            };
            P.windowSystem.AddWindow(this);
        }

        public void Dispose() { P.windowSystem.RemoveWindow(this); }

        public override void Draw()
        {
            GatheringTest();
        }

        private Vector3 PlayerPos = Vector3.Zero;
        private int gatheringType = 0;
        private int maxDistance = 0;
        private int nodeSet = 0;
        private int NodeId = 0;

        public void GatheringTest()
        {
            ImGui.SetNextItemWidth(100);
            ImGui.SliderInt("##GatheringType", ref gatheringType, 0, 5);
            ImGui.SameLine();
            ImGui.Text("Gathering Type");

            ImGui.SetNextItemWidth(100);
            ImGui.InputInt("###MaxDistance", ref maxDistance);
            ImGui.SameLine();
            ImGui.Text("Max Distance");

            ImGui.SetNextItemWidth(100);
            ImGui.InputInt("###NodeSet", ref nodeSet);
            ImGui.SameLine();
            ImGui.Text("Node Set");

            ImGui.SetNextItemWidth(100);
            ImGui.InputInt("###NodeFinderId", ref NodeId);
            ImGui.SameLine();
            ImGui.Text("Node Id Search");

            if (ImGui.Button("Set to current node"))
            {
                if (Svc.Targets.Target != null)
                {
                    NodeId = Svc.Targets.Target.DataId.ToInt();
                }
            }

            ImGui.SameLine();

            if (ImGui.Button("Clear Target Node"))
            {
                NodeId = 0;
            }


            ImGui.Text("Statuses");
            ImGui.Text($"Gathering [Normal]: {Svc.Condition[ConditionFlag.Gathering]}");
            ImGuiEx.HelpMarker("Interacting with Gathering Node", sameLine: true);

            ImGui.Text($"Gathering [Gathering42] {Svc.Condition[ConditionFlag.ExecutingGatheringAction]}");
            ImGuiEx.HelpMarker("Interacting with Gathering Node/Using Buffs", sameLine: true);

            PlayerPos = Svc.ClientState.LocalPlayer?.Position ?? Vector3.Zero;

            PlayerPos = new Vector3(
                MathF.Round(PlayerPos.X, 2),
                MathF.Round(PlayerPos.Y, 2),
                MathF.Round(PlayerPos.Z, 2)
                );

            foreach (var x in Svc.Objects)
            {
                if (x.ObjectKind == ObjectKind.GatheringPoint)
                {
                    Vector3 rounded = new Vector3(
                        MathF.Round(x.Position.X, 2),
                        MathF.Round(x.Position.Y, 2),
                        MathF.Round(x.Position.Z, 2)
                        );

                    if (maxDistance != 0)
                    {
                        if (Player.DistanceTo(rounded) > maxDistance)
                            continue;
                    }

                    if (NodeId != 0)
                    {
                        if (x.DataId != NodeId)
                            continue;
                    }

                    if (ImGui.Button($"Set Target## {x.Position}"))
                    {
                        IGameObject? GameObject = null;
                        Utils.TryGetObjectByDataId(x.DataId, out GameObject);

                        Utils.TargetgameObject(GameObject);
                    }

                    ImGui.SameLine();


                    if (ImGui.Button("Copy##" + x.Position))
                    {
                        string clipBoardText = string.Empty;

                        clipBoardText += $"new GathNodeInfo\n" +
                                          "{\n" +
                                         $"    ZoneId = {Svc.ClientState.TerritoryType},\n" +
                                         $"    NodeId = {x.DataId},\n" +
                                         $"    Position = new Vector3 ({rounded.X}f, {rounded.Y}f, {rounded.Z}f),\n" +
                                         $"    LandZone = new Vector3 ({PlayerPos.X}f, {PlayerPos.Y}f, {PlayerPos.Z}f),\n" +
                                         $"    GatheringType = {gatheringType},\n" +
                                         $"    NodeSet = {nodeSet}\n" +
                                          "},\n";
                        ImGui.SetClipboardText(clipBoardText);
                    }
                    ImGui.SameLine();
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
    }
}
