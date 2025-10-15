using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.ARR.Unknown_0
{
    public class MIN_415 : RouteInfo
    {
        public override uint Id => 415;
        public override uint ExpansionId => 0;
        public override uint ZoneId => 0;
        public override uint GatherType => 0;
        public override Vector2 MapPosition => new Vector2(-454.09f, 455.89f);
        public override int Radius => 163;

        public override HashSet<uint> NodeIds => new()
        {
            31761,
            31762,
            31763,
            31764,
        };

        public override HashSet<uint> ItemIds => new()
        {
            6,
            12,
            7588,
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
                NodeId = 31761,
            },
            new NodeInfo
            {
                NodeId = 31762,
            },
            new NodeInfo
            {
                NodeId = 31763,
            },
            new NodeInfo
            {
                NodeId = 31764,
            },
        };
    }
}
