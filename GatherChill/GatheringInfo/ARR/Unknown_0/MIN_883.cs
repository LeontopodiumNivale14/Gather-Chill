using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.ARR.Unknown_0
{
    public class MIN_883 : RouteInfo
    {
        public override uint Id => 883;
        public override uint ExpansionId => 0;
        public override uint ZoneId => 0;
        public override uint GatherType => 0;
        public override Vector2 MapPosition => new Vector2(40.8555f, -133.648f);
        public override int Radius => 41;

        public override HashSet<uint> NodeIds => new()
        {
            34258,
            34259,
            34260,
            34261,
            34262,
            34263,
            34264,
            34265,
            34266,
            34267,
            34268,
            34269,
        };

        public override HashSet<uint> ItemIds => new()
        {
            2003168,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 34258,
            },
            new NodeInfo
            {
                NodeId = 34259,
            },
            new NodeInfo
            {
                NodeId = 34260,
            },
            new NodeInfo
            {
                NodeId = 34261,
            },
            new NodeInfo
            {
                NodeId = 34262,
            },
            new NodeInfo
            {
                NodeId = 34263,
            },
            new NodeInfo
            {
                NodeId = 34264,
            },
            new NodeInfo
            {
                NodeId = 34265,
            },
            new NodeInfo
            {
                NodeId = 34266,
            },
            new NodeInfo
            {
                NodeId = 34267,
            },
            new NodeInfo
            {
                NodeId = 34268,
            },
            new NodeInfo
            {
                NodeId = 34269,
            },
        };
    }
}
