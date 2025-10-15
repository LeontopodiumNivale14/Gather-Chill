using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.Heavensward.TheDravanianHinterlands_399
{
    public class MIN_808 : RouteInfo
    {
        public override uint Id => 808;
        public override uint ExpansionId => 1;
        public override uint ZoneId => 399;
        public override uint GatherType => 0;
        public override Vector2 MapPosition => new Vector2(-354.179f, -142.325f);
        public override int Radius => 114;

        public override HashSet<uint> NodeIds => new()
        {
            33855,
            33856,
            33857,
            33858,
            33859,
            33860,
        };

        public override HashSet<uint> ItemIds => new()
        {
            10,
            33229,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 33855,
            },
            new NodeInfo
            {
                NodeId = 33856,
            },
            new NodeInfo
            {
                NodeId = 33857,
            },
            new NodeInfo
            {
                NodeId = 33858,
            },
            new NodeInfo
            {
                NodeId = 33859,
            },
            new NodeInfo
            {
                NodeId = 33860,
            },
        };
    }
}
