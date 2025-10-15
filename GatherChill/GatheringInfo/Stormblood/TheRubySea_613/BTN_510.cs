using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.Stormblood.TheRubySea_613
{
	public class BTN_510 : RouteInfo
	{
		public override uint Id => 510;
		public override uint ExpansionId => 2;
		public override uint ZoneId => 613;
		public override uint GatherType => 3;
		public override Vector2 MapPosition => new Vector2(-623.521f, -723.683f);
		public override int Radius => 145;

		public override HashSet<uint> NodeIds => new()
		{
			32218,
			32219,
			32220,
			32221,
			32222,
			32223,
		};

		public override HashSet<uint> ItemIds => new()
		{
			13,
			17946,
			19862,
			19864,
		};

		public override List<NodeInfo> Nodes => new()
		{

		};
	}
}
