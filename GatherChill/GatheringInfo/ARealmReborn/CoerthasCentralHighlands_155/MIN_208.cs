using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.ARealmReborn.CoerthasCentralHighlands_155
{
	public class MIN_208 : RouteInfo
	{
		public override uint Id => 208;
		public override uint ExpansionId => 0;
		public override uint ZoneId => 155;
		public override uint GatherType => 0;
		public override Vector2 MapPosition => new Vector2(320.064f, -237.28f);
		public override int Radius => 41;

		public override HashSet<uint> NodeIds => new()
		{
			30508,
			30509,
			30510,
			30511,
		};

		public override HashSet<uint> ItemIds => new()
		{
			3,
			2001426,
		};

		public override List<NodeInfo> Nodes => new()
		{

		};
	}
}
