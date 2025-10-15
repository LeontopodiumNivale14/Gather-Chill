using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.ARR.Unknown_0
{
    public class BTN_1203 : RouteInfo
    {
        public override uint Id => 1203;
        public override uint ExpansionId => 0;
        public override uint ZoneId => 0;
        public override uint GatherType => 2;
        public override Vector2 MapPosition => new Vector2(718.285f, -311.478f);
        public override int Radius => 25;

        public override HashSet<uint> NodeIds => new()
        {
            35270,
            35271,
            35272,
        };

        public override HashSet<uint> ItemIds => new()
        {
            13,
            48010,
            48037,
            48052,
            48070,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 35270,
            },
            new NodeInfo
            {
                NodeId = 35271,
            },
            new NodeInfo
            {
                NodeId = 35272,
            },
        };
    }
}
