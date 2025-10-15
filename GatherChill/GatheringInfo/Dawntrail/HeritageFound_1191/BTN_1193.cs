using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.Dawntrail.HeritageFound_1191
{
    public class BTN_1193 : RouteInfo
    {
        public override uint Id => 1193;
        public override uint ExpansionId => 5;
        public override uint ZoneId => 1191;
        public override uint GatherType => 2;
        public override Vector2 MapPosition => new Vector2(636.682f, -41.9671f);
        public override int Radius => 1;

        public override HashSet<uint> NodeIds => new()
        {
            35242,
        };

        public override HashSet<uint> ItemIds => new()
        {
            45972,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 35242,
            },
        };
    }
}
