using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.ARR.Unknown_0
{
    public class MIN_104 : RouteInfo
    {
        public override uint Id => 104;
        public override uint ExpansionId => 0;
        public override uint ZoneId => 0;
        public override uint GatherType => 0;
        public override Vector2 MapPosition => new Vector2(184.095f, -13.7376f);
        public override int Radius => 40;

        public override HashSet<uint> NodeIds => new()
        {
            30770,
            30771,
            30772,
            30773,
            30774,
            30775,
            30776,
            30777,
        };

        public override HashSet<uint> ItemIds => new()
        {
            2000486,
            2000487,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 30770,
            },
            new NodeInfo
            {
                NodeId = 30771,
            },
            new NodeInfo
            {
                NodeId = 30772,
            },
            new NodeInfo
            {
                NodeId = 30773,
            },
            new NodeInfo
            {
                NodeId = 30774,
            },
            new NodeInfo
            {
                NodeId = 30775,
            },
            new NodeInfo
            {
                NodeId = 30776,
            },
            new NodeInfo
            {
                NodeId = 30777,
            },
        };
    }
}
