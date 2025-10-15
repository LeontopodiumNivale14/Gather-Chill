using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.ARR.Unknown_0
{
    public class BTN_914 : RouteInfo
    {
        public override uint Id => 914;
        public override uint ExpansionId => 0;
        public override uint ZoneId => 0;
        public override uint GatherType => 3;
        public override Vector2 MapPosition => new Vector2(-43.7514f, -153.075f);
        public override int Radius => 20;

        public override HashSet<uint> NodeIds => new()
        {
            34391,
            34392,
            34393,
        };

        public override HashSet<uint> ItemIds => new()
        {
            8,
            38303,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 34391,
            },
            new NodeInfo
            {
                NodeId = 34392,
            },
            new NodeInfo
            {
                NodeId = 34393,
            },
        };
    }
}
