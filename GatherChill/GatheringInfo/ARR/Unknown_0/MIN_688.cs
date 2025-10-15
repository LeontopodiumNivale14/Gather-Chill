using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.ARR.Unknown_0
{
    public class MIN_688 : RouteInfo
    {
        public override uint Id => 688;
        public override uint ExpansionId => 0;
        public override uint ZoneId => 0;
        public override uint GatherType => 0;
        public override Vector2 MapPosition => new Vector2(120.45f, 50.0027f);
        public override int Radius => 54;

        public override HashSet<uint> NodeIds => new()
        {
            33000,
            33001,
            33002,
        };

        public override HashSet<uint> ItemIds => new()
        {
            10,
            29512,
            29517,
            29525,
            29528,
            29534,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 33000,
            },
            new NodeInfo
            {
                NodeId = 33001,
            },
            new NodeInfo
            {
                NodeId = 33002,
            },
        };
    }
}
