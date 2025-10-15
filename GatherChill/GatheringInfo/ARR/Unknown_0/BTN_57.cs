using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.ARR.Unknown_0
{
    public class BTN_57 : RouteInfo
    {
        public override uint Id => 57;
        public override uint ExpansionId => 0;
        public override uint ZoneId => 0;
        public override uint GatherType => 2;
        public override Vector2 MapPosition => new Vector2(243.343f, 40.5581f);
        public override int Radius => 27;

        public override HashSet<uint> NodeIds => new()
        {
            30200,
            30201,
            30202,
            30203,
        };

        public override HashSet<uint> ItemIds => new()
        {
            2000281,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 30200,
            },
            new NodeInfo
            {
                NodeId = 30201,
            },
            new NodeInfo
            {
                NodeId = 30202,
            },
            new NodeInfo
            {
                NodeId = 30203,
            },
        };
    }
}
