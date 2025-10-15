using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.ARR.Unknown_0
{
    public class MIN_878 : RouteInfo
    {
        public override uint Id => 878;
        public override uint ExpansionId => 0;
        public override uint ZoneId => 0;
        public override uint GatherType => 1;
        public override Vector2 MapPosition => new Vector2(-351.494f, 373.589f);
        public override int Radius => 39;

        public override HashSet<uint> NodeIds => new()
        {
            34212,
            34213,
            34214,
            34215,
            34216,
            34217,
            34218,
            34219,
            34220,
            34221,
            34222,
            34223,
        };

        public override HashSet<uint> ItemIds => new()
        {
            2003161,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 34212,
            },
            new NodeInfo
            {
                NodeId = 34213,
            },
            new NodeInfo
            {
                NodeId = 34214,
            },
            new NodeInfo
            {
                NodeId = 34215,
            },
            new NodeInfo
            {
                NodeId = 34216,
            },
            new NodeInfo
            {
                NodeId = 34217,
            },
            new NodeInfo
            {
                NodeId = 34218,
            },
            new NodeInfo
            {
                NodeId = 34219,
            },
            new NodeInfo
            {
                NodeId = 34220,
            },
            new NodeInfo
            {
                NodeId = 34221,
            },
            new NodeInfo
            {
                NodeId = 34222,
            },
            new NodeInfo
            {
                NodeId = 34223,
            },
        };
    }
}
