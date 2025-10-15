using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.ARR.Unknown_0
{
    public class BTN_70 : RouteInfo
    {
        public override uint Id => 70;
        public override uint ExpansionId => 0;
        public override uint ZoneId => 0;
        public override uint GatherType => 2;
        public override Vector2 MapPosition => new Vector2(-210.338f, 30.9443f);
        public override int Radius => 13;

        public override HashSet<uint> NodeIds => new()
        {
            30260,
            30261,
            30262,
            30263,
        };

        public override HashSet<uint> ItemIds => new()
        {
            2000309,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 30260,
            },
            new NodeInfo
            {
                NodeId = 30261,
            },
            new NodeInfo
            {
                NodeId = 30262,
            },
            new NodeInfo
            {
                NodeId = 30263,
            },
        };
    }
}
