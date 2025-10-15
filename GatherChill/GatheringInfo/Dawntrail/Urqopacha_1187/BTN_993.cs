using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.Dawntrail.Urqopacha_1187
{
    public class BTN_993 : RouteInfo
    {
        public override uint Id => 993;
        public override uint ExpansionId => 5;
        public override uint ZoneId => 1187;
        public override uint GatherType => 3;
        public override Vector2 MapPosition => new Vector2(333.886f, -475.854f);
        public override int Radius => 196;

        public override HashSet<uint> NodeIds => new()
        {
            34863,
            34864,
            34865,
            34866,
            34867,
            34868,
        };

        public override HashSet<uint> ItemIds => new()
        {
            11,
            43989,
            44853,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 34863,
            },
            new NodeInfo
            {
                NodeId = 34864,
            },
            new NodeInfo
            {
                NodeId = 34865,
            },
            new NodeInfo
            {
                NodeId = 34866,
            },
            new NodeInfo
            {
                NodeId = 34867,
            },
            new NodeInfo
            {
                NodeId = 34868,
            },
        };
    }
}
