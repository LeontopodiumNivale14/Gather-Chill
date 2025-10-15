using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.ARR.Unknown_0
{
    public class MIN_278 : RouteInfo
    {
        public override uint Id => 278;
        public override uint ExpansionId => 0;
        public override uint ZoneId => 0;
        public override uint GatherType => 1;
        public override Vector2 MapPosition => new Vector2(-326.569f, 543.135f);
        public override int Radius => 21;

        public override HashSet<uint> NodeIds => new()
        {
            31269,
            31270,
            31271,
            31272,
        };

        public override HashSet<uint> ItemIds => new()
        {
            2001837,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 31269,
            },
            new NodeInfo
            {
                NodeId = 31270,
            },
            new NodeInfo
            {
                NodeId = 31271,
            },
            new NodeInfo
            {
                NodeId = 31272,
            },
        };
    }
}
