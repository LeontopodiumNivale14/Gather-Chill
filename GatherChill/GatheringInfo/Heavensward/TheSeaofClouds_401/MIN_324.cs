using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.Heavensward.TheSeaofClouds_401
{
    public class MIN_324 : RouteInfo
    {
        public override uint Id => 324;
        public override uint ExpansionId => 1;
        public override uint ZoneId => 401;
        public override uint GatherType => 1;
        public override Vector2 MapPosition => new Vector2(690.035f, 756.03f);
        public override int Radius => 114;

        public override HashSet<uint> NodeIds => new()
        {
            31465,
        };

        public override HashSet<uint> ItemIds => new()
        {
            12540,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 31465,
            },
        };
    }
}
