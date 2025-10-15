using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.ARR.Unknown_0
{
    public class BTN_91 : RouteInfo
    {
        public override uint Id => 91;
        public override uint ExpansionId => 0;
        public override uint ZoneId => 0;
        public override uint GatherType => 3;
        public override Vector2 MapPosition => new Vector2(468.08f, -414.915f);
        public override int Radius => 79;

        public override HashSet<uint> NodeIds => new()
        {
            30668,
            30669,
            30670,
            30671,
            30672,
            30673,
            30674,
            30675,
            30676,
            30677,
        };

        public override HashSet<uint> ItemIds => new()
        {
            2000926,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 30668,
            },
            new NodeInfo
            {
                NodeId = 30669,
            },
            new NodeInfo
            {
                NodeId = 30670,
            },
            new NodeInfo
            {
                NodeId = 30671,
            },
            new NodeInfo
            {
                NodeId = 30672,
            },
            new NodeInfo
            {
                NodeId = 30673,
            },
            new NodeInfo
            {
                NodeId = 30674,
            },
            new NodeInfo
            {
                NodeId = 30675,
            },
            new NodeInfo
            {
                NodeId = 30676,
            },
            new NodeInfo
            {
                NodeId = 30677,
            },
        };
    }
}
