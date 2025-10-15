using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.ARR.Unknown_0
{
    public class BTN_873 : RouteInfo
    {
        public override uint Id => 873;
        public override uint ExpansionId => 0;
        public override uint ZoneId => 0;
        public override uint GatherType => 2;
        public override Vector2 MapPosition => new Vector2(-788.704f, 493.733f);
        public override int Radius => 53;

        public override HashSet<uint> NodeIds => new()
        {
            34172,
            34173,
            34174,
            34175,
            34176,
            34177,
        };

        public override HashSet<uint> ItemIds => new()
        {
            2003152,
            2003153,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 34172,
            },
            new NodeInfo
            {
                NodeId = 34173,
            },
            new NodeInfo
            {
                NodeId = 34174,
            },
            new NodeInfo
            {
                NodeId = 34175,
            },
            new NodeInfo
            {
                NodeId = 34176,
            },
            new NodeInfo
            {
                NodeId = 34177,
            },
        };
    }
}
