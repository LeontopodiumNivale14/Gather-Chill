using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.ARealmReborn.EasternLaNoscea_137
{
    public class BTN_141 : RouteInfo
    {
        public override uint Id => 141;
        public override uint ExpansionId => 0;
        public override uint ZoneId => 137;
        public override uint GatherType => 3;
        public override Vector2 MapPosition => new Vector2(-21.2806f, 379.288f);
        public override int Radius => 53;

        public override HashSet<uint> NodeIds => new()
        {
            30361,
            30362,
            30363,
            30364,
        };

        public override HashSet<uint> ItemIds => new()
        {
            7,
            4792,
            4793,
            4840,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 30361,
            },
            new NodeInfo
            {
                NodeId = 30362,
            },
            new NodeInfo
            {
                NodeId = 30363,
            },
            new NodeInfo
            {
                NodeId = 30364,
            },
        };
    }
}
