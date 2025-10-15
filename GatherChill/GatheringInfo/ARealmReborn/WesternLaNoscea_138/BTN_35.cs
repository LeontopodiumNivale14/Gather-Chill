using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.ARealmReborn.WesternLaNoscea_138
{
    public class BTN_35 : RouteInfo
    {
        public override uint Id => 35;
        public override uint ExpansionId => 0;
        public override uint ZoneId => 138;
        public override uint GatherType => 2;
        public override Vector2 MapPosition => new Vector2(278.732f, 127.178f);
        public override int Radius => 80;

        public override HashSet<uint> NodeIds => new()
        {
            30098,
            30099,
            30100,
            30101,
        };

        public override HashSet<uint> ItemIds => new()
        {
            3,
            5599,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 30098,
            },
            new NodeInfo
            {
                NodeId = 30099,
            },
            new NodeInfo
            {
                NodeId = 30100,
            },
            new NodeInfo
            {
                NodeId = 30101,
            },
        };
    }
}
