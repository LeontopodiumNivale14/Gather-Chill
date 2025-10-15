using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.ARR.Unknown_0
{
    public class BTN_377 : RouteInfo
    {
        public override uint Id => 377;
        public override uint ExpansionId => 0;
        public override uint ZoneId => 0;
        public override uint GatherType => 3;
        public override Vector2 MapPosition => new Vector2(-296.279f, -412.941f);
        public override int Radius => 292;

        public override HashSet<uint> NodeIds => new()
        {
            31543,
            31545,
            31575,
            31577,
            31607,
            31609,
        };

        public override HashSet<uint> ItemIds => new()
        {
            12,
            12878,
            12879,
            13753,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 31543,
            },
            new NodeInfo
            {
                NodeId = 31545,
            },
            new NodeInfo
            {
                NodeId = 31575,
            },
            new NodeInfo
            {
                NodeId = 31577,
            },
            new NodeInfo
            {
                NodeId = 31607,
            },
            new NodeInfo
            {
                NodeId = 31609,
            },
        };
    }
}
