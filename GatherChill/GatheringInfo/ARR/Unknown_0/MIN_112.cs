using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.ARR.Unknown_0
{
    public class MIN_112 : RouteInfo
    {
        public override uint Id => 112;
        public override uint ExpansionId => 0;
        public override uint ZoneId => 0;
        public override uint GatherType => 0;
        public override Vector2 MapPosition => new Vector2(17.7146f, -110.326f);
        public override int Radius => 39;

        public override HashSet<uint> NodeIds => new()
        {
            30820,
            30821,
            30822,
            30823,
            30824,
            30825,
            30826,
            30827,
            30828,
            30829,
        };

        public override HashSet<uint> ItemIds => new()
        {
            2000498,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 30820,
            },
            new NodeInfo
            {
                NodeId = 30821,
            },
            new NodeInfo
            {
                NodeId = 30822,
            },
            new NodeInfo
            {
                NodeId = 30823,
            },
            new NodeInfo
            {
                NodeId = 30824,
            },
            new NodeInfo
            {
                NodeId = 30825,
            },
            new NodeInfo
            {
                NodeId = 30826,
            },
            new NodeInfo
            {
                NodeId = 30827,
            },
            new NodeInfo
            {
                NodeId = 30828,
            },
            new NodeInfo
            {
                NodeId = 30829,
            },
        };
    }
}
