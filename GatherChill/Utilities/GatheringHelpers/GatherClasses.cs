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

    #region Route + Expansion Info

    public class RouteInfo
    {
        // Important Information first
        public uint ExpansionId { get; set; } = 0;
        public string ExpansionName { get; set; } = "ARR";
        public uint ZoneId { get; set; } = 0;
        public string ZoneName { get; set; } = "Unknown";

        // Map Information
        public Vector2 MapCenter { get; set; }
        public uint MapRadius { get; set; }
        public uint GatheringType { get; set; }

        public HashSet<uint> NodeIds { get; set; } = new();
        public Dictionary<uint, string> Items { get; set; } = new();
        public HashSet<uint> ItemIds { get; set; } = new();
    }

    public static Dictionary<uint, RouteInfo> GatheringDatabase = new();

    public static Dictionary<uint, string> ExpansionInfo = new();

    public class RouteStorage
    {
        public HashSet<uint> NodeIds { get; set;} = new();
        public List<RouteData> Routes { get; set;} = new();
    }

    #endregion

    #region RouteInfo

    /// <summary>
    /// <b>NodeId [uint]: </b>Id Attached to the node <br></br>
    /// <b>NodePosition [Vector3]: </b>Location of the node <br></br>
    /// <b>LandZone [Vector3]: </b>Guarentee'd land zone, good if not <br></br>
    /// <b>RadialPositioning [bool]: </b> Bool to use radial positioning or fixed. <br></br>
    /// <b>Inner/Outer Radius [float]: </b> Setting the radius's for the rings. 
    /// <b>Start/End Angles [float]</b> The amount <br></br>
    /// </summary>
    public class RouteData
    {
        public uint NodeId { get; set; } = 0;
        public Vector3 NodePosition { get; set; } = Vector3.Zero;
        public Vector3 LandZone { get; set; } = Vector3.Zero;
        public bool RadialPositioning { get; set; } = false;
        public float InnerRadius { get; set; } = 1.0f;
        public float OuterRadius { get; set; } = 3.0f;
        public float StartAngle { get; set; } = 0.0f;
        public float EndAngle { get; set; } = 360.0f;
        public float RotationOffset { get; set; } = 0.0f;
    }

    public static Dictionary<uint, List<RouteData>> RouteDatabase = new();

    #endregion
}
