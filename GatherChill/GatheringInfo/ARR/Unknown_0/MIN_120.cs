using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.ARR.Unknown_0
{
    public class MIN_120 : RouteInfo
    {
        public override uint Id => 120;
        public override uint ExpansionId => 0;
        public override uint ZoneId => 0;
        public override uint GatherType => 0;
        public override Vector2 MapPosition => new Vector2(547.534f, 202.521f);
        public override int Radius => 37;

        public override HashSet<uint> NodeIds => new()
        {
            30888,
            30889,
            30890,
            30891,
            30892,
            30893,
            30894,
            30895,
            30896,
            30897,
        };

        public override HashSet<uint> ItemIds => new()
        {
            2000508,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 30888,
            },
            new NodeInfo
            {
                NodeId = 30889,
            },
            new NodeInfo
            {
                NodeId = 30890,
            },
            new NodeInfo
            {
                NodeId = 30891,
            },
            new NodeInfo
            {
                NodeId = 30892,
            },
            new NodeInfo
            {
                NodeId = 30893,
            },
            new NodeInfo
            {
                NodeId = 30894,
            },
            new NodeInfo
            {
                NodeId = 30895,
            },
            new NodeInfo
            {
                NodeId = 30896,
            },
            new NodeInfo
            {
                NodeId = 30897,
            },
        };
    }
}
