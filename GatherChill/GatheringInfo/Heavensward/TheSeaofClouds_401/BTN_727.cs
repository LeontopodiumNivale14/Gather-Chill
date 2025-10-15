using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.Heavensward.TheSeaofClouds_401
{
    public class BTN_727 : RouteInfo
    {
        public override uint Id => 727;
        public override uint ExpansionId => 1;
        public override uint ZoneId => 401;
        public override uint GatherType => 3;
        public override Vector2 MapPosition => new Vector2(664.387f, 728.874f);
        public override int Radius => 117;

        public override HashSet<uint> NodeIds => new()
        {
            33307,
            33308,
            33309,
            33310,
            33311,
            33312,
        };

        public override HashSet<uint> ItemIds => new()
        {
            12,
            30500,
            33230,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 33307,
            },
            new NodeInfo
            {
                NodeId = 33308,
            },
            new NodeInfo
            {
                NodeId = 33309,
            },
            new NodeInfo
            {
                NodeId = 33310,
            },
            new NodeInfo
            {
                NodeId = 33311,
            },
            new NodeInfo
            {
                NodeId = 33312,
            },
        };
    }
}
