using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.Heavensward.TheDiadem_901
{
    public class MIN_702 : RouteInfo
    {
        public override uint Id => 702;
        public override uint ExpansionId => 1;
        public override uint ZoneId => 901;
        public override uint GatherType => 1;
        public override Vector2 MapPosition => new Vector2(125.923f, -6.81933f);
        public override int Radius => 867;

        public override HashSet<uint> NodeIds => new()
        {
            33041,
            33048,
            33055,
            33060,
            33062,
            33067,
            33069,
            33074,
            33081,
            33085,
            33090,
            33097,
            33104,
            33111,
            33116,
            33118,
            33123,
            33125,
            33130,
        };

        public override HashSet<uint> ItemIds => new()
        {
            29903,
            29913,
            29923,
            29933,
            29943,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 33041,
            },
            new NodeInfo
            {
                NodeId = 33048,
            },
            new NodeInfo
            {
                NodeId = 33055,
            },
            new NodeInfo
            {
                NodeId = 33060,
            },
            new NodeInfo
            {
                NodeId = 33062,
            },
            new NodeInfo
            {
                NodeId = 33067,
            },
            new NodeInfo
            {
                NodeId = 33069,
            },
            new NodeInfo
            {
                NodeId = 33074,
            },
            new NodeInfo
            {
                NodeId = 33081,
            },
            new NodeInfo
            {
                NodeId = 33085,
            },
            new NodeInfo
            {
                NodeId = 33090,
            },
            new NodeInfo
            {
                NodeId = 33097,
            },
            new NodeInfo
            {
                NodeId = 33104,
            },
            new NodeInfo
            {
                NodeId = 33111,
            },
            new NodeInfo
            {
                NodeId = 33116,
            },
            new NodeInfo
            {
                NodeId = 33118,
            },
            new NodeInfo
            {
                NodeId = 33123,
            },
            new NodeInfo
            {
                NodeId = 33125,
            },
            new NodeInfo
            {
                NodeId = 33130,
            },
        };
    }
}
