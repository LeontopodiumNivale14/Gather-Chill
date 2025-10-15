using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.ARR.Unknown_0
{
    public class BTN_443 : RouteInfo
    {
        public override uint Id => 443;
        public override uint ExpansionId => 0;
        public override uint ZoneId => 0;
        public override uint GatherType => 3;
        public override Vector2 MapPosition => new Vector2(518.086f, 393.651f);
        public override int Radius => 21;

        public override HashSet<uint> NodeIds => new()
        {
            31907,
            31908,
            31909,
            31910,
        };

        public override HashSet<uint> ItemIds => new()
        {
            2002135,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 31907,
            },
            new NodeInfo
            {
                NodeId = 31908,
            },
            new NodeInfo
            {
                NodeId = 31909,
            },
            new NodeInfo
            {
                NodeId = 31910,
            },
        };
    }
}
