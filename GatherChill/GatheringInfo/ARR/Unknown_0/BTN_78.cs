using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.ARR.Unknown_0
{
    public class BTN_78 : RouteInfo
    {
        public override uint Id => 78;
        public override uint ExpansionId => 0;
        public override uint ZoneId => 0;
        public override uint GatherType => 3;
        public override Vector2 MapPosition => new Vector2(397.226f, 215.785f);
        public override int Radius => 45;

        public override HashSet<uint> NodeIds => new()
        {
            30570,
            30571,
            30572,
            30573,
        };

        public override HashSet<uint> ItemIds => new()
        {
            2000466,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 30570,
            },
            new NodeInfo
            {
                NodeId = 30571,
            },
            new NodeInfo
            {
                NodeId = 30572,
            },
            new NodeInfo
            {
                NodeId = 30573,
            },
        };
    }
}
