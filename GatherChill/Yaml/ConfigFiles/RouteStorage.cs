using GatherChill.Utilities.GatheringHelpers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GatherChill.Yaml.ConfigFiles;

public class RouteStorage
{
    // Zone identification
    public uint ZoneId { get; set; }
    public string ZoneName { get; set; } = string.Empty;
    public uint ExpansionId { get; set; }
    public string ExpansionName { get; set; } = string.Empty;

    /// <summary>
    /// <b>Key: </b>Route Type/ID (uint) <br></br>
    /// <b>Values: </b>List of RouteData for that route type<br></br>
    /// <b>- NodeId</b> <br></br>
    /// <b>- NodePosition</b> <br></br>
    /// <b>- LandZone</b> <br></br>
    /// <b>- RadialPositioning</b> [bool] <br></br>
    /// <b>- InnerRadius</b> <br></br>
    /// <b>- OuterRadius</b> <br></br>
    /// <b>- StartAngle</b> <br></br>
    /// <b>- EndAngle</b> <br></br>
    /// <b>- RotationalOffset</b> <br></br>
    /// </summary>
    public Dictionary<uint, List<GatherClasses.RouteStorage>> Routes { get; set; } = new();

}
