using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.ARR.Unknown_0
{
    public class MIN_108 : RouteInfo
    {
        public override uint Id => 108;
        public override uint ExpansionId => 0;
        public override uint ZoneId => 0;
        public override uint GatherType => 0;
        public override Vector2 MapPosition => new Vector2(-354.066f, -149.187f);
        public override int Radius => 37;

        public override HashSet<uint> NodeIds => new()
        {
            30796,
            30797,
            30798,
            30799,
            30800,
            30801,
        };

        public override HashSet<uint> ItemIds => new()
        {
            2000492,
            2000493,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 30796,
            },
            new NodeInfo
            {
                NodeId = 30797,
            },
            new NodeInfo
            {
                NodeId = 30798,
            },
            new NodeInfo
            {
                NodeId = 30799,
            },
            new NodeInfo
            {
                NodeId = 30800,
            },
            new NodeInfo
            {
                NodeId = 30801,
            },
        };
    }
}
