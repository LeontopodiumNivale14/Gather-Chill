using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.ARR.Unknown_0
{
    public class BTN_399 : RouteInfo
    {
        public override uint Id => 399;
        public override uint ExpansionId => 0;
        public override uint ZoneId => 0;
        public override uint GatherType => 3;
        public override Vector2 MapPosition => new Vector2(-105.207f, 44.716f);
        public override int Radius => 17;

        public override HashSet<uint> NodeIds => new()
        {
            34355,
            34356,
            34357,
        };

        public override HashSet<uint> ItemIds => new()
        {
            13,
            38317,
            38320,
            38322,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 34355,
            },
            new NodeInfo
            {
                NodeId = 34356,
            },
            new NodeInfo
            {
                NodeId = 34357,
            },
        };
    }
}
