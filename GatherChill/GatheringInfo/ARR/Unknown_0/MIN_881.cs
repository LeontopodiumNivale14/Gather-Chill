using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.ARR.Unknown_0
{
    public class MIN_881 : RouteInfo
    {
        public override uint Id => 881;
        public override uint ExpansionId => 0;
        public override uint ZoneId => 0;
        public override uint GatherType => 0;
        public override Vector2 MapPosition => new Vector2(538.123f, 263.174f);
        public override int Radius => 44;

        public override HashSet<uint> NodeIds => new()
        {
            34240,
            34241,
            34242,
            34243,
            34244,
            34245,
            34246,
            34247,
            34248,
            34249,
            34250,
            34251,
        };

        public override HashSet<uint> ItemIds => new()
        {
            2003165,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 34240,
            },
            new NodeInfo
            {
                NodeId = 34241,
            },
            new NodeInfo
            {
                NodeId = 34242,
            },
            new NodeInfo
            {
                NodeId = 34243,
            },
            new NodeInfo
            {
                NodeId = 34244,
            },
            new NodeInfo
            {
                NodeId = 34245,
            },
            new NodeInfo
            {
                NodeId = 34246,
            },
            new NodeInfo
            {
                NodeId = 34247,
            },
            new NodeInfo
            {
                NodeId = 34248,
            },
            new NodeInfo
            {
                NodeId = 34249,
            },
            new NodeInfo
            {
                NodeId = 34250,
            },
            new NodeInfo
            {
                NodeId = 34251,
            },
        };
    }
}
