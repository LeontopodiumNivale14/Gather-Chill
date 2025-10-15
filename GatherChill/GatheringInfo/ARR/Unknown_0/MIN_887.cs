using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.ARR.Unknown_0
{
    public class MIN_887 : RouteInfo
    {
        public override uint Id => 887;
        public override uint ExpansionId => 0;
        public override uint ZoneId => 0;
        public override uint GatherType => 1;
        public override Vector2 MapPosition => new Vector2(-738.432f, -58.0286f);
        public override int Radius => 9;

        public override HashSet<uint> NodeIds => new()
        {
            34292,
            34293,
            34294,
            34295,
        };

        public override HashSet<uint> ItemIds => new()
        {
            2003174,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 34292,
            },
            new NodeInfo
            {
                NodeId = 34293,
            },
            new NodeInfo
            {
                NodeId = 34294,
            },
            new NodeInfo
            {
                NodeId = 34295,
            },
        };
    }
}
