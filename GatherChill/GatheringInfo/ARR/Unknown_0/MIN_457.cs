using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.ARR.Unknown_0
{
    public class MIN_457 : RouteInfo
    {
        public override uint Id => 457;
        public override uint ExpansionId => 0;
        public override uint ZoneId => 0;
        public override uint GatherType => 1;
        public override Vector2 MapPosition => new Vector2(580.347f, -373.445f);
        public override int Radius => 81;

        public override HashSet<uint> NodeIds => new()
        {
            32019,
            32020,
            32021,
            32022,
            32023,
            32024,
            32025,
            32026,
            32027,
            32028,
            32029,
            32030,
        };

        public override HashSet<uint> ItemIds => new()
        {
            2002157,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 32019,
            },
            new NodeInfo
            {
                NodeId = 32020,
            },
            new NodeInfo
            {
                NodeId = 32021,
            },
            new NodeInfo
            {
                NodeId = 32022,
            },
            new NodeInfo
            {
                NodeId = 32023,
            },
            new NodeInfo
            {
                NodeId = 32024,
            },
            new NodeInfo
            {
                NodeId = 32025,
            },
            new NodeInfo
            {
                NodeId = 32026,
            },
            new NodeInfo
            {
                NodeId = 32027,
            },
            new NodeInfo
            {
                NodeId = 32028,
            },
            new NodeInfo
            {
                NodeId = 32029,
            },
            new NodeInfo
            {
                NodeId = 32030,
            },
        };
    }
}
