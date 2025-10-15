using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.ARR.Unknown_0
{
    public class MIN_119 : RouteInfo
    {
        public override uint Id => 119;
        public override uint ExpansionId => 0;
        public override uint ZoneId => 0;
        public override uint GatherType => 0;
        public override Vector2 MapPosition => new Vector2(461.559f, 171.159f);
        public override int Radius => 76;

        public override HashSet<uint> NodeIds => new()
        {
            30878,
            30879,
            30880,
            30881,
            30882,
            30883,
            30884,
            30885,
            30886,
            30887,
        };

        public override HashSet<uint> ItemIds => new()
        {
            2000507,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 30878,
            },
            new NodeInfo
            {
                NodeId = 30879,
            },
            new NodeInfo
            {
                NodeId = 30880,
            },
            new NodeInfo
            {
                NodeId = 30881,
            },
            new NodeInfo
            {
                NodeId = 30882,
            },
            new NodeInfo
            {
                NodeId = 30883,
            },
            new NodeInfo
            {
                NodeId = 30884,
            },
            new NodeInfo
            {
                NodeId = 30885,
            },
            new NodeInfo
            {
                NodeId = 30886,
            },
            new NodeInfo
            {
                NodeId = 30887,
            },
        };
    }
}
