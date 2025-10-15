using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.Stormblood.TheAzimSteppe_622
{
    public class MIN_328 : RouteInfo
    {
        public override uint Id => 328;
        public override uint ExpansionId => 2;
        public override uint ZoneId => 622;
        public override uint GatherType => 0;
        public override Vector2 MapPosition => new Vector2(725.004f, 245.948f);
        public override int Radius => 1;

        public override HashSet<uint> NodeIds => new()
        {
            31442,
        };

        public override HashSet<uint> ItemIds => new()
        {
            32978,
            32979,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 31442,
            },
        };
    }
}
