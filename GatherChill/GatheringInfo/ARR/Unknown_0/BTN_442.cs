using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.ARR.Unknown_0
{
    public class BTN_442 : RouteInfo
    {
        public override uint Id => 442;
        public override uint ExpansionId => 0;
        public override uint ZoneId => 0;
        public override uint GatherType => 2;
        public override Vector2 MapPosition => new Vector2(67.3529f, 528.569f);
        public override int Radius => 132;

        public override HashSet<uint> NodeIds => new()
        {
            31895,
            31896,
            31897,
            31898,
            31899,
            31900,
            31901,
            31902,
        };

        public override HashSet<uint> ItemIds => new()
        {
            2002134,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 31895,
            },
            new NodeInfo
            {
                NodeId = 31896,
            },
            new NodeInfo
            {
                NodeId = 31897,
            },
            new NodeInfo
            {
                NodeId = 31898,
            },
            new NodeInfo
            {
                NodeId = 31899,
            },
            new NodeInfo
            {
                NodeId = 31900,
            },
            new NodeInfo
            {
                NodeId = 31901,
            },
            new NodeInfo
            {
                NodeId = 31902,
            },
        };
    }
}
