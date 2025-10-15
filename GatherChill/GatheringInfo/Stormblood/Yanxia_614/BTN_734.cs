using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.Stormblood.Yanxia_614
{
    public class BTN_734 : RouteInfo
    {
        public override uint Id => 734;
        public override uint ExpansionId => 2;
        public override uint ZoneId => 614;
        public override uint GatherType => 2;
        public override Vector2 MapPosition => new Vector2(357.845f, -568.014f);
        public override int Radius => 97;

        public override HashSet<uint> NodeIds => new()
        {
            33344,
            33345,
            33346,
            33347,
            33348,
            33349,
        };

        public override HashSet<uint> ItemIds => new()
        {
            31125,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 33344,
            },
            new NodeInfo
            {
                NodeId = 33345,
            },
            new NodeInfo
            {
                NodeId = 33346,
            },
            new NodeInfo
            {
                NodeId = 33347,
            },
            new NodeInfo
            {
                NodeId = 33348,
            },
            new NodeInfo
            {
                NodeId = 33349,
            },
        };
    }
}
