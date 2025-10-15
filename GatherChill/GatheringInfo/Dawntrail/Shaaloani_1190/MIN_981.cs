using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.Dawntrail.Shaaloani_1190
{
    public class MIN_981 : RouteInfo
    {
        public override uint Id => 981;
        public override uint ExpansionId => 5;
        public override uint ZoneId => 1190;
        public override uint GatherType => 0;
        public override Vector2 MapPosition => new Vector2(-408.84f, -627.275f);
        public override int Radius => 150;

        public override HashSet<uint> NodeIds => new()
        {
            34791,
            34792,
            34793,
            34794,
            34795,
            34796,
        };

        public override HashSet<uint> ItemIds => new()
        {
            8,
            43995,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 34791,
            },
            new NodeInfo
            {
                NodeId = 34792,
            },
            new NodeInfo
            {
                NodeId = 34793,
            },
            new NodeInfo
            {
                NodeId = 34794,
            },
            new NodeInfo
            {
                NodeId = 34795,
            },
            new NodeInfo
            {
                NodeId = 34796,
            },
        };
    }
}
