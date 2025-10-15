using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.Stormblood.TheLochs_621
{
	public class BTN_515 : RouteInfo
	{
		public override uint Id => 515;
		public override uint ExpansionId => 2;
		public override uint ZoneId => 621;
		public override uint GatherType => 2;
		public override Vector2 MapPosition => new Vector2(477.775f, 273.389f);
		public override int Radius => 120;

		public override HashSet<uint> NodeIds => new()
		{
			31702,
			31703,
			32248,
		};

		public override HashSet<uint> ItemIds => new()
		{
			9,
			19937,
			23221,
		};

		public override List<NodeInfo> Nodes => new()
		{

		};
	}
}
