using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.ARR.Unknown_0
{
    public class BTN_433 : RouteInfo
    {
        public override uint Id => 433;
        public override uint ExpansionId => 0;
        public override uint ZoneId => 0;
        public override uint GatherType => 2;
        public override Vector2 MapPosition => new Vector2(23.7449f, -378.315f);
        public override int Radius => 120;

        public override HashSet<uint> NodeIds => new()
        {
            31823,
            31824,
            31825,
            31826,
            31827,
            31828,
            31829,
            31830,
            31831,
            31832,
            31833,
            31834,
        };

        public override HashSet<uint> ItemIds => new()
        {
            2002120,
            2002121,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 31823,
            },
            new NodeInfo
            {
                NodeId = 31824,
            },
            new NodeInfo
            {
                NodeId = 31825,
            },
            new NodeInfo
            {
                NodeId = 31826,
            },
            new NodeInfo
            {
                NodeId = 31827,
            },
            new NodeInfo
            {
                NodeId = 31828,
            },
            new NodeInfo
            {
                NodeId = 31829,
            },
            new NodeInfo
            {
                NodeId = 31830,
            },
            new NodeInfo
            {
                NodeId = 31831,
            },
            new NodeInfo
            {
                NodeId = 31832,
            },
            new NodeInfo
            {
                NodeId = 31833,
            },
            new NodeInfo
            {
                NodeId = 31834,
            },
        };
    }
}
