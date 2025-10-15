using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.ARealmReborn.CentralThanalan_141
{
    public class MIN_153 : RouteInfo
    {
        public override uint Id => 153;
        public override uint ExpansionId => 0;
        public override uint ZoneId => 141;
        public override uint GatherType => 0;
        public override Vector2 MapPosition => new Vector2(-115.92f, 209.15f);
        public override int Radius => 53;

        public override HashSet<uint> NodeIds => new()
        {
            30001,
            30409,
            30410,
            30411,
            30412,
        };

        public override HashSet<uint> ItemIds => new()
        {
            4,
            6,
            5106,
            5488,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 30001,
            },
            new NodeInfo
            {
                NodeId = 30409,
            },
            new NodeInfo
            {
                NodeId = 30410,
            },
            new NodeInfo
            {
                NodeId = 30411,
            },
            new NodeInfo
            {
                NodeId = 30412,
            },
        };
    }
}
