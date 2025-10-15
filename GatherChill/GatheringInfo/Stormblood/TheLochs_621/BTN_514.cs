using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.Stormblood.TheLochs_621
{
	public class BTN_514 : RouteInfo
	{
		public override uint Id => 514;
		public override uint ExpansionId => 2;
		public override uint ZoneId => 621;
		public override uint GatherType => 2;
		public override Vector2 MapPosition => new Vector2(345.775f, -583.707f);
		public override int Radius => 118;

		public override HashSet<uint> NodeIds => new()
		{
			32242,
			32243,
			32244,
			32245,
			32246,
			32247,
		};

		public override HashSet<uint> ItemIds => new()
		{
			9,
			19933,
			24570,
		};

		public override List<NodeInfo> Nodes => new()
		{

		};
	}
}
