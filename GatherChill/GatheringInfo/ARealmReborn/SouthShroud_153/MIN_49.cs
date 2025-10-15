using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.ARealmReborn.SouthShroud_153
{
	public class MIN_49 : RouteInfo
	{
		public override uint Id => 49;
		public override uint ExpansionId => 0;
		public override uint ZoneId => 153;
		public override uint GatherType => 0;
		public override Vector2 MapPosition => new Vector2(357.951f, 69.0625f);
		public override int Radius => 52;

		public override HashSet<uint> NodeIds => new()
		{
			30111,
			30112,
			30113,
			30503,
		};

		public override HashSet<uint> ItemIds => new()
		{
			3,
			5142,
			5144,
			5525,
		};

		public override List<NodeInfo> Nodes => new()
		{

		};
	}
}
