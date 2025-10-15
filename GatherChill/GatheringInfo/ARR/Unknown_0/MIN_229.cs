using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.ARR.Unknown_0
{
    public class MIN_229 : RouteInfo
    {
        public override uint Id => 229;
        public override uint ExpansionId => 0;
        public override uint ZoneId => 0;
        public override uint GatherType => 1;
        public override Vector2 MapPosition => new Vector2(-114.71f, 157.175f);
        public override int Radius => 48;

        public override HashSet<uint> NodeIds => new()
        {
            31020,
            31021,
            31022,
            31023,
        };

        public override HashSet<uint> ItemIds => new()
        {
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 31020,
            },
            new NodeInfo
            {
                NodeId = 31021,
            },
            new NodeInfo
            {
                NodeId = 31022,
            },
            new NodeInfo
            {
                NodeId = 31023,
            },
        };
    }
}
