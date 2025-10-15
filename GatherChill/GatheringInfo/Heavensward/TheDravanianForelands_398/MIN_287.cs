using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.Heavensward.TheDravanianForelands_398
{
    public class MIN_287 : RouteInfo
    {
        public override uint Id => 287;
        public override uint ExpansionId => 1;
        public override uint ZoneId => 398;
        public override uint GatherType => 0;
        public override Vector2 MapPosition => new Vector2(246.357f, 463.959f);
        public override int Radius => 133;

        public override HashSet<uint> NodeIds => new()
        {
            31328,
            31329,
            31330,
            31331,
            31332,
            31333,
        };

        public override HashSet<uint> ItemIds => new()
        {
            8,
            5112,
            12552,
            12553,
            12631,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 31328,
            },
            new NodeInfo
            {
                NodeId = 31329,
            },
            new NodeInfo
            {
                NodeId = 31330,
            },
            new NodeInfo
            {
                NodeId = 31331,
            },
            new NodeInfo
            {
                NodeId = 31332,
            },
            new NodeInfo
            {
                NodeId = 31333,
            },
        };
    }
}
