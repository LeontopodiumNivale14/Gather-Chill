using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.ARR.Unknown_0
{
    public class BTN_437 : RouteInfo
    {
        public override uint Id => 437;
        public override uint ExpansionId => 0;
        public override uint ZoneId => 0;
        public override uint GatherType => 2;
        public override Vector2 MapPosition => new Vector2(-339.553f, 144.26f);
        public override int Radius => 109;

        public override HashSet<uint> NodeIds => new()
        {
            31857,
            31858,
            31859,
            31860,
            31861,
            31862,
            31863,
            31864,
            31865,
            31866,
            31867,
            31868,
        };

        public override HashSet<uint> ItemIds => new()
        {
            2002127,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 31857,
            },
            new NodeInfo
            {
                NodeId = 31858,
            },
            new NodeInfo
            {
                NodeId = 31859,
            },
            new NodeInfo
            {
                NodeId = 31860,
            },
            new NodeInfo
            {
                NodeId = 31861,
            },
            new NodeInfo
            {
                NodeId = 31862,
            },
            new NodeInfo
            {
                NodeId = 31863,
            },
            new NodeInfo
            {
                NodeId = 31864,
            },
            new NodeInfo
            {
                NodeId = 31865,
            },
            new NodeInfo
            {
                NodeId = 31866,
            },
            new NodeInfo
            {
                NodeId = 31867,
            },
            new NodeInfo
            {
                NodeId = 31868,
            },
        };
    }
}
