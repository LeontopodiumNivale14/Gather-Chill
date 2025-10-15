using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.Heavensward.CoerthasWesternHighlands_397
{
    public class BTN_782 : RouteInfo
    {
        public override uint Id => 782;
        public override uint ExpansionId => 1;
        public override uint ZoneId => 397;
        public override uint GatherType => 3;
        public override Vector2 MapPosition => new Vector2(188.514f, -472.41f);
        public override int Radius => 140;

        public override HashSet<uint> NodeIds => new()
        {
            33631,
            33632,
            33633,
        };

        public override HashSet<uint> ItemIds => new()
        {
            33010,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 33631,
            },
            new NodeInfo
            {
                NodeId = 33632,
            },
            new NodeInfo
            {
                NodeId = 33633,
            },
        };
    }
}
