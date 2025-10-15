using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.Dawntrail.YakTel_1189
{
    public class BTN_1013 : RouteInfo
    {
        public override uint Id => 1013;
        public override uint ExpansionId => 5;
        public override uint ZoneId => 1189;
        public override uint GatherType => 5;
        public override Vector2 MapPosition => new Vector2(510.266f, -720.898f);
        public override int Radius => 1;

        public override HashSet<uint> NodeIds => new()
        {
            34957,
        };

        public override HashSet<uint> ItemIds => new()
        {
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 34957,
            },
        };
    }
}
