using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.ARR.Unknown_0
{
    public class MIN_275 : RouteInfo
    {
        public override uint Id => 275;
        public override uint ExpansionId => 0;
        public override uint ZoneId => 0;
        public override uint GatherType => 1;
        public override Vector2 MapPosition => new Vector2(296.954f, 743.394f);
        public override int Radius => 27;

        public override HashSet<uint> NodeIds => new()
        {
            31247,
            31248,
            31249,
            31250,
        };

        public override HashSet<uint> ItemIds => new()
        {
            2001833,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 31247,
            },
            new NodeInfo
            {
                NodeId = 31248,
            },
            new NodeInfo
            {
                NodeId = 31249,
            },
            new NodeInfo
            {
                NodeId = 31250,
            },
        };
    }
}
