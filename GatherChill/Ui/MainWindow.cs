using GatherChill.Scheduler;
using Dalamud.Interface.Colors;
using Dalamud.Interface.Style;
using Dalamud.Interface.Textures;
using Dalamud.Interface.Utility.Raii;
using Lumina.Excel.Sheets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Lumina.Data.Parsing.Uld.UldRoot;
using ECommons.Logging;
using static Lumina.Data.Parsing.Uld.NodeData;

namespace GatherChill.Ui
{
    internal class MainWindow : Window
    {
        /// <summary>
        /// Constructor for the main window. Adjusts window size, flags, and initializes data.
        /// </summary>
        public MainWindow() :
            base($"Gather & Chill {P.GetType().Assembly.GetName().Version} ###Gather&ChillMainWindow")
        {

            Flags = ImGuiWindowFlags.None;

            // Set up size constraints to ensure window cannot be too small or too large.
            // Increased minimum size to better accommodate larger font sizes
            SizeConstraints = new()
            {
                MinimumSize = new Vector2(500, 500),
                MaximumSize = new Vector2(2000, 2000)
            };

            // Register this window with Dalamud's window system.
            P.windowSystem.AddWindow(this);

            AllowPinning = false;
        }

        public void Dispose()
        {
        }

        private static int gatheringType = 0;
        private static uint GatheringSlot = 0;
        private static string itemName = string.Empty;

        /// <summary>
        /// Primary draw method. Responsible for drawing the entire UI of the main window.
        /// </summary>
        public override void Draw()
        {
            foreach (var entry in GatheringNodeDict)
            {
                ImGui.Text($"Number: {entry.Key}");
                ImGui.SameLine();
                ImGui.Text($"Type: {entry.Value.Name}");
                ImGui.SameLine();
                ImGui.Image(entry.Value.MainIcon.GetWrapOrEmpty().ImGuiHandle, new Vector2(20, 20));
                string textNodes = string.Empty;

                int dictKey = entry.Key.ToInt();
            }

            int tempSlot = (int)Math.Clamp(GatheringSlot, 0, 1205);

            if (ImGui.SliderInt("Items", ref tempSlot, 0, 1205))
            {
                uint newSlot = (uint)tempSlot;

                if (!GatheringPointBaseDict.ContainsKey(newSlot))
                {
                    // Snap to closest key
                    uint closestKey = GatheringPointBaseDict.Keys
                        .OrderBy(k => Math.Abs((int)k - tempSlot))
                        .First();

                    newSlot = closestKey;
                }

                GatheringSlot = newSlot;
            }

            ImGui.Text($"Gathering Slot: {GatheringSlot}");
            foreach (var entry in GatheringPointBaseDict)
            {
                if (entry.Key == GatheringSlot)
                {
                    ImGui.Text($"Gathering Type: {entry.Value.GatheringType}");
                    ImGui.SameLine();
                    ImGui.Text($"Gathering Level: {entry.Value.GatheringLevel}");
                    for (int i = 0; i < entry.Value.Items.Count; i++)
                    {
                        var item = entry.Value.Items.ElementAt(i);
                        if (item == 0)
                            continue;
                        var itemName = Svc.Data.GetExcelSheet<Item>().GetRow(item).Name.ToString();
                        ImGui.Text($"Item: {item} | Name: {itemName}");
                    }
                    string GatheringNodes = string.Empty;
                    for (int i = 0; i < entry.Value.NodeIds.Count; i++)
                    {
                        var nodeId = entry.Value.NodeIds.ElementAt(i);
                        GatheringNodes += $"{nodeId}, ";
                    }
                    ImGui.Text(GatheringNodes);
                }
            }

            GatherItemSearch();
        }

        private static bool openSearchPopup = false;
        private static string searchInput = "";
        private static List<KeyValuePair<uint, string>> filteredItems = new();

        private static void GatherItemSearch()
        {
            if (ImGui.Button("Open Search###GatheringItemSearch"))
            {
                openSearchPopup = true;
                ImGui.OpenPopup("Gathering Item Search");
            }

            if (openSearchPopup)
            {
                ImGui.SetNextWindowSize(new System.Numerics.Vector2(300, 300), ImGuiCond.Once);

                if (ImGui.BeginPopupModal("Gathering Item Search", ref openSearchPopup, ImGuiWindowFlags.AlwaysAutoResize))
                {
                    // Search bar
                    ImGui.InputText("Search", ref searchInput, 100);

                    // Filter dictionary based on value (name)
                    filteredItems = GatheringItems
                        .Where(kv => string.IsNullOrEmpty(searchInput) || kv.Value.Contains(searchInput, StringComparison.OrdinalIgnoreCase))
                        .ToList();

                    ImGui.Separator();

                    // Display filtered results
                    foreach (var kv in filteredItems)
                    {
                        string label = $"{kv.Value} (ID: {kv.Key})";
                        if (ImGui.Selectable(label))
                        {
                            // Handle selection
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
                            openSearchPopup = false;
                            ImGui.CloseCurrentPopup();
                        }
                    }

                    ImGui.EndPopup();
                }
            }
        }
    }
}
