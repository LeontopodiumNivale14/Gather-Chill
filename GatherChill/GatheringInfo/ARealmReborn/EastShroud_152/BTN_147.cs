using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.ARealmReborn.EastShroud_152
{
    public class BTN_147 : RouteInfo
    {
        public override uint Id => 147;
        public override uint ExpansionId => 0;
        public override uint ZoneId => 152;
        public override uint GatherType => 2;
        public override Vector2 MapPosition => new Vector2(-251.3f, 90.4416f);
        public override int Radius => 68;

        public override HashSet<uint> NodeIds => new()
        {
            30385,
            30386,
            30387,
            30388,
        };

        public override HashSet<uint> ItemIds => new()
        {
            4,
            5393,
            5414,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 30385,
            },
            new NodeInfo
            {
                NodeId = 30386,
            },
            new NodeInfo
            {
                NodeId = 30387,
            },
            new NodeInfo
            {
                NodeId = 30388,
            },
        };
    }
}
