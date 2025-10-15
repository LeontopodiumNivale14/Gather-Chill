using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.ARealmReborn.MorDhona_156
{
    public class MIN_206 : RouteInfo
    {
        public override uint Id => 206;
        public override uint ExpansionId => 0;
        public override uint ZoneId => 156;
        public override uint GatherType => 0;
        public override Vector2 MapPosition => new Vector2(-292.375f, -324.513f);
        public override int Radius => 61;

        public override HashSet<uint> NodeIds => new()
        {
            30449,
            30450,
            30451,
            30452,
        };

        public override HashSet<uint> ItemIds => new()
        {
            6,
            2001415,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 30449,
            },
            new NodeInfo
            {
                NodeId = 30450,
            },
            new NodeInfo
            {
                NodeId = 30451,
            },
            new NodeInfo
            {
                NodeId = 30452,
            },
        };
    }
}
