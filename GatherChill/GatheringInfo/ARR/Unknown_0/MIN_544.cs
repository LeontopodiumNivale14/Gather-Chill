using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.ARR.Unknown_0
{
    public class MIN_544 : RouteInfo
    {
        public override uint Id => 544;
        public override uint ExpansionId => 0;
        public override uint ZoneId => 0;
        public override uint GatherType => 1;
        public override Vector2 MapPosition => new Vector2(-65.0677f, -364.29f);
        public override int Radius => 18;

        public override HashSet<uint> NodeIds => new()
        {
            32323,
            32324,
            32325,
        };

        public override HashSet<uint> ItemIds => new()
        {
            12,
            22617,
            22622,
            22628,
            22632,
            22633,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 32323,
            },
            new NodeInfo
            {
                NodeId = 32324,
            },
            new NodeInfo
            {
                NodeId = 32325,
            },
        };
    }
}
