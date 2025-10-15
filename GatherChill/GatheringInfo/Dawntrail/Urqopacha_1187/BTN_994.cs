using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.Dawntrail.Urqopacha_1187
{
    public class BTN_994 : RouteInfo
    {
        public override uint Id => 994;
        public override uint ExpansionId => 5;
        public override uint ZoneId => 1187;
        public override uint GatherType => 3;
        public override Vector2 MapPosition => new Vector2(-692.476f, 123.545f);
        public override int Radius => 149;

        public override HashSet<uint> NodeIds => new()
        {
            34869,
            34870,
            34871,
            34872,
            34873,
            34874,
        };

        public override HashSet<uint> ItemIds => new()
        {
            11,
            43913,
            44025,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 34869,
            },
            new NodeInfo
            {
                NodeId = 34870,
            },
            new NodeInfo
            {
                NodeId = 34871,
            },
            new NodeInfo
            {
                NodeId = 34872,
            },
            new NodeInfo
            {
                NodeId = 34873,
            },
            new NodeInfo
            {
                NodeId = 34874,
            },
        };
    }
}
