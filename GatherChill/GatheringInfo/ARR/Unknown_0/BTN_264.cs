using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.ARR.Unknown_0
{
    public class BTN_264 : RouteInfo
    {
        public override uint Id => 264;
        public override uint ExpansionId => 0;
        public override uint ZoneId => 0;
        public override uint GatherType => 3;
        public override Vector2 MapPosition => new Vector2(-370.22f, 474.652f);
        public override int Radius => 102;

        public override HashSet<uint> NodeIds => new()
        {
            31149,
            31150,
            31151,
            31152,
            31153,
            31154,
        };

        public override HashSet<uint> ItemIds => new()
        {
            2001861,
            2001862,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 31149,
            },
            new NodeInfo
            {
                NodeId = 31150,
            },
            new NodeInfo
            {
                NodeId = 31151,
            },
            new NodeInfo
            {
                NodeId = 31152,
            },
            new NodeInfo
            {
                NodeId = 31153,
            },
            new NodeInfo
            {
                NodeId = 31154,
            },
        };
    }
}
