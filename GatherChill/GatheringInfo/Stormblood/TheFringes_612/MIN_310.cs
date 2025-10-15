using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.Stormblood.TheFringes_612
{
    public class MIN_310 : RouteInfo
    {
        public override uint Id => 310;
        public override uint ExpansionId => 2;
        public override uint ZoneId => 612;
        public override uint GatherType => 0;
        public override Vector2 MapPosition => new Vector2(406.483f, -428.095f);
        public override int Radius => 1;

        public override HashSet<uint> NodeIds => new()
        {
            31439,
        };

        public override HashSet<uint> ItemIds => new()
        {
            32975,
            32977,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 31439,
            },
        };
    }
}
