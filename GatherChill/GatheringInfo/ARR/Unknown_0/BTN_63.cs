using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.ARR.Unknown_0
{
    public class BTN_63 : RouteInfo
    {
        public override uint Id => 63;
        public override uint ExpansionId => 0;
        public override uint ZoneId => 0;
        public override uint GatherType => 3;
        public override Vector2 MapPosition => new Vector2(-266.264f, 357.52f);
        public override int Radius => 47;

        public override HashSet<uint> NodeIds => new()
        {
            30120,
            30121,
            30122,
            30123,
            30124,
            30125,
            30126,
            30127,
            30128,
            30129,
        };

        public override HashSet<uint> ItemIds => new()
        {
            2000294,
            2000295,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 30120,
            },
            new NodeInfo
            {
                NodeId = 30121,
            },
            new NodeInfo
            {
                NodeId = 30122,
            },
            new NodeInfo
            {
                NodeId = 30123,
            },
            new NodeInfo
            {
                NodeId = 30124,
            },
            new NodeInfo
            {
                NodeId = 30125,
            },
            new NodeInfo
            {
                NodeId = 30126,
            },
            new NodeInfo
            {
                NodeId = 30127,
            },
            new NodeInfo
            {
                NodeId = 30128,
            },
            new NodeInfo
            {
                NodeId = 30129,
            },
        };
    }
}
