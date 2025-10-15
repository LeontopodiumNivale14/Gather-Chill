using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.ARealmReborn.EastShroud_152
{
    public class MIN_200 : RouteInfo
    {
        public override uint Id => 200;
        public override uint ExpansionId => 0;
        public override uint ZoneId => 152;
        public override uint GatherType => 1;
        public override Vector2 MapPosition => new Vector2(-41.6145f, 296.446f);
        public override int Radius => 56;

        public override HashSet<uint> NodeIds => new()
        {
            30475,
            30476,
            30477,
            30478,
        };

        public override HashSet<uint> ItemIds => new()
        {
            4,
            10,
            5819,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 30475,
            },
            new NodeInfo
            {
                NodeId = 30476,
            },
            new NodeInfo
            {
                NodeId = 30477,
            },
            new NodeInfo
            {
                NodeId = 30478,
            },
        };
    }
}
