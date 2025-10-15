using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.ARR.Unknown_0
{
    public class BTN_550 : RouteInfo
    {
        public override uint Id => 550;
        public override uint ExpansionId => 0;
        public override uint ZoneId => 0;
        public override uint GatherType => 3;
        public override Vector2 MapPosition => new Vector2(620.513f, -451.284f);
        public override int Radius => 24;

        public override HashSet<uint> NodeIds => new()
        {
            32341,
            32342,
            32343,
        };

        public override HashSet<uint> ItemIds => new()
        {
            12,
            22646,
            22647,
            22657,
            22661,
            22663,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 32341,
            },
            new NodeInfo
            {
                NodeId = 32342,
            },
            new NodeInfo
            {
                NodeId = 32343,
            },
        };
    }
}
