using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.Stormblood.TheLochs_621
{
    public class MIN_737 : RouteInfo
    {
        public override uint Id => 737;
        public override uint ExpansionId => 2;
        public override uint ZoneId => 621;
        public override uint GatherType => 0;
        public override Vector2 MapPosition => new Vector2(-872.069f, 308.926f);
        public override int Radius => 112;

        public override HashSet<uint> NodeIds => new()
        {
            33362,
            33363,
            33364,
            33365,
            33366,
            33367,
        };

        public override HashSet<uint> ItemIds => new()
        {
            31127,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 33362,
            },
            new NodeInfo
            {
                NodeId = 33363,
            },
            new NodeInfo
            {
                NodeId = 33364,
            },
            new NodeInfo
            {
                NodeId = 33365,
            },
            new NodeInfo
            {
                NodeId = 33366,
            },
            new NodeInfo
            {
                NodeId = 33367,
            },
        };
    }
}
