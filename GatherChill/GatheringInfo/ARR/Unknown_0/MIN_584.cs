using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.ARR.Unknown_0
{
    public class MIN_584 : RouteInfo
    {
        public override uint Id => 584;
        public override uint ExpansionId => 0;
        public override uint ZoneId => 0;
        public override uint GatherType => 0;
        public override Vector2 MapPosition => new Vector2(-518.296f, -646.155f);
        public override int Radius => 24;

        public override HashSet<uint> NodeIds => new()
        {
            32580,
            32581,
            32582,
            32583,
        };

        public override HashSet<uint> ItemIds => new()
        {
            2002797,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 32580,
            },
            new NodeInfo
            {
                NodeId = 32581,
            },
            new NodeInfo
            {
                NodeId = 32582,
            },
            new NodeInfo
            {
                NodeId = 32583,
            },
        };
    }
}
