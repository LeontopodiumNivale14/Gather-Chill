using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.ARR.Unknown_0
{
    public class BTN_1072 : RouteInfo
    {
        public override uint Id => 1072;
        public override uint ExpansionId => 0;
        public override uint ZoneId => 0;
        public override uint GatherType => 2;
        public override Vector2 MapPosition => new Vector2(738.794f, 94.5726f);
        public override int Radius => 57;

        public override HashSet<uint> NodeIds => new()
        {
            35213,
            35214,
            35215,
            35216,
            35217,
            35218,
        };

        public override HashSet<uint> ItemIds => new()
        {
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 35213,
            },
            new NodeInfo
            {
                NodeId = 35214,
            },
            new NodeInfo
            {
                NodeId = 35215,
            },
            new NodeInfo
            {
                NodeId = 35216,
            },
            new NodeInfo
            {
                NodeId = 35217,
            },
            new NodeInfo
            {
                NodeId = 35218,
            },
        };
    }
}
