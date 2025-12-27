using System;
using System.Collections.Generic;
using System.Text;

namespace GatherChill.Utilities
{
    internal class ImGui_Util
    {
        public static void Table_VertCenterText(string text)
        {
            ImGui.AlignTextToFramePadding();
            ImGui.TextUnformatted(text);
        }
    }
}
