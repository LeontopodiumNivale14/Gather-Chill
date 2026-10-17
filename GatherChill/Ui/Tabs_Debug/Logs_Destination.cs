using GatherChill.Gui;
using System;
using System.Collections.Generic;
using System.Text;
using static GatherChill.Utilities.Tools.IceLogging;

namespace GatherChill.Ui.Tabs_Debug
{
    internal class Logs_Destination
    {
        public static void Draw()
        {
            ImGuiTableFlags flags = ImGuiTableFlags.RowBg |
                                    ImGuiTableFlags.Borders |
                                    ImGuiTableFlags.ScrollY |
                                    ImGuiTableFlags.SizingFixedFit;

            if (ImGui.BeginTable("Destination Log Viewer", 5, flags))
            {
                ImGui.TableSetupColumn("Timestamp");
                ImGui.TableSetupColumn("Start");
                ImGui.TableSetupColumn("Destination");
                ImGui.TableSetupColumn("Distance");

                ImGui.TableHeadersRow();

                var filteredLogs = DestinationLogs.Logs.AsEnumerable();
                var entryNumber = 0;

                foreach (var log in filteredLogs.OrderByDescending(l => l.Timestamp))
                {
                    ImGui.TableNextRow();

                    ImGui.PushID($"{log.PlayerDestination}_{entryNumber}");

                    ImGui.TableSetColumnIndex(0);
                    ImGui_Ice.Table_VertCenterText(log.Timestamp.ToString("HH:mm:ss"));

                    ImGui.TableNextColumn();
                    ImGui_Ice.Table_VertCenterText($"X: {log.PlayerStart.X:N2}, Y: {log.PlayerStart.Y:N2}, Z: {log.PlayerStart.Z:N2}");

                    ImGui.TableNextColumn();
                    ImGui_Ice.Table_VertCenterText($"X: {log.PlayerDestination.X:N2}, Y: {log.PlayerDestination.Y:N2}, Z: {log.PlayerDestination.Z:N2}");

                    ImGui.TableNextColumn();
                    ImGui_Ice.Table_VertCenterText($"{log.Distance}");

                    ImGui.TableNextColumn();
                    if (ImGui.Button("Copy Info"))
                    {
                        var clipboardText = new StringBuilder();
                        clipboardText.AppendLine($"Start: X: {log.PlayerStart.X:N2}, Y: {log.PlayerStart.Y:N2}, Z: {log.PlayerStart.Z:N2}");
                        clipboardText.Append($"End: X: {log.PlayerDestination.X:N2}, Y: {log.PlayerDestination.Y:N2}, Z: {log.PlayerDestination.Z:N2}");
                        ImGui.SetClipboardText($"{clipboardText}");
                        Notify.Success("Log copied to clipbard");
                    }
                    ImGui.PopID();

                    entryNumber += 1;
                }

                ImGui.EndTable();
            }
        }
    }
}
