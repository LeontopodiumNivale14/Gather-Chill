using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.ARR.Unknown_0
{
    public class BTN_376 : RouteInfo
    {
        public override uint Id => 376;
        public override uint ExpansionId => 0;
        public override uint ZoneId => 0;
        public override uint GatherType => 3;
        public override Vector2 MapPosition => new Vector2(-77.7697f, -423.287f);
        public override int Radius => 284;

        public override HashSet<uint> NodeIds => new()
        {
            31542,
            31544,
            31574,
            31576,
            31606,
            31608,
        };

        public override HashSet<uint> ItemIds => new()
        {
            12,
            12878,
            12879,
            13753,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 31542,
            },
            new NodeInfo
            {
                NodeId = 31544,
            },
            new NodeInfo
            {
                NodeId = 31574,
            },
            new NodeInfo
            {
                NodeId = 31576,
            },
            new NodeInfo
            {
                NodeId = 31606,
            },
            new NodeInfo
            {
                NodeId = 31608,
            },
        };
    }
}
