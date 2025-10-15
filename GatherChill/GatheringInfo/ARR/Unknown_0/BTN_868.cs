using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.ARR.Unknown_0
{
    public class BTN_868 : RouteInfo
    {
        public override uint Id => 868;
        public override uint ExpansionId => 0;
        public override uint ZoneId => 0;
        public override uint GatherType => 2;
        public override Vector2 MapPosition => new Vector2(450.981f, -22.9764f);
        public override int Radius => 112;

        public override HashSet<uint> NodeIds => new()
        {
            34134,
            34135,
            34136,
            34137,
            34138,
            34139,
            34140,
            34141,
            34142,
            34143,
            34144,
            34145,
        };

        public override HashSet<uint> ItemIds => new()
        {
            2003145,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 34134,
            },
            new NodeInfo
            {
                NodeId = 34135,
            },
            new NodeInfo
            {
                NodeId = 34136,
            },
            new NodeInfo
            {
                NodeId = 34137,
            },
            new NodeInfo
            {
                NodeId = 34138,
            },
            new NodeInfo
            {
                NodeId = 34139,
            },
            new NodeInfo
            {
                NodeId = 34140,
            },
            new NodeInfo
            {
                NodeId = 34141,
            },
            new NodeInfo
            {
                NodeId = 34142,
            },
            new NodeInfo
            {
                NodeId = 34143,
            },
            new NodeInfo
            {
                NodeId = 34144,
            },
            new NodeInfo
            {
                NodeId = 34145,
            },
        };
    }
}
