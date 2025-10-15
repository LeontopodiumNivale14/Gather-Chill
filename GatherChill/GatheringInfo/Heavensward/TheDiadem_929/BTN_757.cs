using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.Heavensward.TheDiadem_929
{
    public class BTN_757 : RouteInfo
    {
        public override uint Id => 757;
        public override uint ExpansionId => 1;
        public override uint ZoneId => 929;
        public override uint GatherType => 3;
        public override Vector2 MapPosition => new Vector2(397.876f, 580.866f);
        public override int Radius => 11;

        public override HashSet<uint> NodeIds => new()
        {
            33587,
        };

        public override HashSet<uint> ItemIds => new()
        {
            29945,
            31317,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 33587,
            },
        };
    }
}
