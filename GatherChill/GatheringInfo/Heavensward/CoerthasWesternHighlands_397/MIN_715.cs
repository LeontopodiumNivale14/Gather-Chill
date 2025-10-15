using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.Heavensward.CoerthasWesternHighlands_397
{
    public class MIN_715 : RouteInfo
    {
        public override uint Id => 715;
        public override uint ExpansionId => 1;
        public override uint ZoneId => 397;
        public override uint GatherType => 0;
        public override Vector2 MapPosition => new Vector2(-410.042f, 392.406f);
        public override int Radius => 126;

        public override HashSet<uint> NodeIds => new()
        {
            33235,
            33236,
            33237,
            33238,
            33239,
            33240,
        };

        public override HashSet<uint> ItemIds => new()
        {
            29671,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 33235,
            },
            new NodeInfo
            {
                NodeId = 33236,
            },
            new NodeInfo
            {
                NodeId = 33237,
            },
            new NodeInfo
            {
                NodeId = 33238,
            },
            new NodeInfo
            {
                NodeId = 33239,
            },
            new NodeInfo
            {
                NodeId = 33240,
            },
        };
    }
}
