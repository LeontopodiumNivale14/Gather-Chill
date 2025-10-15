using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.ARR.Unknown_0
{
    public class MIN_454 : RouteInfo
    {
        public override uint Id => 454;
        public override uint ExpansionId => 0;
        public override uint ZoneId => 0;
        public override uint GatherType => 1;
        public override Vector2 MapPosition => new Vector2(513.542f, -131.618f);
        public override int Radius => 61;

        public override HashSet<uint> NodeIds => new()
        {
            31997,
            31998,
            31999,
            32000,
            32001,
            32002,
            32003,
            32004,
            32005,
            32006,
            32007,
            32008,
        };

        public override HashSet<uint> ItemIds => new()
        {
            2002152,
            2002153,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 31997,
            },
            new NodeInfo
            {
                NodeId = 31998,
            },
            new NodeInfo
            {
                NodeId = 31999,
            },
            new NodeInfo
            {
                NodeId = 32000,
            },
            new NodeInfo
            {
                NodeId = 32001,
            },
            new NodeInfo
            {
                NodeId = 32002,
            },
            new NodeInfo
            {
                NodeId = 32003,
            },
            new NodeInfo
            {
                NodeId = 32004,
            },
            new NodeInfo
            {
                NodeId = 32005,
            },
            new NodeInfo
            {
                NodeId = 32006,
            },
            new NodeInfo
            {
                NodeId = 32007,
            },
            new NodeInfo
            {
                NodeId = 32008,
            },
        };
    }
}
