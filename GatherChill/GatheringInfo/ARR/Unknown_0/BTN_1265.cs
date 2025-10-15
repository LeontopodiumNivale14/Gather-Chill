using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.ARR.Unknown_0
{
    public class BTN_1265 : RouteInfo
    {
        public override uint Id => 1265;
        public override uint ExpansionId => 0;
        public override uint ZoneId => 0;
        public override uint GatherType => 2;
        public override Vector2 MapPosition => new Vector2(310.054f, 18.2871f);
        public override int Radius => 81;

        public override HashSet<uint> NodeIds => new()
        {
            35395,
            35396,
            35397,
            35398,
            35399,
            35400,
        };

        public override HashSet<uint> ItemIds => new()
        {
            47390,
            47391,
            47392,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 35395,
            },
            new NodeInfo
            {
                NodeId = 35396,
            },
            new NodeInfo
            {
                NodeId = 35397,
            },
            new NodeInfo
            {
                NodeId = 35398,
            },
            new NodeInfo
            {
                NodeId = 35399,
            },
            new NodeInfo
            {
                NodeId = 35400,
            },
        };
    }
}
