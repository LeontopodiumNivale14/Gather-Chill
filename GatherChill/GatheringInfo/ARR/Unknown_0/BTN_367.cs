using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.ARR.Unknown_0
{
    public class BTN_367 : RouteInfo
    {
        public override uint Id => 367;
        public override uint ExpansionId => 0;
        public override uint ZoneId => 0;
        public override uint GatherType => 2;
        public override Vector2 MapPosition => new Vector2(553.78f, 320.134f);
        public override int Radius => 100;

        public override HashSet<uint> NodeIds => new()
        {
            31523,
            31525,
            31555,
            31557,
            31587,
            31589,
        };

        public override HashSet<uint> ItemIds => new()
        {
            10,
            12579,
            12586,
            12891,
            13752,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 31523,
            },
            new NodeInfo
            {
                NodeId = 31525,
            },
            new NodeInfo
            {
                NodeId = 31555,
            },
            new NodeInfo
            {
                NodeId = 31557,
            },
            new NodeInfo
            {
                NodeId = 31587,
            },
            new NodeInfo
            {
                NodeId = 31589,
            },
        };
    }
}
