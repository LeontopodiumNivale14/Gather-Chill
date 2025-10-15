using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.ARR.Unknown_0
{
    public class BTN_257 : RouteInfo
    {
        public override uint Id => 257;
        public override uint ExpansionId => 0;
        public override uint ZoneId => 0;
        public override uint GatherType => 3;
        public override Vector2 MapPosition => new Vector2(700.958f, 219.109f);
        public override int Radius => 96;

        public override HashSet<uint> NodeIds => new()
        {
            31095,
            31096,
            31097,
            31098,
            31099,
            31100,
            31101,
            31102,
            31103,
            31104,
            31105,
            31106,
        };

        public override HashSet<uint> ItemIds => new()
        {
            2001852,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 31095,
            },
            new NodeInfo
            {
                NodeId = 31096,
            },
            new NodeInfo
            {
                NodeId = 31097,
            },
            new NodeInfo
            {
                NodeId = 31098,
            },
            new NodeInfo
            {
                NodeId = 31099,
            },
            new NodeInfo
            {
                NodeId = 31100,
            },
            new NodeInfo
            {
                NodeId = 31101,
            },
            new NodeInfo
            {
                NodeId = 31102,
            },
            new NodeInfo
            {
                NodeId = 31103,
            },
            new NodeInfo
            {
                NodeId = 31104,
            },
            new NodeInfo
            {
                NodeId = 31105,
            },
            new NodeInfo
            {
                NodeId = 31106,
            },
        };
    }
}
