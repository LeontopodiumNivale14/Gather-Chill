using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.Heavensward.TheSeaofClouds_401
{
    public class MIN_728 : RouteInfo
    {
        public override uint Id => 728;
        public override uint ExpansionId => 1;
        public override uint ZoneId => 401;
        public override uint GatherType => 0;
        public override Vector2 MapPosition => new Vector2(370.034f, -750.682f);
        public override int Radius => 96;

        public override HashSet<uint> NodeIds => new()
        {
            33313,
            33314,
            33315,
            33316,
            33317,
            33318,
        };

        public override HashSet<uint> ItemIds => new()
        {
            13,
            30500,
            33230,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 33313,
            },
            new NodeInfo
            {
                NodeId = 33314,
            },
            new NodeInfo
            {
                NodeId = 33315,
            },
            new NodeInfo
            {
                NodeId = 33316,
            },
            new NodeInfo
            {
                NodeId = 33317,
            },
            new NodeInfo
            {
                NodeId = 33318,
            },
        };
    }
}
