using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.ARR.Unknown_0
{
    public class MIN_587 : RouteInfo
    {
        public override uint Id => 587;
        public override uint ExpansionId => 0;
        public override uint ZoneId => 0;
        public override uint GatherType => 0;
        public override Vector2 MapPosition => new Vector2(562.066f, -44.0172f);
        public override int Radius => 38;

        public override HashSet<uint> NodeIds => new()
        {
            32602,
            32603,
            32604,
            32605,
            32606,
            32607,
            32608,
            32609,
            32610,
            32611,
            32612,
            32613,
        };

        public override HashSet<uint> ItemIds => new()
        {
            2002802,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 32602,
            },
            new NodeInfo
            {
                NodeId = 32603,
            },
            new NodeInfo
            {
                NodeId = 32604,
            },
            new NodeInfo
            {
                NodeId = 32605,
            },
            new NodeInfo
            {
                NodeId = 32606,
            },
            new NodeInfo
            {
                NodeId = 32607,
            },
            new NodeInfo
            {
                NodeId = 32608,
            },
            new NodeInfo
            {
                NodeId = 32609,
            },
            new NodeInfo
            {
                NodeId = 32610,
            },
            new NodeInfo
            {
                NodeId = 32611,
            },
            new NodeInfo
            {
                NodeId = 32612,
            },
            new NodeInfo
            {
                NodeId = 32613,
            },
        };
    }
}
