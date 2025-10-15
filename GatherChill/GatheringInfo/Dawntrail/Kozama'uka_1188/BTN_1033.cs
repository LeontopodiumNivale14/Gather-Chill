using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.Dawntrail.Kozamauka_1188
{
    public class BTN_1033 : RouteInfo
    {
        public override uint Id => 1033;
        public override uint ExpansionId => 5;
        public override uint ZoneId => 1188;
        public override uint GatherType => 2;
        public override Vector2 MapPosition => new Vector2(-702.708f, 577.589f);
        public override int Radius => 108;

        public override HashSet<uint> NodeIds => new()
        {
            34993,
        };

        public override HashSet<uint> ItemIds => new()
        {
            44137,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 34993,
            },
        };
    }
}
