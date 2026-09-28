using Dalamud.Interface.Utility.Raii;
using GatherChill.Gui;
using GatherChill.Ui.RouteWindowTabs;
using GatherChill.Utilities.Tools;
using System.Collections.Generic;

namespace GatherChill.Ui.Tabs_MainWindow
{
    internal class RouteEditor
    {
        public enum tabSelector
        {
            RouteSelector,
            RouteEditor,
        }

        public static tabSelector PreviousTab = tabSelector.RouteSelector;
        public static tabSelector CurrentTab = tabSelector.RouteSelector;

        private static RouteInfo.RouteTable? RouteTable = null;
        private static List<RouteInfo.RouteItem> TableItems = [];
        private static int ItemCount = 0;

        public static void Draw()
        {
            var childColors = C.UseIceTheme ? ImRaii.PushColor(ImGuiCol.ChildBg, Theme_Colors.ChildBg) : default;

            using (var child = ImRaii.Child("Route Editor Tab", default, true))
            {
                if (!child.Success)
                    return;

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
}
