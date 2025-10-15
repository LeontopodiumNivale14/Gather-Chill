using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.ARealmReborn.MiddleLaNoscea_134
{
    public class MIN_239 : RouteInfo
    {
        public override uint Id => 239;
        public override uint ExpansionId => 0;
        public override uint ZoneId => 134;
        public override uint GatherType => 1;
        public override Vector2 MapPosition => new Vector2(141.502f, 275.942f);
        public override int Radius => 38;

        public override HashSet<uint> NodeIds => new()
        {
            31033,
        };

        public override HashSet<uint> ItemIds => new()
        {
            14,
            7760,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 31033,
            },
        };
    }
}
