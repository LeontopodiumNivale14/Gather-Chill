using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.ARR.Unknown_0
{
    public class MIN_117 : RouteInfo
    {
        public override uint Id => 117;
        public override uint ExpansionId => 0;
        public override uint ZoneId => 0;
        public override uint GatherType => 0;
        public override Vector2 MapPosition => new Vector2(218.341f, 438.523f);
        public override int Radius => 30;

        public override HashSet<uint> NodeIds => new()
        {
            30864,
            30865,
            30866,
            30867,
            30868,
            30869,
            30870,
            30871,
            30872,
            30873,
        };

        public override HashSet<uint> ItemIds => new()
        {
            2000504,
            2000505,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 30864,
            },
            new NodeInfo
            {
                NodeId = 30865,
            },
            new NodeInfo
            {
                NodeId = 30866,
            },
            new NodeInfo
            {
                NodeId = 30867,
            },
            new NodeInfo
            {
                NodeId = 30868,
            },
            new NodeInfo
            {
                NodeId = 30869,
            },
            new NodeInfo
            {
                NodeId = 30870,
            },
            new NodeInfo
            {
                NodeId = 30871,
            },
            new NodeInfo
            {
                NodeId = 30872,
            },
            new NodeInfo
            {
                NodeId = 30873,
            },
        };
    }
}
