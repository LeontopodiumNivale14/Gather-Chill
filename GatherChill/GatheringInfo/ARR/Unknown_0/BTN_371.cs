using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.ARR.Unknown_0
{
    public class BTN_371 : RouteInfo
    {
        public override uint Id => 371;
        public override uint ExpansionId => 0;
        public override uint ZoneId => 0;
        public override uint GatherType => 2;
        public override Vector2 MapPosition => new Vector2(645.466f, -276.286f);
        public override int Radius => 33;

        public override HashSet<uint> NodeIds => new()
        {
            31531,
            31533,
            31563,
            31595,
        };

        public override HashSet<uint> ItemIds => new()
        {
            13,
            12579,
            12586,
            12891,
            13752,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 31531,
            },
            new NodeInfo
            {
                NodeId = 31533,
            },
            new NodeInfo
            {
                NodeId = 31563,
            },
            new NodeInfo
            {
                NodeId = 31595,
            },
        };
    }
}
