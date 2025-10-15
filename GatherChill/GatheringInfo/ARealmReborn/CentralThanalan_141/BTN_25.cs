using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.ARealmReborn.CentralThanalan_141
{
    public class BTN_25 : RouteInfo
    {
        public override uint Id => 25;
        public override uint ExpansionId => 0;
        public override uint ZoneId => 141;
        public override uint GatherType => 3;
        public override Vector2 MapPosition => new Vector2(175.348f, -50.0458f);
        public override int Radius => 54;

        public override HashSet<uint> NodeIds => new()
        {
            30072,
            30073,
            30074,
            30075,
        };

        public override HashSet<uint> ItemIds => new()
        {
            6,
            4781,
            4830,
            5538,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 30072,
            },
            new NodeInfo
            {
                NodeId = 30073,
            },
            new NodeInfo
            {
                NodeId = 30074,
            },
            new NodeInfo
            {
                NodeId = 30075,
            },
        };
    }
}
