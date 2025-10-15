using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.ARR.Unknown_0
{
    public class BTN_408 : RouteInfo
    {
        public override uint Id => 408;
        public override uint ExpansionId => 0;
        public override uint ZoneId => 0;
        public override uint GatherType => 3;
        public override Vector2 MapPosition => new Vector2(453.269f, 383.584f);
        public override int Radius => 189;

        public override HashSet<uint> NodeIds => new()
        {
            31742,
            31743,
            31744,
            31745,
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
                NodeId = 31742,
            },
            new NodeInfo
            {
                NodeId = 31743,
            },
            new NodeInfo
            {
                NodeId = 31744,
            },
            new NodeInfo
            {
                NodeId = 31745,
            },
        };
    }
}
