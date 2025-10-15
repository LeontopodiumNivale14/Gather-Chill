using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.ARealmReborn.MiddleLaNoscea_134
{
    public class MIN_164 : RouteInfo
    {
        public override uint Id => 164;
        public override uint ExpansionId => 0;
        public override uint ZoneId => 134;
        public override uint GatherType => 1;
        public override Vector2 MapPosition => new Vector2(-294.28f, -577.596f);
        public override int Radius => 74;

        public override HashSet<uint> NodeIds => new()
        {
            30453,
            30454,
            30455,
            30456,
        };

        public override HashSet<uint> ItemIds => new()
        {
            2,
            5599,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 30453,
            },
            new NodeInfo
            {
                NodeId = 30454,
            },
            new NodeInfo
            {
                NodeId = 30455,
            },
            new NodeInfo
            {
                NodeId = 30456,
            },
        };
    }
}
