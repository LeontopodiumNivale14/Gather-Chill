using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.Endwalker.Elpis_961
{
    public class BTN_844 : RouteInfo
    {
        public override uint Id => 844;
        public override uint ExpansionId => 4;
        public override uint ZoneId => 961;
        public override uint GatherType => 2;
        public override Vector2 MapPosition => new Vector2(-618.569f, 83.6178f);
        public override int Radius => 123;

        public override HashSet<uint> NodeIds => new()
        {
            34009,
            34010,
            34011,
            34012,
            34013,
            34014,
        };

        public override HashSet<uint> ItemIds => new()
        {
            12,
            36095,
            36096,
            36193,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 34009,
            },
            new NodeInfo
            {
                NodeId = 34010,
            },
            new NodeInfo
            {
                NodeId = 34011,
            },
            new NodeInfo
            {
                NodeId = 34012,
            },
            new NodeInfo
            {
                NodeId = 34013,
            },
            new NodeInfo
            {
                NodeId = 34014,
            },
        };
    }
}
