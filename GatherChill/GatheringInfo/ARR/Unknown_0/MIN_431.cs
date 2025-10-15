using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.ARR.Unknown_0
{
    public class MIN_431 : RouteInfo
    {
        public override uint Id => 431;
        public override uint ExpansionId => 0;
        public override uint ZoneId => 0;
        public override uint GatherType => 0;
        public override Vector2 MapPosition => new Vector2(335.306f, -319.715f);
        public override int Radius => 115;

        public override HashSet<uint> NodeIds => new()
        {
            31810,
            31811,
            31812,
            31813,
        };

        public override HashSet<uint> ItemIds => new()
        {
            2,
            8,
            7589,
            12535,
            13750,
            17570,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 31810,
            },
            new NodeInfo
            {
                NodeId = 31811,
            },
            new NodeInfo
            {
                NodeId = 31812,
            },
            new NodeInfo
            {
                NodeId = 31813,
            },
        };
    }
}
