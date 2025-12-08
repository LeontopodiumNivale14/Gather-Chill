using System.Collections.Generic;
using System.Text.Json.Serialization;
using ECommons.Configuration;

namespace GatherChill;
public class Config
{
    [JsonIgnore]
    public const int CurrentConfigVersion = 1;
    public Vector4 Picto_NodeColor { get; set; } = new Vector4(0, 0, 0, 0);
    public Vector4 Picto_LandAreaColor { get; set; } = new Vector4(0, 0, 0, 0);
    public Vector4 Picto_RadialColor { get; set; } = new Vector4(0, 0, 0, 0);
    public Vector4 Picto_TextColor { get; set; } = new Vector4(0, 0, 0, 0);


    public void Save()
    {
        EzConfig.Save();
    }
}