using Lumina.Excel.Sheets;
using System.Collections.Generic;

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
            P.windowSystem.RemoveWindow(this);
        }

        private static int gatheringType = 0;
        private static uint GatheringSlot = 0;
        private static string itemName = string.Empty;

        /// <summary>
        /// Primary draw method. Responsible for drawing the entire UI of the main window.
        /// </summary>
        public override void Draw()
        {

        }
    }
}
