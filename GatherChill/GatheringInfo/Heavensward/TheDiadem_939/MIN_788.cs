using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.Heavensward.TheDiadem_939
{
    public class MIN_788 : RouteInfo
    {
        public override uint Id => 788;
        public override uint ExpansionId => 1;
        public override uint ZoneId => 939;
        public override uint GatherType => 0;
        public override Vector2 MapPosition => new Vector2(10.0632f, -208.93f);
        public override int Radius => 855;

        public override HashSet<uint> NodeIds => new()
        {
            33646,
            33651,
            33653,
            33658,
            33660,
            33665,
            33672,
            33679,
            33690,
            33691,
            33694,
            33699,
            33701,
            33706,
            33708,
            33713,
            33720,
            33727,
            33734,
        };

        public override HashSet<uint> ItemIds => new()
        {
            29941,
            31313,
            32013,
            32022,
            32032,
            32042,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 33646,
            },
            new NodeInfo
            {
                NodeId = 33651,
            },
            new NodeInfo
            {
                NodeId = 33653,
            },
            new NodeInfo
            {
                NodeId = 33658,
            },
            new NodeInfo
            {
                NodeId = 33660,
            },
            new NodeInfo
            {
                NodeId = 33665,
            },
            new NodeInfo
            {
                NodeId = 33672,
            },
            new NodeInfo
            {
                NodeId = 33679,
            },
            new NodeInfo
            {
                NodeId = 33690,
            },
            new NodeInfo
            {
                NodeId = 33691,
            },
            new NodeInfo
            {
                NodeId = 33694,
            },
            new NodeInfo
            {
                NodeId = 33699,
            },
            new NodeInfo
            {
                NodeId = 33701,
            },
            new NodeInfo
            {
                NodeId = 33706,
            },
            new NodeInfo
            {
                NodeId = 33708,
            },
            new NodeInfo
            {
                NodeId = 33713,
            },
            new NodeInfo
            {
                NodeId = 33720,
            },
            new NodeInfo
            {
                NodeId = 33727,
            },
            new NodeInfo
            {
                NodeId = 33734,
            },
        };
    }
}
