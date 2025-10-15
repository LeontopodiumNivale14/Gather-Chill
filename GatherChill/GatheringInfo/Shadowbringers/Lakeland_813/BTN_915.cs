using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.Shadowbringers.Lakeland_813
{
    public class BTN_915 : RouteInfo
    {
        public override uint Id => 915;
        public override uint ExpansionId => 3;
        public override uint ZoneId => 813;
        public override uint GatherType => 5;
        public override Vector2 MapPosition => new Vector2(-815.208f, 781.933f);
        public override int Radius => 67;

        public override HashSet<uint> NodeIds => new()
        {
            34394,
            34395,
            34396,
        };

        public override HashSet<uint> ItemIds => new()
        {
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 34394,
            },
            new NodeInfo
            {
                NodeId = 34395,
            },
            new NodeInfo
            {
                NodeId = 34396,
            },
        };
    }
}
