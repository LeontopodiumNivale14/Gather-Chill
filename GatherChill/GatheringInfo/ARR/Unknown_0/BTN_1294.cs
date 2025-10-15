using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.ARR.Unknown_0
{
    public class BTN_1294 : RouteInfo
    {
        public override uint Id => 1294;
        public override uint ExpansionId => 0;
        public override uint ZoneId => 0;
        public override uint GatherType => 2;
        public override Vector2 MapPosition => new Vector2(-560.081f, -657.904f);
        public override int Radius => 24;

        public override HashSet<uint> NodeIds => new()
        {
            35447,
            35448,
            35449,
            35450,
            35451,
            35452,
        };

        public override HashSet<uint> ItemIds => new()
        {
            47420,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 35447,
            },
            new NodeInfo
            {
                NodeId = 35448,
            },
            new NodeInfo
            {
                NodeId = 35449,
            },
            new NodeInfo
            {
                NodeId = 35450,
            },
            new NodeInfo
            {
                NodeId = 35451,
            },
            new NodeInfo
            {
                NodeId = 35452,
            },
        };
    }
}
