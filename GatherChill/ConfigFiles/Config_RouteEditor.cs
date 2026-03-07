using System;
using System.Collections.Generic;
using System.Text;

namespace GatherChill.ConfigFiles;

public partial class Config
{
    public string SaveLocation { get; set; } = Svc.PluginInterface.GetPluginConfigDirectory();

}
