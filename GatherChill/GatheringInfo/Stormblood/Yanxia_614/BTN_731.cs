using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.Stormblood.Yanxia_614
{
    public class BTN_731 : RouteInfo
    {
        public override uint Id => 731;
        public override uint ExpansionId => 2;
        public override uint ZoneId => 614;
        public override uint GatherType => 3;
        public override Vector2 MapPosition => new Vector2(-266.009f, 464.614f);
        public override int Radius => 96;

        public override HashSet<uint> NodeIds => new()
        {
            33331,
            33332,
            33333,
            33334,
            33335,
            33336,
        };

        public override HashSet<uint> ItemIds => new()
        {
            10,
            30502,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 33331,
            },
            new NodeInfo
            {
                NodeId = 33332,
            },
            new NodeInfo
            {
                NodeId = 33333,
            },
            new NodeInfo
            {
                NodeId = 33334,
            },
            new NodeInfo
            {
                NodeId = 33335,
            },
            new NodeInfo
            {
                NodeId = 33336,
            },
        };
    }
}
