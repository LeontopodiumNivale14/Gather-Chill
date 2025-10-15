using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.ARealmReborn.NorthShroud_154
{
    public class BTN_12 : RouteInfo
    {
        public override uint Id => 12;
        public override uint ExpansionId => 0;
        public override uint ZoneId => 154;
        public override uint GatherType => 2;
        public override Vector2 MapPosition => new Vector2(403.796f, 257.111f);
        public override int Radius => 64;

        public override HashSet<uint> NodeIds => new()
        {
            30016,
            30017,
            30018,
            30081,
        };

        public override HashSet<uint> ItemIds => new()
        {
            5,
            5380,
            5396,
            5509,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 30016,
            },
            new NodeInfo
            {
                NodeId = 30017,
            },
            new NodeInfo
            {
                NodeId = 30018,
            },
            new NodeInfo
            {
                NodeId = 30081,
            },
        };
    }
}
