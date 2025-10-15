using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.Heavensward.TheDiadem_929
{
    public class BTN_748 : RouteInfo
    {
        public override uint Id => 748;
        public override uint ExpansionId => 1;
        public override uint ZoneId => 929;
        public override uint GatherType => 3;
        public override Vector2 MapPosition => new Vector2(196.713f, -52.9128f);
        public override int Radius => 795;

        public override HashSet<uint> NodeIds => new()
        {
            33488,
            33493,
            33500,
            33507,
            33514,
            33519,
            33521,
            33526,
            33528,
            33540,
            33547,
            33554,
            33559,
            33561,
            33566,
            33568,
            33573,
            33580,
        };

        public override HashSet<uint> ItemIds => new()
        {
            29936,
            31277,
            31288,
            31298,
            31308,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 33488,
            },
            new NodeInfo
            {
                NodeId = 33493,
            },
            new NodeInfo
            {
                NodeId = 33500,
            },
            new NodeInfo
            {
                NodeId = 33507,
            },
            new NodeInfo
            {
                NodeId = 33514,
            },
            new NodeInfo
            {
                NodeId = 33519,
            },
            new NodeInfo
            {
                NodeId = 33521,
            },
            new NodeInfo
            {
                NodeId = 33526,
            },
            new NodeInfo
            {
                NodeId = 33528,
            },
            new NodeInfo
            {
                NodeId = 33540,
            },
            new NodeInfo
            {
                NodeId = 33547,
            },
            new NodeInfo
            {
                NodeId = 33554,
            },
            new NodeInfo
            {
                NodeId = 33559,
            },
            new NodeInfo
            {
                NodeId = 33561,
            },
            new NodeInfo
            {
                NodeId = 33566,
            },
            new NodeInfo
            {
                NodeId = 33568,
            },
            new NodeInfo
            {
                NodeId = 33573,
            },
            new NodeInfo
            {
                NodeId = 33580,
            },
        };
    }
}
