using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.ARealmReborn.NorthShroud_154
{
    public class BTN_15 : RouteInfo
    {
        public override uint Id => 15;
        public override uint ExpansionId => 0;
        public override uint ZoneId => 154;
        public override uint GatherType => 2;
        public override Vector2 MapPosition => new Vector2(310.224f, 146.93f);
        public override int Radius => 71;

        public override HashSet<uint> NodeIds => new()
        {
            30024,
            30025,
            30026,
            30309,
        };

        public override HashSet<uint> ItemIds => new()
        {
            5,
            5383,
            5402,
            5534,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 30024,
            },
            new NodeInfo
            {
                NodeId = 30025,
            },
            new NodeInfo
            {
                NodeId = 30026,
            },
            new NodeInfo
            {
                NodeId = 30309,
            },
        };
    }
}
