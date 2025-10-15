using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.ARR.Unknown_0
{
    public class MIN_118 : RouteInfo
    {
        public override uint Id => 118;
        public override uint ExpansionId => 0;
        public override uint ZoneId => 0;
        public override uint GatherType => 0;
        public override Vector2 MapPosition => new Vector2(328.179f, 285.287f);
        public override int Radius => 19;

        public override HashSet<uint> NodeIds => new()
        {
            30874,
            30875,
            30876,
            30877,
        };

        public override HashSet<uint> ItemIds => new()
        {
            2000506,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 30874,
            },
            new NodeInfo
            {
                NodeId = 30875,
            },
            new NodeInfo
            {
                NodeId = 30876,
            },
            new NodeInfo
            {
                NodeId = 30877,
            },
        };
    }
}
