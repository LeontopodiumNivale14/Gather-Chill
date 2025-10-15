using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.ARealmReborn.NorthShroud_154
{
    public class BTN_243 : RouteInfo
    {
        public override uint Id => 243;
        public override uint ExpansionId => 0;
        public override uint ZoneId => 154;
        public override uint GatherType => 2;
        public override Vector2 MapPosition => new Vector2(51.6109f, -12.2171f);
        public override int Radius => 31;

        public override HashSet<uint> NodeIds => new()
        {
            31037,
            31038,
            31039,
            31040,
        };

        public override HashSet<uint> ItemIds => new()
        {
            5,
            2001388,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 31037,
            },
            new NodeInfo
            {
                NodeId = 31038,
            },
            new NodeInfo
            {
                NodeId = 31039,
            },
            new NodeInfo
            {
                NodeId = 31040,
            },
        };
    }
}
