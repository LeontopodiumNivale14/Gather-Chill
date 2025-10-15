using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.ARR.Unknown_0
{
    public class BTN_414 : RouteInfo
    {
        public override uint Id => 414;
        public override uint ExpansionId => 0;
        public override uint ZoneId => 0;
        public override uint GatherType => 3;
        public override Vector2 MapPosition => new Vector2(343.451f, -384.188f);
        public override int Radius => 54;

        public override HashSet<uint> NodeIds => new()
        {
            31760,
        };

        public override HashSet<uint> ItemIds => new()
        {
            16,
            17,
            19,
            12882,
            12900,
            13753,
            17571,
            17689,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 31760,
            },
        };
    }
}
