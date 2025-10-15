using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.ARR.Unknown_0
{
    public class BTN_1293 : RouteInfo
    {
        public override uint Id => 1293;
        public override uint ExpansionId => 0;
        public override uint ZoneId => 0;
        public override uint GatherType => 3;
        public override Vector2 MapPosition => new Vector2(142.647f, -182.85f);
        public override int Radius => 17;

        public override HashSet<uint> NodeIds => new()
        {
            35441,
            35442,
            35443,
            35444,
            35445,
            35446,
        };

        public override HashSet<uint> ItemIds => new()
        {
            47419,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 35441,
            },
            new NodeInfo
            {
                NodeId = 35442,
            },
            new NodeInfo
            {
                NodeId = 35443,
            },
            new NodeInfo
            {
                NodeId = 35444,
            },
            new NodeInfo
            {
                NodeId = 35445,
            },
            new NodeInfo
            {
                NodeId = 35446,
            },
        };
    }
}
