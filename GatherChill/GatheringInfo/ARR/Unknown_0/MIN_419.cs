using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.ARR.Unknown_0
{
    public class MIN_419 : RouteInfo
    {
        public override uint Id => 419;
        public override uint ExpansionId => 0;
        public override uint ZoneId => 0;
        public override uint GatherType => 0;
        public override Vector2 MapPosition => new Vector2(316.516f, -729.667f);
        public override int Radius => 95;

        public override HashSet<uint> NodeIds => new()
        {
            31777,
            31778,
            31779,
            31780,
        };

        public override HashSet<uint> ItemIds => new()
        {
            2,
            8,
            7588,
            12534,
            12535,
            12537,
            13750,
            17570,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 31777,
            },
            new NodeInfo
            {
                NodeId = 31778,
            },
            new NodeInfo
            {
                NodeId = 31779,
            },
            new NodeInfo
            {
                NodeId = 31780,
            },
        };
    }
}
