using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.ARR.Unknown_0
{
    public class BTN_912 : RouteInfo
    {
        public override uint Id => 912;
        public override uint ExpansionId => 0;
        public override uint ZoneId => 0;
        public override uint GatherType => 3;
        public override Vector2 MapPosition => new Vector2(27.2247f, 694.231f);
        public override int Radius => 6;

        public override HashSet<uint> NodeIds => new()
        {
            34385,
            34386,
            34387,
        };

        public override HashSet<uint> ItemIds => new()
        {
            13,
            38302,
            38307,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 34385,
            },
            new NodeInfo
            {
                NodeId = 34386,
            },
            new NodeInfo
            {
                NodeId = 34387,
            },
        };
    }
}
