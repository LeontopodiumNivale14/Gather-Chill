using Dalamud.Interface.Utility.Raii;
using GatherChill.Gui;
using GatherChill.Ui.Tabs_Debug;
using System;
using System.Collections.Generic;
using System.Text;

namespace GatherChill.Ui.Tabs_MainWindow
{
    internal class LogsWindow
    {
        // Its own viewer, separate from the standalone replay window, so the two keep their own selections
        private static readonly ReplayViewer Replays = new();
        private static bool _replaysTabOpen;

        public static void Draw()
        {
            using var colors = ImGui_Ice.PushChildBG();
            using (var child = ImRaii.Child("Logs Window Container", default, true))
            {
                if (!child.Success)
                    return;

                using (var tabbar = ImRaii.TabBar("Logs Window"))
                {
                    if (!tabbar.Success)
                        return;

                    MainLogs();
                    DestinationLogs();
                    ReplayLogs();
                }
            }
        }

        private static void MainLogs()
        {
            using (var tabItem = ImRaii.TabItem("Plugin Logs"))
            {
                if (!tabItem.Success)
                    return;

                using (var child = ImRaii.Child("Plugin Logs Child", default, true))
                {
                    if (!child.Success)
                        return;

                    Logs_Normal.Draw();
                }
            }
        }

        private static void DestinationLogs()
        {
            using (var tabItem = ImRaii.TabItem("Destination Logs"))
            {
                if (!tabItem.Success)
                    return;

                using (var child = ImRaii.Child("Plugin Desination Child", default, true))
                {
                    if (!child.Success)
                        return;

                    Logs_Destination.Draw();
                }
            }
        }

        private static void ReplayLogs()
        {
            using (var tabItem = ImRaii.TabItem("Replays"))
            {
                if (!tabItem.Success)
                {
                    // Another tab is showing, so the next time this one opens it reloads the files
                    _replaysTabOpen = false;
                    return;
                }

                // First frame of the tab being shown: pick up any replays saved since last time
                if (!_replaysTabOpen)
                {
                    _replaysTabOpen = true;
                    Replays.Refresh();
                }

                using (var child = ImRaii.Child("Plugin Replays Child", default, true))
                {
                    if (!child.Success)
                        return;

                    Replays.Draw();
                }
            }
        }
    }
}