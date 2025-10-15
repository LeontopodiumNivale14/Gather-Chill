using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.Heavensward.TheDravanianHinterlands_399
{
    public class BTN_809 : RouteInfo
    {
        public override uint Id => 809;
        public override uint ExpansionId => 1;
        public override uint ZoneId => 399;
        public override uint GatherType => 2;
        public override Vector2 MapPosition => new Vector2(763.043f, 107.23f);
        public override int Radius => 135;

        public override HashSet<uint> NodeIds => new()
        {
            33861,
            33862,
            33863,
            33864,
            33865,
            33866,
        };

        public override HashSet<uint> ItemIds => new()
        {
            10,
            33229,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 33861,
            },
            new NodeInfo
            {
                NodeId = 33862,
            },
            new NodeInfo
            {
                NodeId = 33863,
            },
            new NodeInfo
            {
                NodeId = 33864,
            },
            new NodeInfo
            {
                NodeId = 33865,
            },
            new NodeInfo
            {
                NodeId = 33866,
            },
        };
    }
}
