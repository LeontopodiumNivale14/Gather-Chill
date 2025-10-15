using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.Endwalker.UltimaThule_960
{
    public class BTN_858 : RouteInfo
    {
        public override uint Id => 858;
        public override uint ExpansionId => 4;
        public override uint ZoneId => 960;
        public override uint GatherType => 3;
        public override Vector2 MapPosition => new Vector2(330.483f, -412.647f);
        public override int Radius => 8;

        public override HashSet<uint> NodeIds => new()
        {
            34045,
        };

        public override HashSet<uint> ItemIds => new()
        {
            36214,
            36217,
            37691,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 34045,
            },
        };
    }
}
