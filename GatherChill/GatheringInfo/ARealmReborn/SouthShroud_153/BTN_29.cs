using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.ARealmReborn.SouthShroud_153
{
	public class BTN_29 : RouteInfo
	{
		public override uint Id => 29;
		public override uint ExpansionId => 0;
		public override uint ZoneId => 153;
		public override uint GatherType => 3;
		public override Vector2 MapPosition => new Vector2(-201.065f, 357.826f);
		public override int Radius => 66;

		public override HashSet<uint> NodeIds => new()
		{
			30063,
			30064,
			30065,
			30319,
		};

		public override HashSet<uint> ItemIds => new()
		{
			3,
			4798,
			5346,
			5360,
		};

		public override List<NodeInfo> Nodes => new()
		{

		};
	}
}
