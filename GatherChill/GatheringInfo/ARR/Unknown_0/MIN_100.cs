using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.ARR.Unknown_0
{
    public class MIN_100 : RouteInfo
    {
        public override uint Id => 100;
        public override uint ExpansionId => 0;
        public override uint ZoneId => 0;
        public override uint GatherType => 0;
        public override Vector2 MapPosition => new Vector2(101.641f, 117.07f);
        public override int Radius => 102;

        public override HashSet<uint> NodeIds => new()
        {
            30736,
            30737,
            30738,
            30739,
            30740,
            30741,
            30742,
            30743,
            30744,
            30745,
        };

        public override HashSet<uint> ItemIds => new()
        {
            2000480,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 30736,
            },
            new NodeInfo
            {
                NodeId = 30737,
            },
            new NodeInfo
            {
                NodeId = 30738,
            },
            new NodeInfo
            {
                NodeId = 30739,
            },
            new NodeInfo
            {
                NodeId = 30740,
            },
            new NodeInfo
            {
                NodeId = 30741,
            },
            new NodeInfo
            {
                NodeId = 30742,
            },
            new NodeInfo
            {
                NodeId = 30743,
            },
            new NodeInfo
            {
                NodeId = 30744,
            },
            new NodeInfo
            {
                NodeId = 30745,
            },
        };
    }
}
