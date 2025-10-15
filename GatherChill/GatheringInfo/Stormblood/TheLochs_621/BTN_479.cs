using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.Stormblood.TheLochs_621
{
	public class BTN_479 : RouteInfo
	{
		public override uint Id => 479;
		public override uint ExpansionId => 2;
		public override uint ZoneId => 621;
		public override uint GatherType => 5;
		public override Vector2 MapPosition => new Vector2(218.948f, 363.148f);
		public override int Radius => 79;

		public override HashSet<uint> NodeIds => new()
		{
			32116,
			32117,
			32118,
		};

		public override HashSet<uint> ItemIds => new()
		{
		};

		public override List<NodeInfo> Nodes => new()
		{

		};
	}
}
