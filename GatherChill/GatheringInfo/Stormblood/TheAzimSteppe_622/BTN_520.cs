using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.Stormblood.TheAzimSteppe_622
{
	public class BTN_520 : RouteInfo
	{
		public override uint Id => 520;
		public override uint ExpansionId => 2;
		public override uint ZoneId => 622;
		public override uint GatherType => 3;
		public override Vector2 MapPosition => new Vector2(212.347f, 417.473f);
		public override int Radius => 140;

		public override HashSet<uint> NodeIds => new()
		{
			32268,
			33607,
			33608,
		};

		public override HashSet<uint> ItemIds => new()
		{
			11,
			20012,
			23221,
		};

		public override List<NodeInfo> Nodes => new()
		{

		};
	}
}
