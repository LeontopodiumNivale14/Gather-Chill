using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.Stormblood.Yanxia_614
{
    public class BTN_471 : RouteInfo
    {
        public override uint Id => 471;
        public override uint ExpansionId => 2;
        public override uint ZoneId => 614;
        public override uint GatherType => 5;
        public override Vector2 MapPosition => new Vector2(693.214f, 780.084f);
        public override int Radius => 108;

        public override HashSet<uint> NodeIds => new()
        {
            32092,
            32093,
            32094,
        };

        public override HashSet<uint> ItemIds => new()
        {
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 32092,
            },
            new NodeInfo
            {
                NodeId = 32093,
            },
            new NodeInfo
            {
                NodeId = 32094,
            },
        };
    }
}
