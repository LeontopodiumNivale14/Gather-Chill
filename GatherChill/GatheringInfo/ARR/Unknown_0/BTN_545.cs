using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.ARR.Unknown_0
{
    public class BTN_545 : RouteInfo
    {
        public override uint Id => 545;
        public override uint ExpansionId => 0;
        public override uint ZoneId => 0;
        public override uint GatherType => 3;
        public override Vector2 MapPosition => new Vector2(-516.215f, -377.633f);
        public override int Radius => 14;

        public override HashSet<uint> NodeIds => new()
        {
            32326,
            32327,
            32328,
        };

        public override HashSet<uint> ItemIds => new()
        {
            12,
            22642,
            22649,
            22650,
            22651,
            22667,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 32326,
            },
            new NodeInfo
            {
                NodeId = 32327,
            },
            new NodeInfo
            {
                NodeId = 32328,
            },
        };
    }
}
