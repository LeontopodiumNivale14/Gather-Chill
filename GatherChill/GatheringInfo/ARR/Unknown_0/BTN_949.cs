using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.ARR.Unknown_0
{
    public class BTN_949 : RouteInfo
    {
        public override uint Id => 949;
        public override uint ExpansionId => 0;
        public override uint ZoneId => 0;
        public override uint GatherType => 3;
        public override Vector2 MapPosition => new Vector2(583.878f, 177.719f);
        public override int Radius => 23;

        public override HashSet<uint> NodeIds => new()
        {
            34547,
            34548,
            34549,
            34550,
        };

        public override HashSet<uint> ItemIds => new()
        {
            2003521,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 34547,
            },
            new NodeInfo
            {
                NodeId = 34548,
            },
            new NodeInfo
            {
                NodeId = 34549,
            },
            new NodeInfo
            {
                NodeId = 34550,
            },
        };
    }
}
