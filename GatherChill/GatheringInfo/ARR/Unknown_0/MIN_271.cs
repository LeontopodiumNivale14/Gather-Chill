using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.ARR.Unknown_0
{
    public class MIN_271 : RouteInfo
    {
        public override uint Id => 271;
        public override uint ExpansionId => 0;
        public override uint ZoneId => 0;
        public override uint GatherType => 0;
        public override Vector2 MapPosition => new Vector2(678.704f, -8.46797f);
        public override int Radius => 33;

        public override HashSet<uint> NodeIds => new()
        {
            31207,
            31208,
            31209,
            31210,
            31211,
            31212,
            31213,
            31214,
            31215,
            31216,
            31217,
            31218,
        };

        public override HashSet<uint> ItemIds => new()
        {
            2001827,
            2001828,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 31207,
            },
            new NodeInfo
            {
                NodeId = 31208,
            },
            new NodeInfo
            {
                NodeId = 31209,
            },
            new NodeInfo
            {
                NodeId = 31210,
            },
            new NodeInfo
            {
                NodeId = 31211,
            },
            new NodeInfo
            {
                NodeId = 31212,
            },
            new NodeInfo
            {
                NodeId = 31213,
            },
            new NodeInfo
            {
                NodeId = 31214,
            },
            new NodeInfo
            {
                NodeId = 31215,
            },
            new NodeInfo
            {
                NodeId = 31216,
            },
            new NodeInfo
            {
                NodeId = 31217,
            },
            new NodeInfo
            {
                NodeId = 31218,
            },
        };
    }
}
