using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.ARR.Unknown_0
{
    public class MIN_685 : RouteInfo
    {
        public override uint Id => 685;
        public override uint ExpansionId => 0;
        public override uint ZoneId => 0;
        public override uint GatherType => 1;
        public override Vector2 MapPosition => new Vector2(-713.243f, 568.889f);
        public override int Radius => 19;

        public override HashSet<uint> NodeIds => new()
        {
            32991,
            32992,
            32993,
        };

        public override HashSet<uint> ItemIds => new()
        {
            10,
            29516,
            29521,
            29523,
            29530,
            29536,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 32991,
            },
            new NodeInfo
            {
                NodeId = 32992,
            },
            new NodeInfo
            {
                NodeId = 32993,
            },
        };
    }
}
