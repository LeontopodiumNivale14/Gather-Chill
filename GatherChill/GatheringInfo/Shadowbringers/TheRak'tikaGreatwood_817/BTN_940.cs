using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.Shadowbringers.TheRaktikaGreatwood_817
{
    public class BTN_940 : RouteInfo
    {
        public override uint Id => 940;
        public override uint ExpansionId => 3;
        public override uint ZoneId => 817;
        public override uint GatherType => 2;
        public override Vector2 MapPosition => new Vector2(-287.543f, 688.695f);
        public override int Radius => 135;

        public override HashSet<uint> NodeIds => new()
        {
            34484,
            34485,
            34486,
        };

        public override HashSet<uint> ItemIds => new()
        {
            41288,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 34484,
            },
            new NodeInfo
            {
                NodeId = 34485,
            },
            new NodeInfo
            {
                NodeId = 34486,
            },
        };
    }
}
