using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.ARR.Unknown_0
{
    public class MIN_269 : RouteInfo
    {
        public override uint Id => 269;
        public override uint ExpansionId => 0;
        public override uint ZoneId => 0;
        public override uint GatherType => 0;
        public override Vector2 MapPosition => new Vector2(362.085f, 582.56f);
        public override int Radius => 24;

        public override HashSet<uint> NodeIds => new()
        {
            31197,
            31198,
            31199,
            31200,
        };

        public override HashSet<uint> ItemIds => new()
        {
            2001824,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 31197,
            },
            new NodeInfo
            {
                NodeId = 31198,
            },
            new NodeInfo
            {
                NodeId = 31199,
            },
            new NodeInfo
            {
                NodeId = 31200,
            },
        };
    }
}
