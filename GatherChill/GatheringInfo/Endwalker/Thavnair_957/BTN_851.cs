using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.Endwalker.Thavnair_957
{
    public class BTN_851 : RouteInfo
    {
        public override uint Id => 851;
        public override uint ExpansionId => 4;
        public override uint ZoneId => 957;
        public override uint GatherType => 2;
        public override Vector2 MapPosition => new Vector2(-355.353f, -347.8f);
        public override int Radius => 5;

        public override HashSet<uint> NodeIds => new()
        {
            34038,
        };

        public override HashSet<uint> ItemIds => new()
        {
            36303,
            36305,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 34038,
            },
        };
    }
}
