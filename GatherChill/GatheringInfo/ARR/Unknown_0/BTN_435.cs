using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.ARR.Unknown_0
{
    public class BTN_435 : RouteInfo
    {
        public override uint Id => 435;
        public override uint ExpansionId => 0;
        public override uint ZoneId => 0;
        public override uint GatherType => 2;
        public override Vector2 MapPosition => new Vector2(-173.789f, -605.449f);
        public override int Radius => 100;

        public override HashSet<uint> NodeIds => new()
        {
            31839,
            31840,
            31841,
            31842,
            31843,
            31844,
        };

        public override HashSet<uint> ItemIds => new()
        {
            2002123,
            2002124,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 31839,
            },
            new NodeInfo
            {
                NodeId = 31840,
            },
            new NodeInfo
            {
                NodeId = 31841,
            },
            new NodeInfo
            {
                NodeId = 31842,
            },
            new NodeInfo
            {
                NodeId = 31843,
            },
            new NodeInfo
            {
                NodeId = 31844,
            },
        };
    }
}
