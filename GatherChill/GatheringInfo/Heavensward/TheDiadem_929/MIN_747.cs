using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.Heavensward.TheDiadem_929
{
    public class MIN_747 : RouteInfo
    {
        public override uint Id => 747;
        public override uint ExpansionId => 1;
        public override uint ZoneId => 929;
        public override uint GatherType => 1;
        public override Vector2 MapPosition => new Vector2(-348.377f, -370.076f);
        public override int Radius => 2;

        public override HashSet<uint> NodeIds => new()
        {
            33434,
        };

        public override HashSet<uint> ItemIds => new()
        {
            29943,
            31285,
            31294,
            31305,
            31315,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 33434,
            },
        };
    }
}
