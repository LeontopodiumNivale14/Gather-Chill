using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.Stormblood.TheRubySea_613
{
    public class MIN_671 : RouteInfo
    {
        public override uint Id => 671;
        public override uint ExpansionId => 2;
        public override uint ZoneId => 613;
        public override uint GatherType => 0;
        public override Vector2 MapPosition => new Vector2(-476.388f, 716.816f);
        public override int Radius => 134;

        public override HashSet<uint> NodeIds => new()
        {
            32929,
            32930,
            32931,
            32932,
            32933,
            32934,
        };

        public override HashSet<uint> ItemIds => new()
        {
            28782,
            28787,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 32929,
            },
            new NodeInfo
            {
                NodeId = 32930,
            },
            new NodeInfo
            {
                NodeId = 32931,
            },
            new NodeInfo
            {
                NodeId = 32932,
            },
            new NodeInfo
            {
                NodeId = 32933,
            },
            new NodeInfo
            {
                NodeId = 32934,
            },
        };
    }
}
