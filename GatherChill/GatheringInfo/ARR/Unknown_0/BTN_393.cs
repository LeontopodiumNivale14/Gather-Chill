using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.ARR.Unknown_0
{
    public class BTN_393 : RouteInfo
    {
        public override uint Id => 393;
        public override uint ExpansionId => 0;
        public override uint ZoneId => 0;
        public override uint GatherType => 3;
        public override Vector2 MapPosition => new Vector2(-18.5142f, 29.9363f);
        public override int Radius => 10;

        public override HashSet<uint> NodeIds => new()
        {
            32269,
            32766,
            33851,
        };

        public override HashSet<uint> ItemIds => new()
        {
            13,
            38305,
            38308,
            38314,
            38319,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 32269,
            },
            new NodeInfo
            {
                NodeId = 32766,
            },
            new NodeInfo
            {
                NodeId = 33851,
            },
        };
    }
}
