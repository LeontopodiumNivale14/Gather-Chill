using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.ARR.Unknown_0
{
    public class MIN_1046 : RouteInfo
    {
        public override uint Id => 1046;
        public override uint ExpansionId => 0;
        public override uint ZoneId => 0;
        public override uint GatherType => 0;
        public override Vector2 MapPosition => new Vector2(-456.175f, -730.489f);
        public override int Radius => 59;

        public override HashSet<uint> NodeIds => new()
        {
            35049,
            35050,
            35051,
            35052,
            35053,
            35054,
            35055,
            35056,
        };

        public override HashSet<uint> ItemIds => new()
        {
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 35049,
            },
            new NodeInfo
            {
                NodeId = 35050,
            },
            new NodeInfo
            {
                NodeId = 35051,
            },
            new NodeInfo
            {
                NodeId = 35052,
            },
            new NodeInfo
            {
                NodeId = 35053,
            },
            new NodeInfo
            {
                NodeId = 35054,
            },
            new NodeInfo
            {
                NodeId = 35055,
            },
            new NodeInfo
            {
                NodeId = 35056,
            },
        };
    }
}
