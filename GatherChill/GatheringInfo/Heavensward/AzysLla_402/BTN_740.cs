using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.Heavensward.AzysLla_402
{
    public class BTN_740 : RouteInfo
    {
        public override uint Id => 740;
        public override uint ExpansionId => 1;
        public override uint ZoneId => 402;
        public override uint GatherType => 2;
        public override Vector2 MapPosition => new Vector2(-776.38f, -321.422f);
        public override int Radius => 92;

        public override HashSet<uint> NodeIds => new()
        {
            33380,
            33381,
            33382,
            33383,
            33384,
            33385,
        };

        public override HashSet<uint> ItemIds => new()
        {
            31130,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 33380,
            },
            new NodeInfo
            {
                NodeId = 33381,
            },
            new NodeInfo
            {
                NodeId = 33382,
            },
            new NodeInfo
            {
                NodeId = 33383,
            },
            new NodeInfo
            {
                NodeId = 33384,
            },
            new NodeInfo
            {
                NodeId = 33385,
            },
        };
    }
}
