using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.ARR.Unknown_0
{
    public class BTN_426 : RouteInfo
    {
        public override uint Id => 426;
        public override uint ExpansionId => 0;
        public override uint ZoneId => 0;
        public override uint GatherType => 2;
        public override Vector2 MapPosition => new Vector2(-302.891f, -172.21f);
        public override int Radius => 102;

        public override HashSet<uint> NodeIds => new()
        {
            31794,
            31795,
            31796,
        };

        public override HashSet<uint> ItemIds => new()
        {
            7,
            13,
            7590,
            9518,
            13752,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 31794,
            },
            new NodeInfo
            {
                NodeId = 31795,
            },
            new NodeInfo
            {
                NodeId = 31796,
            },
        };
    }
}
