using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.Heavensward.TheDiadem_901
{
    public class MIN_710 : RouteInfo
    {
        public override uint Id => 710;
        public override uint ExpansionId => 1;
        public override uint ZoneId => 901;
        public override uint GatherType => 1;
        public override Vector2 MapPosition => new Vector2(625.294f, -404.155f);
        public override int Radius => 12;

        public override HashSet<uint> NodeIds => new()
        {
            33230,
        };

        public override HashSet<uint> ItemIds => new()
        {
            29947,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 33230,
            },
        };
    }
}
