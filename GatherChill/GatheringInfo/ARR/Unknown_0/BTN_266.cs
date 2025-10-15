using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.ARR.Unknown_0
{
    public class BTN_266 : RouteInfo
    {
        public override uint Id => 266;
        public override uint ExpansionId => 0;
        public override uint ZoneId => 0;
        public override uint GatherType => 3;
        public override Vector2 MapPosition => new Vector2(200.893f, 38.3271f);
        public override int Radius => 68;

        public override HashSet<uint> NodeIds => new()
        {
            31167,
            31168,
            31169,
            31170,
            31171,
            31172,
            31173,
            31174,
            31175,
            31176,
            31177,
            31178,
        };

        public override HashSet<uint> ItemIds => new()
        {
            2001865,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 31167,
            },
            new NodeInfo
            {
                NodeId = 31168,
            },
            new NodeInfo
            {
                NodeId = 31169,
            },
            new NodeInfo
            {
                NodeId = 31170,
            },
            new NodeInfo
            {
                NodeId = 31171,
            },
            new NodeInfo
            {
                NodeId = 31172,
            },
            new NodeInfo
            {
                NodeId = 31173,
            },
            new NodeInfo
            {
                NodeId = 31174,
            },
            new NodeInfo
            {
                NodeId = 31175,
            },
            new NodeInfo
            {
                NodeId = 31176,
            },
            new NodeInfo
            {
                NodeId = 31177,
            },
            new NodeInfo
            {
                NodeId = 31178,
            },
        };
    }
}
