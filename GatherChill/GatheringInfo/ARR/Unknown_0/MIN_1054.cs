using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.ARR.Unknown_0
{
    public class MIN_1054 : RouteInfo
    {
        public override uint Id => 1054;
        public override uint ExpansionId => 0;
        public override uint ZoneId => 0;
        public override uint GatherType => 0;
        public override Vector2 MapPosition => new Vector2(407.355f, -799.866f);
        public override int Radius => 70;

        public override HashSet<uint> NodeIds => new()
        {
            35099,
            35100,
            35101,
            35102,
            35103,
            35104,
        };

        public override HashSet<uint> ItemIds => new()
        {
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 35099,
            },
            new NodeInfo
            {
                NodeId = 35100,
            },
            new NodeInfo
            {
                NodeId = 35101,
            },
            new NodeInfo
            {
                NodeId = 35102,
            },
            new NodeInfo
            {
                NodeId = 35103,
            },
            new NodeInfo
            {
                NodeId = 35104,
            },
        };
    }
}
