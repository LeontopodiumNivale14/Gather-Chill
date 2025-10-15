using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.ARR.Unknown_0
{
    public class MIN_1242 : RouteInfo
    {
        public override uint Id => 1242;
        public override uint ExpansionId => 0;
        public override uint ZoneId => 0;
        public override uint GatherType => 1;
        public override Vector2 MapPosition => new Vector2(355.958f, 440.139f);
        public override int Radius => 75;

        public override HashSet<uint> NodeIds => new()
        {
            35339,
            35340,
            35341,
            35342,
            35343,
            35344,
        };

        public override HashSet<uint> ItemIds => new()
        {
            47369,
            47374,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 35339,
            },
            new NodeInfo
            {
                NodeId = 35340,
            },
            new NodeInfo
            {
                NodeId = 35341,
            },
            new NodeInfo
            {
                NodeId = 35342,
            },
            new NodeInfo
            {
                NodeId = 35343,
            },
            new NodeInfo
            {
                NodeId = 35344,
            },
        };
    }
}
