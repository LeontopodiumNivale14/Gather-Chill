using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.Heavensward.TheDiadem_901
{
    public class MIN_700 : RouteInfo
    {
        public override uint Id => 700;
        public override uint ExpansionId => 1;
        public override uint ZoneId => 901;
        public override uint GatherType => 0;
        public override Vector2 MapPosition => new Vector2(17.8756f, -182.38f);
        public override int Radius => 897;

        public override HashSet<uint> NodeIds => new()
        {
            33039,
            33044,
            33046,
            33051,
            33053,
            33058,
            33065,
            33072,
            33079,
            33084,
            33087,
            33092,
            33094,
            33099,
            33101,
            33106,
            33113,
            33120,
            33127,
            33132,
        };

        public override HashSet<uint> ItemIds => new()
        {
            29902,
            29911,
            29921,
            29931,
            29941,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 33039,
            },
            new NodeInfo
            {
                NodeId = 33044,
            },
            new NodeInfo
            {
                NodeId = 33046,
            },
            new NodeInfo
            {
                NodeId = 33051,
            },
            new NodeInfo
            {
                NodeId = 33053,
            },
            new NodeInfo
            {
                NodeId = 33058,
            },
            new NodeInfo
            {
                NodeId = 33065,
            },
            new NodeInfo
            {
                NodeId = 33072,
            },
            new NodeInfo
            {
                NodeId = 33079,
            },
            new NodeInfo
            {
                NodeId = 33084,
            },
            new NodeInfo
            {
                NodeId = 33087,
            },
            new NodeInfo
            {
                NodeId = 33092,
            },
            new NodeInfo
            {
                NodeId = 33094,
            },
            new NodeInfo
            {
                NodeId = 33099,
            },
            new NodeInfo
            {
                NodeId = 33101,
            },
            new NodeInfo
            {
                NodeId = 33106,
            },
            new NodeInfo
            {
                NodeId = 33113,
            },
            new NodeInfo
            {
                NodeId = 33120,
            },
            new NodeInfo
            {
                NodeId = 33127,
            },
            new NodeInfo
            {
                NodeId = 33132,
            },
        };
    }
}
