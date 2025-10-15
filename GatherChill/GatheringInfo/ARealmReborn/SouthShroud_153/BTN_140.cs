using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.ARealmReborn.SouthShroud_153
{
    public class BTN_140 : RouteInfo
    {
        public override uint Id => 140;
        public override uint ExpansionId => 0;
        public override uint ZoneId => 153;
        public override uint GatherType => 3;
        public override Vector2 MapPosition => new Vector2(-1.32293f, 357.514f);
        public override int Radius => 83;

        public override HashSet<uint> NodeIds => new()
        {
            30357,
            30358,
            30359,
            30360,
        };

        public override HashSet<uint> ItemIds => new()
        {
            3,
            4814,
            4841,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 30357,
            },
            new NodeInfo
            {
                NodeId = 30358,
            },
            new NodeInfo
            {
                NodeId = 30359,
            },
            new NodeInfo
            {
                NodeId = 30360,
            },
        };
    }
}
