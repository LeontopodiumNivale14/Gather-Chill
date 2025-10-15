using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.Shadowbringers.TheRaktikaGreatwood_817
{
    public class MIN_760 : RouteInfo
    {
        public override uint Id => 760;
        public override uint ExpansionId => 3;
        public override uint ZoneId => 817;
        public override uint GatherType => 0;
        public override Vector2 MapPosition => new Vector2(-694.669f, 435.817f);
        public override int Radius => 1;

        public override HashSet<uint> NodeIds => new()
        {
            33590,
        };

        public override HashSet<uint> ItemIds => new()
        {
            32951,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 33590,
            },
        };
    }
}
