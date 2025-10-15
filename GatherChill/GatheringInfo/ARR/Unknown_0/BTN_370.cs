using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.ARR.Unknown_0
{
    public class BTN_370 : RouteInfo
    {
        public override uint Id => 370;
        public override uint ExpansionId => 0;
        public override uint ZoneId => 0;
        public override uint GatherType => 2;
        public override Vector2 MapPosition => new Vector2(714.218f, -187.773f);
        public override int Radius => 45;

        public override HashSet<uint> NodeIds => new()
        {
            31530,
            31532,
            31562,
            31594,
        };

        public override HashSet<uint> ItemIds => new()
        {
            13,
            12579,
            12586,
            12891,
            13752,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 31530,
            },
            new NodeInfo
            {
                NodeId = 31532,
            },
            new NodeInfo
            {
                NodeId = 31562,
            },
            new NodeInfo
            {
                NodeId = 31594,
            },
        };
    }
}
