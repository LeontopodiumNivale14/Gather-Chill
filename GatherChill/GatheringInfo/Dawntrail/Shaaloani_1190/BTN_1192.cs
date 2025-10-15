using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.Dawntrail.Shaaloani_1190
{
    public class BTN_1192 : RouteInfo
    {
        public override uint Id => 1192;
        public override uint ExpansionId => 5;
        public override uint ZoneId => 1190;
        public override uint GatherType => 3;
        public override Vector2 MapPosition => new Vector2(674.087f, -261.149f);
        public override int Radius => 11;

        public override HashSet<uint> NodeIds => new()
        {
            35241,
        };

        public override HashSet<uint> ItemIds => new()
        {
            45971,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 35241,
            },
        };
    }
}
