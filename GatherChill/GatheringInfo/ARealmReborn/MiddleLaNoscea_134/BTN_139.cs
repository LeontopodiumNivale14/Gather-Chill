using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.ARealmReborn.MiddleLaNoscea_134
{
	public class BTN_139 : RouteInfo
	{
		public override uint Id => 139;
		public override uint ExpansionId => 0;
		public override uint ZoneId => 134;
		public override uint GatherType => 2;
		public override Vector2 MapPosition => new Vector2(-236.255f, -387.589f);
		public override int Radius => 50;

		public override HashSet<uint> NodeIds => new()
		{
			30321,
			30322,
			30323,
			30324,
		};

		public override HashSet<uint> ItemIds => new()
		{
			2,
			8,
			5815,
		};

		public override List<NodeInfo> Nodes => new()
		{

		};
	}
}
