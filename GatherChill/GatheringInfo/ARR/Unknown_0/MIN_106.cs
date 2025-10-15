using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.ARR.Unknown_0
{
    public class MIN_106 : RouteInfo
    {
        public override uint Id => 106;
        public override uint ExpansionId => 0;
        public override uint ZoneId => 0;
        public override uint GatherType => 0;
        public override Vector2 MapPosition => new Vector2(-267.496f, -52.3517f);
        public override int Radius => 22;

        public override HashSet<uint> NodeIds => new()
        {
            30788,
            30789,
            30790,
            30791,
        };

        public override HashSet<uint> ItemIds => new()
        {
            2000490,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 30788,
            },
            new NodeInfo
            {
                NodeId = 30789,
            },
            new NodeInfo
            {
                NodeId = 30790,
            },
            new NodeInfo
            {
                NodeId = 30791,
            },
        };
    }
}
