using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.ARR.Unknown_0
{
    public class MIN_383 : RouteInfo
    {
        public override uint Id => 383;
        public override uint ExpansionId => 0;
        public override uint ZoneId => 0;
        public override uint GatherType => 0;
        public override Vector2 MapPosition => new Vector2(320.002f, -293.01f);
        public override int Radius => 106;

        public override HashSet<uint> NodeIds => new()
        {
            31621,
            31622,
            31623,
            31649,
            31650,
            31651,
            31677,
            31678,
            31679,
        };

        public override HashSet<uint> ItemIds => new()
        {
            9,
            12534,
            12535,
            12537,
            13750,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 31621,
            },
            new NodeInfo
            {
                NodeId = 31622,
            },
            new NodeInfo
            {
                NodeId = 31623,
            },
            new NodeInfo
            {
                NodeId = 31649,
            },
            new NodeInfo
            {
                NodeId = 31650,
            },
            new NodeInfo
            {
                NodeId = 31651,
            },
            new NodeInfo
            {
                NodeId = 31677,
            },
            new NodeInfo
            {
                NodeId = 31678,
            },
            new NodeInfo
            {
                NodeId = 31679,
            },
        };
    }
}
