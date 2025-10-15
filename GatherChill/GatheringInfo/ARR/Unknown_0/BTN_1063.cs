using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.ARR.Unknown_0
{
    public class BTN_1063 : RouteInfo
    {
        public override uint Id => 1063;
        public override uint ExpansionId => 0;
        public override uint ZoneId => 0;
        public override uint GatherType => 3;
        public override Vector2 MapPosition => new Vector2(235.496f, 90.8001f);
        public override int Radius => 83;

        public override HashSet<uint> NodeIds => new()
        {
            35159,
            35160,
            35161,
            35162,
            35163,
            35164,
        };

        public override HashSet<uint> ItemIds => new()
        {
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 35159,
            },
            new NodeInfo
            {
                NodeId = 35160,
            },
            new NodeInfo
            {
                NodeId = 35161,
            },
            new NodeInfo
            {
                NodeId = 35162,
            },
            new NodeInfo
            {
                NodeId = 35163,
            },
            new NodeInfo
            {
                NodeId = 35164,
            },
        };
    }
}
