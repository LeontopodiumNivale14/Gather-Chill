using Dalamud.Interface.Utility.Raii;
using GatherChill.Ui.RouteWindowTabs;

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

        public enum tabSelector
        {
            RouteSelector,
            RouteEditor,
        }

        public tabSelector PreviousTab = tabSelector.RouteSelector;
        public tabSelector CurrentTab = tabSelector.RouteSelector;

        public override void Draw()
        {
            if (PreviousTab == tabSelector.RouteEditor && CurrentTab == tabSelector.RouteSelector)
                Route_Editor.SavePreviousRoute();

            PreviousTab = CurrentTab;
            CurrentTab = tabSelector.RouteSelector;

            if (Route_Editor.LastSavedAt != null)
            {
                using (ImRaii.PushColor(ImGuiCol.Text, new Vector4(0.5f, 1f, 0.5f, 1f)))
                    ImGui.Text($"Last Saved: {Route_Editor.LastSavedAt}");
            }

            if (ImGui.BeginTabBar("Route Editor: Tab Bar"))
            {
                if (ImGui.BeginTabItem("Route Selector"))
                {
                    CurrentTab = tabSelector.RouteSelector;
                    Route_Selector.Draw();
                    ImGui.EndTabItem();
                }

                if (ImGui.BeginTabItem("Gather List"))
                {
                    Route_GatherList.Draw();
                    ImGui.EndTabItem();
                }

                if (ImGui.BeginTabItem($"Route Editor [{Route_Editor.SelectedRoute}]"))
                {
                    CurrentTab = tabSelector.RouteEditor;
                    Route_Editor.Draw();
                    ImGui.EndTabItem();
                }

                if (ImGui.BeginTabItem("Save Location"))
                {
                    Route_UpdateAll.Draw();
                    ImGui.EndTabItem();
                }

                ImGui.EndTabBar();
            }
        }
    }
}
