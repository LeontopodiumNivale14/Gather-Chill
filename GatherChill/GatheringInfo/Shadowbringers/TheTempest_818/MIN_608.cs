using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.Shadowbringers.TheTempest_818
{
    public class MIN_608 : RouteInfo
    {
        public override uint Id => 608;
        public override uint ExpansionId => 3;
        public override uint ZoneId => 818;
        public override uint GatherType => 0;
        public override Vector2 MapPosition => new Vector2(-275.274f, 7.34678f);
        public override int Radius => 15;

        public override HashSet<uint> NodeIds => new()
        {
            32682,
        };

        public override HashSet<uint> ItemIds => new()
        {
            27731,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 32682,
            },
        };
    }
}
