using GatherChill.Utilities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GatherChill.ConfigYaml;

public class RouteConfig : IYamlConfig
{
    public uint RouteNumber { get; set; }
    public string Expansion { get; set; } = "";
    public string Area { get; set; } = "";
    public uint AreaId { get; set; }

    public HashSet<uint> ListNodeIds { get; set; } = new HashSet<uint>();

    public List<Data.GathNodeInfo> GatherPoints { get; set; } = new();

    public static string GetConfigPath(uint routeNumber, string expansion, string area)
    {
        return Path.Combine(
            Svc.PluginInterface.ConfigDirectory.FullName,
            "Routes",
            expansion,
            area,
            $"Route{routeNumber}.yaml"
        );
    }

    public void Save()
    {
        string path = GetConfigPath(RouteNumber, Expansion, Area);
        string directory = Path.GetDirectoryName(path)!;
        if (!Directory.Exists(directory))
            Directory.CreateDirectory(directory);

        YamlConfig.Save(this, path);
    }
}

