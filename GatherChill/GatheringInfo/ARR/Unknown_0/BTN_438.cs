using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.ARR.Unknown_0
{
    public class BTN_438 : RouteInfo
    {
        public override uint Id => 438;
        public override uint ExpansionId => 0;
        public override uint ZoneId => 0;
        public override uint GatherType => 3;
        public override Vector2 MapPosition => new Vector2(315.359f, -550.855f);
        public override int Radius => 33;

        public override HashSet<uint> NodeIds => new()
        {
            31869,
            31870,
            31871,
            31872,
        };

        public override HashSet<uint> ItemIds => new()
        {
            2002128,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 31869,
            },
            new NodeInfo
            {
                NodeId = 31870,
            },
            new NodeInfo
            {
                NodeId = 31871,
            },
            new NodeInfo
            {
                NodeId = 31872,
            },
        };
    }
}
