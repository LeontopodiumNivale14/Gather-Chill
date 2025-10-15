using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.ARR.Unknown_0
{
    public class BTN_404 : RouteInfo
    {
        public override uint Id => 404;
        public override uint ExpansionId => 0;
        public override uint ZoneId => 0;
        public override uint GatherType => 2;
        public override Vector2 MapPosition => new Vector2(654.935f, -202.156f);
        public override int Radius => 105;

        public override HashSet<uint> NodeIds => new()
        {
            31726,
            31727,
            31728,
            31729,
        };

        public override HashSet<uint> ItemIds => new()
        {
            4,
            10,
            5394,
            13752,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 31726,
            },
            new NodeInfo
            {
                NodeId = 31727,
            },
            new NodeInfo
            {
                NodeId = 31728,
            },
            new NodeInfo
            {
                NodeId = 31729,
            },
        };
    }
}
