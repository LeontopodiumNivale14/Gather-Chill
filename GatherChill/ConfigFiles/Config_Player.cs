using System;
using System.Collections.Generic;
using System.Text;

namespace GatherChill.ConfigFiles;

public partial class Config
{
    public bool SelfRepair { get; set; } = false;
    public int Minimum_DarkMatter { get; set; } = 11;
    public int Repair_Amount { get; set; } = 50;
    public bool Extract_Spiritbond { get; set; } = false;
}
