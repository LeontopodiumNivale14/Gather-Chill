using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.ARR.Unknown_0
{
    public class BTN_958 : RouteInfo
    {
        public override uint Id => 958;
        public override uint ExpansionId => 0;
        public override uint ZoneId => 0;
        public override uint GatherType => 2;
        public override Vector2 MapPosition => new Vector2(373.199f, -602.974f);
        public override int Radius => 65;

        public override HashSet<uint> NodeIds => new()
        {
            34619,
            34620,
            34621,
            34622,
            34623,
            34624,
        };

        public override HashSet<uint> ItemIds => new()
        {
            2003534,
            2003535,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 34619,
            },
            new NodeInfo
            {
                NodeId = 34620,
            },
            new NodeInfo
            {
                NodeId = 34621,
            },
            new NodeInfo
            {
                NodeId = 34622,
            },
            new NodeInfo
            {
                NodeId = 34623,
            },
            new NodeInfo
            {
                NodeId = 34624,
            },
        };
    }
}
