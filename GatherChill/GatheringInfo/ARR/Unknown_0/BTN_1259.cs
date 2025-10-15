using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.ARR.Unknown_0
{
    public class BTN_1259 : RouteInfo
    {
        public override uint Id => 1259;
        public override uint ExpansionId => 0;
        public override uint ZoneId => 0;
        public override uint GatherType => 2;
        public override Vector2 MapPosition => new Vector2(-320.723f, -609.003f);
        public override int Radius => 77;

        public override HashSet<uint> NodeIds => new()
        {
            35389,
            35390,
            35391,
            35392,
            35393,
            35394,
        };

        public override HashSet<uint> ItemIds => new()
        {
            47389,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 35389,
            },
            new NodeInfo
            {
                NodeId = 35390,
            },
            new NodeInfo
            {
                NodeId = 35391,
            },
            new NodeInfo
            {
                NodeId = 35392,
            },
            new NodeInfo
            {
                NodeId = 35393,
            },
            new NodeInfo
            {
                NodeId = 35394,
            },
        };
    }
}
