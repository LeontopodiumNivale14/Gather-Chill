using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.Stormblood.Yanxia_614
{
	public class BTN_772 : RouteInfo
	{
		public override uint Id => 772;
		public override uint ExpansionId => 2;
		public override uint ZoneId => 614;
		public override uint GatherType => 2;
		public override Vector2 MapPosition => new Vector2(-139.81f, -356.409f);
		public override int Radius => 1;

		public override HashSet<uint> NodeIds => new()
		{
			33023,
		};

		public override HashSet<uint> ItemIds => new()
		{
			33000,
			33001,
		};

		public override List<NodeInfo> Nodes => new()
		{

		};
	}
}
