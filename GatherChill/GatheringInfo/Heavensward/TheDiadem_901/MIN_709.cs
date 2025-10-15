using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.Heavensward.TheDiadem_901
{
    public class MIN_709 : RouteInfo
    {
        public override uint Id => 709;
        public override uint ExpansionId => 1;
        public override uint ZoneId => 901;
        public override uint GatherType => 0;
        public override Vector2 MapPosition => new Vector2(-422.225f, -595.808f);
        public override int Radius => 12;

        public override HashSet<uint> NodeIds => new()
        {
            33229,
        };

        public override HashSet<uint> ItemIds => new()
        {
            29946,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 33229,
            },
        };
    }
}
