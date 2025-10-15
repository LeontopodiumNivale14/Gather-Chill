using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.Endwalker.Elpis_961
{
    public class BTN_845 : RouteInfo
    {
        public override uint Id => 845;
        public override uint ExpansionId => 4;
        public override uint ZoneId => 961;
        public override uint GatherType => 2;
        public override Vector2 MapPosition => new Vector2(320.464f, 183.111f);
        public override int Radius => 111;

        public override HashSet<uint> NodeIds => new()
        {
            34015,
            34016,
            34017,
        };

        public override HashSet<uint> ItemIds => new()
        {
            36302,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 34015,
            },
            new NodeInfo
            {
                NodeId = 34016,
            },
            new NodeInfo
            {
                NodeId = 34017,
            },
        };
    }
}
