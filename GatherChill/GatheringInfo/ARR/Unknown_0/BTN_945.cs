using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.ARR.Unknown_0
{
    public class BTN_945 : RouteInfo
    {
        public override uint Id => 945;
        public override uint ExpansionId => 0;
        public override uint ZoneId => 0;
        public override uint GatherType => 2;
        public override Vector2 MapPosition => new Vector2(-636.669f, -471.977f);
        public override int Radius => 58;

        public override HashSet<uint> NodeIds => new()
        {
            34513,
            34514,
            34515,
            34516,
        };

        public override HashSet<uint> ItemIds => new()
        {
            2003515,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 34513,
            },
            new NodeInfo
            {
                NodeId = 34514,
            },
            new NodeInfo
            {
                NodeId = 34515,
            },
            new NodeInfo
            {
                NodeId = 34516,
            },
        };
    }
}
