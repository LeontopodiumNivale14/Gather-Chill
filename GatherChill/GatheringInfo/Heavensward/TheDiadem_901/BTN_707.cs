using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.Heavensward.TheDiadem_901
{
    public class BTN_707 : RouteInfo
    {
        public override uint Id => 707;
        public override uint ExpansionId => 1;
        public override uint ZoneId => 901;
        public override uint GatherType => 2;
        public override Vector2 MapPosition => new Vector2(99.8117f, -86.9026f);
        public override int Radius => 844;

        public override HashSet<uint> NodeIds => new()
        {
            33137,
            33144,
            33151,
            33157,
            33163,
            33170,
            33182,
            33189,
            33200,
            33208,
            33215,
            33221,
            33227,
        };

        public override HashSet<uint> ItemIds => new()
        {
            29898,
            29905,
            29915,
            29925,
            29935,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 33137,
            },
            new NodeInfo
            {
                NodeId = 33144,
            },
            new NodeInfo
            {
                NodeId = 33151,
            },
            new NodeInfo
            {
                NodeId = 33157,
            },
            new NodeInfo
            {
                NodeId = 33163,
            },
            new NodeInfo
            {
                NodeId = 33170,
            },
            new NodeInfo
            {
                NodeId = 33182,
            },
            new NodeInfo
            {
                NodeId = 33189,
            },
            new NodeInfo
            {
                NodeId = 33200,
            },
            new NodeInfo
            {
                NodeId = 33208,
            },
            new NodeInfo
            {
                NodeId = 33215,
            },
            new NodeInfo
            {
                NodeId = 33221,
            },
            new NodeInfo
            {
                NodeId = 33227,
            },
        };
    }
}
