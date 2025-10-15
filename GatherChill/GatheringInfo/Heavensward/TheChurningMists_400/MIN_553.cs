using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.Heavensward.TheChurningMists_400
{
    public class MIN_553 : RouteInfo
    {
        public override uint Id => 553;
        public override uint ExpansionId => 1;
        public override uint ZoneId => 400;
        public override uint GatherType => 1;
        public override Vector2 MapPosition => new Vector2(667.941f, 169.727f);
        public override int Radius => 110;

        public override HashSet<uint> NodeIds => new()
        {
            32356,
            32357,
            32358,
            32359,
            32360,
            32361,
        };

        public override HashSet<uint> ItemIds => new()
        {
            11,
            23152,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 32356,
            },
            new NodeInfo
            {
                NodeId = 32357,
            },
            new NodeInfo
            {
                NodeId = 32358,
            },
            new NodeInfo
            {
                NodeId = 32359,
            },
            new NodeInfo
            {
                NodeId = 32360,
            },
            new NodeInfo
            {
                NodeId = 32361,
            },
        };
    }
}
