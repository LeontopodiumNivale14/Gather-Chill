using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.ARR.Unknown_0
{
    public class MIN_1223 : RouteInfo
    {
        public override uint Id => 1223;
        public override uint ExpansionId => 0;
        public override uint ZoneId => 0;
        public override uint GatherType => 1;
        public override Vector2 MapPosition => new Vector2(317.427f, -46.6781f);
        public override int Radius => 61;

        public override HashSet<uint> NodeIds => new()
        {
            35305,
            35306,
            35307,
            35308,
            35309,
            35310,
        };

        public override HashSet<uint> ItemIds => new()
        {
            47359,
            47360,
            47361,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 35305,
            },
            new NodeInfo
            {
                NodeId = 35306,
            },
            new NodeInfo
            {
                NodeId = 35307,
            },
            new NodeInfo
            {
                NodeId = 35308,
            },
            new NodeInfo
            {
                NodeId = 35309,
            },
            new NodeInfo
            {
                NodeId = 35310,
            },
        };
    }
}
