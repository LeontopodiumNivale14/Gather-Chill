using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.ARealmReborn.UpperLaNoscea_139
{
    public class BTN_143 : RouteInfo
    {
        public override uint Id => 143;
        public override uint ExpansionId => 0;
        public override uint ZoneId => 139;
        public override uint GatherType => 3;
        public override Vector2 MapPosition => new Vector2(655.668f, 161.987f);
        public override int Radius => 57;

        public override HashSet<uint> NodeIds => new()
        {
            30369,
            30370,
            30371,
            30372,
        };

        public override HashSet<uint> ItemIds => new()
        {
            5,
            4815,
            4845,
            5054,
            5564,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 30369,
            },
            new NodeInfo
            {
                NodeId = 30370,
            },
            new NodeInfo
            {
                NodeId = 30371,
            },
            new NodeInfo
            {
                NodeId = 30372,
            },
        };
    }
}
