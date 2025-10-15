using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.Heavensward.TheDiadem_901
{
    public class BTN_705 : RouteInfo
    {
        public override uint Id => 705;
        public override uint ExpansionId => 1;
        public override uint ZoneId => 901;
        public override uint GatherType => 3;
        public override Vector2 MapPosition => new Vector2(47.485f, 61.4981f);
        public override int Radius => 857;

        public override HashSet<uint> NodeIds => new()
        {
            33135,
            33140,
            33142,
            33147,
            33149,
            33154,
            33161,
            33168,
            33175,
            33180,
            33183,
            33188,
            33190,
            33195,
            33197,
            33202,
            33209,
            33216,
            33223,
            33228,
        };

        public override HashSet<uint> ItemIds => new()
        {
            29900,
            29908,
            29918,
            29928,
            29938,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 33135,
            },
            new NodeInfo
            {
                NodeId = 33140,
            },
            new NodeInfo
            {
                NodeId = 33142,
            },
            new NodeInfo
            {
                NodeId = 33147,
            },
            new NodeInfo
            {
                NodeId = 33149,
            },
            new NodeInfo
            {
                NodeId = 33154,
            },
            new NodeInfo
            {
                NodeId = 33161,
            },
            new NodeInfo
            {
                NodeId = 33168,
            },
            new NodeInfo
            {
                NodeId = 33175,
            },
            new NodeInfo
            {
                NodeId = 33180,
            },
            new NodeInfo
            {
                NodeId = 33183,
            },
            new NodeInfo
            {
                NodeId = 33188,
            },
            new NodeInfo
            {
                NodeId = 33190,
            },
            new NodeInfo
            {
                NodeId = 33195,
            },
            new NodeInfo
            {
                NodeId = 33197,
            },
            new NodeInfo
            {
                NodeId = 33202,
            },
            new NodeInfo
            {
                NodeId = 33209,
            },
            new NodeInfo
            {
                NodeId = 33216,
            },
            new NodeInfo
            {
                NodeId = 33223,
            },
            new NodeInfo
            {
                NodeId = 33228,
            },
        };
    }
}
