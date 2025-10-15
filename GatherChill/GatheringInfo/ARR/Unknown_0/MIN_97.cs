using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.ARR.Unknown_0
{
    public class MIN_97 : RouteInfo
    {
        public override uint Id => 97;
        public override uint ExpansionId => 0;
        public override uint ZoneId => 0;
        public override uint GatherType => 0;
        public override Vector2 MapPosition => new Vector2(57.866f, 254.97f);
        public override int Radius => 49;

        public override HashSet<uint> NodeIds => new()
        {
            30712,
            30713,
            30714,
            30715,
            30716,
            30717,
            30718,
            30719,
            30720,
            30721,
        };

        public override HashSet<uint> ItemIds => new()
        {
            2000475,
            2000476,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 30712,
            },
            new NodeInfo
            {
                NodeId = 30713,
            },
            new NodeInfo
            {
                NodeId = 30714,
            },
            new NodeInfo
            {
                NodeId = 30715,
            },
            new NodeInfo
            {
                NodeId = 30716,
            },
            new NodeInfo
            {
                NodeId = 30717,
            },
            new NodeInfo
            {
                NodeId = 30718,
            },
            new NodeInfo
            {
                NodeId = 30719,
            },
            new NodeInfo
            {
                NodeId = 30720,
            },
            new NodeInfo
            {
                NodeId = 30721,
            },
        };
    }
}
