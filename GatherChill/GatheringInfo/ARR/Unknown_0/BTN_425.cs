using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.ARR.Unknown_0
{
    public class BTN_425 : RouteInfo
    {
        public override uint Id => 425;
        public override uint ExpansionId => 0;
        public override uint ZoneId => 0;
        public override uint GatherType => 2;
        public override Vector2 MapPosition => new Vector2(310.328f, -303.074f);
        public override int Radius => 117;

        public override HashSet<uint> NodeIds => new()
        {
            31791,
            31792,
            31793,
        };

        public override HashSet<uint> ItemIds => new()
        {
            5,
            11,
            4816,
            5395,
            13752,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 31791,
            },
            new NodeInfo
            {
                NodeId = 31792,
            },
            new NodeInfo
            {
                NodeId = 31793,
            },
        };
    }
}
