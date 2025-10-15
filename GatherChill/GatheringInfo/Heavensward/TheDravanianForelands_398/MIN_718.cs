using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.Heavensward.TheDravanianForelands_398
{
    public class MIN_718 : RouteInfo
    {
        public override uint Id => 718;
        public override uint ExpansionId => 1;
        public override uint ZoneId => 398;
        public override uint GatherType => 0;
        public override Vector2 MapPosition => new Vector2(-277.189f, -594.885f);
        public override int Radius => 104;

        public override HashSet<uint> NodeIds => new()
        {
            33253,
            33254,
            33255,
            33256,
            33257,
            33258,
        };

        public override HashSet<uint> ItemIds => new()
        {
            29676,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 33253,
            },
            new NodeInfo
            {
                NodeId = 33254,
            },
            new NodeInfo
            {
                NodeId = 33255,
            },
            new NodeInfo
            {
                NodeId = 33256,
            },
            new NodeInfo
            {
                NodeId = 33257,
            },
            new NodeInfo
            {
                NodeId = 33258,
            },
        };
    }
}
