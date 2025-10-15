using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.ARealmReborn.CentralShroud_148
{
	public class BTN_11 : RouteInfo
	{
		public override uint Id => 11;
		public override uint ExpansionId => 0;
		public override uint ZoneId => 148;
		public override uint GatherType => 2;
		public override Vector2 MapPosition => new Vector2(71.0216f, -179.027f);
		public override int Radius => 63;

		public override HashSet<uint> NodeIds => new()
		{
			30005,
			30012,
			30013,
			30014,
			30015,
		};

		public override HashSet<uint> ItemIds => new()
		{
			3,
			4,
			5050,
			5380,
			5396,
			5509,
		};

		public override List<NodeInfo> Nodes => new()
		{

		};
	}
}
