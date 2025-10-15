using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.Shadowbringers.Kholusia_814
{
    public class MIN_650 : RouteInfo
    {
        public override uint Id => 650;
        public override uint ExpansionId => 3;
        public override uint ZoneId => 814;
        public override uint GatherType => 1;
        public override Vector2 MapPosition => new Vector2(-579.451f, 341.791f);
        public override int Radius => 142;

        public override HashSet<uint> NodeIds => new()
        {
            32821,
            32822,
            32823,
            32824,
            32825,
            32826,
        };

        public override HashSet<uint> ItemIds => new()
        {
            8,
            28199,
            28203,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 32821,
            },
            new NodeInfo
            {
                NodeId = 32822,
            },
            new NodeInfo
            {
                NodeId = 32823,
            },
            new NodeInfo
            {
                NodeId = 32824,
            },
            new NodeInfo
            {
                NodeId = 32825,
            },
            new NodeInfo
            {
                NodeId = 32826,
            },
        };
    }
}
