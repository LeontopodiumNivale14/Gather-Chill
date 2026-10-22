using GatherChill.Ui.Tables;
using GatherChill.Utilities.Tools;
using System;
using System.Collections.Generic;
using System.Text;

namespace GatherChill.Ui.Tabs_Debug
{
    internal class Logs_Normal
    {
        private static Table_logs.LogTable? LogTable;

        public static void Draw()
        {
            LogTable ??= new Table_logs.LogTable();

            if (ImGui.Button("copy Logs"))
            {
                IceLogging.LogSystem.CopyToClipboard();
            }
            LogTable.Reload();
            LogTable.Draw();
        }
    }
}
