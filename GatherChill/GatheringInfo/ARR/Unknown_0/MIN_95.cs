using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.ARR.Unknown_0
{
    public class MIN_95 : RouteInfo
    {
        public override uint Id => 95;
        public override uint ExpansionId => 0;
        public override uint ZoneId => 0;
        public override uint GatherType => 0;
        public override Vector2 MapPosition => new Vector2(-17.5169f, 185.82f);
        public override int Radius => 14;

        public override HashSet<uint> NodeIds => new()
        {
            30704,
            30705,
            30706,
            30707,
        };

        public override HashSet<uint> ItemIds => new()
        {
            2000473,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 30704,
            },
            new NodeInfo
            {
                NodeId = 30705,
            },
            new NodeInfo
            {
                NodeId = 30706,
            },
            new NodeInfo
            {
                NodeId = 30707,
            },
        };
    }
}
