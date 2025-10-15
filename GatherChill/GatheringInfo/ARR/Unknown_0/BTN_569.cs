using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.ARR.Unknown_0
{
    public class BTN_569 : RouteInfo
    {
        public override uint Id => 569;
        public override uint ExpansionId => 0;
        public override uint ZoneId => 0;
        public override uint GatherType => 3;
        public override Vector2 MapPosition => new Vector2(-429.561f, -457.315f);
        public override int Radius => 29;

        public override HashSet<uint> NodeIds => new()
        {
            32456,
            32457,
            32458,
            32459,
        };

        public override HashSet<uint> ItemIds => new()
        {
            2002774,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 32456,
            },
            new NodeInfo
            {
                NodeId = 32457,
            },
            new NodeInfo
            {
                NodeId = 32458,
            },
            new NodeInfo
            {
                NodeId = 32459,
            },
        };
    }
}
