using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.Heavensward.AzysLla_402
{
    public class MIN_741 : RouteInfo
    {
        public override uint Id => 741;
        public override uint ExpansionId => 1;
        public override uint ZoneId => 402;
        public override uint GatherType => 0;
        public override Vector2 MapPosition => new Vector2(-429.815f, -621.692f);
        public override int Radius => 104;

        public override HashSet<uint> NodeIds => new()
        {
            33386,
            33387,
            33388,
            33389,
            33390,
            33391,
        };

        public override HashSet<uint> ItemIds => new()
        {
            31132,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 33386,
            },
            new NodeInfo
            {
                NodeId = 33387,
            },
            new NodeInfo
            {
                NodeId = 33388,
            },
            new NodeInfo
            {
                NodeId = 33389,
            },
            new NodeInfo
            {
                NodeId = 33390,
            },
            new NodeInfo
            {
                NodeId = 33391,
            },
        };
    }
}
