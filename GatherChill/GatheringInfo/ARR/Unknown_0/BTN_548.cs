using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.ARR.Unknown_0
{
    public class BTN_548 : RouteInfo
    {
        public override uint Id => 548;
        public override uint ExpansionId => 0;
        public override uint ZoneId => 0;
        public override uint GatherType => 3;
        public override Vector2 MapPosition => new Vector2(-73.5484f, -348.038f);
        public override int Radius => 15;

        public override HashSet<uint> NodeIds => new()
        {
            32335,
            32336,
            32337,
        };

        public override HashSet<uint> ItemIds => new()
        {
            12,
            22643,
            22648,
            22654,
            22658,
            22659,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 32335,
            },
            new NodeInfo
            {
                NodeId = 32336,
            },
            new NodeInfo
            {
                NodeId = 32337,
            },
        };
    }
}
