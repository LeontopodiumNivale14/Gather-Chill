using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.ARR.Unknown_0
{
    public class MIN_1290 : RouteInfo
    {
        public override uint Id => 1290;
        public override uint ExpansionId => 0;
        public override uint ZoneId => 0;
        public override uint GatherType => 1;
        public override Vector2 MapPosition => new Vector2(-479.18f, -530.402f);
        public override int Radius => 25;

        public override HashSet<uint> NodeIds => new()
        {
            35351,
            35352,
            35353,
            35354,
            35355,
            35356,
        };

        public override HashSet<uint> ItemIds => new()
        {
            47416,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 35351,
            },
            new NodeInfo
            {
                NodeId = 35352,
            },
            new NodeInfo
            {
                NodeId = 35353,
            },
            new NodeInfo
            {
                NodeId = 35354,
            },
            new NodeInfo
            {
                NodeId = 35355,
            },
            new NodeInfo
            {
                NodeId = 35356,
            },
        };
    }
}
