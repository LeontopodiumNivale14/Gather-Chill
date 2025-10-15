using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.ARR.Unknown_0
{
    public class MIN_273 : RouteInfo
    {
        public override uint Id => 273;
        public override uint ExpansionId => 0;
        public override uint ZoneId => 0;
        public override uint GatherType => 0;
        public override Vector2 MapPosition => new Vector2(758.819f, 2.44411f);
        public override int Radius => 13;

        public override HashSet<uint> NodeIds => new()
        {
            31231,
            31232,
            31233,
            31234,
        };

        public override HashSet<uint> ItemIds => new()
        {
            2001830,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 31231,
            },
            new NodeInfo
            {
                NodeId = 31232,
            },
            new NodeInfo
            {
                NodeId = 31233,
            },
            new NodeInfo
            {
                NodeId = 31234,
            },
        };
    }
}
