using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.ARR.Unknown_0
{
    public class MIN_379 : RouteInfo
    {
        public override uint Id => 379;
        public override uint ExpansionId => 0;
        public override uint ZoneId => 0;
        public override uint GatherType => 0;
        public override Vector2 MapPosition => new Vector2(286.129f, 218.171f);
        public override int Radius => 115;

        public override HashSet<uint> NodeIds => new()
        {
            31611,
            31613,
            31639,
            31641,
            31667,
            31669,
        };

        public override HashSet<uint> ItemIds => new()
        {
            8,
            12534,
            12535,
            12537,
            13750,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 31611,
            },
            new NodeInfo
            {
                NodeId = 31613,
            },
            new NodeInfo
            {
                NodeId = 31639,
            },
            new NodeInfo
            {
                NodeId = 31641,
            },
            new NodeInfo
            {
                NodeId = 31667,
            },
            new NodeInfo
            {
                NodeId = 31669,
            },
        };
    }
}
