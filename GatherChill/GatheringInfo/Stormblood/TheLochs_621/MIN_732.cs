using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.Stormblood.TheLochs_621
{
    public class MIN_732 : RouteInfo
    {
        public override uint Id => 732;
        public override uint ExpansionId => 2;
        public override uint ZoneId => 621;
        public override uint GatherType => 0;
        public override Vector2 MapPosition => new Vector2(461.188f, -283.818f);
        public override int Radius => 114;

        public override HashSet<uint> NodeIds => new()
        {
            33337,
            33338,
            33339,
            33340,
            33341,
            33342,
        };

        public override HashSet<uint> ItemIds => new()
        {
            9,
            30502,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 33337,
            },
            new NodeInfo
            {
                NodeId = 33338,
            },
            new NodeInfo
            {
                NodeId = 33339,
            },
            new NodeInfo
            {
                NodeId = 33340,
            },
            new NodeInfo
            {
                NodeId = 33341,
            },
            new NodeInfo
            {
                NodeId = 33342,
            },
        };
    }
}
