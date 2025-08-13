using Lumina.Excel.Sheets;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GatherChill.Yaml.ConfigFiles;

public class GatherIndex : IYamlConfig
{
    public class ExpansionData
    {
        public uint ExpansionId { get; set; }
        public string ExpansionName { get; set; } = string.Empty;
        /// <summary>
        /// <b>Key: </b>ZoneName <br></br>
        /// <b>Values: </b>ZoneRoutes class<br></br>
        /// </summary>
        public Dictionary<string, ZoneRoutes> Zones { get; set; } = new();
    }

    public class ZoneRoutes
    {
        public uint ZoneId { get; set; }
        public string ZoneName { get; set; } = string.Empty;
        /// <summary>
        /// <b>Key: </b>Route identifier (could be gathering type or custom name) <br></br>
        /// <b>Values: </b>RouteInfo class<br></br>
        /// </summary>
        public Dictionary<uint, RouteInfo> Routes { get; set; } = new();
    }

    public class RouteInfo
    {
        public Vector2 MapCenter { get; set; }
        public uint MapRadius { get; set; }
        public uint GatheringType { get; set; }
        public HashSet<uint> NodeIds { get; set; } = new();
    }

    /// <summary>
    /// <b>Key: </b>ExpansionName <br></br>
    /// <b>Values: </b>ExpansionData class<br></br>
    /// </summary>
    public Dictionary<string, ExpansionData> Expansions { get; set; } = new();

    // General Saving
    public static string ConfigPath => Path.Combine(Svc.PluginInterface.ConfigDirectory.FullName, "GatherIndex.Yaml");
    public void Save() => YamlConfig.Save(this, ConfigPath);
}