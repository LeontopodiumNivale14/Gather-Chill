using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.Heavensward.TheDravanianHinterlands_399
{
    public class MIN_717 : RouteInfo
    {
        public override uint Id => 717;
        public override uint ExpansionId => 1;
        public override uint ZoneId => 399;
        public override uint GatherType => 1;
        public override Vector2 MapPosition => new Vector2(410.087f, 279.303f);
        public override int Radius => 114;

        public override HashSet<uint> NodeIds => new()
        {
            33247,
            33248,
            33249,
            33250,
            33251,
            33252,
        };

        public override HashSet<uint> ItemIds => new()
        {
            29676,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 33247,
            },
            new NodeInfo
            {
                NodeId = 33248,
            },
            new NodeInfo
            {
                NodeId = 33249,
            },
            new NodeInfo
            {
                NodeId = 33250,
            },
            new NodeInfo
            {
                NodeId = 33251,
            },
            new NodeInfo
            {
                NodeId = 33252,
            },
        };
    }
}
