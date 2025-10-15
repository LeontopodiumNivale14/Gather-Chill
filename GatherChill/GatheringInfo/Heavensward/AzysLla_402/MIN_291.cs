using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.Heavensward.AzysLla_402
{
    public class MIN_291 : RouteInfo
    {
        public override uint Id => 291;
        public override uint ExpansionId => 1;
        public override uint ZoneId => 402;
        public override uint GatherType => 0;
        public override Vector2 MapPosition => new Vector2(-854.475f, -310.704f);
        public override int Radius => 67;

        public override HashSet<uint> NodeIds => new()
        {
            31706,
        };

        public override HashSet<uint> ItemIds => new()
        {
            16725,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 31706,
            },
        };
    }
}
