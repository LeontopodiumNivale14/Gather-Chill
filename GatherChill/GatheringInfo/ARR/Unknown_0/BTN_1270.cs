using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.ARR.Unknown_0
{
    public class BTN_1270 : RouteInfo
    {
        public override uint Id => 1270;
        public override uint ExpansionId => 0;
        public override uint ZoneId => 0;
        public override uint GatherType => 3;
        public override Vector2 MapPosition => new Vector2(-71.4478f, 122.355f);
        public override int Radius => 74;

        public override HashSet<uint> NodeIds => new()
        {
            35409,
            35410,
            35411,
            35412,
            35413,
            35414,
        };

        public override HashSet<uint> ItemIds => new()
        {
            47393,
            47394,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 35409,
            },
            new NodeInfo
            {
                NodeId = 35410,
            },
            new NodeInfo
            {
                NodeId = 35411,
            },
            new NodeInfo
            {
                NodeId = 35412,
            },
            new NodeInfo
            {
                NodeId = 35413,
            },
            new NodeInfo
            {
                NodeId = 35414,
            },
        };
    }
}
