using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.Dawntrail.Urqopacha_1187
{
    public class BTN_992 : RouteInfo
    {
        public override uint Id => 992;
        public override uint ExpansionId => 5;
        public override uint ZoneId => 1187;
        public override uint GatherType => 3;
        public override Vector2 MapPosition => new Vector2(-131.226f, -357.237f);
        public override int Radius => 157;

        public override HashSet<uint> NodeIds => new()
        {
            34857,
            34858,
            34859,
            34860,
            34861,
            34862,
        };

        public override HashSet<uint> ItemIds => new()
        {
            11,
            43899,
            44024,
            44042,
            44850,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 34857,
            },
            new NodeInfo
            {
                NodeId = 34858,
            },
            new NodeInfo
            {
                NodeId = 34859,
            },
            new NodeInfo
            {
                NodeId = 34860,
            },
            new NodeInfo
            {
                NodeId = 34861,
            },
            new NodeInfo
            {
                NodeId = 34862,
            },
        };
    }
}
