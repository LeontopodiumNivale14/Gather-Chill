using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.ARR.Unknown_0
{
    public class MIN_277 : RouteInfo
    {
        public override uint Id => 277;
        public override uint ExpansionId => 0;
        public override uint ZoneId => 0;
        public override uint GatherType => 0;
        public override Vector2 MapPosition => new Vector2(-267.071f, 517.823f);
        public override int Radius => 75;

        public override HashSet<uint> NodeIds => new()
        {
            31257,
            31258,
            31259,
            31260,
            31261,
            31262,
            31263,
            31264,
            31265,
            31266,
            31267,
            31268,
        };

        public override HashSet<uint> ItemIds => new()
        {
            2001836,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 31257,
            },
            new NodeInfo
            {
                NodeId = 31258,
            },
            new NodeInfo
            {
                NodeId = 31259,
            },
            new NodeInfo
            {
                NodeId = 31260,
            },
            new NodeInfo
            {
                NodeId = 31261,
            },
            new NodeInfo
            {
                NodeId = 31262,
            },
            new NodeInfo
            {
                NodeId = 31263,
            },
            new NodeInfo
            {
                NodeId = 31264,
            },
            new NodeInfo
            {
                NodeId = 31265,
            },
            new NodeInfo
            {
                NodeId = 31266,
            },
            new NodeInfo
            {
                NodeId = 31267,
            },
            new NodeInfo
            {
                NodeId = 31268,
            },
        };
    }
}
