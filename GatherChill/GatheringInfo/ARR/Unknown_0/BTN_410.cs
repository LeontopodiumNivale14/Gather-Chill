using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.ARR.Unknown_0
{
    public class BTN_410 : RouteInfo
    {
        public override uint Id => 410;
        public override uint ExpansionId => 0;
        public override uint ZoneId => 0;
        public override uint GatherType => 3;
        public override Vector2 MapPosition => new Vector2(539.57f, -586.719f);
        public override int Radius => 103;

        public override HashSet<uint> NodeIds => new()
        {
            31750,
            31751,
            31752,
            31753,
        };

        public override HashSet<uint> ItemIds => new()
        {
            4,
            10,
            7594,
            13753,
            17571,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 31750,
            },
            new NodeInfo
            {
                NodeId = 31751,
            },
            new NodeInfo
            {
                NodeId = 31752,
            },
            new NodeInfo
            {
                NodeId = 31753,
            },
        };
    }
}
