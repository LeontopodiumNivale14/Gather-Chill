using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.Heavensward.TheDiadem_939
{
    public class BTN_796 : RouteInfo
    {
        public override uint Id => 796;
        public override uint ExpansionId => 1;
        public override uint ZoneId => 939;
        public override uint GatherType => 2;
        public override Vector2 MapPosition => new Vector2(127.392f, -199.825f);
        public override int Radius => 872;

        public override HashSet<uint> NodeIds => new()
        {
            33744,
            33751,
            33758,
            33763,
            33765,
            33770,
            33772,
            33777,
            33784,
            33788,
            33793,
            33800,
            33807,
            33814,
            33819,
            33821,
            33826,
            33828,
            33833,
        };

        public override HashSet<uint> ItemIds => new()
        {
            29935,
            31307,
            32009,
            32016,
            32025,
            32036,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 33744,
            },
            new NodeInfo
            {
                NodeId = 33751,
            },
            new NodeInfo
            {
                NodeId = 33758,
            },
            new NodeInfo
            {
                NodeId = 33763,
            },
            new NodeInfo
            {
                NodeId = 33765,
            },
            new NodeInfo
            {
                NodeId = 33770,
            },
            new NodeInfo
            {
                NodeId = 33772,
            },
            new NodeInfo
            {
                NodeId = 33777,
            },
            new NodeInfo
            {
                NodeId = 33784,
            },
            new NodeInfo
            {
                NodeId = 33788,
            },
            new NodeInfo
            {
                NodeId = 33793,
            },
            new NodeInfo
            {
                NodeId = 33800,
            },
            new NodeInfo
            {
                NodeId = 33807,
            },
            new NodeInfo
            {
                NodeId = 33814,
            },
            new NodeInfo
            {
                NodeId = 33819,
            },
            new NodeInfo
            {
                NodeId = 33821,
            },
            new NodeInfo
            {
                NodeId = 33826,
            },
            new NodeInfo
            {
                NodeId = 33828,
            },
            new NodeInfo
            {
                NodeId = 33833,
            },
        };
    }
}
