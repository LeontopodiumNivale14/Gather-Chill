using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.Shadowbringers.AmhAraeng_815
{
    public class MIN_919 : RouteInfo
    {
        public override uint Id => 919;
        public override uint ExpansionId => 3;
        public override uint ZoneId => 815;
        public override uint GatherType => 1;
        public override Vector2 MapPosition => new Vector2(-324.793f, -149.69f);
        public override int Radius => 115;

        public override HashSet<uint> NodeIds => new()
        {
            34409,
            34410,
            34411,
            34412,
            34413,
            34414,
        };

        public override HashSet<uint> ItemIds => new()
        {
            10,
            38821,
            38822,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 34409,
            },
            new NodeInfo
            {
                NodeId = 34410,
            },
            new NodeInfo
            {
                NodeId = 34411,
            },
            new NodeInfo
            {
                NodeId = 34412,
            },
            new NodeInfo
            {
                NodeId = 34413,
            },
            new NodeInfo
            {
                NodeId = 34414,
            },
        };
    }
}
