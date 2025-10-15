using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.Shadowbringers.Lakeland_813
{
    public class MIN_780 : RouteInfo
    {
        public override uint Id => 780;
        public override uint ExpansionId => 3;
        public override uint ZoneId => 813;
        public override uint GatherType => 0;
        public override Vector2 MapPosition => new Vector2(405.602f, 28.6827f);
        public override int Radius => 121;

        public override HashSet<uint> NodeIds => new()
        {
            33625,
            33626,
            33627,
        };

        public override HashSet<uint> ItemIds => new()
        {
            32990,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 33625,
            },
            new NodeInfo
            {
                NodeId = 33626,
            },
            new NodeInfo
            {
                NodeId = 33627,
            },
        };
    }
}
