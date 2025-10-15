using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.ARR.Unknown_0
{
    public class MIN_461 : RouteInfo
    {
        public override uint Id => 461;
        public override uint ExpansionId => 0;
        public override uint ZoneId => 0;
        public override uint GatherType => 1;
        public override Vector2 MapPosition => new Vector2(-357.045f, 482.193f);
        public override int Radius => 30;

        public override HashSet<uint> NodeIds => new()
        {
            32053,
            32054,
            32055,
            32056,
            32057,
            32058,
            32059,
            32060,
            32061,
            32062,
            32063,
            32064,
        };

        public override HashSet<uint> ItemIds => new()
        {
            2002163,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 32053,
            },
            new NodeInfo
            {
                NodeId = 32054,
            },
            new NodeInfo
            {
                NodeId = 32055,
            },
            new NodeInfo
            {
                NodeId = 32056,
            },
            new NodeInfo
            {
                NodeId = 32057,
            },
            new NodeInfo
            {
                NodeId = 32058,
            },
            new NodeInfo
            {
                NodeId = 32059,
            },
            new NodeInfo
            {
                NodeId = 32060,
            },
            new NodeInfo
            {
                NodeId = 32061,
            },
            new NodeInfo
            {
                NodeId = 32062,
            },
            new NodeInfo
            {
                NodeId = 32063,
            },
            new NodeInfo
            {
                NodeId = 32064,
            },
        };
    }
}
