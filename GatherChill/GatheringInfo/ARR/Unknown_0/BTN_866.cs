using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.ARR.Unknown_0
{
    public class BTN_866 : RouteInfo
    {
        public override uint Id => 866;
        public override uint ExpansionId => 0;
        public override uint ZoneId => 0;
        public override uint GatherType => 2;
        public override Vector2 MapPosition => new Vector2(626.199f, -663.274f);
        public override int Radius => 217;

        public override HashSet<uint> NodeIds => new()
        {
            34116,
            34117,
            34118,
            34119,
            34120,
            34121,
            34122,
            34123,
            34124,
            34125,
            34126,
            34127,
        };

        public override HashSet<uint> ItemIds => new()
        {
            2003142,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 34116,
            },
            new NodeInfo
            {
                NodeId = 34117,
            },
            new NodeInfo
            {
                NodeId = 34118,
            },
            new NodeInfo
            {
                NodeId = 34119,
            },
            new NodeInfo
            {
                NodeId = 34120,
            },
            new NodeInfo
            {
                NodeId = 34121,
            },
            new NodeInfo
            {
                NodeId = 34122,
            },
            new NodeInfo
            {
                NodeId = 34123,
            },
            new NodeInfo
            {
                NodeId = 34124,
            },
            new NodeInfo
            {
                NodeId = 34125,
            },
            new NodeInfo
            {
                NodeId = 34126,
            },
            new NodeInfo
            {
                NodeId = 34127,
            },
        };
    }
}
