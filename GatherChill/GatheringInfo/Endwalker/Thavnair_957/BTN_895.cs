using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.Endwalker.Thavnair_957
{
    public class BTN_895 : RouteInfo
    {
        public override uint Id => 895;
        public override uint ExpansionId => 4;
        public override uint ZoneId => 957;
        public override uint GatherType => 5;
        public override Vector2 MapPosition => new Vector2(146.095f, 163.509f);
        public override int Radius => 77;

        public override HashSet<uint> NodeIds => new()
        {
            34320,
            34321,
            34322,
        };

        public override HashSet<uint> ItemIds => new()
        {
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 34320,
            },
            new NodeInfo
            {
                NodeId = 34321,
            },
            new NodeInfo
            {
                NodeId = 34322,
            },
        };
    }
}
