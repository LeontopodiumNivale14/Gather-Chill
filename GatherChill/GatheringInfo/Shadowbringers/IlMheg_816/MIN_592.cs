using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.Shadowbringers.IlMheg_816
{
    public class MIN_592 : RouteInfo
    {
        public override uint Id => 592;
        public override uint ExpansionId => 3;
        public override uint ZoneId => 816;
        public override uint GatherType => 0;
        public override Vector2 MapPosition => new Vector2(-747.763f, -126.908f);
        public override int Radius => 120;

        public override HashSet<uint> NodeIds => new()
        {
            32626,
            32627,
            32628,
            32629,
            32630,
            32631,
        };

        public override HashSet<uint> ItemIds => new()
        {
            13,
            26754,
            26756,
            27697,
            27968,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 32626,
            },
            new NodeInfo
            {
                NodeId = 32627,
            },
            new NodeInfo
            {
                NodeId = 32628,
            },
            new NodeInfo
            {
                NodeId = 32629,
            },
            new NodeInfo
            {
                NodeId = 32630,
            },
            new NodeInfo
            {
                NodeId = 32631,
            },
        };
    }
}
