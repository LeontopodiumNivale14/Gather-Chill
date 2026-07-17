using Dalamud.Interface.Utility.Raii;
using GatherChill.Ui.RouteWindowTabs;
using GatherChill.Utilities.Tools;
using System.Collections.Generic;

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

        private RouteInfo.RouteTable? RouteTable = null;
        private List<RouteInfo.RouteItem> TableItems = [];
        private int ItemCount = 0;


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
                if (ImGui.BeginTabItem("Route Selector V2"))
                {
                    try
                    {
                        if (RouteTable == null && P.routeEditor.Routes.Count > 0)
                        {
                            foreach (var route in P.routeEditor.Routes)
                            {
                                RouteInfo.RouteItem routeItem = new() { RouteId = route.Key };
                                TableItems.Add(routeItem);
                            }
                            ItemCount = TableItems.Count();
                            RouteTable = new(TableItems);
                        }
                        ImGui.Text($"Count: {ItemCount:N0}");
                        if (RouteTable != null)
                        {
                            ImGui.SameLine();
                            ImGui.Text($"{RouteTable?.FilteredRows?.Count():N0}");
                        }
                        RouteTable?.Draw();
                    }
                    catch (Exception ex)
                    {
                        IceLogging.Error(ex.Message, "Drawing Mission Table");
                    }
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
