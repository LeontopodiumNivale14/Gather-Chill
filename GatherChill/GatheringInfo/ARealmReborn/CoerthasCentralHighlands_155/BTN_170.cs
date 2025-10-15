using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.ARealmReborn.CoerthasCentralHighlands_155
{
    public class BTN_170 : RouteInfo
    {
        public override uint Id => 170;
        public override uint ExpansionId => 0;
        public override uint ZoneId => 155;
        public override uint GatherType => 2;
        public override Vector2 MapPosition => new Vector2(259.895f, -82.31f);
        public override int Radius => 62;

        public override HashSet<uint> NodeIds => new()
        {
            31045,
            31046,
            31047,
            31048,
        };

        public override HashSet<uint> ItemIds => new()
        {
            3,
            2001424,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 31045,
            },
            new NodeInfo
            {
                NodeId = 31046,
            },
            new NodeInfo
            {
                NodeId = 31047,
            },
            new NodeInfo
            {
                NodeId = 31048,
            },
        };
    }
}
