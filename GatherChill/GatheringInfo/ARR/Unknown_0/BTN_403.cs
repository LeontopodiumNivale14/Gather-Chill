using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.ARR.Unknown_0
{
    public class BTN_403 : RouteInfo
    {
        public override uint Id => 403;
        public override uint ExpansionId => 0;
        public override uint ZoneId => 0;
        public override uint GatherType => 2;
        public override Vector2 MapPosition => new Vector2(475.297f, 366.936f);
        public override int Radius => 182;

        public override HashSet<uint> NodeIds => new()
        {
            31722,
            31723,
            31724,
            31725,
        };

        public override HashSet<uint> ItemIds => new()
        {
            7,
            13,
            7590,
            9518,
            13752,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 31722,
            },
            new NodeInfo
            {
                NodeId = 31723,
            },
            new NodeInfo
            {
                NodeId = 31724,
            },
            new NodeInfo
            {
                NodeId = 31725,
            },
        };
    }
}
