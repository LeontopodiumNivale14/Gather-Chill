using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.ARR.Unknown_0
{
    public class BTN_951 : RouteInfo
    {
        public override uint Id => 951;
        public override uint ExpansionId => 0;
        public override uint ZoneId => 0;
        public override uint GatherType => 3;
        public override Vector2 MapPosition => new Vector2(540.526f, 583.796f);
        public override int Radius => 41;

        public override HashSet<uint> NodeIds => new()
        {
            34563,
            34564,
            34565,
            34566,
            34567,
            34568,
            34569,
            34570,
            34571,
            34572,
            34573,
            34574,
        };

        public override HashSet<uint> ItemIds => new()
        {
            2003524,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 34563,
            },
            new NodeInfo
            {
                NodeId = 34564,
            },
            new NodeInfo
            {
                NodeId = 34565,
            },
            new NodeInfo
            {
                NodeId = 34566,
            },
            new NodeInfo
            {
                NodeId = 34567,
            },
            new NodeInfo
            {
                NodeId = 34568,
            },
            new NodeInfo
            {
                NodeId = 34569,
            },
            new NodeInfo
            {
                NodeId = 34570,
            },
            new NodeInfo
            {
                NodeId = 34571,
            },
            new NodeInfo
            {
                NodeId = 34572,
            },
            new NodeInfo
            {
                NodeId = 34573,
            },
            new NodeInfo
            {
                NodeId = 34574,
            },
        };
    }
}
