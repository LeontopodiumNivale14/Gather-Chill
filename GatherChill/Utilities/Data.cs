using Dalamud.Game.ClientState.Statuses;
using Dalamud.Interface.Textures;
using Dalamud.Interface.Textures.TextureWraps;
using ECommons;
using ECommons.ExcelServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GatherChill.Utilities;

public static unsafe class Data
{

    #region Gathering Actions

    public class GatheringActions
    {
        public string ActionName { get; set; }
        public string InternalName { get; set; }
        public int StatusId { get; set; }
        public string StatusName { get; set; }
    }

    public static Dictionary<string, GatheringActions> BtnActionDict = new()
    {
        { "BoonIncrease1", new GatheringActions
        {
            ActionName = "Pioneer's Gift I",
            InternalName = "",
            StatusId = 2666,
            StatusName = "Gift of the Land"
        }},
        { "BoonIncrease2", new GatheringActions
        {
            ActionName = "Pioneer's Gift II",
            InternalName = "",
            StatusId = 759,
            StatusName = "Gift of the Land II"
        }},
        { "Tidings", new GatheringActions
        {
            ActionName = "Nophica's Tidings",
            InternalName = "",
            StatusId = 2667,
            StatusName = "Gatherer's Bounty"
        }},
        { "YieldI", new GatheringActions
        {
            ActionName = "Blessed Harvest",
            InternalName = "",
            StatusId = 219,
            StatusName = "Gathering Yield Up"
        }},
        { "YieldII", new GatheringActions
        {
            ActionName = "Blessed Harvest II",
            InternalName = "",
            StatusId = 219,
            StatusName = "Gathering Yield Up"
        }},
        { "IntegrityIncrease", new GatheringActions
        {
            ActionName = "Ageless Words",
            InternalName = "",
        }},
        { "BonusIntegrityChance", new GatheringActions
        {
            ActionName = "",
            InternalName = "",
            StatusId = 2765,
            StatusName = ""
        }},
    };

    public static Dictionary<string, GatheringActions> MinActionDict = new();

    #endregion

    #region Gathering Node Information

    public class GatheringTypes
    {
        public required string Name { get; set; }
        public required ISharedImmediateTexture? MainIcon { get; set; }
        public required ISharedImmediateTexture? ShinyIcon { get; set; }
    }

    public static Dictionary<uint, GatheringTypes> GatheringNodeDict = new();

    public class GatheringPointBaseInformation
    {
        public int GatheringType { get; set; }
        public int GatheringLevel { get; set; }
        public HashSet<uint> Items { get; set; }
    }
    public static Dictionary<int, GatheringPointBaseInformation> GatheringPointBaseDict = new();

    #endregion
}
