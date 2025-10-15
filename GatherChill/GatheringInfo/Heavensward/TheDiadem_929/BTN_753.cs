using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.Heavensward.TheDiadem_929
{
    public class BTN_753 : RouteInfo
    {
        public override uint Id => 753;
        public override uint ExpansionId => 1;
        public override uint ZoneId => 929;
        public override uint GatherType => 2;
        public override Vector2 MapPosition => new Vector2(-306.71f, 387.597f);
        public override int Radius => 596;

        public override HashSet<uint> NodeIds => new()
        {
            33535,
            33578,
        };

        public override HashSet<uint> ItemIds => new()
        {
            29935,
            31280,
            31286,
            31296,
            31307,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 33535,
            },
            new NodeInfo
            {
                NodeId = 33578,
            },
        };
    }
}
