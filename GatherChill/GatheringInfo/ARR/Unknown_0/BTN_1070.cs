using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.ARR.Unknown_0
{
    public class BTN_1070 : RouteInfo
    {
        public override uint Id => 1070;
        public override uint ExpansionId => 0;
        public override uint ZoneId => 0;
        public override uint GatherType => 3;
        public override Vector2 MapPosition => new Vector2(847.588f, -775.125f);
        public override int Radius => 81;

        public override HashSet<uint> NodeIds => new()
        {
            35201,
            35202,
            35203,
            35204,
            35205,
            35206,
        };

        public override HashSet<uint> ItemIds => new()
        {
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 35201,
            },
            new NodeInfo
            {
                NodeId = 35202,
            },
            new NodeInfo
            {
                NodeId = 35203,
            },
            new NodeInfo
            {
                NodeId = 35204,
            },
            new NodeInfo
            {
                NodeId = 35205,
            },
            new NodeInfo
            {
                NodeId = 35206,
            },
        };
    }
}
