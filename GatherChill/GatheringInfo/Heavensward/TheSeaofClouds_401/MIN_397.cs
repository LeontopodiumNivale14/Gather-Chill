using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.Heavensward.TheSeaofClouds_401
{
    public class MIN_397 : RouteInfo
    {
        public override uint Id => 397;
        public override uint ExpansionId => 1;
        public override uint ZoneId => 401;
        public override uint GatherType => 0;
        public override Vector2 MapPosition => new Vector2(747.888f, -399.18f);
        public override int Radius => 59;

        public override HashSet<uint> NodeIds => new()
        {
            31509,
        };

        public override HashSet<uint> ItemIds => new()
        {
            14151,
            15646,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 31509,
            },
        };
    }
}
