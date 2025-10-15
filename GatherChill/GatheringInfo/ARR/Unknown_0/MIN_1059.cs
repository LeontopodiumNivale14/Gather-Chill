using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.ARR.Unknown_0
{
    public class MIN_1059 : RouteInfo
    {
        public override uint Id => 1059;
        public override uint ExpansionId => 0;
        public override uint ZoneId => 0;
        public override uint GatherType => 0;
        public override Vector2 MapPosition => new Vector2(-269.249f, 138.282f);
        public override int Radius => 61;

        public override HashSet<uint> NodeIds => new()
        {
            35129,
            35130,
            35131,
            35132,
            35133,
            35134,
        };

        public override HashSet<uint> ItemIds => new()
        {
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 35129,
            },
            new NodeInfo
            {
                NodeId = 35130,
            },
            new NodeInfo
            {
                NodeId = 35131,
            },
            new NodeInfo
            {
                NodeId = 35132,
            },
            new NodeInfo
            {
                NodeId = 35133,
            },
            new NodeInfo
            {
                NodeId = 35134,
            },
        };
    }
}
