using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.Shadowbringers.TheRaktikaGreatwood_817
{
    public class BTN_644 : RouteInfo
    {
        public override uint Id => 644;
        public override uint ExpansionId => 3;
        public override uint ZoneId => 817;
        public override uint GatherType => 5;
        public override Vector2 MapPosition => new Vector2(-613.073f, 405.823f);
        public override int Radius => 123;

        public override HashSet<uint> NodeIds => new()
        {
            32801,
            32802,
            32803,
        };

        public override HashSet<uint> ItemIds => new()
        {
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 32801,
            },
            new NodeInfo
            {
                NodeId = 32802,
            },
            new NodeInfo
            {
                NodeId = 32803,
            },
        };
    }
}
