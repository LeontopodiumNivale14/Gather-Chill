using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.ARR.Unknown_0
{
    public class MIN_448 : RouteInfo
    {
        public override uint Id => 448;
        public override uint ExpansionId => 0;
        public override uint ZoneId => 0;
        public override uint GatherType => 0;
        public override Vector2 MapPosition => new Vector2(218.781f, -408.64f);
        public override int Radius => 26;

        public override HashSet<uint> NodeIds => new()
        {
            31947,
            31948,
            31949,
            31950,
            31951,
            31952,
            31953,
            31954,
            31955,
            31956,
            31957,
            31958,
        };

        public override HashSet<uint> ItemIds => new()
        {
            2002143,
            2002144,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 31947,
            },
            new NodeInfo
            {
                NodeId = 31948,
            },
            new NodeInfo
            {
                NodeId = 31949,
            },
            new NodeInfo
            {
                NodeId = 31950,
            },
            new NodeInfo
            {
                NodeId = 31951,
            },
            new NodeInfo
            {
                NodeId = 31952,
            },
            new NodeInfo
            {
                NodeId = 31953,
            },
            new NodeInfo
            {
                NodeId = 31954,
            },
            new NodeInfo
            {
                NodeId = 31955,
            },
            new NodeInfo
            {
                NodeId = 31956,
            },
            new NodeInfo
            {
                NodeId = 31957,
            },
            new NodeInfo
            {
                NodeId = 31958,
            },
        };
    }
}
