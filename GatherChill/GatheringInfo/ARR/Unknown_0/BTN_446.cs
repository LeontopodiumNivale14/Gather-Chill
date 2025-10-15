using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.ARR.Unknown_0
{
    public class BTN_446 : RouteInfo
    {
        public override uint Id => 446;
        public override uint ExpansionId => 0;
        public override uint ZoneId => 0;
        public override uint GatherType => 3;
        public override Vector2 MapPosition => new Vector2(-721.487f, 210.922f);
        public override int Radius => 54;

        public override HashSet<uint> NodeIds => new()
        {
            31929,
            31930,
            31931,
            31932,
            31933,
            31934,
            31935,
            31936,
            31937,
            31938,
            31939,
            31940,
        };

        public override HashSet<uint> ItemIds => new()
        {
            2002140,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 31929,
            },
            new NodeInfo
            {
                NodeId = 31930,
            },
            new NodeInfo
            {
                NodeId = 31931,
            },
            new NodeInfo
            {
                NodeId = 31932,
            },
            new NodeInfo
            {
                NodeId = 31933,
            },
            new NodeInfo
            {
                NodeId = 31934,
            },
            new NodeInfo
            {
                NodeId = 31935,
            },
            new NodeInfo
            {
                NodeId = 31936,
            },
            new NodeInfo
            {
                NodeId = 31937,
            },
            new NodeInfo
            {
                NodeId = 31938,
            },
            new NodeInfo
            {
                NodeId = 31939,
            },
            new NodeInfo
            {
                NodeId = 31940,
            },
        };
    }
}
