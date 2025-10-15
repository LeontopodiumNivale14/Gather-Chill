using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.ARR.Unknown_0
{
    public class BTN_946 : RouteInfo
    {
        public override uint Id => 946;
        public override uint ExpansionId => 0;
        public override uint ZoneId => 0;
        public override uint GatherType => 3;
        public override Vector2 MapPosition => new Vector2(805.83f, -574.404f);
        public override int Radius => 55;

        public override HashSet<uint> NodeIds => new()
        {
            34517,
            34518,
            34519,
            34520,
            34521,
            34522,
        };

        public override HashSet<uint> ItemIds => new()
        {
            2003516,
            2003517,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 34517,
            },
            new NodeInfo
            {
                NodeId = 34518,
            },
            new NodeInfo
            {
                NodeId = 34519,
            },
            new NodeInfo
            {
                NodeId = 34520,
            },
            new NodeInfo
            {
                NodeId = 34521,
            },
            new NodeInfo
            {
                NodeId = 34522,
            },
        };
    }
}
