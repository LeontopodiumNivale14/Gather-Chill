using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.ARR.Unknown_0
{
    public class BTN_1204 : RouteInfo
    {
        public override uint Id => 1204;
        public override uint ExpansionId => 0;
        public override uint ZoneId => 0;
        public override uint GatherType => 2;
        public override Vector2 MapPosition => new Vector2(-92.5189f, -783.81f);
        public override int Radius => 29;

        public override HashSet<uint> NodeIds => new()
        {
            35273,
            35274,
            35275,
        };

        public override HashSet<uint> ItemIds => new()
        {
            13,
            48034,
            48043,
            48064,
            48082,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 35273,
            },
            new NodeInfo
            {
                NodeId = 35274,
            },
            new NodeInfo
            {
                NodeId = 35275,
            },
        };
    }
}
