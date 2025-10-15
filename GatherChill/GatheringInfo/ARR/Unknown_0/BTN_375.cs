using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.ARR.Unknown_0
{
    public class BTN_375 : RouteInfo
    {
        public override uint Id => 375;
        public override uint ExpansionId => 0;
        public override uint ZoneId => 0;
        public override uint GatherType => 2;
        public override Vector2 MapPosition => new Vector2(-321.7f, -364.453f);
        public override int Radius => 289;

        public override HashSet<uint> NodeIds => new()
        {
            31539,
            31541,
            31571,
            31573,
            31603,
            31605,
        };

        public override HashSet<uint> ItemIds => new()
        {
            12,
            12579,
            12586,
            12891,
            13752,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 31539,
            },
            new NodeInfo
            {
                NodeId = 31541,
            },
            new NodeInfo
            {
                NodeId = 31571,
            },
            new NodeInfo
            {
                NodeId = 31573,
            },
            new NodeInfo
            {
                NodeId = 31603,
            },
            new NodeInfo
            {
                NodeId = 31605,
            },
        };
    }
}
