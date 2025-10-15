using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.Heavensward.CoerthasWesternHighlands_397
{
    public class BTN_552 : RouteInfo
    {
        public override uint Id => 552;
        public override uint ExpansionId => 1;
        public override uint ZoneId => 397;
        public override uint GatherType => 3;
        public override Vector2 MapPosition => new Vector2(519.459f, -387.066f);
        public override int Radius => 91;

        public override HashSet<uint> NodeIds => new()
        {
            32350,
            32351,
            32352,
            32353,
            32354,
            32355,
        };

        public override HashSet<uint> ItemIds => new()
        {
            9,
            23150,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 32350,
            },
            new NodeInfo
            {
                NodeId = 32351,
            },
            new NodeInfo
            {
                NodeId = 32352,
            },
            new NodeInfo
            {
                NodeId = 32353,
            },
            new NodeInfo
            {
                NodeId = 32354,
            },
            new NodeInfo
            {
                NodeId = 32355,
            },
        };
    }
}
