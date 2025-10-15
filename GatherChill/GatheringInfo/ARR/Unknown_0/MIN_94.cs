using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.ARR.Unknown_0
{
    public class MIN_94 : RouteInfo
    {
        public override uint Id => 94;
        public override uint ExpansionId => 0;
        public override uint ZoneId => 0;
        public override uint GatherType => 0;
        public override Vector2 MapPosition => new Vector2(309.068f, 117.776f);
        public override int Radius => 15;

        public override HashSet<uint> NodeIds => new()
        {
            30694,
            30695,
            30696,
            30697,
            30698,
            30699,
            30700,
            30701,
            30702,
            30703,
        };

        public override HashSet<uint> ItemIds => new()
        {
            2000471,
            2000472,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 30694,
            },
            new NodeInfo
            {
                NodeId = 30695,
            },
            new NodeInfo
            {
                NodeId = 30696,
            },
            new NodeInfo
            {
                NodeId = 30697,
            },
            new NodeInfo
            {
                NodeId = 30698,
            },
            new NodeInfo
            {
                NodeId = 30699,
            },
            new NodeInfo
            {
                NodeId = 30700,
            },
            new NodeInfo
            {
                NodeId = 30701,
            },
            new NodeInfo
            {
                NodeId = 30702,
            },
            new NodeInfo
            {
                NodeId = 30703,
            },
        };
    }
}
