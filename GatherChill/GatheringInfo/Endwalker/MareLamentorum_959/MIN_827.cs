using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.Endwalker.MareLamentorum_959
{
    public class MIN_827 : RouteInfo
    {
        public override uint Id => 827;
        public override uint ExpansionId => 4;
        public override uint ZoneId => 959;
        public override uint GatherType => 1;
        public override Vector2 MapPosition => new Vector2(8.7394f, 661.411f);
        public override int Radius => 103;

        public override HashSet<uint> NodeIds => new()
        {
            33960,
            34046,
            34047,
        };

        public override HashSet<uint> ItemIds => new()
        {
            11,
            36285,
            38937,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 33960,
            },
            new NodeInfo
            {
                NodeId = 34046,
            },
            new NodeInfo
            {
                NodeId = 34047,
            },
        };
    }
}
