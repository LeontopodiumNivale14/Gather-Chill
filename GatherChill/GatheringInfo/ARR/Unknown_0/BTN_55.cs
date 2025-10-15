using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.ARR.Unknown_0
{
    public class BTN_55 : RouteInfo
    {
        public override uint Id => 55;
        public override uint ExpansionId => 0;
        public override uint ZoneId => 0;
        public override uint GatherType => 2;
        public override Vector2 MapPosition => new Vector2(383.687f, 79.6678f);
        public override int Radius => 42;

        public override HashSet<uint> NodeIds => new()
        {
            30180,
            30181,
            30182,
            30183,
            30184,
            30185,
            30186,
            30187,
            30188,
            30189,
        };

        public override HashSet<uint> ItemIds => new()
        {
            2000275,
            2000277,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 30180,
            },
            new NodeInfo
            {
                NodeId = 30181,
            },
            new NodeInfo
            {
                NodeId = 30182,
            },
            new NodeInfo
            {
                NodeId = 30183,
            },
            new NodeInfo
            {
                NodeId = 30184,
            },
            new NodeInfo
            {
                NodeId = 30185,
            },
            new NodeInfo
            {
                NodeId = 30186,
            },
            new NodeInfo
            {
                NodeId = 30187,
            },
            new NodeInfo
            {
                NodeId = 30188,
            },
            new NodeInfo
            {
                NodeId = 30189,
            },
        };
    }
}
