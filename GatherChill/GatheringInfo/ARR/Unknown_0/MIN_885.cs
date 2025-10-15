using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.ARR.Unknown_0
{
    public class MIN_885 : RouteInfo
    {
        public override uint Id => 885;
        public override uint ExpansionId => 0;
        public override uint ZoneId => 0;
        public override uint GatherType => 1;
        public override Vector2 MapPosition => new Vector2(-11.9306f, -277.904f);
        public override int Radius => 22;

        public override HashSet<uint> NodeIds => new()
        {
            34274,
            34275,
            34276,
            34277,
            34278,
            34279,
        };

        public override HashSet<uint> ItemIds => new()
        {
            2003170,
            2003171,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 34274,
            },
            new NodeInfo
            {
                NodeId = 34275,
            },
            new NodeInfo
            {
                NodeId = 34276,
            },
            new NodeInfo
            {
                NodeId = 34277,
            },
            new NodeInfo
            {
                NodeId = 34278,
            },
            new NodeInfo
            {
                NodeId = 34279,
            },
        };
    }
}
