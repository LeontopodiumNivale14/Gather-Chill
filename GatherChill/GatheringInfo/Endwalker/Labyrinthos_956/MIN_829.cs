using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.Endwalker.Labyrinthos_956
{
    public class MIN_829 : RouteInfo
    {
        public override uint Id => 829;
        public override uint ExpansionId => 4;
        public override uint ZoneId => 956;
        public override uint GatherType => 0;
        public override Vector2 MapPosition => new Vector2(550.357f, -12.493f);
        public override int Radius => 9;

        public override HashSet<uint> NodeIds => new()
        {
            33962,
        };

        public override HashSet<uint> ItemIds => new()
        {
            36293,
            36295,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 33962,
            },
        };
    }
}
