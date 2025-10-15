using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.ARR.Unknown_0
{
    public class BTN_369 : RouteInfo
    {
        public override uint Id => 369;
        public override uint ExpansionId => 0;
        public override uint ZoneId => 0;
        public override uint GatherType => 3;
        public override Vector2 MapPosition => new Vector2(514.907f, 365.917f);
        public override int Radius => 117;

        public override HashSet<uint> NodeIds => new()
        {
            31527,
            31529,
            31559,
            31561,
            31591,
            31593,
        };

        public override HashSet<uint> ItemIds => new()
        {
            10,
            12878,
            12879,
            13753,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 31527,
            },
            new NodeInfo
            {
                NodeId = 31529,
            },
            new NodeInfo
            {
                NodeId = 31559,
            },
            new NodeInfo
            {
                NodeId = 31561,
            },
            new NodeInfo
            {
                NodeId = 31591,
            },
            new NodeInfo
            {
                NodeId = 31593,
            },
        };
    }
}
