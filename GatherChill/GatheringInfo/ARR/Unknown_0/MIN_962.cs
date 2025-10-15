using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.ARR.Unknown_0
{
    public class MIN_962 : RouteInfo
    {
        public override uint Id => 962;
        public override uint ExpansionId => 0;
        public override uint ZoneId => 0;
        public override uint GatherType => 1;
        public override Vector2 MapPosition => new Vector2(233.827f, -49.8295f);
        public override int Radius => 31;

        public override HashSet<uint> NodeIds => new()
        {
            34647,
            34648,
            34649,
            34650,
            34651,
            34652,
            34653,
            34654,
            34655,
            34656,
            34657,
            34658,
        };

        public override HashSet<uint> ItemIds => new()
        {
            2003541,
            2003542,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 34647,
            },
            new NodeInfo
            {
                NodeId = 34648,
            },
            new NodeInfo
            {
                NodeId = 34649,
            },
            new NodeInfo
            {
                NodeId = 34650,
            },
            new NodeInfo
            {
                NodeId = 34651,
            },
            new NodeInfo
            {
                NodeId = 34652,
            },
            new NodeInfo
            {
                NodeId = 34653,
            },
            new NodeInfo
            {
                NodeId = 34654,
            },
            new NodeInfo
            {
                NodeId = 34655,
            },
            new NodeInfo
            {
                NodeId = 34656,
            },
            new NodeInfo
            {
                NodeId = 34657,
            },
            new NodeInfo
            {
                NodeId = 34658,
            },
        };
    }
}
