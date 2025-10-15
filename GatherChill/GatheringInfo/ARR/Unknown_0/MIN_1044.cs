using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.ARR.Unknown_0
{
    public class MIN_1044 : RouteInfo
    {
        public override uint Id => 1044;
        public override uint ExpansionId => 0;
        public override uint ZoneId => 0;
        public override uint GatherType => 0;
        public override Vector2 MapPosition => new Vector2(-110.743f, -173.258f);
        public override int Radius => 69;

        public override HashSet<uint> NodeIds => new()
        {
            35033,
            35034,
            35035,
            35036,
            35037,
            35038,
            35039,
            35040,
        };

        public override HashSet<uint> ItemIds => new()
        {
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 35033,
            },
            new NodeInfo
            {
                NodeId = 35034,
            },
            new NodeInfo
            {
                NodeId = 35035,
            },
            new NodeInfo
            {
                NodeId = 35036,
            },
            new NodeInfo
            {
                NodeId = 35037,
            },
            new NodeInfo
            {
                NodeId = 35038,
            },
            new NodeInfo
            {
                NodeId = 35039,
            },
            new NodeInfo
            {
                NodeId = 35040,
            },
        };
    }
}
