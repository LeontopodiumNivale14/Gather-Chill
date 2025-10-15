using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.Stormblood.TheFringes_612
{
	public class BTN_537 : RouteInfo
	{
		public override uint Id => 537;
		public override uint ExpansionId => 2;
		public override uint ZoneId => 612;
		public override uint GatherType => 2;
		public override Vector2 MapPosition => new Vector2(-330.288f, -13.205f);
		public override int Radius => 12;

		public override HashSet<uint> NodeIds => new()
		{
			32310,
		};

		public override HashSet<uint> ItemIds => new()
		{
			22419,
			23180,
		};

		public override List<NodeInfo> Nodes => new()
		{

		};
	}
}
