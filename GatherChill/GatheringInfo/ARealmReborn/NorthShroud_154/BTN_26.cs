using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.ARealmReborn.NorthShroud_154
{
    public class BTN_26 : RouteInfo
    {
        public override uint Id => 26;
        public override uint ExpansionId => 0;
        public override uint ZoneId => 154;
        public override uint GatherType => 3;
        public override Vector2 MapPosition => new Vector2(92.5351f, 234.694f);
        public override int Radius => 68;

        public override HashSet<uint> NodeIds => new()
        {
            30054,
            30055,
            30056,
            30316,
        };

        public override HashSet<uint> ItemIds => new()
        {
            5,
            4788,
            4789,
            4822,
            5559,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 30054,
            },
            new NodeInfo
            {
                NodeId = 30055,
            },
            new NodeInfo
            {
                NodeId = 30056,
            },
            new NodeInfo
            {
                NodeId = 30316,
            },
        };
    }
}
