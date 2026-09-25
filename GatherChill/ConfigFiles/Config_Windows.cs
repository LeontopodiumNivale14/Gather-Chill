using GatherChill.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace GatherChill.ConfigFiles;

public partial class Config
{
    public WindowSelection SelectedTab { get; set; } = WindowSelection.ItemViewer;
    public bool UseIceTheme { get; set; } = true;
}
