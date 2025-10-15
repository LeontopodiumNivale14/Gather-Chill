using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.ARR.Unknown_0
{
    public class MIN_449 : RouteInfo
    {
        public override uint Id => 449;
        public override uint ExpansionId => 0;
        public override uint ZoneId => 0;
        public override uint GatherType => 0;
        public override Vector2 MapPosition => new Vector2(114.318f, -632.192f);
        public override int Radius => 15;

        public override HashSet<uint> NodeIds => new()
        {
            31959,
            31960,
            31961,
            31962,
        };

        public override HashSet<uint> ItemIds => new()
        {
            2002145,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 31959,
            },
            new NodeInfo
            {
                NodeId = 31960,
            },
            new NodeInfo
            {
                NodeId = 31961,
            },
            new NodeInfo
            {
                NodeId = 31962,
            },
        };
    }
}
