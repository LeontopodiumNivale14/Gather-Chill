using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.Heavensward.TheDravanianForelands_398
{
    public class BTN_725 : RouteInfo
    {
        public override uint Id => 725;
        public override uint ExpansionId => 1;
        public override uint ZoneId => 398;
        public override uint GatherType => 2;
        public override Vector2 MapPosition => new Vector2(642.264f, -575.01f);
        public override int Radius => 111;

        public override HashSet<uint> NodeIds => new()
        {
            33295,
            33296,
            33297,
            33298,
            33299,
            33300,
        };

        public override HashSet<uint> ItemIds => new()
        {
            8,
            30499,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 33295,
            },
            new NodeInfo
            {
                NodeId = 33296,
            },
            new NodeInfo
            {
                NodeId = 33297,
            },
            new NodeInfo
            {
                NodeId = 33298,
            },
            new NodeInfo
            {
                NodeId = 33299,
            },
            new NodeInfo
            {
                NodeId = 33300,
            },
        };
    }
}
