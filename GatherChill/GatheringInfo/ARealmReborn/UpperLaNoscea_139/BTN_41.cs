using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.ARealmReborn.UpperLaNoscea_139
{
    public class BTN_41 : RouteInfo
    {
        public override uint Id => 41;
        public override uint ExpansionId => 0;
        public override uint ZoneId => 139;
        public override uint GatherType => 3;
        public override Vector2 MapPosition => new Vector2(-357.874f, 158.046f);
        public override int Radius => 58;

        public override HashSet<uint> NodeIds => new()
        {
            30150,
            30151,
            30308,
            30315,
        };

        public override HashSet<uint> ItemIds => new()
        {
            5,
            4812,
            4819,
            5542,
            6145,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 30150,
            },
            new NodeInfo
            {
                NodeId = 30151,
            },
            new NodeInfo
            {
                NodeId = 30308,
            },
            new NodeInfo
            {
                NodeId = 30315,
            },
        };
    }
}
