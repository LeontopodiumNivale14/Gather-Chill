using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.Heavensward.CoerthasWesternHighlands_397
{
    public class MIN_551 : RouteInfo
    {
        public override uint Id => 551;
        public override uint ExpansionId => 1;
        public override uint ZoneId => 397;
        public override uint GatherType => 0;
        public override Vector2 MapPosition => new Vector2(1.14695f, 621.308f);
        public override int Radius => 121;

        public override HashSet<uint> NodeIds => new()
        {
            32344,
            32345,
            32346,
            32347,
            32348,
            32349,
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
                NodeId = 32344,
            },
            new NodeInfo
            {
                NodeId = 32345,
            },
            new NodeInfo
            {
                NodeId = 32346,
            },
            new NodeInfo
            {
                NodeId = 32347,
            },
            new NodeInfo
            {
                NodeId = 32348,
            },
            new NodeInfo
            {
                NodeId = 32349,
            },
        };
    }
}
