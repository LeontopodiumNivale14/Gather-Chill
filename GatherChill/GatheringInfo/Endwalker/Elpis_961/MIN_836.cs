using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.Endwalker.Elpis_961
{
    public class MIN_836 : RouteInfo
    {
        public override uint Id => 836;
        public override uint ExpansionId => 4;
        public override uint ZoneId => 961;
        public override uint GatherType => 1;
        public override Vector2 MapPosition => new Vector2(-395.933f, -714.755f);
        public override int Radius => 5;

        public override HashSet<uint> NodeIds => new()
        {
            33969,
        };

        public override HashSet<uint> ItemIds => new()
        {
            36179,
            36215,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 33969,
            },
        };
    }
}
