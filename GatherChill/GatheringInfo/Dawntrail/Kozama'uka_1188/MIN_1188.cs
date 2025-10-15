using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.Dawntrail.Kozamauka_1188
{
    public class MIN_1188 : RouteInfo
    {
        public override uint Id => 1188;
        public override uint ExpansionId => 5;
        public override uint ZoneId => 1188;
        public override uint GatherType => 1;
        public override Vector2 MapPosition => new Vector2(-472.814f, -193.465f);
        public override int Radius => 10;

        public override HashSet<uint> NodeIds => new()
        {
            35237,
        };

        public override HashSet<uint> ItemIds => new()
        {
            45970,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 35237,
            },
        };
    }
}
