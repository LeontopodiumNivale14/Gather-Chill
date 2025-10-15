using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.ARR.Unknown_0
{
    public class BTN_412 : RouteInfo
    {
        public override uint Id => 412;
        public override uint ExpansionId => 0;
        public override uint ZoneId => 0;
        public override uint GatherType => 3;
        public override Vector2 MapPosition => new Vector2(-336.125f, -152.65f);
        public override int Radius => 93;

        public override HashSet<uint> NodeIds => new()
        {
            31758,
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
                NodeId = 31758,
            },
        };
    }
}
