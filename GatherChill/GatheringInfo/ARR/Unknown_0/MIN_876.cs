using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.ARR.Unknown_0
{
    public class MIN_876 : RouteInfo
    {
        public override uint Id => 876;
        public override uint ExpansionId => 0;
        public override uint ZoneId => 0;
        public override uint GatherType => 0;
        public override Vector2 MapPosition => new Vector2(412.668f, 73.8633f);
        public override int Radius => 21;

        public override HashSet<uint> NodeIds => new()
        {
            34194,
            34195,
            34196,
            34197,
            34198,
            34199,
        };

        public override HashSet<uint> ItemIds => new()
        {
            2003157,
            2003158,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 34194,
            },
            new NodeInfo
            {
                NodeId = 34195,
            },
            new NodeInfo
            {
                NodeId = 34196,
            },
            new NodeInfo
            {
                NodeId = 34197,
            },
            new NodeInfo
            {
                NodeId = 34198,
            },
            new NodeInfo
            {
                NodeId = 34199,
            },
        };
    }
}
