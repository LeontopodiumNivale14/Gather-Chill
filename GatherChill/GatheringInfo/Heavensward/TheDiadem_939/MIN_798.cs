using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.Heavensward.TheDiadem_939
{
    public class MIN_798 : RouteInfo
    {
        public override uint Id => 798;
        public override uint ExpansionId => 1;
        public override uint ZoneId => 939;
        public override uint GatherType => 0;
        public override Vector2 MapPosition => new Vector2(-428.003f, -593.445f);
        public override int Radius => 12;

        public override HashSet<uint> NodeIds => new()
        {
            33836,
        };

        public override HashSet<uint> ItemIds => new()
        {
            29946,
            31318,
            32047,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 33836,
            },
        };
    }
}
