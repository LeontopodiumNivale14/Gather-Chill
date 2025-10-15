using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.ARR.Unknown_0
{
    public class MIN_131 : RouteInfo
    {
        public override uint Id => 131;
        public override uint ExpansionId => 0;
        public override uint ZoneId => 0;
        public override uint GatherType => 1;
        public override Vector2 MapPosition => new Vector2(513.571f, -410.023f);
        public override int Radius => 77;

        public override HashSet<uint> NodeIds => new()
        {
            30972,
            30973,
            30974,
            30975,
            30976,
            30977,
            30978,
            30979,
            30980,
            30981,
        };

        public override HashSet<uint> ItemIds => new()
        {
            2000908,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 30972,
            },
            new NodeInfo
            {
                NodeId = 30973,
            },
            new NodeInfo
            {
                NodeId = 30974,
            },
            new NodeInfo
            {
                NodeId = 30975,
            },
            new NodeInfo
            {
                NodeId = 30976,
            },
            new NodeInfo
            {
                NodeId = 30977,
            },
            new NodeInfo
            {
                NodeId = 30978,
            },
            new NodeInfo
            {
                NodeId = 30979,
            },
            new NodeInfo
            {
                NodeId = 30980,
            },
            new NodeInfo
            {
                NodeId = 30981,
            },
        };
    }
}
