using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.ARR.Unknown_0
{
    public class BTN_690 : RouteInfo
    {
        public override uint Id => 690;
        public override uint ExpansionId => 0;
        public override uint ZoneId => 0;
        public override uint GatherType => 3;
        public override Vector2 MapPosition => new Vector2(-665.466f, 603.872f);
        public override int Radius => 23;

        public override HashSet<uint> NodeIds => new()
        {
            33006,
            33007,
            33008,
        };

        public override HashSet<uint> ItemIds => new()
        {
            10,
            29542,
            29547,
            29549,
            29556,
            29562,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 33006,
            },
            new NodeInfo
            {
                NodeId = 33007,
            },
            new NodeInfo
            {
                NodeId = 33008,
            },
        };
    }
}
