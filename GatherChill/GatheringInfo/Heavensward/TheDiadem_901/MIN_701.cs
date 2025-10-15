using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.Heavensward.TheDiadem_901
{
    public class MIN_701 : RouteInfo
    {
        public override uint Id => 701;
        public override uint ExpansionId => 1;
        public override uint ZoneId => 901;
        public override uint GatherType => 1;
        public override Vector2 MapPosition => new Vector2(79.9003f, -125.358f);
        public override int Radius => 834;

        public override HashSet<uint> NodeIds => new()
        {
            33040,
            33047,
            33052,
            33054,
            33059,
            33061,
            33066,
            33073,
            33080,
            33086,
            33091,
            33093,
            33098,
            33105,
            33112,
            33119,
            33124,
            33126,
            33131,
        };

        public override HashSet<uint> ItemIds => new()
        {
            29897,
            29912,
            29922,
            29932,
            29942,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 33040,
            },
            new NodeInfo
            {
                NodeId = 33047,
            },
            new NodeInfo
            {
                NodeId = 33052,
            },
            new NodeInfo
            {
                NodeId = 33054,
            },
            new NodeInfo
            {
                NodeId = 33059,
            },
            new NodeInfo
            {
                NodeId = 33061,
            },
            new NodeInfo
            {
                NodeId = 33066,
            },
            new NodeInfo
            {
                NodeId = 33073,
            },
            new NodeInfo
            {
                NodeId = 33080,
            },
            new NodeInfo
            {
                NodeId = 33086,
            },
            new NodeInfo
            {
                NodeId = 33091,
            },
            new NodeInfo
            {
                NodeId = 33093,
            },
            new NodeInfo
            {
                NodeId = 33098,
            },
            new NodeInfo
            {
                NodeId = 33105,
            },
            new NodeInfo
            {
                NodeId = 33112,
            },
            new NodeInfo
            {
                NodeId = 33119,
            },
            new NodeInfo
            {
                NodeId = 33124,
            },
            new NodeInfo
            {
                NodeId = 33126,
            },
            new NodeInfo
            {
                NodeId = 33131,
            },
        };
    }
}
