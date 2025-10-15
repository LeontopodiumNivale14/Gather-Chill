using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.ARR.Unknown_0
{
    public class MIN_909 : RouteInfo
    {
        public override uint Id => 909;
        public override uint ExpansionId => 0;
        public override uint ZoneId => 0;
        public override uint GatherType => 0;
        public override Vector2 MapPosition => new Vector2(-588.174f, 564.293f);
        public override int Radius => 13;

        public override HashSet<uint> NodeIds => new()
        {
            34376,
            34377,
            34378,
        };

        public override HashSet<uint> ItemIds => new()
        {
            10,
            38278,
            38283,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 34376,
            },
            new NodeInfo
            {
                NodeId = 34377,
            },
            new NodeInfo
            {
                NodeId = 34378,
            },
        };
    }
}
