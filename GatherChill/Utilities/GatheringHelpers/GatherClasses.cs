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
        // Important Information first
        public uint ExpansionId { get; set; } = 0;
        public string ExpansionName { get; set; } = "ARR";
        public uint ZoneId { get; set; } = 0;
        public string ZoneName { get; set; } = "???";

        // Map Information
        public Vector2 MapCenter { get; set; }
        public uint MapRadius { get; set; }
        public uint GatheringType { get; set; }

        public HashSet<uint> NodeIds { get; set; } = new();
        public Dictionary<uint, string> Items { get; set; } = new();
        public HashSet<uint> ItemIds { get; set; } = new();
    }

    public static Dictionary<uint, RouteInfo> RouteDatabase = new();

    #endregion

    public static Dictionary<uint, string> ExpansionInfo = new();
}
