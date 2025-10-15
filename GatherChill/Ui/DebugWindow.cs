using Dalamud.Game.ClientState.Conditions;
using Dalamud.Game.ClientState.Objects.Enums;
using Dalamud.Game.ClientState.Objects.Types;
using Dalamud.Interface.Colors;
using Dalamud.Interface.Utility.Raii;
using ECommons.GameHelpers;
using ECommons.UIHelpers.AddonMasterImplementations;
using GatherChill.Scheduler;
using GatherChill.Scheduler.Tasks;
using GatherChill.Ui.DebugTabs;
using GatherChill.Utilities;
using Lumina.Excel.Sheets;
using System.Collections.Generic;

namespace GatherChill.Ui;

internal class DebugWindow : Window
{
    public DebugWindow() :
        base($"Gather & Chill Debug {P.GetType().Assembly.GetName().Version} ###GatherChillDebug")
    {
        Flags = ImGuiWindowFlags.None;
        SizeConstraints = new WindowSizeConstraints
        {
            MinimumSize = new Vector2(100, 100),
            MaximumSize = new Vector2(3000, 3000)
        };
        P.windowSystem.AddWindow(this);
    }

    private string[] DebugTypes =
    [
        "Route Editor V2"
    ];
    private int selectedDebugIndex = 0;


    public void Dispose() { P.windowSystem.RemoveWindow(this); }

    public override void Draw()
    {
        float spacing = 10f;
        float leftPanelWidth = 200f;
        float rightPanelWidth = ImGui.GetContentRegionAvail().X - leftPanelWidth - spacing;
        float childHeight = ImGui.GetContentRegionAvail().Y;

        if (ImGui.BeginChild("DebugSelector", new System.Numerics.Vector2(leftPanelWidth, childHeight), true))
        {
            for (int i = 0; i < DebugTypes.Length; i++)
            {
                bool isSelected = (selectedDebugIndex == i);
                string label = isSelected ? $"→ {DebugTypes[i]}" : $"   {DebugTypes[i]}";

                if (ImGui.Selectable(label, isSelected))
                {
                    selectedDebugIndex = i;
                }
            }
            ImGui.EndChild();
        }

        ImGui.SameLine(0, spacing);

        if (ImGui.BeginChild("DebugContent", new System.Numerics.Vector2(rightPanelWidth, childHeight), true))
        {
            switch (selectedDebugIndex)
            {
                // HUD Elements (0-5)
                case 0: RouteEditorV2.Draw(); break;
                
                default: ImGui.Text("Unknown Debug View"); break;
            }
        }
        ImGui.EndChild();

        /*

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
            if (ImGui.BeginTabItem("Main Items"))
            {
                ItemTable();
                ImGui.EndTabItem();
            }
            if (ImGui.BeginTabItem("New Route Table"))
            {
                RouteInfoTab.Draw();
            }
            if (ImGui.BeginTabItem("Route Editor V2"))
            {
                RouteEditorV2.Draw();
            }
        }
        ImGui.EndTabBar();
        */
    }

    #region Gathering Testing

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

        GatherItemSearch();

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

    private static bool openSearchPopup = false;
    private static string searchInput = "";
    private static List<KeyValuePair<uint, string>> filteredItems = new();

    private static void GatherItemSearch()
    {
        if (ImGui.Button("Open Search###GatheringItemSearch"))
        {
            ImGui.OpenPopup("Gathering Item Search");
        }

        ImGui.SetNextWindowSize(new System.Numerics.Vector2(300, 300), ImGuiCond.Once);

        if (ImGui.BeginPopup("Gathering Item Search"))
        {
            // Search bar
            ImGui.InputText("Search", ref searchInput, 100);

            // Filter dictionary based on value (name)
            filteredItems = GatheringItems
                .Where(kv => string.IsNullOrEmpty(searchInput) || kv.Value.Contains(searchInput, StringComparison.OrdinalIgnoreCase))
                .ToList();

            ImGui.Separator();

            // Limit list display to 10 items’ worth of height
            float itemHeight = ImGui.GetTextLineHeightWithSpacing();
            float listHeight = itemHeight * 10;

            ImGui.BeginChild("##itemList", new System.Numerics.Vector2(0, listHeight), true);

            foreach (var kv in filteredItems)
            {
                string label = $"{kv.Value} (ID: {kv.Key})";
                if (ImGui.Selectable(label))
                {
                    Console.WriteLine($"Selected: {kv.Value} ({kv.Key})");
                    if (!C.GatheringList.Any(x => x.ItemName == kv.Value))
                    {
                        C.GatheringList.Add(new GatheringConfig()
                        {
                            GatheringAmount = 1,
                            ItemId = kv.Key,
                            ItemName = kv.Value
                        });
                    }
                    ImGui.CloseCurrentPopup();
                }
            }

            ImGui.EndChild();
            ImGui.EndPopup();
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

    string itemSearch = "";
    string nodeSearch = "";

    private void ItemTable()
    {
        var GatherItemSheet = Svc.Data.GetExcelSheet<GatheringItem>();
        var EventItems = Svc.Data.GetExcelSheet<EventItem>();
        var ItemSheet = Svc.Data.GetExcelSheet<Item>();

        ImGui.SetNextItemWidth(200);
        if (ImGui.InputText("###MainItemSearch", ref itemSearch, 200))
        {
            itemSearch = itemSearch.Trim();
        }
        ImGui.SameLine();
        ImGui.Text("Item Search");

        ImGui.SetNextItemWidth(200);
        if (ImGui.InputText("###MainNodeSearch", ref nodeSearch, 200))
        {
            nodeSearch = nodeSearch.Trim();
        }
        ImGui.SameLine();
        ImGui.Text("Node Search");


        if (ImGui.BeginTable("###DebugNormalNodeItems", 5, ImGuiTableFlags.Resizable | ImGuiTableFlags.RowBg | ImGuiTableFlags.Borders | ImGuiTableFlags.SizingFixedFit))
        {
            ImGui.TableSetupColumn("SetId");
            ImGui.TableSetupColumn("Level");
            ImGui.TableSetupColumn("Items");
            ImGui.TableSetupColumn("NodeIds");
            ImGui.TableSetupColumn("Leve?");

            ImGui.TableHeadersRow();

            foreach (var entry in GatheringPointBaseDict)
            {
                var setId = entry.Key;
                var nodeLevel = entry.Value.GatheringLevel;
                List<string> gatheringItems = new();
                bool leveItem = false;
                foreach (var item in entry.Value.Items)
                {
                    if (ItemSheet.TryGetRow(item, out var GatherItem))
                    {
                        gatheringItems.Add($"{GatherItem.Name}");
                    }
                    else if (EventItems.TryGetRow(item, out var EventItem))
                    {
                        leveItem = true;
                        gatheringItems.Add($"{EventItem.Name}");
                    }
                }
                List<string> allNodes = new();
                foreach (var item in entry.Value.NodeIds)
                {
                    allNodes.Add($"{item}");
                }

                if (!string.IsNullOrEmpty(itemSearch) && !gatheringItems.Any(item => item.Contains(itemSearch, StringComparison.OrdinalIgnoreCase)))
                {
                    continue;
                }
                if (!string.IsNullOrEmpty(nodeSearch) && !allNodes.Any(node => node.Contains(nodeSearch, StringComparison.OrdinalIgnoreCase)))
                {
                    continue;
                }

                ImGui.TableNextRow();

                ImGui.TableSetColumnIndex(0);
                ImGui.Text($"{setId}");

                ImGui.TableNextColumn();
                ImGui.Text($"{nodeLevel}");

                ImGui.TableNextColumn();
                if (gatheringItems.Count > 0)
                {
                    string allItems = string.Join(", ", gatheringItems);
                    ImGui.Text($"{allItems}");
                    if (ImGui.IsItemHovered())
                    {
                        ImGui.BeginTooltip();
                        foreach (var item in gatheringItems)
                        {
                            ImGui.Text($"{item}");
                        }
                        ImGui.EndTooltip();
                    }
                }

                ImGui.TableNextColumn();
                if (allNodes.Count > 0)
                {
                    string nodeList = string.Join(", ", allNodes);
                    ImGui.Text($"{nodeList}");
                }

                ImGui.TableNextColumn();
                FancyCheckmark(leveItem);
            }

            ImGui.EndTable();
        }
    }

    public static void FancyCheckmark(bool enabled)
    {
        float columnWidth = ImGui.GetColumnWidth();  // Get column width
        float rowHeight = ImGui.GetTextLineHeightWithSpacing();  // Get row height

        Vector2 iconSize = ImGui.CalcTextSize($"{FontAwesome.Cross}"); // Get icon size
        float iconWidth = iconSize.X;
        float iconHeight = iconSize.Y;

        float cursorX = ImGui.GetCursorPosX() + (columnWidth - iconWidth) * 0.5f;
        float cursorY = ImGui.GetCursorPosY() + (rowHeight - iconHeight) * 0.5f;

        cursorX = Math.Max(cursorX, ImGui.GetCursorPosX()); // Prevent negative padding
        cursorY = Math.Max(cursorY, ImGui.GetCursorPosY());

        ImGui.SetCursorPos(new Vector2(cursorX, cursorY));

        if (!enabled)
        {
            FontAwesome.Print(ImGuiColors.DalamudRed, FontAwesome.Cross);
        }
        else if (enabled)
        {
            FontAwesome.Print(ImGuiColors.HealerGreen, FontAwesome.Check);
        }
    }

    #endregion
}