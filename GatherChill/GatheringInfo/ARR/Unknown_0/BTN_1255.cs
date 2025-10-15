using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.ARR.Unknown_0
{
    public class BTN_1255 : RouteInfo
    {
        public override uint Id => 1255;
        public override uint ExpansionId => 0;
        public override uint ZoneId => 0;
        public override uint GatherType => 2;
        public override Vector2 MapPosition => new Vector2(717.303f, -324.509f);
        public override int Radius => 78;

        public override HashSet<uint> NodeIds => new()
        {
            35383,
            35384,
            35385,
            35386,
            35387,
            35388,
        };

        public override HashSet<uint> ItemIds => new()
        {
            47388,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 35383,
            },
            new NodeInfo
            {
                NodeId = 35384,
            },
            new NodeInfo
            {
                NodeId = 35385,
            },
            new NodeInfo
            {
                NodeId = 35386,
            },
            new NodeInfo
            {
                NodeId = 35387,
            },
            new NodeInfo
            {
                NodeId = 35388,
            },
        };
    }
}
