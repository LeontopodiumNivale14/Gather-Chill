using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.Dawntrail.Shaaloani_1190
{
    public class BTN_1002 : RouteInfo
    {
        public override uint Id => 1002;
        public override uint ExpansionId => 5;
        public override uint ZoneId => 1190;
        public override uint GatherType => 3;
        public override Vector2 MapPosition => new Vector2(82.7677f, 610.512f);
        public override int Radius => 124;

        public override HashSet<uint> NodeIds => new()
        {
            34917,
            34918,
            34919,
            34920,
            34921,
            34922,
        };

        public override HashSet<uint> ItemIds => new()
        {
            10,
            43984,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 34917,
            },
            new NodeInfo
            {
                NodeId = 34918,
            },
            new NodeInfo
            {
                NodeId = 34919,
            },
            new NodeInfo
            {
                NodeId = 34920,
            },
            new NodeInfo
            {
                NodeId = 34921,
            },
            new NodeInfo
            {
                NodeId = 34922,
            },
        };
    }
}
