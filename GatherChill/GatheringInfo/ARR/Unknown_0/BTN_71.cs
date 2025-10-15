using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.ARR.Unknown_0
{
    public class BTN_71 : RouteInfo
    {
        public override uint Id => 71;
        public override uint ExpansionId => 0;
        public override uint ZoneId => 0;
        public override uint GatherType => 2;
        public override Vector2 MapPosition => new Vector2(-183.186f, -63.9846f);
        public override int Radius => 40;

        public override HashSet<uint> NodeIds => new()
        {
            30264,
            30265,
            30266,
            30267,
            30268,
            30269,
            30270,
            30271,
            30272,
            30273,
        };

        public override HashSet<uint> ItemIds => new()
        {
            2000271,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 30264,
            },
            new NodeInfo
            {
                NodeId = 30265,
            },
            new NodeInfo
            {
                NodeId = 30266,
            },
            new NodeInfo
            {
                NodeId = 30267,
            },
            new NodeInfo
            {
                NodeId = 30268,
            },
            new NodeInfo
            {
                NodeId = 30269,
            },
            new NodeInfo
            {
                NodeId = 30270,
            },
            new NodeInfo
            {
                NodeId = 30271,
            },
            new NodeInfo
            {
                NodeId = 30272,
            },
            new NodeInfo
            {
                NodeId = 30273,
            },
        };
    }
}
