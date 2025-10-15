using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.ARR.Unknown_0
{
    public class MIN_583 : RouteInfo
    {
        public override uint Id => 583;
        public override uint ExpansionId => 0;
        public override uint ZoneId => 0;
        public override uint GatherType => 1;
        public override Vector2 MapPosition => new Vector2(26.805f, 196.806f);
        public override int Radius => 48;

        public override HashSet<uint> NodeIds => new()
        {
            32568,
            32569,
            32570,
            32571,
            32572,
            32573,
            32574,
            32575,
            32576,
            32577,
            32578,
            32579,
        };

        public override HashSet<uint> ItemIds => new()
        {
            2002796,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 32568,
            },
            new NodeInfo
            {
                NodeId = 32569,
            },
            new NodeInfo
            {
                NodeId = 32570,
            },
            new NodeInfo
            {
                NodeId = 32571,
            },
            new NodeInfo
            {
                NodeId = 32572,
            },
            new NodeInfo
            {
                NodeId = 32573,
            },
            new NodeInfo
            {
                NodeId = 32574,
            },
            new NodeInfo
            {
                NodeId = 32575,
            },
            new NodeInfo
            {
                NodeId = 32576,
            },
            new NodeInfo
            {
                NodeId = 32577,
            },
            new NodeInfo
            {
                NodeId = 32578,
            },
            new NodeInfo
            {
                NodeId = 32579,
            },
        };
    }
}
