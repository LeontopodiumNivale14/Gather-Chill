using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.ARealmReborn.SouthernThanalan_146
{
    public class BTN_149 : RouteInfo
    {
        public override uint Id => 149;
        public override uint ExpansionId => 0;
        public override uint ZoneId => 146;
        public override uint GatherType => 3;
        public override Vector2 MapPosition => new Vector2(-380.823f, 511.151f);
        public override int Radius => 62;

        public override HashSet<uint> NodeIds => new()
        {
            30393,
            30394,
            30395,
            30396,
        };

        public override HashSet<uint> ItemIds => new()
        {
            6,
            4846,
            5055,
            7012,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 30393,
            },
            new NodeInfo
            {
                NodeId = 30394,
            },
            new NodeInfo
            {
                NodeId = 30395,
            },
            new NodeInfo
            {
                NodeId = 30396,
            },
        };
    }
}
