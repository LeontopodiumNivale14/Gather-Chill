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
        private static int GatheringSlot = 0;
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
            }

            if (ImGui.SliderInt("Items", ref GatheringSlot, 0, 1205))
            {
                if (!GatheringPointBaseDict.ContainsKey(GatheringSlot))
                {
                    // Snap to closest key
                    int closestKey = GatheringPointBaseDict.Keys
                        .OrderBy(k => Math.Abs(k - GatheringSlot))
                        .First();

                    GatheringSlot = closestKey;
                }
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
                }
            }
        }
    }
}
