using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.ARR.Unknown_0
{
    public class MIN_98 : RouteInfo
    {
        public override uint Id => 98;
        public override uint ExpansionId => 0;
        public override uint ZoneId => 0;
        public override uint GatherType => 0;
        public override Vector2 MapPosition => new Vector2(-142.325f, 270.03f);
        public override int Radius => 36;

        public override HashSet<uint> NodeIds => new()
        {
            30722,
            30723,
            30724,
            30725,
            30726,
            30727,
            30728,
            30729,
            30730,
            30731,
        };

        public override HashSet<uint> ItemIds => new()
        {
            2000477,
            2000478,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 30722,
            },
            new NodeInfo
            {
                NodeId = 30723,
            },
            new NodeInfo
            {
                NodeId = 30724,
            },
            new NodeInfo
            {
                NodeId = 30725,
            },
            new NodeInfo
            {
                NodeId = 30726,
            },
            new NodeInfo
            {
                NodeId = 30727,
            },
            new NodeInfo
            {
                NodeId = 30728,
            },
            new NodeInfo
            {
                NodeId = 30729,
            },
            new NodeInfo
            {
                NodeId = 30730,
            },
            new NodeInfo
            {
                NodeId = 30731,
            },
        };
    }
}
