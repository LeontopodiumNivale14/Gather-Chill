using GatherChill.Utilities.GatheringHelpers;
using System;
using System.Collections.Generic;

namespace GatherChill.ConfigFiles;

public partial class Config
{
    // Auto Buff Settings
    public Gather_Util.ProfileClass User_AutoProfile { get; set; } = new();
    public List<Gather_Util.BuffRule> User_BuffRules { get; set; } = new();
    public List<Gather_Util.BuffFolder> User_BuffFolders { get; set; } = new();
}