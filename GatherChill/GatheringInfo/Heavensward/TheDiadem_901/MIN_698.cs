using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.Heavensward.TheDiadem_901
{
    public class MIN_698 : RouteInfo
    {
        public override uint Id => 698;
        public override uint ExpansionId => 1;
        public override uint ZoneId => 901;
        public override uint GatherType => 0;
        public override Vector2 MapPosition => new Vector2(127.828f, -14.0608f);
        public override int Radius => 907;

        public override HashSet<uint> NodeIds => new()
        {
            33037,
            33042,
            33049,
            33056,
            33063,
            33068,
            33070,
            33075,
            33077,
            33082,
            33089,
            33096,
            33103,
            33108,
            33110,
            33115,
            33117,
            33122,
            33129,
        };

        public override HashSet<uint> ItemIds => new()
        {
            29896,
            29909,
            29919,
            29929,
            29939,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 33037,
            },
            new NodeInfo
            {
                NodeId = 33042,
            },
            new NodeInfo
            {
                NodeId = 33049,
            },
            new NodeInfo
            {
                NodeId = 33056,
            },
            new NodeInfo
            {
                NodeId = 33063,
            },
            new NodeInfo
            {
                NodeId = 33068,
            },
            new NodeInfo
            {
                NodeId = 33070,
            },
            new NodeInfo
            {
                NodeId = 33075,
            },
            new NodeInfo
            {
                NodeId = 33077,
            },
            new NodeInfo
            {
                NodeId = 33082,
            },
            new NodeInfo
            {
                NodeId = 33089,
            },
            new NodeInfo
            {
                NodeId = 33096,
            },
            new NodeInfo
            {
                NodeId = 33103,
            },
            new NodeInfo
            {
                NodeId = 33108,
            },
            new NodeInfo
            {
                NodeId = 33110,
            },
            new NodeInfo
            {
                NodeId = 33115,
            },
            new NodeInfo
            {
                NodeId = 33117,
            },
            new NodeInfo
            {
                NodeId = 33122,
            },
            new NodeInfo
            {
                NodeId = 33129,
            },
        };
    }
}
