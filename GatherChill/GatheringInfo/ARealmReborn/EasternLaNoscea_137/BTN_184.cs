using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.ARealmReborn.EasternLaNoscea_137
{
    public class BTN_184 : RouteInfo
    {
        public override uint Id => 184;
        public override uint ExpansionId => 0;
        public override uint ZoneId => 137;
        public override uint GatherType => 2;
        public override Vector2 MapPosition => new Vector2(316.979f, 613.406f);
        public override int Radius => 64;

        public override HashSet<uint> NodeIds => new()
        {
            30353,
            30354,
            30355,
            30356,
        };

        public override HashSet<uint> ItemIds => new()
        {
            7,
            13,
            5814,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 30353,
            },
            new NodeInfo
            {
                NodeId = 30354,
            },
            new NodeInfo
            {
                NodeId = 30355,
            },
            new NodeInfo
            {
                NodeId = 30356,
            },
        };
    }
}
