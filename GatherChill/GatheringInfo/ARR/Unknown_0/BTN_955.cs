using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.ARR.Unknown_0
{
    public class BTN_955 : RouteInfo
    {
        public override uint Id => 955;
        public override uint ExpansionId => 0;
        public override uint ZoneId => 0;
        public override uint GatherType => 2;
        public override Vector2 MapPosition => new Vector2(508.655f, -283.107f);
        public override int Radius => 66;

        public override HashSet<uint> NodeIds => new()
        {
            34597,
            34598,
            34599,
            34600,
            34601,
            34602,
        };

        public override HashSet<uint> ItemIds => new()
        {
            2003529,
            2003530,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 34597,
            },
            new NodeInfo
            {
                NodeId = 34598,
            },
            new NodeInfo
            {
                NodeId = 34599,
            },
            new NodeInfo
            {
                NodeId = 34600,
            },
            new NodeInfo
            {
                NodeId = 34601,
            },
            new NodeInfo
            {
                NodeId = 34602,
            },
        };
    }
}
