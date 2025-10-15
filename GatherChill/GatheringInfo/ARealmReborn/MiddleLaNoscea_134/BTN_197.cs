using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.ARealmReborn.MiddleLaNoscea_134
{
	public class BTN_197 : RouteInfo
	{
		public override uint Id => 197;
		public override uint ExpansionId => 0;
		public override uint ZoneId => 134;
		public override uint GatherType => 2;
		public override Vector2 MapPosition => new Vector2(-69.7346f, -6.67298f);
		public override int Radius => 73;

		public override HashSet<uint> NodeIds => new()
		{
			30373,
			30374,
			30375,
			30376,
		};

		public override HashSet<uint> ItemIds => new()
		{
			2,
			8,
		};

		public override List<NodeInfo> Nodes => new()
		{

		};
	}
}
