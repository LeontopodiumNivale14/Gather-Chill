using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.ARR.Unknown_0
{
    public class BTN_563 : RouteInfo
    {
        public override uint Id => 563;
        public override uint ExpansionId => 0;
        public override uint ZoneId => 0;
        public override uint GatherType => 2;
        public override Vector2 MapPosition => new Vector2(-218.844f, -410.823f);
        public override int Radius => 120;

        public override HashSet<uint> NodeIds => new()
        {
            32406,
            32407,
            32408,
            32409,
            32410,
            32411,
            32412,
            32413,
            32414,
            32415,
            32416,
            32417,
        };

        public override HashSet<uint> ItemIds => new()
        {
            2002766,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 32406,
            },
            new NodeInfo
            {
                NodeId = 32407,
            },
            new NodeInfo
            {
                NodeId = 32408,
            },
            new NodeInfo
            {
                NodeId = 32409,
            },
            new NodeInfo
            {
                NodeId = 32410,
            },
            new NodeInfo
            {
                NodeId = 32411,
            },
            new NodeInfo
            {
                NodeId = 32412,
            },
            new NodeInfo
            {
                NodeId = 32413,
            },
            new NodeInfo
            {
                NodeId = 32414,
            },
            new NodeInfo
            {
                NodeId = 32415,
            },
            new NodeInfo
            {
                NodeId = 32416,
            },
            new NodeInfo
            {
                NodeId = 32417,
            },
        };
    }
}
