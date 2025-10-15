using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.ARR.Unknown_0
{
    public class BTN_861 : RouteInfo
    {
        public override uint Id => 861;
        public override uint ExpansionId => 0;
        public override uint ZoneId => 0;
        public override uint GatherType => 2;
        public override Vector2 MapPosition => new Vector2(-178.25f, -410.99f);
        public override int Radius => 44;

        public override HashSet<uint> NodeIds => new()
        {
            34070,
            34071,
            34072,
            34073,
            34074,
            34075,
        };

        public override HashSet<uint> ItemIds => new()
        {
            2003134,
            2003135,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 34070,
            },
            new NodeInfo
            {
                NodeId = 34071,
            },
            new NodeInfo
            {
                NodeId = 34072,
            },
            new NodeInfo
            {
                NodeId = 34073,
            },
            new NodeInfo
            {
                NodeId = 34074,
            },
            new NodeInfo
            {
                NodeId = 34075,
            },
        };
    }
}
