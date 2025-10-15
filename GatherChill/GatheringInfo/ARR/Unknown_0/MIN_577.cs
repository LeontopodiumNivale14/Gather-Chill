using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.ARR.Unknown_0
{
    public class MIN_577 : RouteInfo
    {
        public override uint Id => 577;
        public override uint ExpansionId => 0;
        public override uint ZoneId => 0;
        public override uint GatherType => 0;
        public override Vector2 MapPosition => new Vector2(-300.589f, -222.65f);
        public override int Radius => 107;

        public override HashSet<uint> NodeIds => new()
        {
            32518,
            32519,
            32520,
            32521,
            32522,
            32523,
            32524,
            32525,
            32526,
            32527,
            32528,
            32529,
        };

        public override HashSet<uint> ItemIds => new()
        {
            2002787,
            2002788,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 32518,
            },
            new NodeInfo
            {
                NodeId = 32519,
            },
            new NodeInfo
            {
                NodeId = 32520,
            },
            new NodeInfo
            {
                NodeId = 32521,
            },
            new NodeInfo
            {
                NodeId = 32522,
            },
            new NodeInfo
            {
                NodeId = 32523,
            },
            new NodeInfo
            {
                NodeId = 32524,
            },
            new NodeInfo
            {
                NodeId = 32525,
            },
            new NodeInfo
            {
                NodeId = 32526,
            },
            new NodeInfo
            {
                NodeId = 32527,
            },
            new NodeInfo
            {
                NodeId = 32528,
            },
            new NodeInfo
            {
                NodeId = 32529,
            },
        };
    }
}
