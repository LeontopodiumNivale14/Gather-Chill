using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.ARealmReborn.CentralThanalan_141
{
    public class MIN_157 : RouteInfo
    {
        public override uint Id => 157;
        public override uint ExpansionId => 0;
        public override uint ZoneId => 141;
        public override uint GatherType => 0;
        public override Vector2 MapPosition => new Vector2(131.248f, 274.46f);
        public override int Radius => 61;

        public override HashSet<uint> NodeIds => new()
        {
            30425,
            30426,
            30427,
            30428,
        };

        public override HashSet<uint> ItemIds => new()
        {
            4,
            6,
            5432,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 30425,
            },
            new NodeInfo
            {
                NodeId = 30426,
            },
            new NodeInfo
            {
                NodeId = 30427,
            },
            new NodeInfo
            {
                NodeId = 30428,
            },
        };
    }
}
