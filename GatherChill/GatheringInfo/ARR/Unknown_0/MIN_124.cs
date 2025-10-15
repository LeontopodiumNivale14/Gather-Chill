using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.ARR.Unknown_0
{
    public class MIN_124 : RouteInfo
    {
        public override uint Id => 124;
        public override uint ExpansionId => 0;
        public override uint ZoneId => 0;
        public override uint GatherType => 0;
        public override Vector2 MapPosition => new Vector2(476.885f, 318.269f);
        public override int Radius => 27;

        public override HashSet<uint> NodeIds => new()
        {
            30922,
            30923,
            30924,
            30925,
            30926,
            30927,
        };

        public override HashSet<uint> ItemIds => new()
        {
            2000897,
            2000898,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 30922,
            },
            new NodeInfo
            {
                NodeId = 30923,
            },
            new NodeInfo
            {
                NodeId = 30924,
            },
            new NodeInfo
            {
                NodeId = 30925,
            },
            new NodeInfo
            {
                NodeId = 30926,
            },
            new NodeInfo
            {
                NodeId = 30927,
            },
        };
    }
}
