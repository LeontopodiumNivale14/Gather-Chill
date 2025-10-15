using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.ARR.Unknown_0
{
    public class BTN_374 : RouteInfo
    {
        public override uint Id => 374;
        public override uint ExpansionId => 0;
        public override uint ZoneId => 0;
        public override uint GatherType => 2;
        public override Vector2 MapPosition => new Vector2(-127.921f, -499.558f);
        public override int Radius => 131;

        public override HashSet<uint> NodeIds => new()
        {
            31538,
            31540,
            31570,
            31572,
            31602,
            31604,
        };

        public override HashSet<uint> ItemIds => new()
        {
            12,
            12579,
            12586,
            12891,
            13752,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 31538,
            },
            new NodeInfo
            {
                NodeId = 31540,
            },
            new NodeInfo
            {
                NodeId = 31570,
            },
            new NodeInfo
            {
                NodeId = 31572,
            },
            new NodeInfo
            {
                NodeId = 31602,
            },
            new NodeInfo
            {
                NodeId = 31604,
            },
        };
    }
}
