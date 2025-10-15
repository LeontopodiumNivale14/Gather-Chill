using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.Stormblood.TheAzimSteppe_622
{
    public class MIN_812 : RouteInfo
    {
        public override uint Id => 812;
        public override uint ExpansionId => 2;
        public override uint ZoneId => 622;
        public override uint GatherType => 1;
        public override Vector2 MapPosition => new Vector2(42.0215f, 359.479f);
        public override int Radius => 93;

        public override HashSet<uint> NodeIds => new()
        {
            33879,
            33880,
            33881,
            33882,
            33883,
            33884,
        };

        public override HashSet<uint> ItemIds => new()
        {
            12,
            33233,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 33879,
            },
            new NodeInfo
            {
                NodeId = 33880,
            },
            new NodeInfo
            {
                NodeId = 33881,
            },
            new NodeInfo
            {
                NodeId = 33882,
            },
            new NodeInfo
            {
                NodeId = 33883,
            },
            new NodeInfo
            {
                NodeId = 33884,
            },
        };
    }
}
