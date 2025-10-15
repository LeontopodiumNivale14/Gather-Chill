using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.Heavensward.TheDravanianForelands_398
{
    public class MIN_669 : RouteInfo
    {
        public override uint Id => 669;
        public override uint ExpansionId => 1;
        public override uint ZoneId => 398;
        public override uint GatherType => 0;
        public override Vector2 MapPosition => new Vector2(420.835f, -598.352f);
        public override int Radius => 110;

        public override HashSet<uint> NodeIds => new()
        {
            32917,
            32918,
            32919,
            32920,
            32921,
            32922,
        };

        public override HashSet<uint> ItemIds => new()
        {
            28780,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 32917,
            },
            new NodeInfo
            {
                NodeId = 32918,
            },
            new NodeInfo
            {
                NodeId = 32919,
            },
            new NodeInfo
            {
                NodeId = 32920,
            },
            new NodeInfo
            {
                NodeId = 32921,
            },
            new NodeInfo
            {
                NodeId = 32922,
            },
        };
    }
}
