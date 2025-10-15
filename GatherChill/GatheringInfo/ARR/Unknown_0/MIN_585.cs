using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.ARR.Unknown_0
{
    public class MIN_585 : RouteInfo
    {
        public override uint Id => 585;
        public override uint ExpansionId => 0;
        public override uint ZoneId => 0;
        public override uint GatherType => 0;
        public override Vector2 MapPosition => new Vector2(7.40216f, -573.708f);
        public override int Radius => 69;

        public override HashSet<uint> NodeIds => new()
        {
            32584,
            32585,
            32586,
            32587,
            32588,
            32589,
        };

        public override HashSet<uint> ItemIds => new()
        {
            2002798,
            2002799,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 32584,
            },
            new NodeInfo
            {
                NodeId = 32585,
            },
            new NodeInfo
            {
                NodeId = 32586,
            },
            new NodeInfo
            {
                NodeId = 32587,
            },
            new NodeInfo
            {
                NodeId = 32588,
            },
            new NodeInfo
            {
                NodeId = 32589,
            },
        };
    }
}
