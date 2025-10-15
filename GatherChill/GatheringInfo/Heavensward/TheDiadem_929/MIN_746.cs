using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.Heavensward.TheDiadem_929
{
    public class MIN_746 : RouteInfo
    {
        public override uint Id => 746;
        public override uint ExpansionId => 1;
        public override uint ZoneId => 929;
        public override uint GatherType => 1;
        public override Vector2 MapPosition => new Vector2(94.8895f, -133.217f);
        public override int Radius => 951;

        public override HashSet<uint> NodeIds => new()
        {
            33396,
            33403,
            33410,
            33415,
            33417,
            33422,
            33424,
            33429,
            33436,
            33440,
            33445,
            33452,
            33459,
            33466,
            33471,
            33473,
            33478,
            33480,
            33485,
        };

        public override HashSet<uint> ItemIds => new()
        {
            29943,
            31285,
            31295,
            31305,
            31315,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 33396,
            },
            new NodeInfo
            {
                NodeId = 33403,
            },
            new NodeInfo
            {
                NodeId = 33410,
            },
            new NodeInfo
            {
                NodeId = 33415,
            },
            new NodeInfo
            {
                NodeId = 33417,
            },
            new NodeInfo
            {
                NodeId = 33422,
            },
            new NodeInfo
            {
                NodeId = 33424,
            },
            new NodeInfo
            {
                NodeId = 33429,
            },
            new NodeInfo
            {
                NodeId = 33436,
            },
            new NodeInfo
            {
                NodeId = 33440,
            },
            new NodeInfo
            {
                NodeId = 33445,
            },
            new NodeInfo
            {
                NodeId = 33452,
            },
            new NodeInfo
            {
                NodeId = 33459,
            },
            new NodeInfo
            {
                NodeId = 33466,
            },
            new NodeInfo
            {
                NodeId = 33471,
            },
            new NodeInfo
            {
                NodeId = 33473,
            },
            new NodeInfo
            {
                NodeId = 33478,
            },
            new NodeInfo
            {
                NodeId = 33480,
            },
            new NodeInfo
            {
                NodeId = 33485,
            },
        };
    }
}
