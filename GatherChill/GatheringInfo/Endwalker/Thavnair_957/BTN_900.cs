using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.Endwalker.Thavnair_957
{
    public class BTN_900 : RouteInfo
    {
        public override uint Id => 900;
        public override uint ExpansionId => 4;
        public override uint ZoneId => 957;
        public override uint GatherType => 5;
        public override Vector2 MapPosition => new Vector2(-738.249f, 753.881f);
        public override int Radius => 1;

        public override HashSet<uint> NodeIds => new()
        {
            34333,
        };

        public override HashSet<uint> ItemIds => new()
        {
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 34333,
            },
        };
    }
}
