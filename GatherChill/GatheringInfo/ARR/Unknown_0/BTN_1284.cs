using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.ARR.Unknown_0
{
    public class BTN_1284 : RouteInfo
    {
        public override uint Id => 1284;
        public override uint ExpansionId => 0;
        public override uint ZoneId => 0;
        public override uint GatherType => 3;
        public override Vector2 MapPosition => new Vector2(652.32f, 424.825f);
        public override int Radius => 76;

        public override HashSet<uint> NodeIds => new()
        {
            35429,
            35430,
            35431,
            35432,
            35433,
            35434,
        };

        public override HashSet<uint> ItemIds => new()
        {
            47400,
            47405,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 35429,
            },
            new NodeInfo
            {
                NodeId = 35430,
            },
            new NodeInfo
            {
                NodeId = 35431,
            },
            new NodeInfo
            {
                NodeId = 35432,
            },
            new NodeInfo
            {
                NodeId = 35433,
            },
            new NodeInfo
            {
                NodeId = 35434,
            },
        };
    }
}
