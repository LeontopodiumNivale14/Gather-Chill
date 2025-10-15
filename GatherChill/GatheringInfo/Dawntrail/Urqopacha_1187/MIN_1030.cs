using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.Dawntrail.Urqopacha_1187
{
    public class MIN_1030 : RouteInfo
    {
        public override uint Id => 1030;
        public override uint ExpansionId => 5;
        public override uint ZoneId => 1187;
        public override uint GatherType => 1;
        public override Vector2 MapPosition => new Vector2(778.438f, 404.459f);
        public override int Radius => 18;

        public override HashSet<uint> NodeIds => new()
        {
            34990,
        };

        public override HashSet<uint> ItemIds => new()
        {
            44139,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 34990,
            },
        };
    }
}
