using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.Heavensward.TheDiadem_939
{
    public class BTN_793 : RouteInfo
    {
        public override uint Id => 793;
        public override uint ExpansionId => 1;
        public override uint ZoneId => 939;
        public override uint GatherType => 3;
        public override Vector2 MapPosition => new Vector2(88.0376f, -76.9201f);
        public override int Radius => 842;

        public override HashSet<uint> NodeIds => new()
        {
            33741,
            33746,
            33748,
            33753,
            33760,
            33767,
            33774,
            33779,
            33781,
            33785,
            33791,
            33798,
            33803,
            33805,
            33810,
            33812,
            33817,
            33824,
            33831,
        };

        public override HashSet<uint> ItemIds => new()
        {
            29937,
            31309,
            32010,
            32018,
            32029,
            32038,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 33741,
            },
            new NodeInfo
            {
                NodeId = 33746,
            },
            new NodeInfo
            {
                NodeId = 33748,
            },
            new NodeInfo
            {
                NodeId = 33753,
            },
            new NodeInfo
            {
                NodeId = 33760,
            },
            new NodeInfo
            {
                NodeId = 33767,
            },
            new NodeInfo
            {
                NodeId = 33774,
            },
            new NodeInfo
            {
                NodeId = 33779,
            },
            new NodeInfo
            {
                NodeId = 33781,
            },
            new NodeInfo
            {
                NodeId = 33785,
            },
            new NodeInfo
            {
                NodeId = 33791,
            },
            new NodeInfo
            {
                NodeId = 33798,
            },
            new NodeInfo
            {
                NodeId = 33803,
            },
            new NodeInfo
            {
                NodeId = 33805,
            },
            new NodeInfo
            {
                NodeId = 33810,
            },
            new NodeInfo
            {
                NodeId = 33812,
            },
            new NodeInfo
            {
                NodeId = 33817,
            },
            new NodeInfo
            {
                NodeId = 33824,
            },
            new NodeInfo
            {
                NodeId = 33831,
            },
        };
    }
}
