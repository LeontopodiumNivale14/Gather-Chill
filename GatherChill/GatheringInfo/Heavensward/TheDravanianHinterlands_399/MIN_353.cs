using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.Heavensward.TheDravanianHinterlands_399
{
	public class MIN_353 : RouteInfo
	{
		public override uint Id => 353;
		public override uint ExpansionId => 1;
		public override uint ZoneId => 399;
		public override uint GatherType => 0;
		public override Vector2 MapPosition => new Vector2(138.347f, 124.96f);
		public override int Radius => 121;

		public override HashSet<uint> NodeIds => new()
		{
			31352,
			31353,
			31354,
			31355,
			31356,
			31357,
		};

		public override HashSet<uint> ItemIds => new()
		{
			10,
			5117,
			5127,
			12558,
			12942,
			13760,
			17557,
		};

		public override List<NodeInfo> Nodes => new()
		{

		};
	}
}
