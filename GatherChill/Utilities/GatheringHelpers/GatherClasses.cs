using Dalamud.Interface.Textures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GatherChill.Utilities.GatheringHelpers;

public static class GatherClasses
{
    #region Gathering Icon Types

    public static Dictionary<uint, GatheringTypes> GatheringTypeIcons = new();

    public class GatheringTypes
    {
        public required string Name { get; set; }
        public required ISharedImmediateTexture? MainIcon { get; set; }
        public required ISharedImmediateTexture? ShinyIcon { get; set; }
    }

    #endregion

    #region Route Information

    public class RouteInfo
    {
        public Vector2 MapCenter { get; set; }
        public uint MapRadius { get; set; }
        public uint GatheringType {  get; set; }

        public uint ExpansionId { get; set; }
        public uint ExpansionName { get; set; }
        public uint ZoneId { get; set; }
        public string ZoneName { get; set; } = "???";
        
    }

    #endregion
}
