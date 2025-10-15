using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.ARR.Unknown_0
{
    public class BTN_1205 : RouteInfo
    {
        public override uint Id => 1205;
        public override uint ExpansionId => 0;
        public override uint ZoneId => 0;
        public override uint GatherType => 3;
        public override Vector2 MapPosition => new Vector2(-577.276f, -402.919f);
        public override int Radius => 4;

        public override HashSet<uint> NodeIds => new()
        {
            35276,
            35277,
            35278,
        };

        public override HashSet<uint> ItemIds => new()
        {
            13,
            48022,
            48049,
            48076,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 35276,
            },
            new NodeInfo
            {
                NodeId = 35277,
            },
            new NodeInfo
            {
                NodeId = 35278,
            },
        };
    }
}
