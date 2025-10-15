using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.Heavensward.TheDravanianForelands_398
{
    public class MIN_306 : RouteInfo
    {
        public override uint Id => 306;
        public override uint ExpansionId => 1;
        public override uint ZoneId => 398;
        public override uint GatherType => 0;
        public override Vector2 MapPosition => new Vector2(406.49f, 480.398f);
        public override int Radius => 1;

        public override HashSet<uint> NodeIds => new()
        {
            31437,
        };

        public override HashSet<uint> ItemIds => new()
        {
            32970,
            32971,
            32972,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 31437,
            },
        };
    }
}
