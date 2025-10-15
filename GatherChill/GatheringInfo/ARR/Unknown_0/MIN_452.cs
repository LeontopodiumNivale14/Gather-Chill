using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.ARR.Unknown_0
{
    public class MIN_452 : RouteInfo
    {
        public override uint Id => 452;
        public override uint ExpansionId => 0;
        public override uint ZoneId => 0;
        public override uint GatherType => 1;
        public override Vector2 MapPosition => new Vector2(-138.764f, -365.154f);
        public override int Radius => 65;

        public override HashSet<uint> NodeIds => new()
        {
            31981,
            31982,
            31983,
            31984,
            31985,
            31986,
            31987,
            31988,
            31989,
            31990,
            31991,
            31992,
        };

        public override HashSet<uint> ItemIds => new()
        {
            2002150,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 31981,
            },
            new NodeInfo
            {
                NodeId = 31982,
            },
            new NodeInfo
            {
                NodeId = 31983,
            },
            new NodeInfo
            {
                NodeId = 31984,
            },
            new NodeInfo
            {
                NodeId = 31985,
            },
            new NodeInfo
            {
                NodeId = 31986,
            },
            new NodeInfo
            {
                NodeId = 31987,
            },
            new NodeInfo
            {
                NodeId = 31988,
            },
            new NodeInfo
            {
                NodeId = 31989,
            },
            new NodeInfo
            {
                NodeId = 31990,
            },
            new NodeInfo
            {
                NodeId = 31991,
            },
            new NodeInfo
            {
                NodeId = 31992,
            },
        };
    }
}
