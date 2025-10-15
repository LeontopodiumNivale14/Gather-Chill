using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.ARealmReborn.SouthernThanalan_146
{
    public class BTN_133 : RouteInfo
    {
        public override uint Id => 133;
        public override uint ExpansionId => 0;
        public override uint ZoneId => 146;
        public override uint GatherType => 3;
        public override Vector2 MapPosition => new Vector2(-49.8718f, -645.186f);
        public override int Radius => 52;

        public override HashSet<uint> NodeIds => new()
        {
            30333,
            30334,
            30335,
            30336,
        };

        public override HashSet<uint> ItemIds => new()
        {
            6,
            4790,
            4839,
            4843,
            7011,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 30333,
            },
            new NodeInfo
            {
                NodeId = 30334,
            },
            new NodeInfo
            {
                NodeId = 30335,
            },
            new NodeInfo
            {
                NodeId = 30336,
            },
        };
    }
}
