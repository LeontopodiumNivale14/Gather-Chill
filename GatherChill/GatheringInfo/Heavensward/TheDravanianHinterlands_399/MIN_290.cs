using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.Heavensward.TheDravanianHinterlands_399
{
	public class MIN_290 : RouteInfo
	{
		public override uint Id => 290;
		public override uint ExpansionId => 1;
		public override uint ZoneId => 399;
		public override uint GatherType => 1;
		public override Vector2 MapPosition => new Vector2(-299.827f, 385.204f);
		public override int Radius => 82;

		public override HashSet<uint> NodeIds => new()
		{
			31705,
		};

		public override HashSet<uint> ItemIds => new()
		{
			16726,
		};

		public override List<NodeInfo> Nodes => new()
		{

		};
	}
}
