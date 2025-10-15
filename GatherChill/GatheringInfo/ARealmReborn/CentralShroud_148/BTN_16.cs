using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.ARealmReborn.CentralShroud_148
{
	public class BTN_16 : RouteInfo
	{
		public override uint Id => 16;
		public override uint ExpansionId => 0;
		public override uint ZoneId => 148;
		public override uint GatherType => 2;
		public override Vector2 MapPosition => new Vector2(241.89f, -54.0485f);
		public override int Radius => 59;

		public override HashSet<uint> NodeIds => new()
		{
			30027,
			30028,
			30029,
			30030,
		};

		public override HashSet<uint> ItemIds => new()
		{
			4,
			5352,
			5383,
			5402,
			5534,
		};

		public override List<NodeInfo> Nodes => new()
		{

		};
	}
}
