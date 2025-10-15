using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.ARR.Unknown_0
{
    public class BTN_255 : RouteInfo
    {
        public override uint Id => 255;
        public override uint ExpansionId => 0;
        public override uint ZoneId => 0;
        public override uint GatherType => 2;
        public override Vector2 MapPosition => new Vector2(252.807f, 197.907f);
        public override int Radius => 88;

        public override HashSet<uint> NodeIds => new()
        {
            31077,
            31078,
            31079,
            31080,
            31081,
            31082,
        };

        public override HashSet<uint> ItemIds => new()
        {
            2001848,
            2001849,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 31077,
            },
            new NodeInfo
            {
                NodeId = 31078,
            },
            new NodeInfo
            {
                NodeId = 31079,
            },
            new NodeInfo
            {
                NodeId = 31080,
            },
            new NodeInfo
            {
                NodeId = 31081,
            },
            new NodeInfo
            {
                NodeId = 31082,
            },
        };
    }
}
