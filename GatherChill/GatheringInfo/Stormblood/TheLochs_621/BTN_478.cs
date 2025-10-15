using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.Stormblood.TheLochs_621
{
	public class BTN_478 : RouteInfo
	{
		public override uint Id => 478;
		public override uint ExpansionId => 2;
		public override uint ZoneId => 621;
		public override uint GatherType => 5;
		public override Vector2 MapPosition => new Vector2(58.5879f, 194.305f);
		public override int Radius => 104;

		public override HashSet<uint> NodeIds => new()
		{
			32113,
			32114,
			32115,
		};

		public override HashSet<uint> ItemIds => new()
		{
		};

		public override List<NodeInfo> Nodes => new()
		{

		};
	}
}
