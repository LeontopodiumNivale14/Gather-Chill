using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.ARR.Unknown_0
{
    public class MIN_129 : RouteInfo
    {
        public override uint Id => 129;
        public override uint ExpansionId => 0;
        public override uint ZoneId => 0;
        public override uint GatherType => 0;
        public override Vector2 MapPosition => new Vector2(168.44f, -547.784f);
        public override int Radius => 90;

        public override HashSet<uint> NodeIds => new()
        {
            30958,
            30959,
            30960,
            30961,
            30962,
            30963,
            30964,
            30965,
            30966,
            30967,
        };

        public override HashSet<uint> ItemIds => new()
        {
            2000905,
            2000906,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 30958,
            },
            new NodeInfo
            {
                NodeId = 30959,
            },
            new NodeInfo
            {
                NodeId = 30960,
            },
            new NodeInfo
            {
                NodeId = 30961,
            },
            new NodeInfo
            {
                NodeId = 30962,
            },
            new NodeInfo
            {
                NodeId = 30963,
            },
            new NodeInfo
            {
                NodeId = 30964,
            },
            new NodeInfo
            {
                NodeId = 30965,
            },
            new NodeInfo
            {
                NodeId = 30966,
            },
            new NodeInfo
            {
                NodeId = 30967,
            },
        };
    }
}
