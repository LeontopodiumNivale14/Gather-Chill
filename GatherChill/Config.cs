using System.Text.Json.Serialization;
using ECommons.Configuration;
namespace GatherChill;
public class Config : IEzConfig
{
    [JsonIgnore]
    public const int CurrentConfigVersion = 1;

    public void Save()
    {
        EzConfig.Save();
    }
}