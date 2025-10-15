using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.Shadowbringers.AmhAraeng_815
{
    public class MIN_713 : RouteInfo
    {
        public override uint Id => 713;
        public override uint ExpansionId => 3;
        public override uint ZoneId => 815;
        public override uint GatherType => 1;
        public override Vector2 MapPosition => new Vector2(-66.1651f, -643.527f);
        public override int Radius => 1;

        public override HashSet<uint> NodeIds => new()
        {
            33233,
        };

        public override HashSet<uint> ItemIds => new()
        {
            29976,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 33233,
            },
        };
    }
}
