using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.ARR.Unknown_0
{
    public class BTN_362 : RouteInfo
    {
        public override uint Id => 362;
        public override uint ExpansionId => 0;
        public override uint ZoneId => 0;
        public override uint GatherType => 2;
        public override Vector2 MapPosition => new Vector2(42.2137f, 541.976f);
        public override int Radius => 144;

        public override HashSet<uint> NodeIds => new()
        {
            31514,
            31516,
            31546,
            31548,
            31578,
            31580,
        };

        public override HashSet<uint> ItemIds => new()
        {
            11,
            12579,
            12586,
            12891,
            13752,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 31514,
            },
            new NodeInfo
            {
                NodeId = 31516,
            },
            new NodeInfo
            {
                NodeId = 31546,
            },
            new NodeInfo
            {
                NodeId = 31548,
            },
            new NodeInfo
            {
                NodeId = 31578,
            },
            new NodeInfo
            {
                NodeId = 31580,
            },
        };
    }
}
