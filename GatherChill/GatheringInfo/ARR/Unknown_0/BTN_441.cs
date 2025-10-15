using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.ARR.Unknown_0
{
    public class BTN_441 : RouteInfo
    {
        public override uint Id => 441;
        public override uint ExpansionId => 0;
        public override uint ZoneId => 0;
        public override uint GatherType => 3;
        public override Vector2 MapPosition => new Vector2(705.721f, 757.539f);
        public override int Radius => 74;

        public override HashSet<uint> NodeIds => new()
        {
            31889,
            31890,
            31891,
            31892,
            31893,
            31894,
        };

        public override HashSet<uint> ItemIds => new()
        {
            2002132,
            2002133,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 31889,
            },
            new NodeInfo
            {
                NodeId = 31890,
            },
            new NodeInfo
            {
                NodeId = 31891,
            },
            new NodeInfo
            {
                NodeId = 31892,
            },
            new NodeInfo
            {
                NodeId = 31893,
            },
            new NodeInfo
            {
                NodeId = 31894,
            },
        };
    }
}
