using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.ARR.Unknown_0
{
    public class BTN_56 : RouteInfo
    {
        public override uint Id => 56;
        public override uint ExpansionId => 0;
        public override uint ZoneId => 0;
        public override uint GatherType => 2;
        public override Vector2 MapPosition => new Vector2(355.525f, -98.9009f);
        public override int Radius => 15;

        public override HashSet<uint> NodeIds => new()
        {
            30190,
            30191,
            30192,
            30193,
            30194,
            30195,
            30196,
            30197,
            30198,
            30199,
        };

        public override HashSet<uint> ItemIds => new()
        {
            2000278,
            2000280,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 30190,
            },
            new NodeInfo
            {
                NodeId = 30191,
            },
            new NodeInfo
            {
                NodeId = 30192,
            },
            new NodeInfo
            {
                NodeId = 30193,
            },
            new NodeInfo
            {
                NodeId = 30194,
            },
            new NodeInfo
            {
                NodeId = 30195,
            },
            new NodeInfo
            {
                NodeId = 30196,
            },
            new NodeInfo
            {
                NodeId = 30197,
            },
            new NodeInfo
            {
                NodeId = 30198,
            },
            new NodeInfo
            {
                NodeId = 30199,
            },
        };
    }
}
