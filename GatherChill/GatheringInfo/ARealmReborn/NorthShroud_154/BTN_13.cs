using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.ARealmReborn.NorthShroud_154
{
    public class BTN_13 : RouteInfo
    {
        public override uint Id => 13;
        public override uint ExpansionId => 0;
        public override uint ZoneId => 154;
        public override uint GatherType => 2;
        public override Vector2 MapPosition => new Vector2(249.253f, 295.467f);
        public override int Radius => 49;

        public override HashSet<uint> NodeIds => new()
        {
            30019,
            30020,
            30021,
            30082,
        };

        public override HashSet<uint> ItemIds => new()
        {
            5,
            4828,
            5498,
            5516,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 30019,
            },
            new NodeInfo
            {
                NodeId = 30020,
            },
            new NodeInfo
            {
                NodeId = 30021,
            },
            new NodeInfo
            {
                NodeId = 30082,
            },
        };
    }
}
