using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.ARR.Unknown_0
{
    public class BTN_60 : RouteInfo
    {
        public override uint Id => 60;
        public override uint ExpansionId => 0;
        public override uint ZoneId => 0;
        public override uint GatherType => 2;
        public override Vector2 MapPosition => new Vector2(48.8431f, 43.5707f);
        public override int Radius => 93;

        public override HashSet<uint> NodeIds => new()
        {
            30224,
            30225,
            30226,
            30227,
            30228,
            30229,
            30230,
            30231,
            30232,
            30233,
        };

        public override HashSet<uint> ItemIds => new()
        {
            2000268,
            2000288,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 30224,
            },
            new NodeInfo
            {
                NodeId = 30225,
            },
            new NodeInfo
            {
                NodeId = 30226,
            },
            new NodeInfo
            {
                NodeId = 30227,
            },
            new NodeInfo
            {
                NodeId = 30228,
            },
            new NodeInfo
            {
                NodeId = 30229,
            },
            new NodeInfo
            {
                NodeId = 30230,
            },
            new NodeInfo
            {
                NodeId = 30231,
            },
            new NodeInfo
            {
                NodeId = 30232,
            },
            new NodeInfo
            {
                NodeId = 30233,
            },
        };
    }
}
