using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.ARR.Unknown_0
{
    public class MIN_111 : RouteInfo
    {
        public override uint Id => 111;
        public override uint ExpansionId => 0;
        public override uint ZoneId => 0;
        public override uint GatherType => 1;
        public override Vector2 MapPosition => new Vector2(-210.282f, 33.3167f);
        public override int Radius => 19;

        public override HashSet<uint> NodeIds => new()
        {
            30816,
            30817,
            30818,
            30819,
        };

        public override HashSet<uint> ItemIds => new()
        {
            2000497,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 30816,
            },
            new NodeInfo
            {
                NodeId = 30817,
            },
            new NodeInfo
            {
                NodeId = 30818,
            },
            new NodeInfo
            {
                NodeId = 30819,
            },
        };
    }
}
