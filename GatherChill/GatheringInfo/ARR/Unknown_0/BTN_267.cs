using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.ARR.Unknown_0
{
    public class BTN_267 : RouteInfo
    {
        public override uint Id => 267;
        public override uint ExpansionId => 0;
        public override uint ZoneId => 0;
        public override uint GatherType => 2;
        public override Vector2 MapPosition => new Vector2(283.128f, -143.862f);
        public override int Radius => 99;

        public override HashSet<uint> NodeIds => new()
        {
            31179,
            31180,
            31181,
            31182,
            31183,
            31184,
        };

        public override HashSet<uint> ItemIds => new()
        {
            2001866,
            2001867,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 31179,
            },
            new NodeInfo
            {
                NodeId = 31180,
            },
            new NodeInfo
            {
                NodeId = 31181,
            },
            new NodeInfo
            {
                NodeId = 31182,
            },
            new NodeInfo
            {
                NodeId = 31183,
            },
            new NodeInfo
            {
                NodeId = 31184,
            },
        };
    }
}
