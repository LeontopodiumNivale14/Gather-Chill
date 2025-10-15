using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.Shadowbringers.TheRaktikaGreatwood_817
{
    public class BTN_624 : RouteInfo
    {
        public override uint Id => 624;
        public override uint ExpansionId => 3;
        public override uint ZoneId => 817;
        public override uint GatherType => 3;
        public override Vector2 MapPosition => new Vector2(50.6335f, -419.325f);
        public override int Radius => 132;

        public override HashSet<uint> NodeIds => new()
        {
            32758,
            33617,
            33618,
        };

        public override HashSet<uint> ItemIds => new()
        {
            10,
            27809,
            30592,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 32758,
            },
            new NodeInfo
            {
                NodeId = 33617,
            },
            new NodeInfo
            {
                NodeId = 33618,
            },
        };
    }
}
