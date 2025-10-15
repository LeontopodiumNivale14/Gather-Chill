using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.Stormblood.TheRubySea_613
{
    public class BTN_530 : RouteInfo
    {
        public override uint Id => 530;
        public override uint ExpansionId => 2;
        public override uint ZoneId => 613;
        public override uint GatherType => 5;
        public override Vector2 MapPosition => new Vector2(901.742f, -948.379f);
        public override int Radius => 42;

        public override HashSet<uint> NodeIds => new()
        {
            32283,
        };

        public override HashSet<uint> ItemIds => new()
        {
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 32283,
            },
        };
    }
}
