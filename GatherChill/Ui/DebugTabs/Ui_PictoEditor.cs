using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GatherChill.Ui.DebugTabs
{
    internal class Ui_PictoEditor
    {
        public static void Draw()
        {
            ImGui.Text("Node Color Editor");
            ImGui.SameLine();
            Vector4 nodeColor = C.Picto_NodeColor;
            if (ImGui.ColorEdit4("###NodeColorEditor", ref nodeColor))
            {
                C.Picto_NodeColor = nodeColor;
                C.Save();
            }

            ImGui.Text("Land Area Color Editor");
            ImGui.SameLine();
            Vector4 landAreaColor = C.Picto_LandAreaColor;
            if (ImGui.ColorEdit4("###LandAreaColorEditor", ref landAreaColor))
            {
                C.Picto_LandAreaColor = landAreaColor;
                C.Save();
            }

            ImGui.Text("Radial Area Color Editor");
            ImGui.SameLine();
            Vector4 radialColor = C.Picto_RadialColor;
            if (ImGui.ColorEdit4("###RadialAreaColorEditor", ref radialColor))
            {
                C.Picto_RadialColor = radialColor;
                C.Save();
            }

            ImGui.Text("Text Color Editor");
            ImGui.SameLine();
            Vector4 textColor = C.Picto_TextColor;
            if (ImGui.ColorEdit4("###TextColorEditor", ref textColor))
            {
                C.Picto_TextColor = textColor;
                C.Save();
            }
        }
    }
}
