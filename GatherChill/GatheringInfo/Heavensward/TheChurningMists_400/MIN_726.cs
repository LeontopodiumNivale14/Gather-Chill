using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.Heavensward.TheChurningMists_400
{
    public class MIN_726 : RouteInfo
    {
        public override uint Id => 726;
        public override uint ExpansionId => 1;
        public override uint ZoneId => 400;
        public override uint GatherType => 0;
        public override Vector2 MapPosition => new Vector2(-680.041f, -747.855f);
        public override int Radius => 132;

        public override HashSet<uint> NodeIds => new()
        {
            33301,
            33302,
            33303,
            33304,
            33305,
            33306,
        };

        public override HashSet<uint> ItemIds => new()
        {
            11,
            30499,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 33301,
            },
            new NodeInfo
            {
                NodeId = 33302,
            },
            new NodeInfo
            {
                NodeId = 33303,
            },
            new NodeInfo
            {
                NodeId = 33304,
            },
            new NodeInfo
            {
                NodeId = 33305,
            },
            new NodeInfo
            {
                NodeId = 33306,
            },
        };
    }
}
