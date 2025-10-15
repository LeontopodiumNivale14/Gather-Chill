using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.ARR.Unknown_0
{
    public class BTN_860 : RouteInfo
    {
        public override uint Id => 860;
        public override uint ExpansionId => 0;
        public override uint ZoneId => 0;
        public override uint GatherType => 3;
        public override Vector2 MapPosition => new Vector2(571.603f, -375.796f);
        public override int Radius => 23;

        public override HashSet<uint> NodeIds => new()
        {
            34066,
            34067,
            34068,
            34069,
        };

        public override HashSet<uint> ItemIds => new()
        {
            2003133,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 34066,
            },
            new NodeInfo
            {
                NodeId = 34067,
            },
            new NodeInfo
            {
                NodeId = 34068,
            },
            new NodeInfo
            {
                NodeId = 34069,
            },
        };
    }
}
