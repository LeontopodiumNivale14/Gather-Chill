using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.ARR.Unknown_0
{
    public class MIN_1045 : RouteInfo
    {
        public override uint Id => 1045;
        public override uint ExpansionId => 0;
        public override uint ZoneId => 0;
        public override uint GatherType => 0;
        public override Vector2 MapPosition => new Vector2(59.3171f, -441.656f);
        public override int Radius => 71;

        public override HashSet<uint> NodeIds => new()
        {
            35041,
            35042,
            35043,
            35044,
            35045,
            35046,
            35047,
            35048,
        };

        public override HashSet<uint> ItemIds => new()
        {
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 35041,
            },
            new NodeInfo
            {
                NodeId = 35042,
            },
            new NodeInfo
            {
                NodeId = 35043,
            },
            new NodeInfo
            {
                NodeId = 35044,
            },
            new NodeInfo
            {
                NodeId = 35045,
            },
            new NodeInfo
            {
                NodeId = 35046,
            },
            new NodeInfo
            {
                NodeId = 35047,
            },
            new NodeInfo
            {
                NodeId = 35048,
            },
        };
    }
}
