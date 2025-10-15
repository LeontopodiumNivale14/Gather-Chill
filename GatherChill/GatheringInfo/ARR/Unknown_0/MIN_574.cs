using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.ARR.Unknown_0
{
    public class MIN_574 : RouteInfo
    {
        public override uint Id => 574;
        public override uint ExpansionId => 0;
        public override uint ZoneId => 0;
        public override uint GatherType => 0;
        public override Vector2 MapPosition => new Vector2(279.009f, 359.578f);
        public override int Radius => 73;

        public override HashSet<uint> NodeIds => new()
        {
            32496,
            32497,
            32498,
            32499,
            32500,
            32501,
            32502,
            32503,
            32504,
            32505,
            32506,
            32507,
        };

        public override HashSet<uint> ItemIds => new()
        {
            2002782,
            2002783,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 32496,
            },
            new NodeInfo
            {
                NodeId = 32497,
            },
            new NodeInfo
            {
                NodeId = 32498,
            },
            new NodeInfo
            {
                NodeId = 32499,
            },
            new NodeInfo
            {
                NodeId = 32500,
            },
            new NodeInfo
            {
                NodeId = 32501,
            },
            new NodeInfo
            {
                NodeId = 32502,
            },
            new NodeInfo
            {
                NodeId = 32503,
            },
            new NodeInfo
            {
                NodeId = 32504,
            },
            new NodeInfo
            {
                NodeId = 32505,
            },
            new NodeInfo
            {
                NodeId = 32506,
            },
            new NodeInfo
            {
                NodeId = 32507,
            },
        };
    }
}
