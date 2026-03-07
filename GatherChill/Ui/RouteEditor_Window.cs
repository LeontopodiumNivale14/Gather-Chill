using Dalamud.Interface.Utility.Raii;
using GatherChill.Ui.RouteWindowTabs;
using Lumina.Excel.Sheets;
using System;
using System.Collections.Generic;
using System.Text;

namespace GatherChill.Ui
{
    internal class RouteEditor_Window : Window
    {
        public RouteEditor_Window() : base($"Gather & Chill Route Editor ###Gather&ChillRouteEditorWindow")
        {
            Flags = ImGuiWindowFlags.None;
            SizeConstraints = new()
            {
                MinimumSize = new(500, 500)
            };

            P.windowSystem.AddWindow( this );
        }

        public void Dispose()
        {
            P.windowSystem.RemoveWindow( this );
        }

        public override void Draw()
        {
            if (ImGui.BeginTabBar("Route Editor: Tab Bar"))
            {
                if (ImGui.BeginTabItem("Route Selector"))
                {
                    Route_Selector.Draw();
                    ImGui.EndTabItem();
                }

                if (ImGui.BeginTabItem("Route Editor"))
                {
                    Route_Editor.Draw();
                    ImGui.EndTabItem();
                }

                if (ImGui.BeginTabItem("Temp"))
                {
                    Route_UpdateAll.Draw();
                    ImGui.EndTabItem();
                }

                ImGui.EndTabBar();
            }
        }
    }
}
