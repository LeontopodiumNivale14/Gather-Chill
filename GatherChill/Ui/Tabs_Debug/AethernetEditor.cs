using Dalamud.Game.ClientState.Objects.Types;
using Dalamud.Interface.Utility.Raii;
using ECommons.GameHelpers;
using GatherChill.GatheringInfo;
using GatherChill.Scheduler.Handlers;
using GatherChill.Utilities.Tools;
using GatherChill.Utilities.Traveling;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Threading.Tasks;
using static FFXIVClientStructs.FFXIV.Client.UI.UI3DModule;
using static GatherChill.Utilities.Traveling.TravelUtil;

namespace GatherChill.Ui.Tabs_Debug;

public class AethernetEditor
{
    public static uint selectedLoc = 0;

    public static void Draw()
    {
        var size = ImGui.GetContentRegionAvail();

        using (var aethertable = ImRaii.Table("Aethershard Editor", 2, ImGuiTableFlags.BordersInnerV | ImGuiTableFlags.RowBg, size))
        {
            if (aethertable.Success)
            {
                ImGui.TableSetupColumn("Selection", ImGuiTableColumnFlags.WidthFixed, 200);
                ImGui.TableSetupColumn("Editor", ImGuiTableColumnFlags.WidthStretch);

                ImGui.TableNextRow();
                ImGui.TableSetColumnIndex(0);
                var aetherDict = TravelUtil.AetherDictionary;

                var currentLoc = Player.Territory.RowId;

                if (ImGuiEx.IconButton(FontAwesomeIcon.Plus, $"Add Entry: {currentLoc}"))
                {
                    if (!aetherDict.ContainsKey(currentLoc))
                    {
                        aetherDict[currentLoc] = new();
                    }
                }

                ImGui.Separator();

                foreach (var entry in aetherDict)
                {
                    bool isSelected = entry.Key == selectedLoc;
                    using (ImRaii.PushId($"{entry.Key}"))
                    {
                        if (ImGui.Selectable($"{entry.Key}", isSelected))
                        {
                            selectedLoc = entry.Key;
                        }
                        if (ImGui.IsItemHovered() && ImGui.IsItemClicked(ImGuiMouseButton.Right))
                        {
                            ImGui.OpenPopup("Export Option");
                        }

                        if (ImGui.BeginPopup("Export Option"))
                        {
                            if (ImGui.Button("Export Entry"))
                            {
                                var export = ExportEntry(entry.Key, entry.Value);
                                ImGui.SetClipboardText(export);

                                ImGui.CloseCurrentPopup();
                            }
                            ImGui.EndPopup();
                        }
                    }
                }

                ImGui.TableNextColumn();
                ShardEditor();
            }
        }
    }

    private static uint selectedShard = 0;
    private static int multiFanSelected = 0;

    public static void ShardEditor()
    {
        if (AetherDictionary.TryGetValue(selectedLoc, out var aetherShards))
        {
            var windowHeight = ImGui.GetContentRegionAvail().Y - 10;
            var windowWidth = ImGui.GetContentRegionAvail().X;
            using (var aethernetWindow = ImRaii.Child("Aethernet Window Editor", new(windowWidth, windowHeight), false))
            {
                if (!aethernetWindow.Success)
                    return;

                var lightHeight = ImGui.GetTextLineHeight() * 7;

                IGameObject? target = null;
                uint territory = 0;

                if (Player.Available)
                {
                    target = Player.Object.TargetObject;
                    territory = Player.Territory.RowId;
                }
                
                if (ImGui.Button("Add to shards"))
                {
                    if (target != null && !aetherShards.ContainsKey(target.BaseId))
                    {
                        TravelUtil.AethershardInfo shard = new()
                        {
                            ShardId = target.BaseId,
                            TerritoryId = territory,
                            Shard_Position = target.Position
                        };
                        aetherShards.Add(target.BaseId, shard);
                    }
                }

                using (var shardSelection = ImRaii.Child("Shard Selection", new(300, lightHeight), true)) 
                { 
                    foreach (var shard in aetherShards)
                    {
                        var id = shard.Key;
                        var shardInfo = shard.Value;

                        var position = shardInfo.Shard_Position;
                        bool isSelected = id == selectedShard;

                        string name = $"{id} - [{position.X:N2}, {position.Y:N2}, {position.Z:N2}]";
                        if (ExcelHelper.Sheet_Aetheryte.TryGetRow(id, out var aetherInfo) && aetherInfo.AethernetName.IsValid)
                        {
                            name = aetherInfo.AethernetName.Value.Name.ToString();
                        }

                        if (ImGui.Selectable(name, isSelected))
                        {
                            selectedShard = id;
                        }

                        if (shardInfo.Multi_Destinations.Count != 0)
                        {
                            PictoManager.DrawAethernetMultiFan(shard.Value, selectedShard, multiFanSelected);
                        }
                        else
                        {
                            PictoManager.DrawAethernetFan(shardInfo, selectedShard);
                        }
                    }
                }

                ImGui.Separator();

                using (var fanmode = ImRaii.TabBar("Fan Selection Mode"))
                {
                    if (fanmode.Success)
                    {
                        if (aetherShards.TryGetValue(selectedShard, out var shard))
                        {
                            using (var bigFan = ImRaii.TabItem("Big Fan"))
                            {
                                if (bigFan.Success)
                                {
                                    ImGui.Text($"Territory: {shard.TerritoryId}");

                                    var position = shard.Shard_Position;
                                    ImGui.Text($"Position: X:{position.X:N2}, Y:{position.Y:N2}, Z:{position.Z:N2}");

                                    var selectionId = shard.SelectionId;
                                    ImGui.SetNextItemWidth(200);
                                    if (ImGui.InputUInt("Selection ID", ref selectionId))
                                    {
                                        shard.SelectionId = selectionId;
                                    }

                                    #region Fan Editor

                                    using (var disabled = ImRaii.Disabled(_isGeneratingFan || !ImGui.IsKeyDown(ImGuiKey.LeftShift)))
                                    {
                                        if (ImGui.Button("Generate Fan [Big]"))
                                        {
                                            _ = GenerateFanForNode(shard, 5, 12);
                                        }
                                    }

                                    using (var disabled = ImRaii.Disabled(_isGeneratingFan || !ImGui.IsKeyDown(ImGuiKey.LeftShift)))
                                    {
                                        if (ImGui.Button("Generate Fan [Normal]"))
                                        {
                                            _ = GenerateFanForNode(shard, 1, 3);
                                        }
                                    }

                                    var fanInfo = shard.DestinationFan;

                                    var fan_Start = fanInfo.Fan_StartAngle;
                                    var fan_End = fanInfo.Fan_EndAngle;
                                    var fan_Min = fanInfo.Fan_DistanceMin;
                                    var fan_Max = fanInfo.Fan_DistanceMax;
                                    var fan_Height = fanInfo.Fan_Height;

                                    ImGui.SetNextItemWidth(100);
                                    if (ImGui.DragFloat("Start", ref fan_Start, 1, 0, 360))
                                    {
                                        fanInfo.Fan_StartAngle = fan_Start;
                                    }

                                    ImGui.SameLine();
                                    ImGui.SetNextItemWidth(100);
                                    if (ImGui.DragFloat("End", ref fan_End, 1, 0, 360))
                                    {
                                        fanInfo.Fan_EndAngle = fan_End;
                                    }

                                    ImGui.SetNextItemWidth(100);
                                    if (ImGui.DragFloat("Min Distance", ref fan_Min, 0.1f, 0, 15))
                                    {
                                        fanInfo.Fan_DistanceMin = fan_Min;
                                    }

                                    ImGui.SameLine();
                                    ImGui.SetNextItemWidth(100);
                                    if (ImGui.DragFloat("Max Distance", ref fan_Max, 0.1f, 0, 15))
                                    {
                                        fanInfo.Fan_DistanceMax = fan_Max;
                                    }

                                    ImGui.SetNextItemWidth(100);
                                    if (ImGui.DragFloat("Height", ref fan_Height, 0.1f, 0, 5))
                                    {
                                        fanInfo.Fan_Height = fan_Height;
                                    }

                                    #endregion
                                }
                            }

                            using (var multiFan = ImRaii.TabItem("Multi-Fan"))
                            {
                                if (multiFan.Success)
                                {
                                    var multiFans = shard.Multi_Destinations;
                                    if (multiFanSelected < 0 || multiFanSelected >= multiFans.Count) 
                                    {
                                        multiFanSelected = 0;
                                    }

                                    if (ImGui.Button("Add New Fan"))
                                    {
                                        FanInfo newFan = new();
                                        multiFans.Add(newFan);
                                    }
                                    if (multiFans.Count == 0)
                                    {
                                        ImGui.Text("No fans exist. Please add one");
                                    }
                                    else
                                    {
                                        var selectedFan = multiFans[multiFanSelected];
                                        ImGui.SameLine();
                                        if (ImGui.Button("Remove Fan"))
                                        {
                                            multiFans.RemoveAt(multiFanSelected);
                                            if (multiFanSelected >= multiFans.Count)
                                                multiFanSelected = Math.Max(0, multiFans.Count - 1);
                                        }

                                        if (ImGuiEx.IconButton(FontAwesomeIcon.ArrowLeft, "MultiFan-Left"))
                                        {
                                            multiFanSelected -= 1;
                                            if (multiFanSelected < 0)
                                                multiFanSelected = 0;
                                        }
                                        ImGui.SameLine();
                                        ImGui.Button($"{multiFanSelected}");
                                        ImGui.SameLine();
                                        if (ImGuiEx.IconButton(FontAwesomeIcon.ArrowRight, "MultiFan-Right"))
                                        {
                                            multiFanSelected += 1;
                                            if (multiFanSelected > multiFans.Count)
                                                multiFanSelected = multiFans.Count;
                                        }

                                        if (selectedFan != null)
                                        {
                                            var fan_Start = selectedFan.Fan_StartAngle;
                                            var fan_End = selectedFan.Fan_EndAngle;
                                            var fan_Min = selectedFan.Fan_DistanceMin;
                                            var fan_Max = selectedFan.Fan_DistanceMax;
                                            var fan_Height = selectedFan.Fan_Height;

                                            ImGui.SetNextItemWidth(100);
                                            if (ImGui.DragFloat("Start", ref fan_Start, 1, 0, 360))
                                            {
                                                selectedFan.Fan_StartAngle = fan_Start;
                                            }

                                            ImGui.SameLine();
                                            ImGui.SetNextItemWidth(100);
                                            if (ImGui.DragFloat("End", ref fan_End, 1, 0, 360))
                                            {
                                                selectedFan.Fan_EndAngle = fan_End;
                                            }

                                            ImGui.SetNextItemWidth(100);
                                            if (ImGui.DragFloat("Min Distance", ref fan_Min, 0.1f, 0, 15))
                                            {
                                                selectedFan.Fan_DistanceMin = fan_Min;
                                            }

                                            ImGui.SameLine();
                                            ImGui.SetNextItemWidth(100);
                                            if (ImGui.DragFloat("Max Distance", ref fan_Max, 0.1f, 0, 15))
                                            {
                                                selectedFan.Fan_DistanceMax = fan_Max;
                                            }

                                            ImGui.SetNextItemWidth(100);
                                            if (ImGui.DragFloat("Height", ref fan_Height, 0.1f, 0, 5))
                                            {
                                                selectedFan.Fan_Height = fan_Height;
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
    }

    private static bool _isGeneratingFan = false;
    private static string _fanGenStatus = string.Empty;

    private static async Task GenerateFanForNode(AethershardInfo shard, float distanceMin, float distanceMax)
    {
        _isGeneratingFan = true;
        _fanGenStatus = string.Empty;

        try
        {
            Vector3 nodePos = shard.Shard_Position;

            const float snapToleranceXZ = 0.5f;
            const float snapToleranceY = 5f;
            float testDistanceMin = distanceMin;
            float testDistanceMax = distanceMax;
            const float distanceStep = 0.5f;
            const int angleSamples = 360;

            var validDistances = new Dictionary<int, List<float>>();
            var validYHeights = new Dictionary<int, float>();

            await Task.Run(() =>
            {
                for (int angleDeg = 0; angleDeg < angleSamples; angleDeg++)
                {
                    bool allDistancesValid = true;
                    var distancesForAngle = new List<float>();
                    float highestY = float.MinValue;

                    for (float dist = testDistanceMin; dist <= testDistanceMax; dist += distanceStep)
                    {
                        float standardAngle = 180f - angleDeg;
                        float rad = standardAngle * (MathF.PI / 180f);
                        Vector3 candidate = new Vector3(
                            nodePos.X + dist * MathF.Sin(rad),
                            nodePos.Y,
                            nodePos.Z + dist * MathF.Cos(rad)
                        );

                        var nearest = P.navmesh.NearestPointReachable(candidate, snapToleranceXZ, snapToleranceY);
                        if (nearest.HasValue)
                        {
                            float xzDist = MathF.Sqrt(
                                MathF.Pow(nearest.Value.X - candidate.X, 2) +
                                MathF.Pow(nearest.Value.Z - candidate.Z, 2)
                            );
                            float yDist = MathF.Abs(nearest.Value.Y - candidate.Y);

                            if (xzDist <= snapToleranceXZ && yDist <= snapToleranceY)
                            {
                                distancesForAngle.Add(dist);
                                if (nearest.Value.Y > highestY)
                                    highestY = nearest.Value.Y;
                            }
                            else
                            {
                                allDistancesValid = false;
                                break;
                            }
                        }
                        else
                        {
                            allDistancesValid = false;
                            break;
                        }
                    }

                    if (allDistancesValid && distancesForAngle.Count > 0)
                    {
                        validDistances[angleDeg] = distancesForAngle;
                        validYHeights[angleDeg] = highestY;
                    }
                }
            });

            if (validDistances.Count == 0)
            {
                _fanGenStatus = "No reachable points found around this node.";
                return;
            }

            bool[] valid = new bool[360];
            foreach (var kvp in validDistances)
                valid[kvp.Key] = true;

            int bestStart = 0, bestLen = 0;
            int currentStart = 0, currentLen = 0;

            for (int i = 0; i < 720; i++)
            {
                if (valid[i % 360])
                {
                    if (currentLen == 0)
                        currentStart = i;
                    currentLen++;

                    if (currentLen > bestLen)
                    {
                        bestLen = currentLen;
                        bestStart = currentStart;
                    }
                }
                else
                {
                    currentLen = 0;
                }

                if (currentLen >= 360)
                    break;
            }

            if (bestLen == 0)
            {
                _fanGenStatus = "Could not find a contiguous arc of reachable angles.";
                return;
            }

            int ffxivStart = bestStart % 360;
            int ffxivEnd = (bestStart + bestLen - 1) % 360;

            float allMin = float.MaxValue, allMax = float.MinValue;
            float arcMaxY = float.MinValue;

            foreach (var kvp in validDistances)
            {
                int normalizedAngle = ((kvp.Key - ffxivStart) % 360 + 360) % 360;
                if (normalizedAngle < bestLen)
                {
                    foreach (var d in kvp.Value)
                    {
                        if (d < allMin) allMin = d;
                        if (d > allMax) allMax = d;
                    }
                }
            }

            foreach (var kvp in validYHeights)
            {
                int normalizedAngle = ((kvp.Key - ffxivStart) % 360 + 360) % 360;
                if (normalizedAngle < bestLen && kvp.Value > arcMaxY)
                    arcMaxY = kvp.Value;
            }

            float fanHeight = 0f;
            if (arcMaxY != float.MinValue && arcMaxY > nodePos.Y)
                fanHeight = MathF.Round((arcMaxY - nodePos.Y) + 0.2f, 2);

            shard.DestinationFan.Fan_StartAngle = ffxivStart;
            shard.DestinationFan.Fan_EndAngle = ffxivEnd;
            shard.DestinationFan.Fan_DistanceMin = MathF.Round(allMin, 1);
            shard.DestinationFan.Fan_DistanceMax = MathF.Round(allMax, 1);
            shard.DestinationFan.Fan_Height = fanHeight;

            _fanGenStatus = $"Generated! Angles: {ffxivStart}→{ffxivEnd} (arc {bestLen}°), Distance: {allMin:F1}→{allMax:F1}, Height: {fanHeight:F2}";
            IceLogging.Info($"[FanGen] Node {shard.Shard_Position}: FFXIV {ffxivStart}→{ffxivEnd}, dist {allMin:F1}→{allMax:F1}, height {fanHeight:F2}");
        }
        catch (Exception ex)
        {
            _fanGenStatus = $"Error: {ex.Message}";
            IceLogging.Error($"[FanGen] Failed: {ex.Message}");
        }
        finally
        {
            _isGeneratingFan = false;
        }
    }
    public static string ExportEntry(uint key, Dictionary<uint, AethershardInfo> shards)
    {
        var sb = new StringBuilder();

        sb.AppendLine($"\t\t[{key}] = new()");
        sb.AppendLine("\t\t{");

        foreach (var shard in shards)
        {
            var shardId = shard.Key;
            var shardInfo = shard.Value;

            sb.AppendLine($"\t\t\t[{shardId}] = new()");
            sb.AppendLine("\t\t\t{");

            sb.AppendLine($"\t\t\t\tShardId = {shardInfo.ShardId},");
            sb.AppendLine($"\t\t\t\tSelectionId = {shardInfo.SelectionId},");
            sb.AppendLine($"\t\t\t\tShard_Position = new Vector3({shardInfo.Shard_Position.X}f, {shardInfo.Shard_Position.Y}f, {shardInfo.Shard_Position.Z}f),");
            sb.AppendLine($"\t\t\t\tTerritoryId = {shardInfo.TerritoryId},");
            sb.AppendLine("\t\t\t\t\tDestinationFan = new()");

            sb.AppendLine("\t\t\t\t\t{");
            sb.AppendLine($"\t\t\t\t\t\tFan_StartAngle = {shardInfo.DestinationFan.Fan_StartAngle}f,");
            sb.AppendLine($"\t\t\t\t\t\tFan_EndAngle = {shardInfo.DestinationFan.Fan_EndAngle}f,");
            sb.AppendLine($"\t\t\t\t\t\tFan_DistanceMin = {shardInfo.DestinationFan.Fan_DistanceMin}f,");
            sb.AppendLine($"\t\t\t\t\t\tFan_DistanceMax = {shardInfo.DestinationFan.Fan_DistanceMax}f,");
            sb.AppendLine($"\t\t\t\t\t\tFan_Height = {shardInfo.DestinationFan.Fan_Height}f,");
            sb.AppendLine("\t\t\t\t\t},");

            sb.AppendLine($"\t\t\t\t Multi_Destinations = new()");
            sb.AppendLine("\t\t\t\t{");
            foreach (var location in shardInfo.Multi_Destinations)
            {
                sb.AppendLine("\t\t\t\t\tnew()");
                sb.AppendLine("\t\t\t\t\t{");

                sb.AppendLine($"\t\t\t\t\t\tFan_StartAngle = {location.Fan_StartAngle}f,");
                sb.AppendLine($"\t\t\t\t\t\tFan_EndAngle = {location.Fan_EndAngle}f,");
                sb.AppendLine($"\t\t\t\t\t\tFan_DistanceMin = {location.Fan_DistanceMin}f,");
                sb.AppendLine($"\t\t\t\t\t\tFan_DistanceMax = {location.Fan_DistanceMax}f,");
                sb.AppendLine($"\t\t\t\t\t\tFan_Height = {location.Fan_Height}f,");

                sb.AppendLine("\t\t\t\t\t},");
            }
            sb.AppendLine("\t\t\t\t},");
            sb.AppendLine("\t\t\t},");
        }

        sb.AppendLine("\t\t},");

        return sb.ToString();
    }
}
