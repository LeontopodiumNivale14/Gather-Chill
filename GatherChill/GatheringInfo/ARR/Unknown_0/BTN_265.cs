using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.ARR.Unknown_0
{
    public class BTN_265 : RouteInfo
    {
        public override uint Id => 265;
        public override uint ExpansionId => 0;
        public override uint ZoneId => 0;
        public override uint GatherType => 2;
        public override Vector2 MapPosition => new Vector2(156.223f, -78.6203f);
        public override int Radius => 70;

        public override HashSet<uint> NodeIds => new()
        {
            31155,
            31156,
            31157,
            31158,
            31159,
            31160,
            31161,
            31162,
            31163,
            31164,
            31165,
            31166,
        };

        public override HashSet<uint> ItemIds => new()
        {
            2001863,
            2001864,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 31155,
            },
            new NodeInfo
            {
                NodeId = 31156,
            },
            new NodeInfo
            {
                NodeId = 31157,
            },
            new NodeInfo
            {
                NodeId = 31158,
            },
            new NodeInfo
            {
                NodeId = 31159,
            },
            new NodeInfo
            {
                NodeId = 31160,
            },
            new NodeInfo
            {
                NodeId = 31161,
            },
            new NodeInfo
            {
                NodeId = 31162,
            },
            new NodeInfo
            {
                NodeId = 31163,
            },
            new NodeInfo
            {
                NodeId = 31164,
            },
            new NodeInfo
            {
                NodeId = 31165,
            },
            new NodeInfo
            {
                NodeId = 31166,
            },
        };
    }
}
