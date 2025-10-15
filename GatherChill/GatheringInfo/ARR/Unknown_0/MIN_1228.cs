using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.ARR.Unknown_0
{
    public class MIN_1228 : RouteInfo
    {
        public override uint Id => 1228;
        public override uint ExpansionId => 0;
        public override uint ZoneId => 0;
        public override uint GatherType => 0;
        public override Vector2 MapPosition => new Vector2(-318.928f, 90.5212f);
        public override int Radius => 60;

        public override HashSet<uint> NodeIds => new()
        {
            35319,
            35320,
            35321,
            35322,
            35323,
            35324,
        };

        public override HashSet<uint> ItemIds => new()
        {
            47362,
            47363,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 35319,
            },
            new NodeInfo
            {
                NodeId = 35320,
            },
            new NodeInfo
            {
                NodeId = 35321,
            },
            new NodeInfo
            {
                NodeId = 35322,
            },
            new NodeInfo
            {
                NodeId = 35323,
            },
            new NodeInfo
            {
                NodeId = 35324,
            },
        };
    }
}
