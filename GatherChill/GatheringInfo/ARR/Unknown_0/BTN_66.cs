using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.ARR.Unknown_0
{
    public class BTN_66 : RouteInfo
    {
        public override uint Id => 66;
        public override uint ExpansionId => 0;
        public override uint ZoneId => 0;
        public override uint GatherType => 2;
        public override Vector2 MapPosition => new Vector2(-226.135f, 200.977f);
        public override int Radius => 24;

        public override HashSet<uint> NodeIds => new()
        {
            30138,
            30139,
            30140,
            30141,
        };

        public override HashSet<uint> ItemIds => new()
        {
            2000300,
            2000302,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 30138,
            },
            new NodeInfo
            {
                NodeId = 30139,
            },
            new NodeInfo
            {
                NodeId = 30140,
            },
            new NodeInfo
            {
                NodeId = 30141,
            },
        };
    }
}
