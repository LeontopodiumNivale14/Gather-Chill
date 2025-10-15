using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.Endwalker.Elpis_961
{
    public class MIN_832 : RouteInfo
    {
        public override uint Id => 832;
        public override uint ExpansionId => 4;
        public override uint ZoneId => 961;
        public override uint GatherType => 1;
        public override Vector2 MapPosition => new Vector2(-661.859f, 754.157f);
        public override int Radius => 6;

        public override HashSet<uint> NodeIds => new()
        {
            33965,
        };

        public override HashSet<uint> ItemIds => new()
        {
            36297,
            36299,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 33965,
            },
        };
    }
}
