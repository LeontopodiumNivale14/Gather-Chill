using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.ARR.Unknown_0
{
    public class BTN_61 : RouteInfo
    {
        public override uint Id => 61;
        public override uint ExpansionId => 0;
        public override uint ZoneId => 0;
        public override uint GatherType => 2;
        public override Vector2 MapPosition => new Vector2(-64.4658f, -66.2429f);
        public override int Radius => 18;

        public override HashSet<uint> NodeIds => new()
        {
            30234,
            30235,
            30236,
            30237,
        };

        public override HashSet<uint> ItemIds => new()
        {
            2000289,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 30234,
            },
            new NodeInfo
            {
                NodeId = 30235,
            },
            new NodeInfo
            {
                NodeId = 30236,
            },
            new NodeInfo
            {
                NodeId = 30237,
            },
        };
    }
}
