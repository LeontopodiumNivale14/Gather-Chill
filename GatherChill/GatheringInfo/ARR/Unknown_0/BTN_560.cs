using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.ARR.Unknown_0
{
    public class BTN_560 : RouteInfo
    {
        public override uint Id => 560;
        public override uint ExpansionId => 0;
        public override uint ZoneId => 0;
        public override uint GatherType => 3;
        public override Vector2 MapPosition => new Vector2(-541.805f, 662.75f);
        public override int Radius => 12;

        public override HashSet<uint> NodeIds => new()
        {
            32384,
            32385,
            32386,
            32387,
        };

        public override HashSet<uint> ItemIds => new()
        {
            2002761,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 32384,
            },
            new NodeInfo
            {
                NodeId = 32385,
            },
            new NodeInfo
            {
                NodeId = 32386,
            },
            new NodeInfo
            {
                NodeId = 32387,
            },
        };
    }
}
