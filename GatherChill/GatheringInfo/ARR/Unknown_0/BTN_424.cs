using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.ARR.Unknown_0
{
    public class BTN_424 : RouteInfo
    {
        public override uint Id => 424;
        public override uint ExpansionId => 0;
        public override uint ZoneId => 0;
        public override uint GatherType => 2;
        public override Vector2 MapPosition => new Vector2(287.994f, 284.311f);
        public override int Radius => 81;

        public override HashSet<uint> NodeIds => new()
        {
            31788,
            31789,
            31790,
        };

        public override HashSet<uint> ItemIds => new()
        {
            4,
            10,
            5394,
            7592,
            13752,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 31788,
            },
            new NodeInfo
            {
                NodeId = 31789,
            },
            new NodeInfo
            {
                NodeId = 31790,
            },
        };
    }
}
