using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.Stormblood.TheFringes_612
{
    public class BTN_506 : RouteInfo
    {
        public override uint Id => 506;
        public override uint ExpansionId => 2;
        public override uint ZoneId => 612;
        public override uint GatherType => 2;
        public override Vector2 MapPosition => new Vector2(-548.306f, -260.54f);
        public override int Radius => 148;

        public override HashSet<uint> NodeIds => new()
        {
            32194,
            32195,
            32196,
            32197,
            32198,
            32199,
        };

        public override HashSet<uint> ItemIds => new()
        {
            8,
            19851,
            19930,
            19936,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 32194,
            },
            new NodeInfo
            {
                NodeId = 32195,
            },
            new NodeInfo
            {
                NodeId = 32196,
            },
            new NodeInfo
            {
                NodeId = 32197,
            },
            new NodeInfo
            {
                NodeId = 32198,
            },
            new NodeInfo
            {
                NodeId = 32199,
            },
        };
    }
}
