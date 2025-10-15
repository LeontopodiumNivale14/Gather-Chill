using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.ARR.Unknown_0
{
    public class BTN_263 : RouteInfo
    {
        public override uint Id => 263;
        public override uint ExpansionId => 0;
        public override uint ZoneId => 0;
        public override uint GatherType => 3;
        public override Vector2 MapPosition => new Vector2(-492.755f, 434.227f);
        public override int Radius => 68;

        public override HashSet<uint> NodeIds => new()
        {
            31145,
            31146,
            31147,
            31148,
        };

        public override HashSet<uint> ItemIds => new()
        {
            2001860,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 31145,
            },
            new NodeInfo
            {
                NodeId = 31146,
            },
            new NodeInfo
            {
                NodeId = 31147,
            },
            new NodeInfo
            {
                NodeId = 31148,
            },
        };
    }
}
