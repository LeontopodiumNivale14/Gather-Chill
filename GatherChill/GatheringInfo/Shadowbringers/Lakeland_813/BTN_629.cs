using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.Shadowbringers.Lakeland_813
{
    public class BTN_629 : RouteInfo
    {
        public override uint Id => 629;
        public override uint ExpansionId => 3;
        public override uint ZoneId => 813;
        public override uint GatherType => 3;
        public override Vector2 MapPosition => new Vector2(239.406f, -41.1346f);
        public override int Radius => 21;

        public override HashSet<uint> NodeIds => new()
        {
            32768,
        };

        public override HashSet<uint> ItemIds => new()
        {
            27836,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 32768,
            },
        };
    }
}
