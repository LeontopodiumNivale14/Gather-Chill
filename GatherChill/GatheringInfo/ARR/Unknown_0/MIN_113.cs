using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.ARR.Unknown_0
{
    public class MIN_113 : RouteInfo
    {
        public override uint Id => 113;
        public override uint ExpansionId => 0;
        public override uint ZoneId => 0;
        public override uint GatherType => 0;
        public override Vector2 MapPosition => new Vector2(273.774f, 60.5564f);
        public override int Radius => 50;

        public override HashSet<uint> NodeIds => new()
        {
            30830,
            30831,
            30832,
            30833,
            30834,
            30835,
            30836,
            30837,
            30838,
            30839,
        };

        public override HashSet<uint> ItemIds => new()
        {
            2000499,
            2000500,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 30830,
            },
            new NodeInfo
            {
                NodeId = 30831,
            },
            new NodeInfo
            {
                NodeId = 30832,
            },
            new NodeInfo
            {
                NodeId = 30833,
            },
            new NodeInfo
            {
                NodeId = 30834,
            },
            new NodeInfo
            {
                NodeId = 30835,
            },
            new NodeInfo
            {
                NodeId = 30836,
            },
            new NodeInfo
            {
                NodeId = 30837,
            },
            new NodeInfo
            {
                NodeId = 30838,
            },
            new NodeInfo
            {
                NodeId = 30839,
            },
        };
    }
}
