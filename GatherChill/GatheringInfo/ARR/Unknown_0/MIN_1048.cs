using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.ARR.Unknown_0
{
    public class MIN_1048 : RouteInfo
    {
        public override uint Id => 1048;
        public override uint ExpansionId => 0;
        public override uint ZoneId => 0;
        public override uint GatherType => 1;
        public override Vector2 MapPosition => new Vector2(98.2044f, 266.364f);
        public override int Radius => 81;

        public override HashSet<uint> NodeIds => new()
        {
            35063,
            35064,
            35065,
            35066,
            35067,
            35068,
        };

        public override HashSet<uint> ItemIds => new()
        {
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 35063,
            },
            new NodeInfo
            {
                NodeId = 35064,
            },
            new NodeInfo
            {
                NodeId = 35065,
            },
            new NodeInfo
            {
                NodeId = 35066,
            },
            new NodeInfo
            {
                NodeId = 35067,
            },
            new NodeInfo
            {
                NodeId = 35068,
            },
        };
    }
}
