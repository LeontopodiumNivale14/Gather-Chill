using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.Stormblood.TheRubySea_613
{
	public class BTN_771 : RouteInfo
	{
		public override uint Id => 771;
		public override uint ExpansionId => 2;
		public override uint ZoneId => 613;
		public override uint GatherType => 2;
		public override Vector2 MapPosition => new Vector2(580.315f, -613.033f);
		public override int Radius => 1;

		public override HashSet<uint> NodeIds => new()
		{
			33022,
		};

		public override HashSet<uint> ItemIds => new()
		{
			32998,
		};

		public override List<NodeInfo> Nodes => new()
		{

		};
	}
}
