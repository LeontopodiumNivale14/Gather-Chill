using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.ARR.Unknown_0
{
    public class BTN_1264 : RouteInfo
    {
        public override uint Id => 1264;
        public override uint ExpansionId => 0;
        public override uint ZoneId => 0;
        public override uint GatherType => 2;
        public override Vector2 MapPosition => new Vector2(299.163f, -22.301f);
        public override int Radius => 58;

        public override HashSet<uint> NodeIds => new()
        {
            35401,
            35402,
            35403,
            35404,
            35405,
            35406,
            35407,
            35408,
        };

        public override HashSet<uint> ItemIds => new()
        {
            47392,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 35401,
            },
            new NodeInfo
            {
                NodeId = 35402,
            },
            new NodeInfo
            {
                NodeId = 35403,
            },
            new NodeInfo
            {
                NodeId = 35404,
            },
            new NodeInfo
            {
                NodeId = 35405,
            },
            new NodeInfo
            {
                NodeId = 35406,
            },
            new NodeInfo
            {
                NodeId = 35407,
            },
            new NodeInfo
            {
                NodeId = 35408,
            },
        };
    }
}
