using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.ARR.Unknown_0
{
    public class MIN_272 : RouteInfo
    {
        public override uint Id => 272;
        public override uint ExpansionId => 0;
        public override uint ZoneId => 0;
        public override uint GatherType => 0;
        public override Vector2 MapPosition => new Vector2(610.875f, 187.396f);
        public override int Radius => 76;

        public override HashSet<uint> NodeIds => new()
        {
            31219,
            31220,
            31221,
            31222,
            31223,
            31224,
            31225,
            31226,
            31227,
            31228,
            31229,
            31230,
        };

        public override HashSet<uint> ItemIds => new()
        {
            2001829,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 31219,
            },
            new NodeInfo
            {
                NodeId = 31220,
            },
            new NodeInfo
            {
                NodeId = 31221,
            },
            new NodeInfo
            {
                NodeId = 31222,
            },
            new NodeInfo
            {
                NodeId = 31223,
            },
            new NodeInfo
            {
                NodeId = 31224,
            },
            new NodeInfo
            {
                NodeId = 31225,
            },
            new NodeInfo
            {
                NodeId = 31226,
            },
            new NodeInfo
            {
                NodeId = 31227,
            },
            new NodeInfo
            {
                NodeId = 31228,
            },
            new NodeInfo
            {
                NodeId = 31229,
            },
            new NodeInfo
            {
                NodeId = 31230,
            },
        };
    }
}
