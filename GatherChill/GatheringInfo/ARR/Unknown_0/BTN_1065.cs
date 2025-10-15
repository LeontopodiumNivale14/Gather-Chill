using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.ARR.Unknown_0
{
    public class BTN_1065 : RouteInfo
    {
        public override uint Id => 1065;
        public override uint ExpansionId => 0;
        public override uint ZoneId => 0;
        public override uint GatherType => 3;
        public override Vector2 MapPosition => new Vector2(-123.274f, 367.09f);
        public override int Radius => 71;

        public override HashSet<uint> NodeIds => new()
        {
            35171,
            35172,
            35173,
            35174,
            35175,
            35176,
        };

        public override HashSet<uint> ItemIds => new()
        {
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 35171,
            },
            new NodeInfo
            {
                NodeId = 35172,
            },
            new NodeInfo
            {
                NodeId = 35173,
            },
            new NodeInfo
            {
                NodeId = 35174,
            },
            new NodeInfo
            {
                NodeId = 35175,
            },
            new NodeInfo
            {
                NodeId = 35176,
            },
        };
    }
}
