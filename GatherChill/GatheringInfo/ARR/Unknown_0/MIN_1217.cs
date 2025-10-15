using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.ARR.Unknown_0
{
    public class MIN_1217 : RouteInfo
    {
        public override uint Id => 1217;
        public override uint ExpansionId => 0;
        public override uint ZoneId => 0;
        public override uint GatherType => 1;
        public override Vector2 MapPosition => new Vector2(-693.598f, -458.293f);
        public override int Radius => 82;

        public override HashSet<uint> NodeIds => new()
        {
            35299,
            35300,
            35301,
            35302,
            35303,
            35304,
        };

        public override HashSet<uint> ItemIds => new()
        {
            47358,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 35299,
            },
            new NodeInfo
            {
                NodeId = 35300,
            },
            new NodeInfo
            {
                NodeId = 35301,
            },
            new NodeInfo
            {
                NodeId = 35302,
            },
            new NodeInfo
            {
                NodeId = 35303,
            },
            new NodeInfo
            {
                NodeId = 35304,
            },
        };
    }
}
