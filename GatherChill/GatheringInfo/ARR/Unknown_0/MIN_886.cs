using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.ARR.Unknown_0
{
    public class MIN_886 : RouteInfo
    {
        public override uint Id => 886;
        public override uint ExpansionId => 0;
        public override uint ZoneId => 0;
        public override uint GatherType => 0;
        public override Vector2 MapPosition => new Vector2(-497.262f, -142.03f);
        public override int Radius => 28;

        public override HashSet<uint> NodeIds => new()
        {
            34280,
            34281,
            34282,
            34283,
            34284,
            34285,
            34286,
            34287,
            34288,
            34289,
            34290,
            34291,
        };

        public override HashSet<uint> ItemIds => new()
        {
            2003172,
            2003173,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 34280,
            },
            new NodeInfo
            {
                NodeId = 34281,
            },
            new NodeInfo
            {
                NodeId = 34282,
            },
            new NodeInfo
            {
                NodeId = 34283,
            },
            new NodeInfo
            {
                NodeId = 34284,
            },
            new NodeInfo
            {
                NodeId = 34285,
            },
            new NodeInfo
            {
                NodeId = 34286,
            },
            new NodeInfo
            {
                NodeId = 34287,
            },
            new NodeInfo
            {
                NodeId = 34288,
            },
            new NodeInfo
            {
                NodeId = 34289,
            },
            new NodeInfo
            {
                NodeId = 34290,
            },
            new NodeInfo
            {
                NodeId = 34291,
            },
        };
    }
}
