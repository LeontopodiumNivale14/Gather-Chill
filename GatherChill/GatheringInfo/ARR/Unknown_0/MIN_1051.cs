using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.ARR.Unknown_0
{
    public class MIN_1051 : RouteInfo
    {
        public override uint Id => 1051;
        public override uint ExpansionId => 0;
        public override uint ZoneId => 0;
        public override uint GatherType => 0;
        public override Vector2 MapPosition => new Vector2(-686.293f, -744.99f);
        public override int Radius => 81;

        public override HashSet<uint> NodeIds => new()
        {
            35081,
            35082,
            35083,
            35084,
            35085,
            35086,
        };

        public override HashSet<uint> ItemIds => new()
        {
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 35081,
            },
            new NodeInfo
            {
                NodeId = 35082,
            },
            new NodeInfo
            {
                NodeId = 35083,
            },
            new NodeInfo
            {
                NodeId = 35084,
            },
            new NodeInfo
            {
                NodeId = 35085,
            },
            new NodeInfo
            {
                NodeId = 35086,
            },
        };
    }
}
