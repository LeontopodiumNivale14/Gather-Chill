using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.ARR.Unknown_0
{
    public class MIN_460 : RouteInfo
    {
        public override uint Id => 460;
        public override uint ExpansionId => 0;
        public override uint ZoneId => 0;
        public override uint GatherType => 0;
        public override Vector2 MapPosition => new Vector2(-656.627f, 626.362f);
        public override int Radius => 23;

        public override HashSet<uint> NodeIds => new()
        {
            32041,
            32042,
            32043,
            32044,
            32045,
            32046,
            32047,
            32048,
            32049,
            32050,
            32051,
            32052,
        };

        public override HashSet<uint> ItemIds => new()
        {
            2002161,
            2002162,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 32041,
            },
            new NodeInfo
            {
                NodeId = 32042,
            },
            new NodeInfo
            {
                NodeId = 32043,
            },
            new NodeInfo
            {
                NodeId = 32044,
            },
            new NodeInfo
            {
                NodeId = 32045,
            },
            new NodeInfo
            {
                NodeId = 32046,
            },
            new NodeInfo
            {
                NodeId = 32047,
            },
            new NodeInfo
            {
                NodeId = 32048,
            },
            new NodeInfo
            {
                NodeId = 32049,
            },
            new NodeInfo
            {
                NodeId = 32050,
            },
            new NodeInfo
            {
                NodeId = 32051,
            },
            new NodeInfo
            {
                NodeId = 32052,
            },
        };
    }
}
