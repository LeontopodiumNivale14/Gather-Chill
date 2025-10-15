using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.Endwalker.Labyrinthos_956
{
    public class BTN_837 : RouteInfo
    {
        public override uint Id => 837;
        public override uint ExpansionId => 4;
        public override uint ZoneId => 956;
        public override uint GatherType => 2;
        public override Vector2 MapPosition => new Vector2(-297.178f, -745.497f);
        public override int Radius => 126;

        public override HashSet<uint> NodeIds => new()
        {
            33970,
            33971,
            33972,
            33973,
            33974,
            33975,
        };

        public override HashSet<uint> ItemIds => new()
        {
            10,
            36190,
            36672,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 33970,
            },
            new NodeInfo
            {
                NodeId = 33971,
            },
            new NodeInfo
            {
                NodeId = 33972,
            },
            new NodeInfo
            {
                NodeId = 33973,
            },
            new NodeInfo
            {
                NodeId = 33974,
            },
            new NodeInfo
            {
                NodeId = 33975,
            },
        };
    }
}
