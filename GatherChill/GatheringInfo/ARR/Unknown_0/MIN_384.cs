using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.ARR.Unknown_0
{
    public class MIN_384 : RouteInfo
    {
        public override uint Id => 384;
        public override uint ExpansionId => 0;
        public override uint ZoneId => 0;
        public override uint GatherType => 0;
        public override Vector2 MapPosition => new Vector2(-349.973f, -194.846f);
        public override int Radius => 146;

        public override HashSet<uint> NodeIds => new()
        {
            31624,
            31626,
            31652,
            31654,
            31680,
            31682,
        };

        public override HashSet<uint> ItemIds => new()
        {
            12,
            12534,
            12535,
            12537,
            13750,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 31624,
            },
            new NodeInfo
            {
                NodeId = 31626,
            },
            new NodeInfo
            {
                NodeId = 31652,
            },
            new NodeInfo
            {
                NodeId = 31654,
            },
            new NodeInfo
            {
                NodeId = 31680,
            },
            new NodeInfo
            {
                NodeId = 31682,
            },
        };
    }
}
