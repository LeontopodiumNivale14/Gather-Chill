using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.ARR.Unknown_0
{
    public class MIN_102 : RouteInfo
    {
        public override uint Id => 102;
        public override uint ExpansionId => 0;
        public override uint ZoneId => 0;
        public override uint GatherType => 0;
        public override Vector2 MapPosition => new Vector2(67.5002f, -67.8429f);
        public override int Radius => 45;

        public override HashSet<uint> NodeIds => new()
        {
            30756,
            30757,
            30758,
            30759,
            30760,
            30761,
            30762,
            30763,
            30764,
            30765,
        };

        public override HashSet<uint> ItemIds => new()
        {
            2000483,
            2000484,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 30756,
            },
            new NodeInfo
            {
                NodeId = 30757,
            },
            new NodeInfo
            {
                NodeId = 30758,
            },
            new NodeInfo
            {
                NodeId = 30759,
            },
            new NodeInfo
            {
                NodeId = 30760,
            },
            new NodeInfo
            {
                NodeId = 30761,
            },
            new NodeInfo
            {
                NodeId = 30762,
            },
            new NodeInfo
            {
                NodeId = 30763,
            },
            new NodeInfo
            {
                NodeId = 30764,
            },
            new NodeInfo
            {
                NodeId = 30765,
            },
        };
    }
}
