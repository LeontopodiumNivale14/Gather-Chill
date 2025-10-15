using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.ARR.Unknown_0
{
    public class MIN_125 : RouteInfo
    {
        public override uint Id => 125;
        public override uint ExpansionId => 0;
        public override uint ZoneId => 0;
        public override uint GatherType => 0;
        public override Vector2 MapPosition => new Vector2(-756.639f, 50.7204f);
        public override int Radius => 57;

        public override HashSet<uint> NodeIds => new()
        {
            30928,
            30929,
            30930,
            30931,
            30932,
            30933,
            30934,
            30935,
            30936,
            30937,
        };

        public override HashSet<uint> ItemIds => new()
        {
            2000899,
            2000900,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 30928,
            },
            new NodeInfo
            {
                NodeId = 30929,
            },
            new NodeInfo
            {
                NodeId = 30930,
            },
            new NodeInfo
            {
                NodeId = 30931,
            },
            new NodeInfo
            {
                NodeId = 30932,
            },
            new NodeInfo
            {
                NodeId = 30933,
            },
            new NodeInfo
            {
                NodeId = 30934,
            },
            new NodeInfo
            {
                NodeId = 30935,
            },
            new NodeInfo
            {
                NodeId = 30936,
            },
            new NodeInfo
            {
                NodeId = 30937,
            },
        };
    }
}
