using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.Heavensward.TheDiadem_929
{
    public class MIN_745 : RouteInfo
    {
        public override uint Id => 745;
        public override uint ExpansionId => 1;
        public override uint ZoneId => 929;
        public override uint GatherType => 1;
        public override Vector2 MapPosition => new Vector2(73.8971f, 1.43788f);
        public override int Radius => 789;

        public override HashSet<uint> NodeIds => new()
        {
            33395,
            33402,
            33407,
            33409,
            33414,
            33416,
            33421,
            33428,
            33435,
            33441,
            33446,
            33448,
            33453,
            33460,
            33467,
            33474,
            33479,
            33481,
            33486,
        };

        public override HashSet<uint> ItemIds => new()
        {
            29942,
            31279,
            31294,
            31304,
            31314,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 33395,
            },
            new NodeInfo
            {
                NodeId = 33402,
            },
            new NodeInfo
            {
                NodeId = 33407,
            },
            new NodeInfo
            {
                NodeId = 33409,
            },
            new NodeInfo
            {
                NodeId = 33414,
            },
            new NodeInfo
            {
                NodeId = 33416,
            },
            new NodeInfo
            {
                NodeId = 33421,
            },
            new NodeInfo
            {
                NodeId = 33428,
            },
            new NodeInfo
            {
                NodeId = 33435,
            },
            new NodeInfo
            {
                NodeId = 33441,
            },
            new NodeInfo
            {
                NodeId = 33446,
            },
            new NodeInfo
            {
                NodeId = 33448,
            },
            new NodeInfo
            {
                NodeId = 33453,
            },
            new NodeInfo
            {
                NodeId = 33460,
            },
            new NodeInfo
            {
                NodeId = 33467,
            },
            new NodeInfo
            {
                NodeId = 33474,
            },
            new NodeInfo
            {
                NodeId = 33479,
            },
            new NodeInfo
            {
                NodeId = 33481,
            },
            new NodeInfo
            {
                NodeId = 33486,
            },
        };
    }
}
