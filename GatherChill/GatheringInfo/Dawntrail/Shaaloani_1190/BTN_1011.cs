using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.Dawntrail.Shaaloani_1190
{
    public class BTN_1011 : RouteInfo
    {
        public override uint Id => 1011;
        public override uint ExpansionId => 5;
        public override uint ZoneId => 1190;
        public override uint GatherType => 5;
        public override Vector2 MapPosition => new Vector2(521.243f, -510.49f);
        public override int Radius => 97;

        public override HashSet<uint> NodeIds => new()
        {
            34953,
            34954,
            34955,
        };

        public override HashSet<uint> ItemIds => new()
        {
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 34953,
            },
            new NodeInfo
            {
                NodeId = 34954,
            },
            new NodeInfo
            {
                NodeId = 34955,
            },
        };
    }
}
