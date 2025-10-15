using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.ARR.Unknown_0
{
    public class MIN_549 : RouteInfo
    {
        public override uint Id => 549;
        public override uint ExpansionId => 0;
        public override uint ZoneId => 0;
        public override uint GatherType => 1;
        public override Vector2 MapPosition => new Vector2(717.051f, -493.828f);
        public override int Radius => 19;

        public override HashSet<uint> NodeIds => new()
        {
            32338,
            32339,
            32340,
        };

        public override HashSet<uint> ItemIds => new()
        {
            12,
            22620,
            22621,
            22631,
            22635,
            22637,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 32338,
            },
            new NodeInfo
            {
                NodeId = 32339,
            },
            new NodeInfo
            {
                NodeId = 32340,
            },
        };
    }
}
