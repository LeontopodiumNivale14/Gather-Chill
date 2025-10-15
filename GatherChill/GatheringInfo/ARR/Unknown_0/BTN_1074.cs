using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.ARR.Unknown_0
{
    public class BTN_1074 : RouteInfo
    {
        public override uint Id => 1074;
        public override uint ExpansionId => 0;
        public override uint ZoneId => 0;
        public override uint GatherType => 3;
        public override Vector2 MapPosition => new Vector2(192.195f, -200.867f);
        public override int Radius => 50;

        public override HashSet<uint> NodeIds => new()
        {
            35225,
            35226,
            35227,
            35228,
            35229,
            35230,
        };

        public override HashSet<uint> ItemIds => new()
        {
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 35225,
            },
            new NodeInfo
            {
                NodeId = 35226,
            },
            new NodeInfo
            {
                NodeId = 35227,
            },
            new NodeInfo
            {
                NodeId = 35228,
            },
            new NodeInfo
            {
                NodeId = 35229,
            },
            new NodeInfo
            {
                NodeId = 35230,
            },
        };
    }
}
