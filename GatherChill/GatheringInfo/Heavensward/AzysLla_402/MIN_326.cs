using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.Heavensward.AzysLla_402
{
    public class MIN_326 : RouteInfo
    {
        public override uint Id => 326;
        public override uint ExpansionId => 1;
        public override uint ZoneId => 402;
        public override uint GatherType => 0;
        public override Vector2 MapPosition => new Vector2(674.229f, -357.905f);
        public override int Radius => 38;

        public override HashSet<uint> NodeIds => new()
        {
            31467,
        };

        public override HashSet<uint> ItemIds => new()
        {
            12945,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 31467,
            },
        };
    }
}
