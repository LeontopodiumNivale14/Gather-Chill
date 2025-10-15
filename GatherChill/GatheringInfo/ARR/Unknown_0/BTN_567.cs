using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.ARR.Unknown_0
{
    public class BTN_567 : RouteInfo
    {
        public override uint Id => 567;
        public override uint ExpansionId => 0;
        public override uint ZoneId => 0;
        public override uint GatherType => 2;
        public override Vector2 MapPosition => new Vector2(-852.034f, 209.54f);
        public override int Radius => 26;

        public override HashSet<uint> NodeIds => new()
        {
            32438,
            32439,
            32440,
            32441,
            32442,
            32443,
        };

        public override HashSet<uint> ItemIds => new()
        {
            2002771,
            2002772,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 32438,
            },
            new NodeInfo
            {
                NodeId = 32439,
            },
            new NodeInfo
            {
                NodeId = 32440,
            },
            new NodeInfo
            {
                NodeId = 32441,
            },
            new NodeInfo
            {
                NodeId = 32442,
            },
            new NodeInfo
            {
                NodeId = 32443,
            },
        };
    }
}
