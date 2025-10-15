using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.Stormblood.TheRubySea_613
{
    public class BTN_467 : RouteInfo
    {
        public override uint Id => 467;
        public override uint ExpansionId => 2;
        public override uint ZoneId => 613;
        public override uint GatherType => 5;
        public override Vector2 MapPosition => new Vector2(-211.51f, 191.973f);
        public override int Radius => 80;

        public override HashSet<uint> NodeIds => new()
        {
            32080,
            32081,
            32082,
        };

        public override HashSet<uint> ItemIds => new()
        {
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 32080,
            },
            new NodeInfo
            {
                NodeId = 32081,
            },
            new NodeInfo
            {
                NodeId = 32082,
            },
        };
    }
}
