using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.ARR.Unknown_0
{
    public class MIN_420 : RouteInfo
    {
        public override uint Id => 420;
        public override uint ExpansionId => 0;
        public override uint ZoneId => 0;
        public override uint GatherType => 0;
        public override Vector2 MapPosition => new Vector2(-250.916f, -421.537f);
        public override int Radius => 378;

        public override HashSet<uint> NodeIds => new()
        {
            31781,
            31782,
            31783,
            31784,
        };

        public override HashSet<uint> ItemIds => new()
        {
            3,
            9,
            7589,
            12534,
            12535,
            12537,
            13750,
            17570,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 31781,
            },
            new NodeInfo
            {
                NodeId = 31782,
            },
            new NodeInfo
            {
                NodeId = 31783,
            },
            new NodeInfo
            {
                NodeId = 31784,
            },
        };
    }
}
