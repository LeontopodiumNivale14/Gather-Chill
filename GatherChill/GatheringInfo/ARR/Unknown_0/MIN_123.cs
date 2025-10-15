using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.ARR.Unknown_0
{
    public class MIN_123 : RouteInfo
    {
        public override uint Id => 123;
        public override uint ExpansionId => 0;
        public override uint ZoneId => 0;
        public override uint GatherType => 0;
        public override Vector2 MapPosition => new Vector2(436.407f, 466.964f);
        public override int Radius => 67;

        public override HashSet<uint> NodeIds => new()
        {
            30912,
            30913,
            30914,
            30915,
            30916,
            30917,
            30918,
            30919,
            30920,
            30921,
        };

        public override HashSet<uint> ItemIds => new()
        {
            2000896,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 30912,
            },
            new NodeInfo
            {
                NodeId = 30913,
            },
            new NodeInfo
            {
                NodeId = 30914,
            },
            new NodeInfo
            {
                NodeId = 30915,
            },
            new NodeInfo
            {
                NodeId = 30916,
            },
            new NodeInfo
            {
                NodeId = 30917,
            },
            new NodeInfo
            {
                NodeId = 30918,
            },
            new NodeInfo
            {
                NodeId = 30919,
            },
            new NodeInfo
            {
                NodeId = 30920,
            },
            new NodeInfo
            {
                NodeId = 30921,
            },
        };
    }
}
