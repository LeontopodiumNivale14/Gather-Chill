using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.ARR.Unknown_0
{
    public class MIN_268 : RouteInfo
    {
        public override uint Id => 268;
        public override uint ExpansionId => 0;
        public override uint ZoneId => 0;
        public override uint GatherType => 0;
        public override Vector2 MapPosition => new Vector2(478.825f, 554.607f);
        public override int Radius => 42;

        public override HashSet<uint> NodeIds => new()
        {
            31185,
            31186,
            31187,
            31188,
            31189,
            31190,
            31191,
            31192,
            31193,
            31194,
            31195,
            31196,
        };

        public override HashSet<uint> ItemIds => new()
        {
            2001822,
            2001823,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 31185,
            },
            new NodeInfo
            {
                NodeId = 31186,
            },
            new NodeInfo
            {
                NodeId = 31187,
            },
            new NodeInfo
            {
                NodeId = 31188,
            },
            new NodeInfo
            {
                NodeId = 31189,
            },
            new NodeInfo
            {
                NodeId = 31190,
            },
            new NodeInfo
            {
                NodeId = 31191,
            },
            new NodeInfo
            {
                NodeId = 31192,
            },
            new NodeInfo
            {
                NodeId = 31193,
            },
            new NodeInfo
            {
                NodeId = 31194,
            },
            new NodeInfo
            {
                NodeId = 31195,
            },
            new NodeInfo
            {
                NodeId = 31196,
            },
        };
    }
}
