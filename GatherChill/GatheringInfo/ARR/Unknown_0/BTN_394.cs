using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.ARR.Unknown_0
{
    public class BTN_394 : RouteInfo
    {
        public override uint Id => 394;
        public override uint ExpansionId => 0;
        public override uint ZoneId => 0;
        public override uint GatherType => 3;
        public override Vector2 MapPosition => new Vector2(-18.8229f, -65.2838f);
        public override int Radius => 12;

        public override HashSet<uint> NodeIds => new()
        {
            34334,
            34347,
            34348,
        };

        public override HashSet<uint> ItemIds => new()
        {
            13,
            38309,
            38312,
            38316,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 34334,
            },
            new NodeInfo
            {
                NodeId = 34347,
            },
            new NodeInfo
            {
                NodeId = 34348,
            },
        };
    }
}
