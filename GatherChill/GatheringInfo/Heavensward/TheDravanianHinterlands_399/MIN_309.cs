using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.Heavensward.TheDravanianHinterlands_399
{
    public class MIN_309 : RouteInfo
    {
        public override uint Id => 309;
        public override uint ExpansionId => 1;
        public override uint ZoneId => 399;
        public override uint GatherType => 1;
        public override Vector2 MapPosition => new Vector2(330.392f, -231.266f);
        public override int Radius => 135;

        public override HashSet<uint> NodeIds => new()
        {
            31444,
            31453,
            31454,
        };

        public override HashSet<uint> ItemIds => new()
        {
            8,
            5214,
            5220,
            12966,
            15949,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 31444,
            },
            new NodeInfo
            {
                NodeId = 31453,
            },
            new NodeInfo
            {
                NodeId = 31454,
            },
        };
    }
}
