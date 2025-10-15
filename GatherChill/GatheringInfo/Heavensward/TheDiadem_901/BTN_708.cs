using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.Heavensward.TheDiadem_901
{
    public class BTN_708 : RouteInfo
    {
        public override uint Id => 708;
        public override uint ExpansionId => 1;
        public override uint ZoneId => 901;
        public override uint GatherType => 2;
        public override Vector2 MapPosition => new Vector2(110.017f, -171.936f);
        public override int Radius => 803;

        public override HashSet<uint> NodeIds => new()
        {
            33148,
            33155,
            33158,
            33165,
            33176,
            33181,
            33187,
            33194,
            33207,
            33214,
            33220,
            33226,
        };

        public override HashSet<uint> ItemIds => new()
        {
            29899,
            29906,
            29915,
            29925,
            29935,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 33148,
            },
            new NodeInfo
            {
                NodeId = 33155,
            },
            new NodeInfo
            {
                NodeId = 33158,
            },
            new NodeInfo
            {
                NodeId = 33165,
            },
            new NodeInfo
            {
                NodeId = 33176,
            },
            new NodeInfo
            {
                NodeId = 33181,
            },
            new NodeInfo
            {
                NodeId = 33187,
            },
            new NodeInfo
            {
                NodeId = 33194,
            },
            new NodeInfo
            {
                NodeId = 33207,
            },
            new NodeInfo
            {
                NodeId = 33214,
            },
            new NodeInfo
            {
                NodeId = 33220,
            },
            new NodeInfo
            {
                NodeId = 33226,
            },
        };
    }
}
