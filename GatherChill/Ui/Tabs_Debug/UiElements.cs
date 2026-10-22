using GatherChill.Gui;
using System;
using System.Collections.Generic;
using System.Text;

namespace GatherChill.Ui.Tabs_Debug
{
    internal class UiElements
    {
        private static int MinAmount = 100;
        private static int MaxAmount = 950;
        private static float Radius = 24;
        private static Vector4 FilColor = new Vector4(0.2f, 0.6f, 1f, 1f);      // Blue #3399ff  - Fill Bar Part #1 (Left Side)

        public static void Draw()
        {
            ImGui.InputInt("Min Gauge", ref MinAmount, 1, 10);
            ImGui.InputInt("Max Gauge", ref MaxAmount, 1, 10);
            ImGui.InputFloat("Radius", ref Radius, 1);

            ImGui_Ice.CircleBar("GP", MinAmount, MaxAmount, Radius, fillColor: ImGui.GetColorU32(FilColor));
        }
    }
}
