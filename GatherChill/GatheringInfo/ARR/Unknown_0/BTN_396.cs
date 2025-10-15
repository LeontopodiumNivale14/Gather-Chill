using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.ARR.Unknown_0
{
    public class BTN_396 : RouteInfo
    {
        public override uint Id => 396;
        public override uint ExpansionId => 0;
        public override uint ZoneId => 0;
        public override uint GatherType => 3;
        public override Vector2 MapPosition => new Vector2(14.7303f, -137.286f);
        public override int Radius => 7;

        public override HashSet<uint> NodeIds => new()
        {
            34349,
            34350,
            34351,
        };

        public override HashSet<uint> ItemIds => new()
        {
            13,
            38311,
            38313,
            38323,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 34349,
            },
            new NodeInfo
            {
                NodeId = 34350,
            },
            new NodeInfo
            {
                NodeId = 34351,
            },
        };
    }
}
