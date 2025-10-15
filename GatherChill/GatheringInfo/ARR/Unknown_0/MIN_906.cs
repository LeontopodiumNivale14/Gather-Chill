using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.ARR.Unknown_0
{
    public class MIN_906 : RouteInfo
    {
        public override uint Id => 906;
        public override uint ExpansionId => 0;
        public override uint ZoneId => 0;
        public override uint GatherType => 1;
        public override Vector2 MapPosition => new Vector2(-387.488f, -226.274f);
        public override int Radius => 13;

        public override HashSet<uint> NodeIds => new()
        {
            34367,
            34368,
            34369,
        };

        public override HashSet<uint> ItemIds => new()
        {
            10,
            38277,
            38282,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 34367,
            },
            new NodeInfo
            {
                NodeId = 34368,
            },
            new NodeInfo
            {
                NodeId = 34369,
            },
        };
    }
}
