using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.Heavensward.TheDravanianHinterlands_399
{
	public class MIN_665 : RouteInfo
	{
		public override uint Id => 665;
		public override uint ExpansionId => 1;
		public override uint ZoneId => 399;
		public override uint GatherType => 0;
		public override Vector2 MapPosition => new Vector2(-857.404f, 241.051f);
		public override int Radius => 88;

		public override HashSet<uint> NodeIds => new()
		{
			32893,
			32894,
			32895,
			32896,
			32897,
			32898,
		};

		public override HashSet<uint> ItemIds => new()
		{
			28777,
		};

		public override List<NodeInfo> Nodes => new()
		{

		};
	}
}
