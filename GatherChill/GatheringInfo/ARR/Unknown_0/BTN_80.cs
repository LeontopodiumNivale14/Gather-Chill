using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.ARR.Unknown_0
{
    public class BTN_80 : RouteInfo
    {
        public override uint Id => 80;
        public override uint ExpansionId => 0;
        public override uint ZoneId => 0;
        public override uint GatherType => 2;
        public override Vector2 MapPosition => new Vector2(390.528f, 370.937f);
        public override int Radius => 51;

        public override HashSet<uint> NodeIds => new()
        {
            30584,
            30585,
            30586,
            30587,
            30588,
            30589,
            30590,
            30591,
            30592,
            30593,
        };

        public override HashSet<uint> ItemIds => new()
        {
            2000468,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 30584,
            },
            new NodeInfo
            {
                NodeId = 30585,
            },
            new NodeInfo
            {
                NodeId = 30586,
            },
            new NodeInfo
            {
                NodeId = 30587,
            },
            new NodeInfo
            {
                NodeId = 30588,
            },
            new NodeInfo
            {
                NodeId = 30589,
            },
            new NodeInfo
            {
                NodeId = 30590,
            },
            new NodeInfo
            {
                NodeId = 30591,
            },
            new NodeInfo
            {
                NodeId = 30592,
            },
            new NodeInfo
            {
                NodeId = 30593,
            },
        };
    }
}
