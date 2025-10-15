using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.ARR.Unknown_0
{
    public class MIN_316 : RouteInfo
    {
        public override uint Id => 316;
        public override uint ExpansionId => 0;
        public override uint ZoneId => 0;
        public override uint GatherType => 1;
        public override Vector2 MapPosition => new Vector2(16.7054f, 13.276f);
        public override int Radius => 32;

        public override HashSet<uint> NodeIds => new()
        {
            31489,
            31700,
            31819,
        };

        public override HashSet<uint> ItemIds => new()
        {
            10,
            38281,
            38284,
            38290,
            38295,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 31489,
            },
            new NodeInfo
            {
                NodeId = 31700,
            },
            new NodeInfo
            {
                NodeId = 31819,
            },
        };
    }
}
