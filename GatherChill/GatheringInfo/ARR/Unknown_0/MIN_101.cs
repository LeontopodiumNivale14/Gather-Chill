using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.ARR.Unknown_0
{
    public class MIN_101 : RouteInfo
    {
        public override uint Id => 101;
        public override uint ExpansionId => 0;
        public override uint ZoneId => 0;
        public override uint GatherType => 0;
        public override Vector2 MapPosition => new Vector2(-189.102f, -300.477f);
        public override int Radius => 74;

        public override HashSet<uint> NodeIds => new()
        {
            30746,
            30747,
            30748,
            30749,
            30750,
            30751,
            30752,
            30753,
            30754,
            30755,
        };

        public override HashSet<uint> ItemIds => new()
        {
            2000481,
            2000482,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 30746,
            },
            new NodeInfo
            {
                NodeId = 30747,
            },
            new NodeInfo
            {
                NodeId = 30748,
            },
            new NodeInfo
            {
                NodeId = 30749,
            },
            new NodeInfo
            {
                NodeId = 30750,
            },
            new NodeInfo
            {
                NodeId = 30751,
            },
            new NodeInfo
            {
                NodeId = 30752,
            },
            new NodeInfo
            {
                NodeId = 30753,
            },
            new NodeInfo
            {
                NodeId = 30754,
            },
            new NodeInfo
            {
                NodeId = 30755,
            },
        };
    }
}
