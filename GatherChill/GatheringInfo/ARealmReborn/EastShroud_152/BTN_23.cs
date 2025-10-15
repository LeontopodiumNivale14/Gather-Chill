using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.ARealmReborn.EastShroud_152
{
	public class BTN_23 : RouteInfo
	{
		public override uint Id => 23;
		public override uint ExpansionId => 0;
		public override uint ZoneId => 152;
		public override uint GatherType => 3;
		public override Vector2 MapPosition => new Vector2(-149.326f, 339.06f);
		public override int Radius => 71;

		public override HashSet<uint> NodeIds => new()
		{
			30045,
			30046,
			30047,
			30313,
		};

		public override HashSet<uint> ItemIds => new()
		{
			4,
			4796,
			4834,
			5541,
			7030,
		};

		public override List<NodeInfo> Nodes => new()
		{

		};
	}
}
