using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.Endwalker.Labyrinthos_956
{
    public class BTN_937 : RouteInfo
    {
        public override uint Id => 937;
        public override uint ExpansionId => 4;
        public override uint ZoneId => 956;
        public override uint GatherType => 2;
        public override Vector2 MapPosition => new Vector2(-121.644f, 721.112f);
        public override int Radius => 49;

        public override HashSet<uint> NodeIds => new()
        {
            34437,
        };

        public override HashSet<uint> ItemIds => new()
        {
            39705,
            41413,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 34437,
            },
        };
    }
}
