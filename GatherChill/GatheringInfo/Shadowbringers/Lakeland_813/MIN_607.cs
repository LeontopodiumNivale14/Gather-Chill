using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.Shadowbringers.Lakeland_813
{
    public class MIN_607 : RouteInfo
    {
        public override uint Id => 607;
        public override uint ExpansionId => 3;
        public override uint ZoneId => 813;
        public override uint GatherType => 0;
        public override Vector2 MapPosition => new Vector2(333.238f, 585.146f);
        public override int Radius => 28;

        public override HashSet<uint> NodeIds => new()
        {
            32681,
        };

        public override HashSet<uint> ItemIds => new()
        {
            27730,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 32681,
            },
        };
    }
}
