using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.ARR.Unknown_0
{
    public class MIN_905 : RouteInfo
    {
        public override uint Id => 905;
        public override uint ExpansionId => 0;
        public override uint ZoneId => 0;
        public override uint GatherType => 0;
        public override Vector2 MapPosition => new Vector2(-480.968f, 276.042f);
        public override int Radius => 19;

        public override HashSet<uint> NodeIds => new()
        {
            34364,
            34365,
            34366,
        };

        public override HashSet<uint> ItemIds => new()
        {
            10,
            38276,
            38286,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 34364,
            },
            new NodeInfo
            {
                NodeId = 34365,
            },
            new NodeInfo
            {
                NodeId = 34366,
            },
        };
    }
}
