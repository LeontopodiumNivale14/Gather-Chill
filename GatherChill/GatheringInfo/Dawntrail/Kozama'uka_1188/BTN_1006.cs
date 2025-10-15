using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.Dawntrail.Kozamauka_1188
{
    public class BTN_1006 : RouteInfo
    {
        public override uint Id => 1006;
        public override uint ExpansionId => 5;
        public override uint ZoneId => 1188;
        public override uint GatherType => 5;
        public override Vector2 MapPosition => new Vector2(270.886f, 481.424f);
        public override int Radius => 118;

        public override HashSet<uint> NodeIds => new()
        {
            34938,
            34939,
            34940,
        };

        public override HashSet<uint> ItemIds => new()
        {
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 34938,
            },
            new NodeInfo
            {
                NodeId = 34939,
            },
            new NodeInfo
            {
                NodeId = 34940,
            },
        };
    }
}
