using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.Dawntrail.YakTel_1189
{
    public class MIN_1189 : RouteInfo
    {
        public override uint Id => 1189;
        public override uint ExpansionId => 5;
        public override uint ZoneId => 1189;
        public override uint GatherType => 0;
        public override Vector2 MapPosition => new Vector2(-177.218f, 771.768f);
        public override int Radius => 20;

        public override HashSet<uint> NodeIds => new()
        {
            35238,
        };

        public override HashSet<uint> ItemIds => new()
        {
            45969,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 35238,
            },
        };
    }
}
