using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.ARR.Unknown_0
{
    public class MIN_580 : RouteInfo
    {
        public override uint Id => 580;
        public override uint ExpansionId => 0;
        public override uint ZoneId => 0;
        public override uint GatherType => 1;
        public override Vector2 MapPosition => new Vector2(-759.548f, 57.9481f);
        public override int Radius => 46;

        public override HashSet<uint> NodeIds => new()
        {
            32546,
            32547,
            32548,
            32549,
            32550,
            32551,
            32552,
            32553,
            32554,
            32555,
            32556,
            32557,
        };

        public override HashSet<uint> ItemIds => new()
        {
            2002791,
            2002792,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 32546,
            },
            new NodeInfo
            {
                NodeId = 32547,
            },
            new NodeInfo
            {
                NodeId = 32548,
            },
            new NodeInfo
            {
                NodeId = 32549,
            },
            new NodeInfo
            {
                NodeId = 32550,
            },
            new NodeInfo
            {
                NodeId = 32551,
            },
            new NodeInfo
            {
                NodeId = 32552,
            },
            new NodeInfo
            {
                NodeId = 32553,
            },
            new NodeInfo
            {
                NodeId = 32554,
            },
            new NodeInfo
            {
                NodeId = 32555,
            },
            new NodeInfo
            {
                NodeId = 32556,
            },
            new NodeInfo
            {
                NodeId = 32557,
            },
        };
    }
}
