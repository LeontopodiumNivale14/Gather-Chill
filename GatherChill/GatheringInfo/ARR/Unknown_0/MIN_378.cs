using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.ARR.Unknown_0
{
    public class MIN_378 : RouteInfo
    {
        public override uint Id => 378;
        public override uint ExpansionId => 0;
        public override uint ZoneId => 0;
        public override uint GatherType => 0;
        public override Vector2 MapPosition => new Vector2(256.208f, 291.561f);
        public override int Radius => 108;

        public override HashSet<uint> NodeIds => new()
        {
            31610,
            31612,
            31638,
            31640,
            31666,
            31668,
        };

        public override HashSet<uint> ItemIds => new()
        {
            8,
            12534,
            12535,
            12537,
            13750,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 31610,
            },
            new NodeInfo
            {
                NodeId = 31612,
            },
            new NodeInfo
            {
                NodeId = 31638,
            },
            new NodeInfo
            {
                NodeId = 31640,
            },
            new NodeInfo
            {
                NodeId = 31666,
            },
            new NodeInfo
            {
                NodeId = 31668,
            },
        };
    }
}
