using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.ARR.Unknown_0
{
    public class BTN_434 : RouteInfo
    {
        public override uint Id => 434;
        public override uint ExpansionId => 0;
        public override uint ZoneId => 0;
        public override uint GatherType => 2;
        public override Vector2 MapPosition => new Vector2(-625.549f, 263.613f);
        public override int Radius => 16;

        public override HashSet<uint> NodeIds => new()
        {
            31835,
            31836,
            31837,
            31838,
        };

        public override HashSet<uint> ItemIds => new()
        {
            2002122,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 31835,
            },
            new NodeInfo
            {
                NodeId = 31836,
            },
            new NodeInfo
            {
                NodeId = 31837,
            },
            new NodeInfo
            {
                NodeId = 31838,
            },
        };
    }
}
