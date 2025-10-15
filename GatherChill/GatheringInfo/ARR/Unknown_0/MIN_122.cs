using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.ARR.Unknown_0
{
    public class MIN_122 : RouteInfo
    {
        public override uint Id => 122;
        public override uint ExpansionId => 0;
        public override uint ZoneId => 0;
        public override uint GatherType => 0;
        public override Vector2 MapPosition => new Vector2(137.124f, 37.0067f);
        public override int Radius => 27;

        public override HashSet<uint> NodeIds => new()
        {
            30908,
            30909,
            30910,
            30911,
        };

        public override HashSet<uint> ItemIds => new()
        {
            2000895,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 30908,
            },
            new NodeInfo
            {
                NodeId = 30909,
            },
            new NodeInfo
            {
                NodeId = 30910,
            },
            new NodeInfo
            {
                NodeId = 30911,
            },
        };
    }
}
