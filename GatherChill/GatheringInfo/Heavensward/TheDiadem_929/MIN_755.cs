using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.Heavensward.TheDiadem_929
{
    public class MIN_755 : RouteInfo
    {
        public override uint Id => 755;
        public override uint ExpansionId => 1;
        public override uint ZoneId => 929;
        public override uint GatherType => 1;
        public override Vector2 MapPosition => new Vector2(627.529f, -407.03f);
        public override int Radius => 11;

        public override HashSet<uint> NodeIds => new()
        {
            33585,
        };

        public override HashSet<uint> ItemIds => new()
        {
            29947,
            31319,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 33585,
            },
        };
    }
}
