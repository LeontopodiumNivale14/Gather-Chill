using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.Endwalker.MareLamentorum_959
{
    public class BTN_902 : RouteInfo
    {
        public override uint Id => 902;
        public override uint ExpansionId => 4;
        public override uint ZoneId => 959;
        public override uint GatherType => 3;
        public override Vector2 MapPosition => new Vector2(640.472f, 310.125f);
        public override int Radius => 110;

        public override HashSet<uint> NodeIds => new()
        {
            34335,
            34336,
            34337,
            34338,
            34339,
            34340,
        };

        public override HashSet<uint> ItemIds => new()
        {
            9,
            35602,
            36090,
            41071,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 34335,
            },
            new NodeInfo
            {
                NodeId = 34336,
            },
            new NodeInfo
            {
                NodeId = 34337,
            },
            new NodeInfo
            {
                NodeId = 34338,
            },
            new NodeInfo
            {
                NodeId = 34339,
            },
            new NodeInfo
            {
                NodeId = 34340,
            },
        };
    }
}
