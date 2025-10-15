using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.ARR.Unknown_0
{
    public class BTN_427 : RouteInfo
    {
        public override uint Id => 427;
        public override uint ExpansionId => 0;
        public override uint ZoneId => 0;
        public override uint GatherType => 3;
        public override Vector2 MapPosition => new Vector2(275.855f, 231.225f);
        public override int Radius => 159;

        public override HashSet<uint> NodeIds => new()
        {
            31797,
            31798,
            31799,
            31800,
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
                NodeId = 31797,
            },
            new NodeInfo
            {
                NodeId = 31798,
            },
            new NodeInfo
            {
                NodeId = 31799,
            },
            new NodeInfo
            {
                NodeId = 31800,
            },
        };
    }
}
