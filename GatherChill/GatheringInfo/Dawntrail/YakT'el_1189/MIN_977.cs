using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.Dawntrail.YakTel_1189
{
    public class MIN_977 : RouteInfo
    {
        public override uint Id => 977;
        public override uint ExpansionId => 5;
        public override uint ZoneId => 1189;
        public override uint GatherType => 0;
        public override Vector2 MapPosition => new Vector2(-439.698f, -103.863f);
        public override int Radius => 108;

        public override HashSet<uint> NodeIds => new()
        {
            34767,
            34768,
            34769,
            34770,
            34771,
            34772,
        };

        public override HashSet<uint> ItemIds => new()
        {
            9,
            43994,
            44854,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 34767,
            },
            new NodeInfo
            {
                NodeId = 34768,
            },
            new NodeInfo
            {
                NodeId = 34769,
            },
            new NodeInfo
            {
                NodeId = 34770,
            },
            new NodeInfo
            {
                NodeId = 34771,
            },
            new NodeInfo
            {
                NodeId = 34772,
            },
        };
    }
}
