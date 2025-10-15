using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.Shadowbringers.TheRaktikaGreatwood_817
{
    public class MIN_595 : RouteInfo
    {
        public override uint Id => 595;
        public override uint ExpansionId => 3;
        public override uint ZoneId => 817;
        public override uint GatherType => 1;
        public override Vector2 MapPosition => new Vector2(-303.368f, -70.79f);
        public override int Radius => 110;

        public override HashSet<uint> NodeIds => new()
        {
            32644,
            32645,
            32646,
            32647,
            32648,
            32649,
        };

        public override HashSet<uint> ItemIds => new()
        {
            13,
            27699,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 32644,
            },
            new NodeInfo
            {
                NodeId = 32645,
            },
            new NodeInfo
            {
                NodeId = 32646,
            },
            new NodeInfo
            {
                NodeId = 32647,
            },
            new NodeInfo
            {
                NodeId = 32648,
            },
            new NodeInfo
            {
                NodeId = 32649,
            },
        };
    }
}
