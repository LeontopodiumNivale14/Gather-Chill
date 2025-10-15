using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.Stormblood.TheAzimSteppe_622
{
    public class MIN_673 : RouteInfo
    {
        public override uint Id => 673;
        public override uint ExpansionId => 2;
        public override uint ZoneId => 622;
        public override uint GatherType => 1;
        public override Vector2 MapPosition => new Vector2(-497.707f, 187.571f);
        public override int Radius => 142;

        public override HashSet<uint> NodeIds => new()
        {
            32941,
            32942,
            32943,
            32944,
            32945,
            32946,
        };

        public override HashSet<uint> ItemIds => new()
        {
            28788,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 32941,
            },
            new NodeInfo
            {
                NodeId = 32942,
            },
            new NodeInfo
            {
                NodeId = 32943,
            },
            new NodeInfo
            {
                NodeId = 32944,
            },
            new NodeInfo
            {
                NodeId = 32945,
            },
            new NodeInfo
            {
                NodeId = 32946,
            },
        };
    }
}
