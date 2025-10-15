using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.Shadowbringers.TheRaktikaGreatwood_817
{
    public class BTN_623 : RouteInfo
    {
        public override uint Id => 623;
        public override uint ExpansionId => 3;
        public override uint ZoneId => 817;
        public override uint GatherType => 3;
        public override Vector2 MapPosition => new Vector2(268.841f, 156.003f);
        public override int Radius => 124;

        public override HashSet<uint> NodeIds => new()
        {
            32752,
            32753,
            32754,
            32755,
            32756,
            32757,
        };

        public override HashSet<uint> ItemIds => new()
        {
            10,
            26755,
            27779,
            27780,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 32752,
            },
            new NodeInfo
            {
                NodeId = 32753,
            },
            new NodeInfo
            {
                NodeId = 32754,
            },
            new NodeInfo
            {
                NodeId = 32755,
            },
            new NodeInfo
            {
                NodeId = 32756,
            },
            new NodeInfo
            {
                NodeId = 32757,
            },
        };
    }
}
