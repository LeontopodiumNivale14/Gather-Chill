using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.Stormblood.TheAzimSteppe_622
{
    public class MIN_536 : RouteInfo
    {
        public override uint Id => 536;
        public override uint ExpansionId => 2;
        public override uint ZoneId => 622;
        public override uint GatherType => 0;
        public override Vector2 MapPosition => new Vector2(131.83f, 785.083f);
        public override int Radius => 23;

        public override HashSet<uint> NodeIds => new()
        {
            32309,
        };

        public override HashSet<uint> ItemIds => new()
        {
            22418,
            24255,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 32309,
            },
        };
    }
}
