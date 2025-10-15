using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.Dawntrail.Shaaloani_1190
{
    public class BTN_999 : RouteInfo
    {
        public override uint Id => 999;
        public override uint ExpansionId => 5;
        public override uint ZoneId => 1190;
        public override uint GatherType => 3;
        public override Vector2 MapPosition => new Vector2(486.737f, 158.473f);
        public override int Radius => 144;

        public override HashSet<uint> NodeIds => new()
        {
            34899,
            34900,
            34901,
            34902,
            34903,
            34904,
        };

        public override HashSet<uint> ItemIds => new()
        {
            10,
            43901,
            43980,
            44852,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 34899,
            },
            new NodeInfo
            {
                NodeId = 34900,
            },
            new NodeInfo
            {
                NodeId = 34901,
            },
            new NodeInfo
            {
                NodeId = 34902,
            },
            new NodeInfo
            {
                NodeId = 34903,
            },
            new NodeInfo
            {
                NodeId = 34904,
            },
        };
    }
}
