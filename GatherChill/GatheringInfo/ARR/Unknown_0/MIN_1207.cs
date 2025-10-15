using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.ARR.Unknown_0
{
    public class MIN_1207 : RouteInfo
    {
        public override uint Id => 1207;
        public override uint ExpansionId => 0;
        public override uint ZoneId => 0;
        public override uint GatherType => 1;
        public override Vector2 MapPosition => new Vector2(416.57f, -747.054f);
        public override int Radius => 63;

        public override HashSet<uint> NodeIds => new()
        {
            35279,
            35280,
            35281,
            35282,
            35283,
            35284,
        };

        public override HashSet<uint> ItemIds => new()
        {
            47354,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 35279,
            },
            new NodeInfo
            {
                NodeId = 35280,
            },
            new NodeInfo
            {
                NodeId = 35281,
            },
            new NodeInfo
            {
                NodeId = 35282,
            },
            new NodeInfo
            {
                NodeId = 35283,
            },
            new NodeInfo
            {
                NodeId = 35284,
            },
        };
    }
}
