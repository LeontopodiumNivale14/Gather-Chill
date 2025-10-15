using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.ARealmReborn.MiddleLaNoscea_134
{
	public class BTN_30 : RouteInfo
	{
		public override uint Id => 30;
		public override uint ExpansionId => 0;
		public override uint ZoneId => 134;
		public override uint GatherType => 3;
		public override Vector2 MapPosition => new Vector2(57.6711f, -118.099f);
		public override int Radius => 51;

		public override HashSet<uint> NodeIds => new()
		{
			30010,
			30076,
			30077,
			30078,
			30079,
		};

		public override HashSet<uint> ItemIds => new()
		{
			2,
			4780,
			4782,
			4824,
			5342,
		};

		public override List<NodeInfo> Nodes => new()
		{

		};
	}
}
