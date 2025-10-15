using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.ARR.Unknown_0
{
    public class MIN_109 : RouteInfo
    {
        public override uint Id => 109;
        public override uint ExpansionId => 0;
        public override uint ZoneId => 0;
        public override uint GatherType => 0;
        public override Vector2 MapPosition => new Vector2(-193.213f, -123.102f);
        public override int Radius => 78;

        public override HashSet<uint> NodeIds => new()
        {
            30802,
            30803,
            30804,
            30805,
            30806,
            30807,
            30808,
            30809,
            30810,
            30811,
        };

        public override HashSet<uint> ItemIds => new()
        {
            2000494,
            2000495,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 30802,
            },
            new NodeInfo
            {
                NodeId = 30803,
            },
            new NodeInfo
            {
                NodeId = 30804,
            },
            new NodeInfo
            {
                NodeId = 30805,
            },
            new NodeInfo
            {
                NodeId = 30806,
            },
            new NodeInfo
            {
                NodeId = 30807,
            },
            new NodeInfo
            {
                NodeId = 30808,
            },
            new NodeInfo
            {
                NodeId = 30809,
            },
            new NodeInfo
            {
                NodeId = 30810,
            },
            new NodeInfo
            {
                NodeId = 30811,
            },
        };
    }
}
