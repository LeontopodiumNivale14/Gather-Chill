using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.Heavensward.TheSeaofClouds_401
{
    public class MIN_308 : RouteInfo
    {
        public override uint Id => 308;
        public override uint ExpansionId => 1;
        public override uint ZoneId => 401;
        public override uint GatherType => 0;
        public override Vector2 MapPosition => new Vector2(-97.4555f, -548.549f);
        public override int Radius => 1;

        public override HashSet<uint> NodeIds => new()
        {
            31438,
        };

        public override HashSet<uint> ItemIds => new()
        {
            32973,
            32974,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 31438,
            },
        };
    }
}
