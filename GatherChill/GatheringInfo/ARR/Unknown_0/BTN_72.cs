using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.ARR.Unknown_0
{
    public class BTN_72 : RouteInfo
    {
        public override uint Id => 72;
        public override uint ExpansionId => 0;
        public override uint ZoneId => 0;
        public override uint GatherType => 2;
        public override Vector2 MapPosition => new Vector2(180.124f, -56.0646f);
        public override int Radius => 29;

        public override HashSet<uint> NodeIds => new()
        {
            30274,
            30275,
            30276,
            30277,
        };

        public override HashSet<uint> ItemIds => new()
        {
            2000311,
            2000316,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 30274,
            },
            new NodeInfo
            {
                NodeId = 30275,
            },
            new NodeInfo
            {
                NodeId = 30276,
            },
            new NodeInfo
            {
                NodeId = 30277,
            },
        };
    }
}
