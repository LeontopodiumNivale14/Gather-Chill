using GatherChill.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace GatherChill.ConfigFiles;

public partial class Config
{
    public WindowSelection SelectedTab { get; set; } = WindowSelection.ItemViewer;
    public bool UseIceTheme { get; set; } = true;
    public bool Compact_Ui { get; set; } = false;
    public bool SafetyDelay_Gather { get; set; } = false;
    public int Gather_DelayTimer { get; set; } = 250;
}
