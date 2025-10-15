using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.ARR.Unknown_0
{
    public class MIN_276 : RouteInfo
    {
        public override uint Id => 276;
        public override uint ExpansionId => 0;
        public override uint ZoneId => 0;
        public override uint GatherType => 0;
        public override Vector2 MapPosition => new Vector2(279.346f, 454.691f);
        public override int Radius => 21;

        public override HashSet<uint> NodeIds => new()
        {
            31251,
            31252,
            31253,
            31254,
            31255,
            31256,
        };

        public override HashSet<uint> ItemIds => new()
        {
            2001834,
            2001835,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 31251,
            },
            new NodeInfo
            {
                NodeId = 31252,
            },
            new NodeInfo
            {
                NodeId = 31253,
            },
            new NodeInfo
            {
                NodeId = 31254,
            },
            new NodeInfo
            {
                NodeId = 31255,
            },
            new NodeInfo
            {
                NodeId = 31256,
            },
        };
    }
}
