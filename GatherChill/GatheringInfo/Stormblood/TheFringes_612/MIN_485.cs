using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.Stormblood.TheFringes_612
{
    public class MIN_485 : RouteInfo
    {
        public override uint Id => 485;
        public override uint ExpansionId => 2;
        public override uint ZoneId => 612;
        public override uint GatherType => 0;
        public override Vector2 MapPosition => new Vector2(-228.264f, -505.993f);
        public override int Radius => 132;

        public override HashSet<uint> NodeIds => new()
        {
            32123,
            32124,
            32125,
            32126,
            32127,
            32128,
        };

        public override HashSet<uint> ItemIds => new()
        {
            10,
            19910,
            20780,
            20782,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 32123,
            },
            new NodeInfo
            {
                NodeId = 32124,
            },
            new NodeInfo
            {
                NodeId = 32125,
            },
            new NodeInfo
            {
                NodeId = 32126,
            },
            new NodeInfo
            {
                NodeId = 32127,
            },
            new NodeInfo
            {
                NodeId = 32128,
            },
        };
    }
}
