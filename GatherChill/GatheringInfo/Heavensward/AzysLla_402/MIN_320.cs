using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.Heavensward.AzysLla_402
{
    public class MIN_320 : RouteInfo
    {
        public override uint Id => 320;
        public override uint ExpansionId => 1;
        public override uint ZoneId => 402;
        public override uint GatherType => 0;
        public override Vector2 MapPosition => new Vector2(59.0003f, -830.959f);
        public override int Radius => 33;

        public override HashSet<uint> NodeIds => new()
        {
            31461,
        };

        public override HashSet<uint> ItemIds => new()
        {
            12538,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 31461,
            },
        };
    }
}
