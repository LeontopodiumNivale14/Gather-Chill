using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.ARR.Unknown_0
{
    public class MIN_274 : RouteInfo
    {
        public override uint Id => 274;
        public override uint ExpansionId => 0;
        public override uint ZoneId => 0;
        public override uint GatherType => 0;
        public override Vector2 MapPosition => new Vector2(321.513f, 652.14f);
        public override int Radius => 52;

        public override HashSet<uint> NodeIds => new()
        {
            31235,
            31236,
            31237,
            31238,
            31239,
            31240,
            31241,
            31242,
            31243,
            31244,
            31245,
            31246,
        };

        public override HashSet<uint> ItemIds => new()
        {
            2001831,
            2001832,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 31235,
            },
            new NodeInfo
            {
                NodeId = 31236,
            },
            new NodeInfo
            {
                NodeId = 31237,
            },
            new NodeInfo
            {
                NodeId = 31238,
            },
            new NodeInfo
            {
                NodeId = 31239,
            },
            new NodeInfo
            {
                NodeId = 31240,
            },
            new NodeInfo
            {
                NodeId = 31241,
            },
            new NodeInfo
            {
                NodeId = 31242,
            },
            new NodeInfo
            {
                NodeId = 31243,
            },
            new NodeInfo
            {
                NodeId = 31244,
            },
            new NodeInfo
            {
                NodeId = 31245,
            },
            new NodeInfo
            {
                NodeId = 31246,
            },
        };
    }
}
