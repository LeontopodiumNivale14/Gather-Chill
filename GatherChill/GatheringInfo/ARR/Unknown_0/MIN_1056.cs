using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.ARR.Unknown_0
{
    public class MIN_1056 : RouteInfo
    {
        public override uint Id => 1056;
        public override uint ExpansionId => 0;
        public override uint ZoneId => 0;
        public override uint GatherType => 0;
        public override Vector2 MapPosition => new Vector2(-503.57f, -322.038f);
        public override int Radius => 60;

        public override HashSet<uint> NodeIds => new()
        {
            35111,
            35112,
            35113,
            35114,
            35115,
            35116,
        };

        public override HashSet<uint> ItemIds => new()
        {
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 35111,
            },
            new NodeInfo
            {
                NodeId = 35112,
            },
            new NodeInfo
            {
                NodeId = 35113,
            },
            new NodeInfo
            {
                NodeId = 35114,
            },
            new NodeInfo
            {
                NodeId = 35115,
            },
            new NodeInfo
            {
                NodeId = 35116,
            },
        };
    }
}
