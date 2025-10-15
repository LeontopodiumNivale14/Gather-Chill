using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.Stormblood.TheLochs_621
{
	public class BTN_477 : RouteInfo
	{
		public override uint Id => 477;
		public override uint ExpansionId => 2;
		public override uint ZoneId => 621;
		public override uint GatherType => 5;
		public override Vector2 MapPosition => new Vector2(-382.079f, -130.398f);
		public override int Radius => 105;

		public override HashSet<uint> NodeIds => new()
		{
			32110,
			32111,
			32112,
		};

		public override HashSet<uint> ItemIds => new()
		{
		};

		public override List<NodeInfo> Nodes => new()
		{

		};
	}
}
