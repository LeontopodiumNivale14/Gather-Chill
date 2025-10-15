using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.ARR.Unknown_0
{
    public class BTN_572 : RouteInfo
    {
        public override uint Id => 572;
        public override uint ExpansionId => 0;
        public override uint ZoneId => 0;
        public override uint GatherType => 3;
        public override Vector2 MapPosition => new Vector2(497.901f, 715.161f);
        public override int Radius => 68;

        public override HashSet<uint> NodeIds => new()
        {
            32478,
            32479,
            32480,
            32481,
            32482,
            32483,
            32484,
            32485,
            32486,
            32487,
            32488,
            32489,
        };

        public override HashSet<uint> ItemIds => new()
        {
            2002779,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 32478,
            },
            new NodeInfo
            {
                NodeId = 32479,
            },
            new NodeInfo
            {
                NodeId = 32480,
            },
            new NodeInfo
            {
                NodeId = 32481,
            },
            new NodeInfo
            {
                NodeId = 32482,
            },
            new NodeInfo
            {
                NodeId = 32483,
            },
            new NodeInfo
            {
                NodeId = 32484,
            },
            new NodeInfo
            {
                NodeId = 32485,
            },
            new NodeInfo
            {
                NodeId = 32486,
            },
            new NodeInfo
            {
                NodeId = 32487,
            },
            new NodeInfo
            {
                NodeId = 32488,
            },
            new NodeInfo
            {
                NodeId = 32489,
            },
        };
    }
}
