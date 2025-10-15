using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.ARR.Unknown_0
{
    public class BTN_260 : RouteInfo
    {
        public override uint Id => 260;
        public override uint ExpansionId => 0;
        public override uint ZoneId => 0;
        public override uint GatherType => 3;
        public override Vector2 MapPosition => new Vector2(378.892f, 658.293f);
        public override int Radius => 26;

        public override HashSet<uint> NodeIds => new()
        {
            31123,
            31124,
            31125,
            31126,
        };

        public override HashSet<uint> ItemIds => new()
        {
            2001856,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 31123,
            },
            new NodeInfo
            {
                NodeId = 31124,
            },
            new NodeInfo
            {
                NodeId = 31125,
            },
            new NodeInfo
            {
                NodeId = 31126,
            },
        };
    }
}
