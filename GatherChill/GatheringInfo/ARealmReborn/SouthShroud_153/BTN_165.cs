using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.ARealmReborn.SouthShroud_153
{
    public class BTN_165 : RouteInfo
    {
        public override uint Id => 165;
        public override uint ExpansionId => 0;
        public override uint ZoneId => 153;
        public override uint GatherType => 2;
        public override Vector2 MapPosition => new Vector2(-202.835f, 20.9149f);
        public override int Radius => 54;

        public override HashSet<uint> NodeIds => new()
        {
            30325,
            30326,
            30327,
            30328,
        };

        public override HashSet<uint> ItemIds => new()
        {
            3,
            9,
            5817,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 30325,
            },
            new NodeInfo
            {
                NodeId = 30326,
            },
            new NodeInfo
            {
                NodeId = 30327,
            },
            new NodeInfo
            {
                NodeId = 30328,
            },
        };
    }
}
