using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.ARR.Unknown_0
{
    public class MIN_336 : RouteInfo
    {
        public override uint Id => 336;
        public override uint ExpansionId => 0;
        public override uint ZoneId => 0;
        public override uint GatherType => 0;
        public override Vector2 MapPosition => new Vector2(-106.925f, 48.134f);
        public override int Radius => 13;

        public override HashSet<uint> NodeIds => new()
        {
            33848,
            33849,
            33850,
        };

        public override HashSet<uint> ItemIds => new()
        {
            10,
            38293,
            38296,
            38298,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 33848,
            },
            new NodeInfo
            {
                NodeId = 33849,
            },
            new NodeInfo
            {
                NodeId = 33850,
            },
        };
    }
}
