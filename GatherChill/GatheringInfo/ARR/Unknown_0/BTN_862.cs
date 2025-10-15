using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.ARR.Unknown_0
{
    public class BTN_862 : RouteInfo
    {
        public override uint Id => 862;
        public override uint ExpansionId => 0;
        public override uint ZoneId => 0;
        public override uint GatherType => 3;
        public override Vector2 MapPosition => new Vector2(-424.438f, -452.943f);
        public override int Radius => 73;

        public override HashSet<uint> NodeIds => new()
        {
            34076,
            34077,
            34078,
            34079,
            34080,
            34081,
            34082,
            34083,
            34084,
            34085,
            34086,
            34087,
        };

        public override HashSet<uint> ItemIds => new()
        {
            2003136,
            2003137,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 34076,
            },
            new NodeInfo
            {
                NodeId = 34077,
            },
            new NodeInfo
            {
                NodeId = 34078,
            },
            new NodeInfo
            {
                NodeId = 34079,
            },
            new NodeInfo
            {
                NodeId = 34080,
            },
            new NodeInfo
            {
                NodeId = 34081,
            },
            new NodeInfo
            {
                NodeId = 34082,
            },
            new NodeInfo
            {
                NodeId = 34083,
            },
            new NodeInfo
            {
                NodeId = 34084,
            },
            new NodeInfo
            {
                NodeId = 34085,
            },
            new NodeInfo
            {
                NodeId = 34086,
            },
            new NodeInfo
            {
                NodeId = 34087,
            },
        };
    }
}
