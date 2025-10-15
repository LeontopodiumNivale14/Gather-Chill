using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.ARR.Unknown_0
{
    public class BTN_86 : RouteInfo
    {
        public override uint Id => 86;
        public override uint ExpansionId => 0;
        public override uint ZoneId => 0;
        public override uint GatherType => 2;
        public override Vector2 MapPosition => new Vector2(-452.134f, -163.2f);
        public override int Radius => 50;

        public override HashSet<uint> NodeIds => new()
        {
            30634,
            30635,
            30636,
            30637,
        };

        public override HashSet<uint> ItemIds => new()
        {
            2000919,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 30634,
            },
            new NodeInfo
            {
                NodeId = 30635,
            },
            new NodeInfo
            {
                NodeId = 30636,
            },
            new NodeInfo
            {
                NodeId = 30637,
            },
        };
    }
}
