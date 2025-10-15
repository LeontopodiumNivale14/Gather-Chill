using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.ARR.Unknown_0
{
    public class BTN_363 : RouteInfo
    {
        public override uint Id => 363;
        public override uint ExpansionId => 0;
        public override uint ZoneId => 0;
        public override uint GatherType => 2;
        public override Vector2 MapPosition => new Vector2(93.6303f, 567.327f);
        public override int Radius => 155;

        public override HashSet<uint> NodeIds => new()
        {
            31515,
            31517,
            31547,
            31549,
            31579,
            31581,
        };

        public override HashSet<uint> ItemIds => new()
        {
            11,
            12579,
            12586,
            12891,
            13752,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 31515,
            },
            new NodeInfo
            {
                NodeId = 31517,
            },
            new NodeInfo
            {
                NodeId = 31547,
            },
            new NodeInfo
            {
                NodeId = 31549,
            },
            new NodeInfo
            {
                NodeId = 31579,
            },
            new NodeInfo
            {
                NodeId = 31581,
            },
        };
    }
}
