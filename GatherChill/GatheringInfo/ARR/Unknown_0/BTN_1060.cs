using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.ARR.Unknown_0
{
    public class BTN_1060 : RouteInfo
    {
        public override uint Id => 1060;
        public override uint ExpansionId => 0;
        public override uint ZoneId => 0;
        public override uint GatherType => 3;
        public override Vector2 MapPosition => new Vector2(230.088f, -37.5933f);
        public override int Radius => 45;

        public override HashSet<uint> NodeIds => new()
        {
            35135,
            35136,
            35137,
            35138,
            35139,
            35140,
            35141,
            35142,
        };

        public override HashSet<uint> ItemIds => new()
        {
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 35135,
            },
            new NodeInfo
            {
                NodeId = 35136,
            },
            new NodeInfo
            {
                NodeId = 35137,
            },
            new NodeInfo
            {
                NodeId = 35138,
            },
            new NodeInfo
            {
                NodeId = 35139,
            },
            new NodeInfo
            {
                NodeId = 35140,
            },
            new NodeInfo
            {
                NodeId = 35141,
            },
            new NodeInfo
            {
                NodeId = 35142,
            },
        };
    }
}
