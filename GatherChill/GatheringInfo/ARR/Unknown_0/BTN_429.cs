using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.ARR.Unknown_0
{
    public class BTN_429 : RouteInfo
    {
        public override uint Id => 429;
        public override uint ExpansionId => 0;
        public override uint ZoneId => 0;
        public override uint GatherType => 3;
        public override Vector2 MapPosition => new Vector2(-339.853f, -177.739f);
        public override int Radius => 129;

        public override HashSet<uint> NodeIds => new()
        {
            31804,
            31805,
            31806,
        };

        public override HashSet<uint> ItemIds => new()
        {
            7,
            13,
            10096,
            13753,
            17571,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 31804,
            },
            new NodeInfo
            {
                NodeId = 31805,
            },
            new NodeInfo
            {
                NodeId = 31806,
            },
        };
    }
}
