using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.Stormblood.ThePeaks_620
{
    public class MIN_489 : RouteInfo
    {
        public override uint Id => 489;
        public override uint ExpansionId => 2;
        public override uint ZoneId => 620;
        public override uint GatherType => 1;
        public override Vector2 MapPosition => new Vector2(94.4946f, -416.617f);
        public override int Radius => 130;

        public override HashSet<uint> NodeIds => new()
        {
            32147,
            32148,
            32149,
            32150,
            32151,
            32152,
        };

        public override HashSet<uint> ItemIds => new()
        {
            11,
            5232,
            19950,
            20007,
            23148,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 32147,
            },
            new NodeInfo
            {
                NodeId = 32148,
            },
            new NodeInfo
            {
                NodeId = 32149,
            },
            new NodeInfo
            {
                NodeId = 32150,
            },
            new NodeInfo
            {
                NodeId = 32151,
            },
            new NodeInfo
            {
                NodeId = 32152,
            },
        };
    }
}
