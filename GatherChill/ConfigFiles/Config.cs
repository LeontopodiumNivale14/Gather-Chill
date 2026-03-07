using System.Collections.Generic;
using System.Text.Json.Serialization;
using ECommons.Configuration;

namespace GatherChill.ConfigFiles;

public partial class Config
{
    public int ConfigVersion { get; set; } = 1;

    public void Save()
    {
        EzConfig.Save();
    }

    public void SaveDebounced()
    {
        EzConfigExtensions.SaveDebounced();
    }
}