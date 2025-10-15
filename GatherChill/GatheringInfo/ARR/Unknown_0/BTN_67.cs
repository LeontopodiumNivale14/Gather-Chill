using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.ARR.Unknown_0
{
    public class BTN_67 : RouteInfo
    {
        public override uint Id => 67;
        public override uint ExpansionId => 0;
        public override uint ZoneId => 0;
        public override uint GatherType => 3;
        public override Vector2 MapPosition => new Vector2(-244.428f, 233.421f);
        public override int Radius => 14;

        public override HashSet<uint> NodeIds => new()
        {
            30142,
            30143,
        };

        public override HashSet<uint> ItemIds => new()
        {
            2000303,
            2000308,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 30142,
            },
            new NodeInfo
            {
                NodeId = 30143,
            },
        };
    }
}
