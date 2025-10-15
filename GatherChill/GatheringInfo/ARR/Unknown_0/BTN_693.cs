using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.ARR.Unknown_0
{
    public class BTN_693 : RouteInfo
    {
        public override uint Id => 693;
        public override uint ExpansionId => 0;
        public override uint ZoneId => 0;
        public override uint GatherType => 3;
        public override Vector2 MapPosition => new Vector2(282.352f, -0.389048f);
        public override int Radius => 31;

        public override HashSet<uint> NodeIds => new()
        {
            33015,
            33016,
            33017,
        };

        public override HashSet<uint> ItemIds => new()
        {
            10,
            29538,
            29543,
            29551,
            29554,
            29560,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 33015,
            },
            new NodeInfo
            {
                NodeId = 33016,
            },
            new NodeInfo
            {
                NodeId = 33017,
            },
        };
    }
}
