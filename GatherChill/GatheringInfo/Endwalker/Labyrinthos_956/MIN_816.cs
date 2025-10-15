using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.Endwalker.Labyrinthos_956
{
    public class MIN_816 : RouteInfo
    {
        public override uint Id => 816;
        public override uint ExpansionId => 4;
        public override uint ZoneId => 956;
        public override uint GatherType => 1;
        public override Vector2 MapPosition => new Vector2(63.741f, -472.148f);
        public override int Radius => 101;

        public override HashSet<uint> NodeIds => new()
        {
            33900,
            33901,
            33902,
            33903,
            33904,
            33905,
        };

        public override HashSet<uint> ItemIds => new()
        {
            13,
            35848,
            36162,
            36180,
            36672,
            41068,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 33900,
            },
            new NodeInfo
            {
                NodeId = 33901,
            },
            new NodeInfo
            {
                NodeId = 33902,
            },
            new NodeInfo
            {
                NodeId = 33903,
            },
            new NodeInfo
            {
                NodeId = 33904,
            },
            new NodeInfo
            {
                NodeId = 33905,
            },
        };
    }
}
