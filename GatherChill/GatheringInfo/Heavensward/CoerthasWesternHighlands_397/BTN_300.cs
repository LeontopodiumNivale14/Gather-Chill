using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.Heavensward.CoerthasWesternHighlands_397
{
    public class BTN_300 : RouteInfo
    {
        public override uint Id => 300;
        public override uint ExpansionId => 1;
        public override uint ZoneId => 397;
        public override uint GatherType => 3;
        public override Vector2 MapPosition => new Vector2(-550.412f, -364.854f);
        public override int Radius => 141;

        public override HashSet<uint> NodeIds => new()
        {
            31406,
            31407,
            31408,
            31409,
            31410,
            31411,
        };

        public override HashSet<uint> ItemIds => new()
        {
            9,
            12642,
            12643,
            14956,
            17558,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 31406,
            },
            new NodeInfo
            {
                NodeId = 31407,
            },
            new NodeInfo
            {
                NodeId = 31408,
            },
            new NodeInfo
            {
                NodeId = 31409,
            },
            new NodeInfo
            {
                NodeId = 31410,
            },
            new NodeInfo
            {
                NodeId = 31411,
            },
        };
    }
}
