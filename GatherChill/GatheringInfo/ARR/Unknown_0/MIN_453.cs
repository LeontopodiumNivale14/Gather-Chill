using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.ARR.Unknown_0
{
    public class MIN_453 : RouteInfo
    {
        public override uint Id => 453;
        public override uint ExpansionId => 0;
        public override uint ZoneId => 0;
        public override uint GatherType => 1;
        public override Vector2 MapPosition => new Vector2(-230.937f, 103.075f);
        public override int Radius => 4;

        public override HashSet<uint> NodeIds => new()
        {
            31993,
            31994,
            31995,
            31996,
        };

        public override HashSet<uint> ItemIds => new()
        {
            2002151,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 31993,
            },
            new NodeInfo
            {
                NodeId = 31994,
            },
            new NodeInfo
            {
                NodeId = 31995,
            },
            new NodeInfo
            {
                NodeId = 31996,
            },
        };
    }
}
