using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.ARealmReborn.SouthShroud_153
{
    public class BTN_24 : RouteInfo
    {
        public override uint Id => 24;
        public override uint ExpansionId => 0;
        public override uint ZoneId => 153;
        public override uint GatherType => 2;
        public override Vector2 MapPosition => new Vector2(84.0889f, 5.99475f);
        public override int Radius => 63;

        public override HashSet<uint> NodeIds => new()
        {
            30048,
            30049,
            30050,
            30314,
        };

        public override HashSet<uint> ItemIds => new()
        {
            3,
            4806,
            5388,
            5535,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 30048,
            },
            new NodeInfo
            {
                NodeId = 30049,
            },
            new NodeInfo
            {
                NodeId = 30050,
            },
            new NodeInfo
            {
                NodeId = 30314,
            },
        };
    }
}
