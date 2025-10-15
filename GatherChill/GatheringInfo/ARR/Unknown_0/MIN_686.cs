using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.ARR.Unknown_0
{
    public class MIN_686 : RouteInfo
    {
        public override uint Id => 686;
        public override uint ExpansionId => 0;
        public override uint ZoneId => 0;
        public override uint GatherType => 1;
        public override Vector2 MapPosition => new Vector2(-863.811f, 122.828f);
        public override int Radius => 32;

        public override HashSet<uint> NodeIds => new()
        {
            32994,
            32995,
            32996,
        };

        public override HashSet<uint> ItemIds => new()
        {
            10,
            29515,
            29519,
            29526,
            29531,
            29535,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 32994,
            },
            new NodeInfo
            {
                NodeId = 32995,
            },
            new NodeInfo
            {
                NodeId = 32996,
            },
        };
    }
}
