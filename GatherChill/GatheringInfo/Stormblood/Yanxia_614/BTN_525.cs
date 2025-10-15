using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.Stormblood.Yanxia_614
{
	public class BTN_525 : RouteInfo
	{
		public override uint Id => 525;
		public override uint ExpansionId => 2;
		public override uint ZoneId => 614;
		public override uint GatherType => 3;
		public override Vector2 MapPosition => new Vector2(328.901f, -685.074f);
		public override int Radius => 26;

		public override HashSet<uint> NodeIds => new()
		{
			32273,
		};

		public override HashSet<uint> ItemIds => new()
		{
			19865,
		};

		public override List<NodeInfo> Nodes => new()
		{

		};
	}
}
