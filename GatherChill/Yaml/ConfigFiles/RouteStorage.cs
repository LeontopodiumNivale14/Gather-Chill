using GatherChill.Utilities.GatheringHelpers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GatherChill.Yaml.ConfigFiles;

public class RouteStorage : IYamlConfig
{
    /// <summary>
    /// <b>Key: </b>Route Type/ID (uint) <br></br>
    /// <b>Values: </b>List of RouteData for that route type<br></br>
    /// </summary>
    public Dictionary<uint, List<GatherClasses.RouteData>> Routes { get; set; } = new();

    // Zone identification
    public uint ZoneId { get; set; }
    public string ZoneName { get; set; } = string.Empty;
    public uint ExpansionId { get; set; }
    public string ExpansionName { get; set; } = string.Empty;

}
