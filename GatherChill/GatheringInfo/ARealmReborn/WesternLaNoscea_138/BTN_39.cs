using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.ARealmReborn.WesternLaNoscea_138
{
    public class BTN_39 : RouteInfo
    {
        public override uint Id => 39;
        public override uint ExpansionId => 0;
        public override uint ZoneId => 138;
        public override uint GatherType => 3;
        public override Vector2 MapPosition => new Vector2(520.736f, 383.144f);
        public override int Radius => 71;

        public override HashSet<uint> NodeIds => new()
        {
            30115,
            30116,
            30117,
            30118,
        };

        public override HashSet<uint> ItemIds => new()
        {
            3,
            4785,
            4820,
            7029,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 30115,
            },
            new NodeInfo
            {
                NodeId = 30116,
            },
            new NodeInfo
            {
                NodeId = 30117,
            },
            new NodeInfo
            {
                NodeId = 30118,
            },
        };
    }
}
