using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.ARR.Unknown_0
{
    public class BTN_571 : RouteInfo
    {
        public override uint Id => 571;
        public override uint ExpansionId => 0;
        public override uint ZoneId => 0;
        public override uint GatherType => 2;
        public override Vector2 MapPosition => new Vector2(-592.392f, -177.016f);
        public override int Radius => 87;

        public override HashSet<uint> NodeIds => new()
        {
            32466,
            32467,
            32468,
            32469,
            32470,
            32471,
            32472,
            32473,
            32474,
            32475,
            32476,
            32477,
        };

        public override HashSet<uint> ItemIds => new()
        {
            2002777,
            2002778,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 32466,
            },
            new NodeInfo
            {
                NodeId = 32467,
            },
            new NodeInfo
            {
                NodeId = 32468,
            },
            new NodeInfo
            {
                NodeId = 32469,
            },
            new NodeInfo
            {
                NodeId = 32470,
            },
            new NodeInfo
            {
                NodeId = 32471,
            },
            new NodeInfo
            {
                NodeId = 32472,
            },
            new NodeInfo
            {
                NodeId = 32473,
            },
            new NodeInfo
            {
                NodeId = 32474,
            },
            new NodeInfo
            {
                NodeId = 32475,
            },
            new NodeInfo
            {
                NodeId = 32476,
            },
            new NodeInfo
            {
                NodeId = 32477,
            },
        };
    }
}
