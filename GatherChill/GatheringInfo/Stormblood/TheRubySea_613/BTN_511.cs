using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.Stormblood.TheRubySea_613
{
	public class BTN_511 : RouteInfo
	{
		public override uint Id => 511;
		public override uint ExpansionId => 2;
		public override uint ZoneId => 613;
		public override uint GatherType => 3;
		public override Vector2 MapPosition => new Vector2(284.899f, 24.4239f);
		public override int Radius => 152;

		public override HashSet<uint> NodeIds => new()
		{
			32224,
			32225,
			32226,
			32227,
			32228,
			32229,
		};

		public override HashSet<uint> ItemIds => new()
		{
			13,
			19879,
			19881,
			19979,
			23151,
		};

		public override List<NodeInfo> Nodes => new()
		{

		};
	}
}
