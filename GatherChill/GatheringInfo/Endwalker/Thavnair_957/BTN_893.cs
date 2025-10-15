using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.Endwalker.Thavnair_957
{
    public class BTN_893 : RouteInfo
    {
        public override uint Id => 893;
        public override uint ExpansionId => 4;
        public override uint ZoneId => 957;
        public override uint GatherType => 5;
        public override Vector2 MapPosition => new Vector2(-666.517f, 561.352f);
        public override int Radius => 128;

        public override HashSet<uint> NodeIds => new()
        {
            34314,
            34315,
            34316,
        };

        public override HashSet<uint> ItemIds => new()
        {
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 34314,
            },
            new NodeInfo
            {
                NodeId = 34315,
            },
            new NodeInfo
            {
                NodeId = 34316,
            },
        };
    }
}
