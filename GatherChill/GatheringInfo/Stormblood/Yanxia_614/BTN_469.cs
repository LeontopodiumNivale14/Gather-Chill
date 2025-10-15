using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.Stormblood.Yanxia_614
{
    public class BTN_469 : RouteInfo
    {
        public override uint Id => 469;
        public override uint ExpansionId => 2;
        public override uint ZoneId => 614;
        public override uint GatherType => 5;
        public override Vector2 MapPosition => new Vector2(-326.21f, 820.399f);
        public override int Radius => 103;

        public override HashSet<uint> NodeIds => new()
        {
            32086,
            32087,
            32088,
        };

        public override HashSet<uint> ItemIds => new()
        {
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 32086,
            },
            new NodeInfo
            {
                NodeId = 32087,
            },
            new NodeInfo
            {
                NodeId = 32088,
            },
        };
    }
}
