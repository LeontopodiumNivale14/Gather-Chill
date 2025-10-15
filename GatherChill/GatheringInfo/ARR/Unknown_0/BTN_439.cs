using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.ARR.Unknown_0
{
    public class BTN_439 : RouteInfo
    {
        public override uint Id => 439;
        public override uint ExpansionId => 0;
        public override uint ZoneId => 0;
        public override uint GatherType => 2;
        public override Vector2 MapPosition => new Vector2(413.04f, 240.59f);
        public override int Radius => 122;

        public override HashSet<uint> NodeIds => new()
        {
            31873,
            31874,
            31875,
            31876,
            31877,
            31878,
            31879,
            31880,
            31881,
            31882,
            31883,
            31884,
        };

        public override HashSet<uint> ItemIds => new()
        {
            2002129,
            2002130,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 31873,
            },
            new NodeInfo
            {
                NodeId = 31874,
            },
            new NodeInfo
            {
                NodeId = 31875,
            },
            new NodeInfo
            {
                NodeId = 31876,
            },
            new NodeInfo
            {
                NodeId = 31877,
            },
            new NodeInfo
            {
                NodeId = 31878,
            },
            new NodeInfo
            {
                NodeId = 31879,
            },
            new NodeInfo
            {
                NodeId = 31880,
            },
            new NodeInfo
            {
                NodeId = 31881,
            },
            new NodeInfo
            {
                NodeId = 31882,
            },
            new NodeInfo
            {
                NodeId = 31883,
            },
            new NodeInfo
            {
                NodeId = 31884,
            },
        };
    }
}
