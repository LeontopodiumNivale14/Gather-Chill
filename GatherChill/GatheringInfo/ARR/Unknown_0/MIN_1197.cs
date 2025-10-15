using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.ARR.Unknown_0
{
    public class MIN_1197 : RouteInfo
    {
        public override uint Id => 1197;
        public override uint ExpansionId => 0;
        public override uint ZoneId => 0;
        public override uint GatherType => 1;
        public override Vector2 MapPosition => new Vector2(653.206f, -225.232f);
        public override int Radius => 4;

        public override HashSet<uint> NodeIds => new()
        {
            35252,
            35253,
            35254,
        };

        public override HashSet<uint> ItemIds => new()
        {
            9,
            48009,
            48036,
            48051,
            48069,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 35252,
            },
            new NodeInfo
            {
                NodeId = 35253,
            },
            new NodeInfo
            {
                NodeId = 35254,
            },
        };
    }
}
