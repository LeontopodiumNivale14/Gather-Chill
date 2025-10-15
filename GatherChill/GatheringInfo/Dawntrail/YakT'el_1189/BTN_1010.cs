using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.Dawntrail.YakTel_1189
{
    public class BTN_1010 : RouteInfo
    {
        public override uint Id => 1010;
        public override uint ExpansionId => 5;
        public override uint ZoneId => 1189;
        public override uint GatherType => 5;
        public override Vector2 MapPosition => new Vector2(674.578f, -675.531f);
        public override int Radius => 126;

        public override HashSet<uint> NodeIds => new()
        {
            34950,
            34951,
            34952,
        };

        public override HashSet<uint> ItemIds => new()
        {
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 34950,
            },
            new NodeInfo
            {
                NodeId = 34951,
            },
            new NodeInfo
            {
                NodeId = 34952,
            },
        };
    }
}
