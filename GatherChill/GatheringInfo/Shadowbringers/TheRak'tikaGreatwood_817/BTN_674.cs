using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.Shadowbringers.TheRaktikaGreatwood_817
{
    public class BTN_674 : RouteInfo
    {
        public override uint Id => 674;
        public override uint ExpansionId => 3;
        public override uint ZoneId => 817;
        public override uint GatherType => 2;
        public override Vector2 MapPosition => new Vector2(516.73f, 69.3332f);
        public override int Radius => 103;

        public override HashSet<uint> NodeIds => new()
        {
            32947,
            32948,
            32949,
            32950,
            32951,
            32952,
        };

        public override HashSet<uint> ItemIds => new()
        {
            28790,
            28798,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 32947,
            },
            new NodeInfo
            {
                NodeId = 32948,
            },
            new NodeInfo
            {
                NodeId = 32949,
            },
            new NodeInfo
            {
                NodeId = 32950,
            },
            new NodeInfo
            {
                NodeId = 32951,
            },
            new NodeInfo
            {
                NodeId = 32952,
            },
        };
    }
}
