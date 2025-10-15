using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.ARealmReborn.CentralThanalan_141
{
    public class MIN_155 : RouteInfo
    {
        public override uint Id => 155;
        public override uint ExpansionId => 0;
        public override uint ZoneId => 141;
        public override uint GatherType => 0;
        public override Vector2 MapPosition => new Vector2(-64.9191f, 54.2496f);
        public override int Radius => 50;

        public override HashSet<uint> NodeIds => new()
        {
            30003,
            30417,
            30418,
            30419,
            30420,
        };

        public override HashSet<uint> ItemIds => new()
        {
            6,
            5107,
            5110,
            5433,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 30003,
            },
            new NodeInfo
            {
                NodeId = 30417,
            },
            new NodeInfo
            {
                NodeId = 30418,
            },
            new NodeInfo
            {
                NodeId = 30419,
            },
            new NodeInfo
            {
                NodeId = 30420,
            },
        };
    }
}
