using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.ARR.Unknown_0
{
    public class BTN_564 : RouteInfo
    {
        public override uint Id => 564;
        public override uint ExpansionId => 0;
        public override uint ZoneId => 0;
        public override uint GatherType => 3;
        public override Vector2 MapPosition => new Vector2(-619.576f, 317.38f);
        public override int Radius => 69;

        public override HashSet<uint> NodeIds => new()
        {
            32418,
            32419,
            32420,
            32421,
        };

        public override HashSet<uint> ItemIds => new()
        {
            2002767,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 32418,
            },
            new NodeInfo
            {
                NodeId = 32419,
            },
            new NodeInfo
            {
                NodeId = 32420,
            },
            new NodeInfo
            {
                NodeId = 32421,
            },
        };
    }
}
