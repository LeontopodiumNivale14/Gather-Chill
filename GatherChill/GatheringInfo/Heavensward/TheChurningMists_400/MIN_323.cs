using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.Heavensward.TheChurningMists_400
{
    public class MIN_323 : RouteInfo
    {
        public override uint Id => 323;
        public override uint ExpansionId => 1;
        public override uint ZoneId => 400;
        public override uint GatherType => 0;
        public override Vector2 MapPosition => new Vector2(-646.789f, 717.146f);
        public override int Radius => 180;

        public override HashSet<uint> NodeIds => new()
        {
            31464,
        };

        public override HashSet<uint> ItemIds => new()
        {
            12536,
            14148,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 31464,
            },
        };
    }
}
