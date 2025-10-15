using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.Heavensward.TheDiadem_939
{
    public class BTN_795 : RouteInfo
    {
        public override uint Id => 795;
        public override uint ExpansionId => 1;
        public override uint ZoneId => 939;
        public override uint GatherType => 2;
        public override Vector2 MapPosition => new Vector2(59.9294f, -26.8895f);
        public override int Radius => 827;

        public override HashSet<uint> NodeIds => new()
        {
            33743,
            33750,
            33755,
            33757,
            33762,
            33764,
            33769,
            33776,
            33783,
            33789,
            33794,
            33796,
            33801,
            33808,
            33815,
            33822,
            33827,
            33829,
            33834,
        };

        public override HashSet<uint> ItemIds => new()
        {
            29934,
            31306,
            32005,
            32015,
            32026,
            32035,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 33743,
            },
            new NodeInfo
            {
                NodeId = 33750,
            },
            new NodeInfo
            {
                NodeId = 33755,
            },
            new NodeInfo
            {
                NodeId = 33757,
            },
            new NodeInfo
            {
                NodeId = 33762,
            },
            new NodeInfo
            {
                NodeId = 33764,
            },
            new NodeInfo
            {
                NodeId = 33769,
            },
            new NodeInfo
            {
                NodeId = 33776,
            },
            new NodeInfo
            {
                NodeId = 33783,
            },
            new NodeInfo
            {
                NodeId = 33789,
            },
            new NodeInfo
            {
                NodeId = 33794,
            },
            new NodeInfo
            {
                NodeId = 33796,
            },
            new NodeInfo
            {
                NodeId = 33801,
            },
            new NodeInfo
            {
                NodeId = 33808,
            },
            new NodeInfo
            {
                NodeId = 33815,
            },
            new NodeInfo
            {
                NodeId = 33822,
            },
            new NodeInfo
            {
                NodeId = 33827,
            },
            new NodeInfo
            {
                NodeId = 33829,
            },
            new NodeInfo
            {
                NodeId = 33834,
            },
        };
    }
}
