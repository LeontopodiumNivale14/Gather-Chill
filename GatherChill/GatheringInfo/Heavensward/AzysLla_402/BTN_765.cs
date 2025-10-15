using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.Heavensward.AzysLla_402
{
    public class BTN_765 : RouteInfo
    {
        public override uint Id => 765;
        public override uint ExpansionId => 1;
        public override uint ZoneId => 402;
        public override uint GatherType => 2;
        public override Vector2 MapPosition => new Vector2(-540.028f, 213.857f);
        public override int Radius => 81;

        public override HashSet<uint> NodeIds => new()
        {
            33600,
            33601,
            33602,
            33603,
            33604,
            33605,
        };

        public override HashSet<uint> ItemIds => new()
        {
            31766,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 33600,
            },
            new NodeInfo
            {
                NodeId = 33601,
            },
            new NodeInfo
            {
                NodeId = 33602,
            },
            new NodeInfo
            {
                NodeId = 33603,
            },
            new NodeInfo
            {
                NodeId = 33604,
            },
            new NodeInfo
            {
                NodeId = 33605,
            },
        };
    }
}
