using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.ARealmReborn.CoerthasCentralHighlands_155
{
	public class BTN_142 : RouteInfo
	{
		public override uint Id => 142;
		public override uint ExpansionId => 0;
		public override uint ZoneId => 155;
		public override uint GatherType => 2;
		public override Vector2 MapPosition => new Vector2(68.6686f, -183.117f);
		public override int Radius => 65;

		public override HashSet<uint> NodeIds => new()
		{
			30365,
			30366,
			30367,
			30368,
		};

		public override HashSet<uint> ItemIds => new()
		{
			3,
			5536,
			6146,
		};

		public override List<NodeInfo> Nodes => new()
		{

		};
	}
}
