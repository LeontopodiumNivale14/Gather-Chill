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
    }
}
