using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.Dawntrail.Shaaloani_1190
{
    public class BTN_990 : RouteInfo
    {
        public override uint Id => 990;
        public override uint ExpansionId => 5;
        public override uint ZoneId => 1190;
        public override uint GatherType => 2;
        public override Vector2 MapPosition => new Vector2(299.902f, -383.208f);
        public override int Radius => 163;

        public override HashSet<uint> NodeIds => new()
        {
            34845,
            34846,
            34847,
            34848,
            34849,
            34850,
        };

        public override HashSet<uint> ItemIds => new()
        {
            10,
            44017,
            44052,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 34845,
            },
            new NodeInfo
            {
                NodeId = 34846,
            },
            new NodeInfo
            {
                NodeId = 34847,
            },
            new NodeInfo
            {
                NodeId = 34848,
            },
            new NodeInfo
            {
                NodeId = 34849,
            },
            new NodeInfo
            {
                NodeId = 34850,
            },
        };
    }
}
