using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.ARealmReborn.CentralShroud_148
{
	public class BTN_21 : RouteInfo
	{
		public override uint Id => 21;
		public override uint ExpansionId => 0;
		public override uint ZoneId => 148;
		public override uint GatherType => 3;
		public override Vector2 MapPosition => new Vector2(44.4254f, 112.402f);
		public override int Radius => 71;

		public override HashSet<uint> NodeIds => new()
		{
			30009,
			30037,
			30039,
			30040,
			30041,
		};

		public override HashSet<uint> ItemIds => new()
		{
			4,
			4795,
			5343,
			5539,
			5540,
		};

		public override List<NodeInfo> Nodes => new()
		{

		};
	}
}
