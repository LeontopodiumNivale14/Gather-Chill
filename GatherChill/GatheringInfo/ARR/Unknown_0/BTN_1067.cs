using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.ARR.Unknown_0
{
    public class BTN_1067 : RouteInfo
    {
        public override uint Id => 1067;
        public override uint ExpansionId => 0;
        public override uint ZoneId => 0;
        public override uint GatherType => 2;
        public override Vector2 MapPosition => new Vector2(591.071f, 477.383f);
        public override int Radius => 83;

        public override HashSet<uint> NodeIds => new()
        {
            35183,
            35184,
            35185,
            35186,
            35187,
            35188,
        };

        public override HashSet<uint> ItemIds => new()
        {
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 35183,
            },
            new NodeInfo
            {
                NodeId = 35184,
            },
            new NodeInfo
            {
                NodeId = 35185,
            },
            new NodeInfo
            {
                NodeId = 35186,
            },
            new NodeInfo
            {
                NodeId = 35187,
            },
            new NodeInfo
            {
                NodeId = 35188,
            },
        };
    }
}
