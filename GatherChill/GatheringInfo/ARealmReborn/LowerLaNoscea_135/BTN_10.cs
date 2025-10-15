using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.ARealmReborn.LowerLaNoscea_135
{
    public class BTN_10 : RouteInfo
    {
        public override uint Id => 10;
        public override uint ExpansionId => 0;
        public override uint ZoneId => 135;
        public override uint GatherType => 2;
        public override Vector2 MapPosition => new Vector2(536.716f, -270f);
        public override int Radius => 37;

        public override HashSet<uint> NodeIds => new()
        {
            30011,
            30022,
            30023,
            30051,
        };

        public override HashSet<uint> ItemIds => new()
        {
            5,
            4803,
            4809,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 30011,
            },
            new NodeInfo
            {
                NodeId = 30022,
            },
            new NodeInfo
            {
                NodeId = 30023,
            },
            new NodeInfo
            {
                NodeId = 30051,
            },
        };
    }
}
