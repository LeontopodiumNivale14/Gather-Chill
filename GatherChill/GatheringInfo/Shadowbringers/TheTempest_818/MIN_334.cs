using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.Shadowbringers.TheTempest_818
{
    public class MIN_334 : RouteInfo
    {
        public override uint Id => 334;
        public override uint ExpansionId => 3;
        public override uint ZoneId => 818;
        public override uint GatherType => 0;
        public override Vector2 MapPosition => new Vector2(180.65f, -849.784f);
        public override int Radius => 1;

        public override HashSet<uint> NodeIds => new()
        {
            31446,
        };

        public override HashSet<uint> ItemIds => new()
        {
            32983,
            32985,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 31446,
            },
        };
    }
}
