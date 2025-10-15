using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.ARR.Unknown_0
{
    public class BTN_957 : RouteInfo
    {
        public override uint Id => 957;
        public override uint ExpansionId => 0;
        public override uint ZoneId => 0;
        public override uint GatherType => 2;
        public override Vector2 MapPosition => new Vector2(622.373f, 325.135f);
        public override int Radius => 46;

        public override HashSet<uint> NodeIds => new()
        {
            34615,
            34616,
            34617,
            34618,
        };

        public override HashSet<uint> ItemIds => new()
        {
            2003533,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 34615,
            },
            new NodeInfo
            {
                NodeId = 34616,
            },
            new NodeInfo
            {
                NodeId = 34617,
            },
            new NodeInfo
            {
                NodeId = 34618,
            },
        };
    }
}
