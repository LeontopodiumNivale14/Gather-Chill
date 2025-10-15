using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.Endwalker.Elpis_961
{
    public class MIN_934 : RouteInfo
    {
        public override uint Id => 934;
        public override uint ExpansionId => 4;
        public override uint ZoneId => 961;
        public override uint GatherType => 0;
        public override Vector2 MapPosition => new Vector2(439.093f, -160.16f);
        public override int Radius => 46;

        public override HashSet<uint> NodeIds => new()
        {
            34419,
        };

        public override HashSet<uint> ItemIds => new()
        {
            39707,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 34419,
            },
        };
    }
}
