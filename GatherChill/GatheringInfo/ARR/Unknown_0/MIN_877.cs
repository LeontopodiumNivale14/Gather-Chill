using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.ARR.Unknown_0
{
    public class MIN_877 : RouteInfo
    {
        public override uint Id => 877;
        public override uint ExpansionId => 0;
        public override uint ZoneId => 0;
        public override uint GatherType => 0;
        public override Vector2 MapPosition => new Vector2(42.3457f, -680.84f);
        public override int Radius => 42;

        public override HashSet<uint> NodeIds => new()
        {
            34200,
            34201,
            34202,
            34203,
            34204,
            34205,
            34206,
            34207,
            34208,
            34209,
            34210,
            34211,
        };

        public override HashSet<uint> ItemIds => new()
        {
            2003159,
            2003160,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 34200,
            },
            new NodeInfo
            {
                NodeId = 34201,
            },
            new NodeInfo
            {
                NodeId = 34202,
            },
            new NodeInfo
            {
                NodeId = 34203,
            },
            new NodeInfo
            {
                NodeId = 34204,
            },
            new NodeInfo
            {
                NodeId = 34205,
            },
            new NodeInfo
            {
                NodeId = 34206,
            },
            new NodeInfo
            {
                NodeId = 34207,
            },
            new NodeInfo
            {
                NodeId = 34208,
            },
            new NodeInfo
            {
                NodeId = 34209,
            },
            new NodeInfo
            {
                NodeId = 34210,
            },
            new NodeInfo
            {
                NodeId = 34211,
            },
        };
    }
}
