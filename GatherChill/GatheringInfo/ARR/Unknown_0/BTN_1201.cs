using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.ARR.Unknown_0
{
    public class BTN_1201 : RouteInfo
    {
        public override uint Id => 1201;
        public override uint ExpansionId => 0;
        public override uint ZoneId => 0;
        public override uint GatherType => 3;
        public override Vector2 MapPosition => new Vector2(-614.98f, 370.605f);
        public override int Radius => 30;

        public override HashSet<uint> NodeIds => new()
        {
            35264,
            35265,
            35266,
        };

        public override HashSet<uint> ItemIds => new()
        {
            13,
            48016,
            48058,
            48067,
            48073,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 35264,
            },
            new NodeInfo
            {
                NodeId = 35265,
            },
            new NodeInfo
            {
                NodeId = 35266,
            },
        };
    }
}
