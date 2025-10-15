using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.Dawntrail.YakTel_1189
{
    public class BTN_1000 : RouteInfo
    {
        public override uint Id => 1000;
        public override uint ExpansionId => 5;
        public override uint ZoneId => 1189;
        public override uint GatherType => 3;
        public override Vector2 MapPosition => new Vector2(318.574f, -304.675f);
        public override int Radius => 141;

        public override HashSet<uint> NodeIds => new()
        {
            34905,
            34906,
            34907,
            34908,
            34909,
            34910,
        };

        public override HashSet<uint> ItemIds => new()
        {
            13,
            43902,
            43986,
            44026,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 34905,
            },
            new NodeInfo
            {
                NodeId = 34906,
            },
            new NodeInfo
            {
                NodeId = 34907,
            },
            new NodeInfo
            {
                NodeId = 34908,
            },
            new NodeInfo
            {
                NodeId = 34909,
            },
            new NodeInfo
            {
                NodeId = 34910,
            },
        };
    }
}
