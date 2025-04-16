using Dalamud.Game.ClientState.Conditions;
using Dalamud.Game.ClientState.Objects.Enums;
using Dalamud.Interface.Utility.Raii;
using ECommons.GameHelpers;
using ECommons.UIHelpers.AddonMasterImplementations;
using GatherChill.Scheduler;
using GatherChill.Scheduler.Tasks;
using System.Collections.Generic;

namespace GatherChill.Ui;

internal class DebugWindow : Window
{
    public DebugWindow() :
        base($"Gather & Chill Debug {P.GetType().Assembly.GetName().Version} ###GatherChillDebug")
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
        ImGui.Text($"JobId: {GetClassJobId()}");

        if (ImGui.BeginTabBar("DebugTabBar"))
        {
            if (ImGui.BeginTabItem("Gathering"))
            {
                GatheringTest();
                ImGui.EndTabItem();
            }
            if (ImGui.BeginTabItem("Movement"))
            {
                MovementTech();
                ImGui.EndTabItem();
            }
            if (ImGui.BeginTabItem("Gathering List"))
            {
                GatheringList();
                ImGui.EndTabItem();
            }
            if (ImGui.BeginTabItem("Config Bools"))
            {
                ConfigSettings();
                ImGui.EndTabItem();
            }

            ImGui.EndTabBar();
        }
    }

    #region Gathering Testing

    private Vector3 PlayerPos = Vector3.Zero;
    private int gatheringType = 0;

    public void GatheringTest()
    {
        ImGui.SliderInt("##GatheringType", ref gatheringType, 0, 5);

        ImGui.Text("Statuses");
        ImGui.Text($"Gathering [Normal]: {Svc.Condition[ConditionFlag.Gathering]}");
        ImGuiEx.HelpMarker("Interacting with Gathering Node", sameLine: true);

        ImGui.Text($"Gathering [Gathering42] {Svc.Condition[ConditionFlag.Gathering42]}");
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

                if (ImGui.Button("Copy##" + x.Position))
                {
                    string clipBoardText = string.Empty;

                    clipBoardText += "new GathNodeInfo\n";
                    clipBoardText += "{\n";
                    clipBoardText += $"    ZoneId = {Svc.ClientState.TerritoryType},\n";
                    clipBoardText += $"    NodeId = {x.DataId},\n";
                    clipBoardText += $"    Position = new Vector3 ({rounded.X}f, {rounded.Y}f, {rounded.Z}f),\n";
                    clipBoardText += $"    LandZone = new Vector3 ({PlayerPos.X}f, {PlayerPos.Y}f, {PlayerPos.Z}f),\n";
                    clipBoardText += $"    GatheringType = {gatheringType},\n";
                    clipBoardText += "},\n";
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

    private Vector3 destination = new Vector3(0.0f, 0.0f, 0.0f);
    private float distance = 1f;

    private void MovementTech()
    {
        ImGui.InputFloat3("My Vector3", ref destination);
        ImGui.InputFloat("Distance", ref distance);

        if (ImGui.Button("Move"))
        {
            TaskMount_Move.Enqueue(destination, distance);
        }
        ImGui.SameLine();
        if (ImGui.Button("Fly"))
        {
            TaskMount_Fly.Enqueue(destination, true, distance);
        }
        if (ImGui.Button("Stop"))
        {
            P.taskManager.Abort();
            P.navmesh.Stop();
        }
        ImGui.SameLine();
        if (ImGui.Button("Set to current Pos:"))
        {
            Vector3 CurrentPos = Svc.ClientState.LocalPlayer?.Position ?? Vector3.Zero;

            CurrentPos = new Vector3(
                MathF.Round(CurrentPos.X, 2),
                MathF.Round(CurrentPos.Y, 2),
                MathF.Round(CurrentPos.Z, 2)
                );
            destination = CurrentPos;
        }
        ImGui.SameLine();
        if (ImGui.Button("Set to target Pos:"))
        {
            Vector3 TargetPos = Svc.Targets.Target?.Position ?? Vector3.Zero;
            TargetPos = new Vector3(
                MathF.Round(TargetPos.X, 2),
                MathF.Round(TargetPos.Y, 2),
                MathF.Round(TargetPos.Z, 2)
                );
            destination = TargetPos;
        }
    }

    private int itemId = 0;
    private int targetId = 0;

    private void GatheringList()
    {
        ImGui.Text("Gathering List");
        ImGui.SameLine();
        ImGui.SetNextItemWidth(100);
        ImGui.InputInt("###ItemIdforDebug", ref itemId);
        ImGui.SameLine();
        
        if (ImGui.Button("Gather Item"))
        {
            TaskGather.Enqueue((uint)itemId);
        }

        bool isRunning = SchedulerMain.AreWeTicking;

        using (ImRaii.Disabled(isRunning))
        {
            ImGui.SameLine();
            if (ImGui.Button("Start###DebugStart"))
            {
                SchedulerMain.EnablePlugin();
            }
        }

        using (ImRaii.Disabled(!isRunning))
        {
            ImGui.SameLine();
            if (ImGui.Button("Stop###DebugStop"))
            {
                SchedulerMain.DisablePlugin();
            }
        }

        if (ImGui.Button("Target Node"))
        {
            var target = Svc.Objects.Where(x => x.IsTargetable)
                                    .Where(x => x.DataId == targetId)
                                    .FirstOrDefault();

            if (target != null)
            {
                TaskTarget.Enqueue(target.DataId);
            }
        }

        ImGui.SameLine();

        ImGui.SetNextItemWidth(150);
        ImGui.InputInt("TargetId", ref targetId);

        if (ImGui.BeginTable("###GatherTableListing", 5, ImGuiTableFlags.RowBg | ImGuiTableFlags.Borders))
        {
            ImGui.TableSetupColumn("ItemId");
            ImGui.TableSetupColumn("Item Name");
            ImGui.TableSetupColumn("Amount");
            ImGui.TableSetupColumn("Have");
            ImGui.TableSetupColumn("Remove");

            ImGui.TableHeadersRow();

            foreach (var entry in GatheringItems)
            {
                var configEntry = C.GatheringList.FirstOrDefault(x => x.ItemName == entry.Value);

                if (configEntry != null)
                {
                    ImGui.TableNextRow();

                    ImGui.PushID((int)entry.Key);

                    // ItemId Column
                    ImGui.TableSetColumnIndex(0);
                    ImGui.Text($"{entry.Key}");
                    if (ImGui.IsItemHovered() && ImGui.IsItemClicked(ImGuiMouseButton.Left))
                    {
                        ImGui.SetClipboardText($"{entry.Key}");
                    }

                    // Item Name Column
                    ImGui.TableNextColumn();
                    ImGui.Text($"{entry.Value}");

                    // Item Amount to Gather Column
                    ImGui.TableNextColumn();
                    int amount = configEntry.GatheringAmount;
                    if (ImGui.InputInt("###AmountToGather", ref amount))
                    {
                        configEntry.GatheringAmount = amount;
                    }

                    ImGui.TableNextColumn();
                    ImGui.Text($"{GetItemCount((int)entry.Key)}");

                    // Remove Button
                    ImGui.TableNextColumn();
                    if (ImGui.Button("Remove##" + entry.Value))
                    {
                        C.GatheringList.RemoveAll(x => x.ItemName == entry.Value);
                    }
                    ImGui.PopID();
                }
            }
            ImGui.EndTable();
        }
    }

    private void ConfigSettings()
    {
        void ConfigurationSettings(string configName)
        {
            var config = C.AbilityConfigDict[configName];
            bool enabled = config.Enable;

            string internalName = GathActionDict[configName].ActionName;

            if (ImGui.Checkbox($"###{configName}_{internalName}_checkbox", ref enabled))
            {
                if (config.Enable != enabled)
                {
                    config.Enable = enabled;
                    C.Save();
                }
            }

            ImGui.SameLine();
            ImGui.Text($"{internalName}");

            if (enabled)
            {
                ImGui.SameLine();
                int requiredGP = config.MinimumGP;
                int minimumGP = GathActionDict[configName].RequiredGp;
                ImGui.SetNextItemWidth(200);
                if (ImGui.SliderInt($"###Slider_{configName}_{internalName}", ref requiredGP, minimumGP, 1200))
                {
                    if (requiredGP != config.MinimumGP)
                    {
                        config.MinimumGP = requiredGP;
                        C.Save();
                    }
                }
            }
        }

        ConfigurationSettings("BoonIncrease1");
        ConfigurationSettings("BoonIncrease2");
        ConfigurationSettings("Tidings");
        ConfigurationSettings("Yield1");
        ConfigurationSettings("Yield2");
        ConfigurationSettings("IntegrityIncrease");
    }

    #endregion
}