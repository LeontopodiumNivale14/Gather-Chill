using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.ARR.Unknown_0
{
    public class BTN_872 : RouteInfo
    {
        public override uint Id => 872;
        public override uint ExpansionId => 0;
        public override uint ZoneId => 0;
        public override uint GatherType => 2;
        public override Vector2 MapPosition => new Vector2(-237.607f, 855.703f);
        public override int Radius => 25;

        public override HashSet<uint> NodeIds => new()
        {
            34168,
            34169,
            34170,
            34171,
        };

        public override HashSet<uint> ItemIds => new()
        {
            2003151,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 34168,
            },
            new NodeInfo
            {
                NodeId = 34169,
            },
            new NodeInfo
            {
                NodeId = 34170,
            },
            new NodeInfo
            {
                NodeId = 34171,
            },
        };
    }
}
