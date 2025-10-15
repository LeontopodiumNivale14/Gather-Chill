using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.ARR.Unknown_0
{
    public class BTN_428 : RouteInfo
    {
        public override uint Id => 428;
        public override uint ExpansionId => 0;
        public override uint ZoneId => 0;
        public override uint GatherType => 3;
        public override Vector2 MapPosition => new Vector2(290.56f, -283.233f);
        public override int Radius => 101;

        public override HashSet<uint> NodeIds => new()
        {
            31801,
            31802,
            31803,
        };

        public override HashSet<uint> ItemIds => new()
        {
            5,
            11,
            5545,
            13753,
            17571,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 31801,
            },
            new NodeInfo
            {
                NodeId = 31802,
            },
            new NodeInfo
            {
                NodeId = 31803,
            },
        };
    }
}
