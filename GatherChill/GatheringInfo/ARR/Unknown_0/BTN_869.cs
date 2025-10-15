using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.ARR.Unknown_0
{
    public class BTN_869 : RouteInfo
    {
        public override uint Id => 869;
        public override uint ExpansionId => 0;
        public override uint ZoneId => 0;
        public override uint GatherType => 3;
        public override Vector2 MapPosition => new Vector2(303.948f, 378.039f);
        public override int Radius => 45;

        public override HashSet<uint> NodeIds => new()
        {
            34146,
            34147,
            34148,
            34149,
        };

        public override HashSet<uint> ItemIds => new()
        {
            2003146,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 34146,
            },
            new NodeInfo
            {
                NodeId = 34147,
            },
            new NodeInfo
            {
                NodeId = 34148,
            },
            new NodeInfo
            {
                NodeId = 34149,
            },
        };
    }
}
