using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.Shadowbringers.TheRaktikaGreatwood_817
{
    public class MIN_596 : RouteInfo
    {
        public override uint Id => 596;
        public override uint ExpansionId => 3;
        public override uint ZoneId => 817;
        public override uint GatherType => 0;
        public override Vector2 MapPosition => new Vector2(209.608f, 380.291f);
        public override int Radius => 124;

        public override HashSet<uint> NodeIds => new()
        {
            32650,
            32651,
            32652,
            32653,
            32654,
            32655,
        };

        public override HashSet<uint> ItemIds => new()
        {
            13,
            26755,
            27776,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 32650,
            },
            new NodeInfo
            {
                NodeId = 32651,
            },
            new NodeInfo
            {
                NodeId = 32652,
            },
            new NodeInfo
            {
                NodeId = 32653,
            },
            new NodeInfo
            {
                NodeId = 32654,
            },
            new NodeInfo
            {
                NodeId = 32655,
            },
        };
    }
}
